using System.Collections.Generic;

namespace Mvc4Async.Models
{
	public class ProdGizWidgetVM
	{
		public ProdGizWidgetVM(
			IEnumerable<Widget> widgets,
			IEnumerable<Product> products,
			IEnumerable<Gizmo> gizmos)
		{
			WidgetList = widgets;
			ProdList = products;
			GizmoList = gizmos;
		}

		public IEnumerable<Widget> WidgetList
		{
			get;
			private set;
		}

		public IEnumerable<Product> ProdList
		{
			get;
			private set;
		}

		public IEnumerable<Gizmo> GizmoList
		{
			get;
			private set;
		}
	}
}