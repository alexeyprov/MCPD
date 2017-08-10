using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CustomUI
{
    public class DrawingCanvas : Canvas
    {
        private IList<Visual> _visuals;

        public DrawingCanvas()
        {
            _visuals = new List<Visual>();
        }

        public T GetVisualAt<T>(Point p) where T : Visual
        {
            return VisualTreeHelper.HitTest(this, p).VisualHit as T;
        }

        public IEnumerable<T> GetVisualsAt<T>(Geometry region) where T : Visual
        {
            List<T> hits = new List<T>();

            VisualTreeHelper.HitTest(
                this,
                null,
                r => 
                {
                    GeometryHitTestResult result = (GeometryHitTestResult)r;
                    T visual = result.VisualHit as T;

                    if (visual != null && result.IntersectionDetail == IntersectionDetail.FullyInside)
                    {
                        hits.Add(visual);
                    }

                    return HitTestResultBehavior.Continue;
                },
                new GeometryHitTestParameters(region));

            return hits;
        }

        public void AddVisual(Visual visual)
        {
            _visuals.Add(visual);

            AddVisualChild(visual);
            AddLogicalChild(visual);
        }

        public void RemoveVisual(Visual visual)
        {
            _visuals.Remove(visual);

            RemoveVisualChild(visual);
            RemoveLogicalChild(visual);
        }

        protected override int VisualChildrenCount
        {
            get
            {
                return _visuals.Count;
            }
        }

        protected override Visual GetVisualChild(int index)
        {
            return _visuals[index];
        }
    }
}
