using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace ImageDataFlow
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Private Fields

        private readonly ITargetBlock<string> _head;
        private CancellationTokenSource _cts;
        private bool _isOpenEnabled;
        private bool _isCancelEnabled;
        private Stream _lastStream;

        #endregion

        #region Constructor

        public MainWindow()
        {
            InitializeComponent();
            _cts = new CancellationTokenSource();
            _head = BuildDataFlowNetwork();

            _isOpenEnabled = true;
        }

        #endregion

        #region Event Handlers

        private void OnOpenExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            // Create a FolderBrowserDialog object to enable the user to 
            // select a folder.
            var dlg = new System.Windows.Forms.FolderBrowserDialog
            {
                ShowNewFolderButton = false
            };

            // Set the selected path to the common Sample Pictures folder
            // if it exists.
            string initialDirectory = Path.Combine(
               Environment.GetFolderPath(Environment.SpecialFolder.CommonPictures),
               "Sample Pictures");
            if (Directory.Exists(initialDirectory))
            {
                dlg.SelectedPath = initialDirectory;
            }

            // Show the dialog and process the dataflow network.
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // Create a new CancellationTokenSource object to enable 
                // cancellation.
                _cts = new CancellationTokenSource();

                // Post the selected path to the network.
                _head.Post(dlg.SelectedPath);

                ResetCommandsState(false);
            }
        }

        private void OnCancelExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            _cts.Cancel();
        }

        private void OnOpenCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _isOpenEnabled;
        }

        private void OnCancelCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _isCancelEnabled;
        }

        #endregion

        #region Implementation

        private ITargetBlock<string> BuildDataFlowNetwork()
        {
            var uiExecutionOptions = new ExecutionDataflowBlockOptions
            {
                TaskScheduler = TaskScheduler.FromCurrentSynchronizationContext()
            };

            IPropagatorBlock<string, IEnumerable<Bitmap>> loadBitmapsBlock =
                new TransformBlock<string, IEnumerable<Bitmap>>(
                    path =>
                    {
                        try
                        {
                            return LoadImages(path);
                        }
                        catch (OperationCanceledException)
                        {
                            return Enumerable.Empty<Bitmap>();
                        }
                    });

            IPropagatorBlock<IEnumerable<Bitmap>, Bitmap> combineBitmapsBlock =
                new TransformBlock<IEnumerable<Bitmap>, Bitmap>(
                    bitmaps =>
                    {
                        try
                        {
                            return CombineImages(bitmaps);
                        }
                        catch (OperationCanceledException)
                        {
                            return null;
                        }
                    });

            ITargetBlock<Bitmap> displayResultsBlock = new ActionBlock<Bitmap>(
                bitmap =>
                {
                    Stream stream = GetStream();
                    bitmap.Save(stream, ImageFormat.Jpeg);
                    stream.Seek(0, SeekOrigin.Begin);

                    CanvasImage.BeginInit();
                    CanvasImage.Source = BitmapFrame.Create(stream);
                    CanvasImage.EndInit();

                    ResetCommandsState(true);
                },
                uiExecutionOptions);

            ITargetBlock<object> cancellationBlock = new ActionBlock<object>(
                _ =>
                {
                    CanvasImage.BeginInit();
                    CanvasImage.Source = new BitmapImage(new Uri("Cancel.png", UriKind.Relative));
                    CanvasImage.EndInit();
                    ResetCommandsState(true);
                },
                uiExecutionOptions);

            // link blocks
            loadBitmapsBlock.LinkTo(combineBitmapsBlock, bitmaps => bitmaps.Any());
            loadBitmapsBlock.LinkTo(cancellationBlock);
            loadBitmapsBlock.Completion.ContinueWith(t => combineBitmapsBlock.Complete());

            combineBitmapsBlock.LinkTo(displayResultsBlock, bitmap => bitmap != null);
            combineBitmapsBlock.LinkTo(cancellationBlock);
            combineBitmapsBlock.Completion.ContinueWith(t => displayResultsBlock.Complete());

            return loadBitmapsBlock;
        }

        private Bitmap CombineImages(IEnumerable<Bitmap> bitmaps)
        {
            Bitmap[] bitmapArray = bitmaps.ToArray();

            // Compute the maximum width and height components of all 
            // bitmaps in the collection.
            var largest = new Rectangle();
            foreach (Bitmap bitmap in bitmapArray)
            {
                if (bitmap.Width > largest.Width)
                    largest.Width = bitmap.Width;
                if (bitmap.Height > largest.Height)
                    largest.Height = bitmap.Height;
            }

            // Create a 32-bit Bitmap object with the greatest dimensions.
            Bitmap result = new Bitmap(
                largest.Width,
                largest.Height,
                PixelFormat.Format32bppArgb);

            // Lock the result Bitmap.
            BitmapData resultBitmapData = result.LockBits(
               new Rectangle(new System.Drawing.Point(), result.Size),
               ImageLockMode.WriteOnly,
               result.PixelFormat);

            // Lock each source bitmap to create a parallel list of BitmapData objects.
            var bitmapDataList = bitmapArray
                .Select(b => b.LockBits(
                    new Rectangle(
                        new System.Drawing.Point(),
                        b.Size),
                    ImageLockMode.ReadOnly,
                    PixelFormat.Format32bppArgb))
                                 .ToList();

            // Compute each column in parallel.
            Parallel.For(
                0,
                largest.Width,
                new ParallelOptions
                {
                    CancellationToken = _cts.Token
                },
                i =>
                {
                    // Compute each row.
                    for (int j = 0; j < largest.Height; j++)
                    {
                        // Counts the number of bitmaps whose dimensions
                        // contain the current location.
                        int count = 0;

                        // The sum of all alpha, red, green, and blue components.
                        int a = 0, r = 0, g = 0, b = 0;

                        // For each bitmap, compute the sum of all color components.
                        foreach (BitmapData bitmapData in bitmapDataList)
                        {
                            // Ensure that we stay within the bounds of the image.
                            if (bitmapData.Width > i && bitmapData.Height > j)
                            {
                                unsafe
                                {
                                    byte* row = (byte*)(bitmapData.Scan0 + (j * bitmapData.Stride));
                                    byte* pix = (byte*)(row + (4 * i));
                                    a += *(pix++);
                                    r += *(pix++);
                                    g += *(pix++);
                                    b += *pix;
                                }
                                count++;
                            }
                        }

                        unsafe
                        {
                            // Compute the average of each color component.
                            a /= count;
                            r /= count;
                            g /= count;
                            b /= count;

                            // Set the result pixel.
                            byte* row = (byte*)(resultBitmapData.Scan0 + (j * resultBitmapData.Stride));
                            byte* pix = row + (4 * i);
                            *(pix++) = (byte)a;
                            *(pix++) = (byte)r;
                            *(pix++) = (byte)g;
                            *pix = (byte)b;
                        }
                    }
                });

            // Unlock the source bitmaps.
            for (int i = 0; i < bitmapArray.Length; i++)
            {
                bitmapArray[i].UnlockBits(bitmapDataList[i]);
            }

            // Unlock the result bitmap.
            result.UnlockBits(resultBitmapData);

            // Return the result.
            return result;
        }

        private IEnumerable<Bitmap> LoadImages(string path)
        {
            CancellationToken token = _cts.Token;
            ICollection<Bitmap> images = new List<Bitmap>();
            foreach (string imageType in new[]
                {
                    "*.bmp", "*.gif", "*.jpg", "*.png", "*.tif"
                })
            {
                foreach (string fileName in Directory.GetFiles(path, imageType))
                {
                    token.ThrowIfCancellationRequested();
                    try
                    {
                        images.Add(new Bitmap(fileName));
                    }
                    catch
                    {
                    }
                }
            }
            return images;
        }

        private void ResetCommandsState(bool enableOpen)
        {
            _isOpenEnabled = enableOpen;
            _isCancelEnabled = !enableOpen;
            CommandManager.InvalidateRequerySuggested();
        }

        private Stream GetStream()
        {
            if (_lastStream != null)
            {
                _lastStream.Dispose();
            }

            _lastStream = new MemoryStream();
            return _lastStream;
        }

        #endregion
    }
}
