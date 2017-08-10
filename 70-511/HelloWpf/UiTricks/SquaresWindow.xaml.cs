using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace HelloWpf.UiTricks
{
    /// <summary>
    /// Interaction logic for SquaresWindow.xaml
    /// </summary>
    public partial class SquaresWindow : Window
    {
        #region Private Fields

        private static readonly Brush _defaultBrush;
        private static readonly Brush _selectedBrush;
        private static readonly Pen _borderPen;
        private static readonly Size _squareSize;
        private static readonly Brush _lassoBrush;
        private static readonly Pen _lassoPen;

        private readonly IList<DrawingVisual> _selectedSquares;
        private Vector? _dragClickOffset;
        private DrawingVisual _multiSelectionArea;
        private Point _multiSelectionTopLeft; 
        
        #endregion

        #region Constructors

        static SquaresWindow()
        {
            _defaultBrush = Brushes.AliceBlue;
            _selectedBrush = Brushes.LightGoldenrodYellow;
            _borderPen = new Pen(Brushes.SteelBlue, 3.0);
            _squareSize = new Size(30.0, 30.0);

            _lassoBrush = Brushes.Transparent;
            _lassoPen = new Pen(Brushes.Black, 2.0)
            {
                DashStyle = DashStyles.Dash
            };
        }

        public SquaresWindow()
        {
            InitializeComponent();

            _selectedSquares = new List<DrawingVisual>();
        }

        #endregion

        #region Event Handlers

        private void DrawingSurface_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point position = e.GetPosition(DrawingSurface);

            if (AddButton.IsChecked ?? false)
            {
                DrawingVisual visual = new DrawingVisual();

                DrawSquare(visual, position, false);

                DrawingSurface.AddVisual(visual);
            }
            else if (DeleteButton.IsChecked ?? false)
            {
                DrawingVisual visual = DrawingSurface.GetVisualAt<DrawingVisual>(position);

                if (visual != null)
                {
                    DrawingSurface.RemoveVisual(visual);
                    _selectedSquares.Remove(visual);
                }
            }
            else if (SelectButton.IsChecked ?? false)
            {
                DrawingVisual square = DrawingSurface.GetVisualAt<DrawingVisual>(position);

                if (square != null)
                {
                    ClearSelection();

                    SelectUnselectSquare(square, true);

                    _selectedSquares.Add(square);
                    _dragClickOffset = position - square.ContentBounds.TopLeft;
                }
            }
            else if (MultiSelectButton.IsChecked ?? false)
            {
                _multiSelectionArea = new DrawingVisual();
                DrawingSurface.AddVisual(_multiSelectionArea);

                _multiSelectionTopLeft = position;

                DrawingSurface.CaptureMouse();
            }
        }

        private void DrawingSurface_MouseMove(object sender, MouseEventArgs e)
        {
            Point position = e.GetPosition(DrawingSurface);

            if (_dragClickOffset != null && _selectedSquares.Count == 1)
            {
                DrawSquare(_selectedSquares[0], position - _dragClickOffset.Value, true);
            }
            else if (_multiSelectionArea != null)
            {
                DrawMultiSelectionArea(position);

                DrawingSurface.ReleaseMouseCapture();
            }
        }

        private void DrawingSurface_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _dragClickOffset = null;
            if (_multiSelectionArea != null)
            {
                ClearSelection();

                // get and process hits
                foreach (DrawingVisual visual in DrawingSurface.GetVisualsAt<DrawingVisual>(
                    new RectangleGeometry(
                        new Rect(
                            _multiSelectionTopLeft,
                            e.GetPosition(DrawingSurface)))))
                {
                    _selectedSquares.Add(visual);

                    SelectUnselectSquare(visual, true);
                }

                DrawingSurface.RemoveVisual(_multiSelectionArea);
                _multiSelectionArea = null;

                DrawingSurface.ReleaseMouseCapture();
            }
        }

        #endregion

        #region Implementation

        private static void SelectUnselectSquare(DrawingVisual square, bool isSelected)
        {
            Point topLeft = new Point(
                square.ContentBounds.Left + _borderPen.Thickness / 2,
                square.ContentBounds.Top + _borderPen.Thickness / 2);

            DrawSquare(square, topLeft, isSelected);
        }

        private static void DrawSquare(DrawingVisual visual, Point position, bool isSelected)
        {
            using (DrawingContext dc = visual.RenderOpen())
            {
                dc.DrawRectangle(
                    isSelected ? _selectedBrush : _defaultBrush,
                    _borderPen,
                    new Rect(position, _squareSize));
            }
        }

        private void DrawMultiSelectionArea(Point position)
        {
            using (DrawingContext dc = _multiSelectionArea.RenderOpen())
            {
                dc.DrawRectangle(
                    _lassoBrush,
                    _lassoPen,
                    new Rect(_multiSelectionTopLeft, position));
            }
        }

        private void ClearSelection()
        {
            foreach (DrawingVisual visual in _selectedSquares)
            {
                SelectUnselectSquare(visual, false);
            }

            _selectedSquares.Clear();
        }

        #endregion
    }
}
