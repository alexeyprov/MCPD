using System;
using System.Collections.Generic;
using System.Data;

namespace LinqToAdo.Provider.Mappings
{
    /// <summary>
    /// A class with extension methods for <see cref="DataRow"/>
    /// objects.
    /// </summary>
    public static class DataRowExtensions
    {
        /// <summary>
        /// Takes the results from an <see cref="IDataReader"/>
        /// object and streams models back on demand.
        /// </summary>
        /// <typeparam name="T">
        /// The type of the model to map back.
        /// </typeparam>
        /// <param name="reader">
        /// The <see cref="IDataReader"/>
        /// which is used to read results from.
        /// </param>
        /// <param name="disposable">
        /// Optional <see cref="IDisposable"/>
        /// implementation to be disposed
        /// of when the enumeration is complete.
        /// </param>
        /// <returns>
        /// A deferred execution sequence of
        /// <typeparamref name="T"/> objects
        /// that were mapped from the <paramref name="reader"/> rows.
        /// </returns>
        public static IEnumerable<T> ToModels<T>(this IDataReader reader, IDisposable disposable = null)
            where T : class, new()
        {
            if (reader == null)
            {
                throw new ArgumentNullException("reader");
            }

            // Dispose of the reader and the data layer when done.
            using (disposable)
            {
                return IterateData<T>(new DataReaderIterator(reader));
            }
        }

        /// <summary>
        /// Converts a sequence of <see cref="DataRow"/> objects
        /// into a sequence of <typeparamref name="T"/> objecs.
        /// </summary>
        /// <typeparam name="T">
        /// The type to convert the
        /// rows in the <paramref name="table"/> to.
        /// </typeparam>
        /// <param name="table">
        /// The <see cref="DataTable"/> that contains the sequence of
        /// <see cref="DataRow"/> objects to map to an sequence of
        /// objects of <typeparamref name="T"/>.
        /// </param>
        /// <returns>
        /// A collection of <typeparamref name="T"/> objects
        /// mapped from the rows in the <paramref name="table"/>.
        /// </returns>
        public static IEnumerable<T> ToModels<T>(this DataTable table)
            where T : class, new()
        {
            if (table == null)
            {
                throw new ArgumentNullException("table");
            }

            return table.AsEnumerable().ToModels<T>();
        }

        /// <summary>
        /// Converts a sequence of <see cref="DataRow"/> objects
        /// into a sequence of <typeparamref name="T"/> objects.
        /// </summary>
        /// <typeparam name="T">
        /// The type to convert the <paramref name="rows"/> to.
        /// </typeparam>
        /// <param name="rows">
        /// The sequence of <see cref="DataRow"/> objects to map 
        /// to a sequence of <typeparamref name="T"/> objects.
        /// </param>
        /// <returns>
        /// The object of <typeparamref name="T"/>
        /// mapped from the <paramref name="rows"/>.
        /// </returns>
        public static IEnumerable<T> ToModels<T>(this IEnumerable<DataRow> rows)
            where T : class, new()
        {
            if (rows == null)
            {
                throw new ArgumentNullException("rows");
            }

            return IterateData<T>(new DataRowsIterator(rows));
        }

        /// <summary>
        /// Converts a <see cref="DataRow"/>
        /// into an object of <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">
        /// The type to convert the <paramref name="row"/> to.
        /// </typeparam>
        /// <param name="row">
        /// The <see cref="DataRow"/> mapped to an object of 
        /// type <typeparamref name="T"/>.
        /// </param>
        /// <returns>
        /// The object of <typeparamref name="T"/>
        /// mapped from the <paramref name="row"/>.
        /// </returns>
        public static T ToModel<T>(this DataRow row)
            where T : class, new()
        {
            if (row == null)
            {
                throw new ArgumentNullException("row");
            }

            IDataAccessor accessor = new DataRowAccessor(row);

            // Create the mapping.
            IEnumerable<DataMapping> mappings = accessor.CreateMappings<T>();

            // Map.
            return accessor.UpdateModel(new T(), mappings);
        }

        private static IEnumerable<T> IterateData<T>(IDataIterator iterator)
            where T : class, new()
        {
            using (iterator as IDisposable)
            {
                IEnumerator<IDataAccessor> enumerator = iterator.Accessors.GetEnumerator();

                // If there are no elements, get out.
                if (!enumerator.MoveNext())
                {
                    yield break;
                }

                // There is a row, get the mappings.
                IEnumerable<DataMapping> mappings = enumerator.Current.CreateMappings<T>();

                // Cycle through the remaining items, using the cached mappings.
                do
                {
                    // Yield the row mapped to the model.
                    yield return enumerator.Current.UpdateModel(new T(), mappings);
                }
                while (enumerator.MoveNext());
            }
        }
    }
}
