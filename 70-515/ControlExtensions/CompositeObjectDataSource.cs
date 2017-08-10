using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ControlExtensions
{
	public class CompositeObjectDataSource : ObjectDataSource
	{
		#region Reflection Member Proxies

		private static MethodInfo _invalidateCacheEntryMethod;
		private static MethodInfo _getCacheMethod;
		private static MethodInfo _getCacheEnabledMethod;
		private static FieldInfo _viewField;

		#endregion

		#region Construction

		static CompositeObjectDataSource()
		{
			const BindingFlags NeedlesslyPrivate = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;

			_invalidateCacheEntryMethod = typeof(ObjectDataSource).GetMethod("InvalidateCacheEntry", NeedlesslyPrivate, null, new Type[0], null);
			PropertyInfo cache = typeof(ObjectDataSource).GetProperty("Cache", NeedlesslyPrivate);
			_getCacheMethod = cache.GetGetMethod(true);
			PropertyInfo cacheEnabled = cache.PropertyType.GetProperty("Enabled", NeedlesslyPrivate);
			_getCacheEnabledMethod = cacheEnabled.GetGetMethod(true);
			_viewField = typeof(ObjectDataSource).GetField("_view", NeedlesslyPrivate);
		}

		public CompositeObjectDataSource()
		{
			// force creation!
			this.GetView();
		}

		public CompositeObjectDataSource(string typeName, string selectMethod)
			: base(typeName, selectMethod)
		{
			// force creation!
			this.GetView();
		}

		#endregion

		#region Overrides

		protected override DataSourceView GetView(string viewName)
		{
			if (viewName == null || (viewName.Length != 0 && !string.Equals(viewName, "DefaultView", StringComparison.OrdinalIgnoreCase)))
			{
				throw new ArgumentException(ExposedSR.GetString(ExposedSR.InvalidViewName, new object[] { this.ID, "DefaultView" }), "viewName");
			}

			return this.GetView();
		}

		#endregion

		#region Implementation

		protected virtual ObjectDataSourceView GetView()
		{
			ObjectDataSourceView view = (ObjectDataSourceView) _viewField.GetValue(this);

			if (view == null)
			{
				view = new CompositeObjectDataSourceView(this, "DefaultView", this.Context);
				_viewField.SetValue(this, view);

				if (base.IsTrackingViewState)
				{
					((IStateManager) view).TrackViewState();
				}
			}

			return view;
		}

		internal void InvalidateCache()
		{
			object cache = _getCacheMethod.Invoke(this, null);
			object cacheEnabled = _getCacheEnabledMethod.Invoke(cache, null);

			if ((bool) cacheEnabled)
			{
				_invalidateCacheEntryMethod.Invoke(this, null);
			}
		}

		#endregion

	}
}
