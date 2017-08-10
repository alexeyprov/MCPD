using System.Collections.Generic;

namespace LinqToAdo.Provider.Mappings
{
    internal interface IDataAccessor
    {
        IEnumerable<DataMapping> CreateMappings<T>();

        /// <summary>Maps a <paramref name="model"/> of type
        /// <typeparamref name="T"/> from a <paramref name="record"/>
        /// using the provided <paramref name="mappings"/>.</summary>
        /// <typeparam name="T">The type of the model that is being
        /// populated.</typeparam>
        /// <param name="record">The <see cref="SqlDataReader"/>
        /// which is feeding the data back.</param>
        /// <param name="model">The object of
        /// <typeparamref name="T"/> to populate
        /// with the data from <paramref name="record"/>.</param>
        /// <param name="mappings"></param>
        /// <returns>The <paramref name="model"/>, populated.</returns>
        T UpdateModel<T>(T model, IEnumerable<DataMapping> mappings)
            where T : class;
    }
}
