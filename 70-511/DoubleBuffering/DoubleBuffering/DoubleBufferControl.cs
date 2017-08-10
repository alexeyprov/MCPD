using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace DoubleBufferExample
{
    public class DoubleBufferControl : Control
    {
        private Bitmap _backBuffer;
        private Graphics _bufferGraphics;
        private BufferedGraphicsContext _graphicsManager;
        private BufferedGraphics _managedBackBuffer;

        private DoubleBufferMethod _paintMethod;
        private GraphicTestMethods _testMethod;

        public DoubleBufferControl()
        {
            _paintMethod = DoubleBufferMethod.NoDoubleBuffer;
            _testMethod = GraphicTestMethods.DrawTest;

            InitializeComponent();

            SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint, true);

            Application.ApplicationExit += (s,e) => MemoryCleanup();
        }

        private void InitializeComponent()
        {
            SuspendLayout();
            // 
            // DoubleBufferControl
            // 
            BackColor = Color.Black;
            Resize += DoubleBufferControl_Resize;
            ResumeLayout(false);
        }

        public DoubleBufferMethod PaintMethod
        {
            get
            {
                return _paintMethod;
            }
            set
            {
                _paintMethod = value;
                RemovePaintMethods();

                switch (value)
                {
                    case DoubleBufferMethod.BuiltInDoubleBuffer:
                        SetStyle(ControlStyles.UserPaint, true);
                        DoubleBuffered = true;
                        break;
                    case DoubleBufferMethod.BuiltInOptimizedDoubleBuffer:
                        SetStyle(
                            ControlStyles.OptimizedDoubleBuffer |
                            ControlStyles.AllPaintingInWmPaint, true);
                        break;
                    case DoubleBufferMethod.ManualDoubleBuffer11:
                        SetStyle(ControlStyles.AllPaintingInWmPaint, true);
                        _backBuffer = new Bitmap(ClientRectangle.Width, ClientRectangle.Height);
                        _bufferGraphics = Graphics.FromImage(_backBuffer);
                        break;

                    case DoubleBufferMethod.ManualDoubleBuffer20:
                        SetStyle(ControlStyles.AllPaintingInWmPaint, true);
                        _graphicsManager = BufferedGraphicsManager.Current;
                        _graphicsManager.MaximumBuffer = new Size(Width + 1, Height + 1);
                        _managedBackBuffer = _graphicsManager.Allocate(CreateGraphics(), ClientRectangle);
                        break;
                }
            }
        }

        public GraphicTestMethods GraphicTest
        {
            get
            {
                return _testMethod;
            }
            set
            {
                using (Graphics graphics = CreateGraphics())
                {
                    graphics.Clear(Color.Wheat);
                }
                _testMethod = value;
            }
        }

        public enum DoubleBufferMethod
        {
            NoDoubleBuffer,
            BuiltInDoubleBuffer,
            BuiltInOptimizedDoubleBuffer,
            ManualDoubleBuffer11,
            ManualDoubleBuffer20
        };

        public enum GraphicTestMethods
        {
            DrawTest,
            FillTest
        };

        private void MemoryCleanup()
        {
            if (_bufferGraphics != null)
            {
                _bufferGraphics.Dispose();
                _bufferGraphics = null;
            }

            if (_backBuffer != null)
            {
                _backBuffer.Dispose();
                _backBuffer = null;
            }

            if (_managedBackBuffer != null)
            {
                _managedBackBuffer.Dispose();
                _managedBackBuffer = null;
            }
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            if (DesignMode)
            {
                base.OnPaint(e);
                return;
            }

            switch (_paintMethod)
            {
                case DoubleBufferMethod.NoDoubleBuffer:
                    base.OnPaint(e);
                    LaunchGraphicTest(e.Graphics);
                    break;

                case DoubleBufferMethod.BuiltInDoubleBuffer:
                    LaunchGraphicTest(e.Graphics);
                    break;

                case DoubleBufferMethod.BuiltInOptimizedDoubleBuffer:
                    LaunchGraphicTest(e.Graphics);
                    break;

                case DoubleBufferMethod.ManualDoubleBuffer11:
                    PaintDoubleBuffer11(e.Graphics);
                    break;

                case DoubleBufferMethod.ManualDoubleBuffer20:
                    PaintDoubleBuffer20(e.Graphics);
                    break;
            }
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            /*            if (_PaintMethod != DoubleBufferMethod.ManualDoubleBuffer11 &&
                            _PaintMethod != DoubleBufferMethod.ManualDoubleBuffer20)

                            base.OnPaintBackground(pevent);*/
        }

        private void RemovePaintMethods()
        {
            DoubleBuffered = false;

            SetStyle(ControlStyles.OptimizedDoubleBuffer, false);

            MemoryCleanup();
        }

        private void DoubleBufferControl_Resize(object sender, EventArgs e)
        {
            switch (_paintMethod)
            {
                case DoubleBufferMethod.ManualDoubleBuffer11:
                    _backBuffer = new Bitmap(ClientRectangle.Width, ClientRectangle.Height);
                    _bufferGraphics = Graphics.FromImage(_backBuffer);
                    break;

                case DoubleBufferMethod.ManualDoubleBuffer20:
                    _graphicsManager.MaximumBuffer = new Size(Width + 1, Height + 1);

                    if (_managedBackBuffer != null)
                    {
                        _managedBackBuffer.Dispose();
                    }

                    _managedBackBuffer = _graphicsManager.Allocate(CreateGraphics(), ClientRectangle);
                    break;
            }

            Refresh();
        }

        private void PaintDoubleBuffer11(Graphics graphics)
        {
            LaunchGraphicTest(_bufferGraphics);

            // this draws the image from the buffer into the form area 
            // (note: DrawImageUnscaled is the fastest way)
            graphics.DrawImageUnscaled(_backBuffer, 0, 0);
        }

        private void PaintDoubleBuffer20(Graphics graphics)
        {
            try
            {
                LaunchGraphicTest(_managedBackBuffer.Graphics);

                // paint the picture in from the back buffer into the form draw area
                _managedBackBuffer.Render(graphics);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        private void LaunchGraphicTest(Graphics graphics)
        {
            Random rnd = new Random();
            graphics.Clear(Color.Wheat);

            switch (GraphicTest)
            {
                case GraphicTestMethods.DrawTest:
                    RunTest(
                        graphics, 
                        rnd,
                        r =>
                        {
                            using (Pen colorPen = new Pen(Color.FromArgb(127, rnd.Next(0, 256), rnd.Next(256), rnd.Next(256))))
                            {
                                graphics.DrawRectangle(colorPen, r);
                            }
                        });
                    break;

                case GraphicTestMethods.FillTest:
                    RunTest(
                        graphics, 
                        rnd,
                        r =>
                        {
                            using (Brush colorBrush = new LinearGradientBrush(
                                r,
                                Color.FromArgb(127, rnd.Next(0, 256), rnd.Next(256), rnd.Next(256)),
                                Color.FromArgb(127, rnd.Next(0, 256), rnd.Next(256), rnd.Next(256)),
                                (LinearGradientMode)rnd.Next(3)))
                            {
                                graphics.FillEllipse(colorBrush, r);
                            }
                        });
                    break;
            }
        }

        private void RunTest(Graphics graphics, Random rnd, Action<Rectangle> test)
        {
            for (int i = 0; i < 1000; i++)
            {
                Rectangle tempRectangle = new Rectangle(
                    rnd.Next(0, Width),
                    rnd.Next(0, Height),
                    Math.Abs(Width - i) + 1,
                    Math.Abs(Height - i) + 1);

                test(tempRectangle);
            }
        }
    }
}
