using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ReflectionUtilities;

namespace ControlExtensions
{
	public class CompositeObjectDataSourceView : ObjectDataSourceView
	{
		#region Reflection Member Proxies

		private static readonly FieldInfo _ownerField;
		private static readonly MethodInfo _getTypeMethod;
		private static readonly MethodInfo _tryGetDataObjectTypeMethod;
		private static readonly MethodInfo _mergeDictionariesMethod;
		private static readonly MethodInfo _getResolvedMethodDataMethod;
		private static readonly MethodInfo _invokeMethodMethod;
		private static readonly MethodInfo _buildObjectValueMethod;
		private static readonly FieldInfo _affectedRowsField;
		private static readonly FieldInfo _parametersField;

		#endregion

		#region Construction

		static CompositeObjectDataSourceView()
		{
			const BindingFlags NeedlesslyPrivate = BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly;
			const BindingFlags NeedlesslyPrivateStatic = BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.DeclaredOnly;

			_ownerField = typeof(ObjectDataSourceView).GetField("_owner", NeedlesslyPrivate);

			Type[] getType = new Type[] { typeof(string) };
			_getTypeMethod = typeof(ObjectDataSourceView).GetMethod("GetType", NeedlesslyPrivate, null, getType, null);

			Type[] tryGetDataObjectType = new Type[] { };
			_tryGetDataObjectTypeMethod = typeof(ObjectDataSourceView).GetMethod("TryGetDataObjectType", NeedlesslyPrivate, null, tryGetDataObjectType, null);

			Type[] mergeDictionaries = new Type[] { typeof(ParameterCollection), typeof(IDictionary), typeof(IDictionary) };
			_mergeDictionariesMethod = typeof(ObjectDataSourceView).GetMethod("MergeDictionaries", NeedlesslyPrivateStatic, null, mergeDictionaries, null);

			Type[] getResolvedMethodData = new Type[] { typeof(Type), typeof(string), typeof(Type), typeof(object), typeof(object), typeof(DataSourceOperation) };
			_getResolvedMethodDataMethod = typeof(ObjectDataSourceView).GetMethod("GetResolvedMethodData", NeedlesslyPrivate, null, getResolvedMethodData, null);

			Type[] buildObjectValue = new Type[] { typeof(object), typeof(Type), typeof(string) };
			_buildObjectValueMethod = typeof(ObjectDataSourceView).GetMethod("BuildObjectValue", NeedlesslyPrivateStatic, null, buildObjectValue, null);

			Type objectDataSourceMethod = typeof(ObjectDataSourceView).GetNestedType("ObjectDataSourceMethod", BindingFlags.NonPublic);
			_parametersField = objectDataSourceMethod.GetField("Parameters", NeedlesslyPrivate);

			Type[] invokeMethod = new Type[] { objectDataSourceMethod };
			_invokeMethodMethod = typeof(ObjectDataSourceView).GetMethod("InvokeMethod", NeedlesslyPrivate, null, invokeMethod, null);

			Type objectDataSourceResult = typeof(ObjectDataSourceView).GetNestedType("ObjectDataSourceResult", BindingFlags.NonPublic);
			_affectedRowsField = objectDataSourceResult.GetField("AffectedRows", NeedlesslyPrivate);
		}

		public CompositeObjectDataSourceView(ObjectDataSource owner, string name, HttpContext context)
			: base(owner, name, context)
		{
		}

		#endregion

		#region Overrides

		protected override int ExecuteDelete(IDictionary keys, IDictionary oldValues)
		{
			if (!this.CanDelete)
			{
				throw new NotSupportedException(ExposedSR.GetString(ExposedSR.DeleteNotSupported, this.Owner.ID));
			}

			// we only change the behavior of sources that provide a DataObjectTypeName
			if (String.IsNullOrEmpty(this.DataObjectTypeName))
				return base.ExecuteDelete(keys, oldValues);

			Type sourceType = this.GetType(this.TypeName);
			Type dataObjectType = this.TryGetDataObjectType();
			IDictionary deleteParameters = new OrderedDictionary(StringComparer.OrdinalIgnoreCase);

			if (this.ConflictDetection == ConflictOptions.CompareAllValues)
			{
				if (oldValues == null || oldValues.Count == 0)
				{
					throw new InvalidOperationException(ExposedSR.GetString(ExposedSR.Pessimistic, ExposedSR.GetString(ExposedSR.Delete), this.Owner.ID, "oldValues"));
				}

				MergeDictionaries(this.DeleteParameters, oldValues, deleteParameters);
			}

			MergeDictionaries(this.DeleteParameters, keys, deleteParameters);
			object dataObject = this.BuildDataObject(dataObjectType, deleteParameters);

			object deleteMethod = this.GetResolvedMethodData(sourceType, this.DeleteMethod, dataObjectType, dataObject, null, DataSourceOperation.Delete);
			IOrderedDictionary parameters = ExtractMethodParameters(deleteMethod);
			ObjectDataSourceMethodEventArgs args = new ObjectDataSourceMethodEventArgs(parameters);
			this.OnDeleting(args);

			if (args.Cancel)
			{
				return 0;
			}

			object result = this.InvokeMethod(deleteMethod);

			this.Owner.InvalidateCache();
			this.OnDataSourceViewChanged(EventArgs.Empty);
			return ExtractAffectedRows(result);
		}

		protected override int ExecuteInsert(IDictionary values)
		{
			if (!this.CanInsert)
			{
				throw new NotSupportedException(ExposedSR.GetString(ExposedSR.InsertNotSupported, this.Owner.ID));
			}

			// we only change the behavior of sources that provide a DataObjectTypeName
			if (String.IsNullOrEmpty(this.DataObjectTypeName))
				return base.ExecuteInsert(values);

			Type sourceType = this.GetType(this.TypeName);
			Type dataObjectType = this.TryGetDataObjectType();

			if (values == null || values.Count == 0)
			{
				throw new InvalidOperationException(ExposedSR.GetString(ExposedSR.InsertRequiresValues, this.Owner.ID));
			}

			IDictionary insertParameters = new OrderedDictionary(StringComparer.OrdinalIgnoreCase);
			MergeDictionaries(this.InsertParameters, values, insertParameters);
			object dataObject = this.BuildDataObject(dataObjectType, insertParameters);

			object insertMethod = this.GetResolvedMethodData(sourceType, this.InsertMethod, dataObjectType, null, dataObject, DataSourceOperation.Insert);
			IOrderedDictionary parameters = ExtractMethodParameters(insertMethod);
			ObjectDataSourceMethodEventArgs args = new ObjectDataSourceMethodEventArgs(parameters);
			this.OnInserting(args);

			if (args.Cancel)
			{
				return 0;
			}

			object result = this.InvokeMethod(insertMethod);

			this.Owner.InvalidateCache();
			this.OnDataSourceViewChanged(EventArgs.Empty);
			return ExtractAffectedRows(result);
		}

		protected override int ExecuteUpdate(IDictionary keys, IDictionary values, IDictionary oldValues)
		{
			if (!this.CanUpdate)
			{
				throw new NotSupportedException(ExposedSR.GetString(ExposedSR.UpdateNotSupported, this.Owner.ID));
			}

			// we only change the behavior of sources that provide a DataObjectTypeName
			if (String.IsNullOrEmpty(this.DataObjectTypeName))
				return base.ExecuteUpdate(keys, values, oldValues);

			object updateMethod;
			Type sourceType = this.GetType(this.TypeName);
			Type dataObjectType = this.TryGetDataObjectType();

			if (this.ConflictDetection == ConflictOptions.CompareAllValues)
			{
				if (oldValues == null || oldValues.Count == 0)
				{
					throw new InvalidOperationException(ExposedSR.GetString(ExposedSR.Pessimistic, ExposedSR.GetString(ExposedSR.Update), this.Owner.ID, "oldValues"));
				}

				IDictionary oldParameters = new OrderedDictionary(StringComparer.OrdinalIgnoreCase);
				MergeDictionaries(this.UpdateParameters, oldValues, oldParameters);
				MergeDictionaries(this.UpdateParameters, keys, oldParameters);
				object oldDataObject = this.BuildDataObject(dataObjectType, oldParameters);

				IDictionary newParameters = new OrderedDictionary(StringComparer.OrdinalIgnoreCase);
				MergeDictionaries(this.UpdateParameters, oldValues, newParameters);
				MergeDictionaries(this.UpdateParameters, keys, newParameters);
				MergeDictionaries(this.UpdateParameters, values, newParameters);
				object newDataObject = this.BuildDataObject(dataObjectType, newParameters);

				// if oldDataObject and newDataObject are the same object, this is a bit odd... but since we
				// built them old-then-new, the resulting object has the correct values. In general we aren't
				// going to be running that way.

				updateMethod = this.GetResolvedMethodData(sourceType, this.UpdateMethod, dataObjectType, oldDataObject, newDataObject, DataSourceOperation.Update);
			}
			else
			{
				IDictionary updateParameters = new OrderedDictionary(StringComparer.OrdinalIgnoreCase);
				MergeDictionaries(this.UpdateParameters, oldValues, updateParameters);
				MergeDictionaries(this.UpdateParameters, keys, updateParameters);
				MergeDictionaries(this.UpdateParameters, values, updateParameters);
				object dataObject = this.BuildDataObject(dataObjectType, updateParameters);

				updateMethod = this.GetResolvedMethodData(sourceType, this.UpdateMethod, dataObjectType, null, dataObject, DataSourceOperation.Update);
			}

			IOrderedDictionary parameters = ExtractMethodParameters(updateMethod);
			ObjectDataSourceMethodEventArgs args = new ObjectDataSourceMethodEventArgs(parameters);
			this.OnUpdating(args);

			if (args.Cancel)
			{
				return 0;
			}

			object result = this.InvokeMethod(updateMethod);

			this.Owner.InvalidateCache();
			this.OnDataSourceViewChanged(EventArgs.Empty);
			return ExtractAffectedRows(result);
		}

		#endregion

		#region Implementation

		// Pseudo-overriden method using recursive GetDescriptor calls
		private object BuildDataObject(Type dataObjectType, IDictionary inputParameters)
		{
			object dataObject = Activator.CreateInstance(dataObjectType);
			CompositePropertyAccessor propertyAccessor = new CompositePropertyAccessor(dataObjectType);

			foreach (DictionaryEntry entry in inputParameters)
			{
				string propertyName = (entry.Key == null) ? string.Empty : entry.Key.ToString();
				object target = dataObject;

				PropertyDescriptor descriptor = propertyAccessor.GetDescriptor(ref target, propertyName);

				if (null == descriptor)
				{
					throw new InvalidOperationException(ExposedSR.GetString(ExposedSR.DataObjectPropertyNotFound, new object[] { propertyName, this.Owner.ID }));
				}

				if (descriptor.IsReadOnly)
				{
					throw new InvalidOperationException(ExposedSR.GetString(ExposedSR.DataObjectPropertyReadOnly, new object[] { propertyName, this.Owner.ID }));
				}

				object propertyValue = BuildObjectValue(entry.Value, descriptor.PropertyType, propertyName);
				descriptor.SetValue(target, propertyValue);
			}

			return dataObject;
		}

		#endregion

		#region Proxies for private methods
		private CompositeObjectDataSource Owner
		{
			get
			{
				return (CompositeObjectDataSource) _ownerField.GetValue(this);
			}
		}

		private Type GetType(string typeName)
		{
			return (Type) _getTypeMethod.Invoke(this, new object[] { typeName });
		}

		private Type TryGetDataObjectType()
		{
			return (Type) _tryGetDataObjectTypeMethod.Invoke(this, null);
		}

		private static void MergeDictionaries(ParameterCollection reference, IDictionary source, IDictionary destination)
		{
			_mergeDictionariesMethod.Invoke(null, new object[] { reference, source, destination });
		}

		private static object BuildObjectValue(object value, Type destinationType, string paramName)
		{
			return _buildObjectValueMethod.Invoke(null, new object[] { value, destinationType, paramName });
		}

		private object GetResolvedMethodData(Type type, string methodName, Type dataObjectType
											 , object oldDataObject, object newDataObject, DataSourceOperation operation)
		{
			return _getResolvedMethodDataMethod.Invoke(this, new object[] { type, methodName, dataObjectType, oldDataObject, newDataObject, operation });
		}

		private IOrderedDictionary ExtractMethodParameters(object method)
		{
			return (IOrderedDictionary) _parametersField.GetValue(method);
		}

		private object InvokeMethod(object method)
		{
			return _invokeMethodMethod.Invoke(this, new object[] { method });
		}

		private int ExtractAffectedRows(object result)
		{
			return (int) _affectedRowsField.GetValue(result);
		}
		#endregion
	}
}
