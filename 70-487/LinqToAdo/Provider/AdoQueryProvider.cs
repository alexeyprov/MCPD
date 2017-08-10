using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq.Expressions;
using System.Reflection;

using LinqToAdo.Provider.Mappings;
using LinqToAdo.Provider.Projections;
using LinqToAdo.Provider.Visitors;
using LinqToAdo.Provider.Visitors.Translators;

namespace LinqToAdo.Provider
{
    public class AdoQueryProvider : QueryProvider
    {
        private static readonly MethodInfo _createModelsMethod;
        private readonly DbConnection _connection;
        private readonly QueryTranslator _translator;

        static AdoQueryProvider()
        {
            _createModelsMethod = typeof(AdoQueryProvider).GetMethod(
                "CreateModels",
                BindingFlags.NonPublic | BindingFlags.Static);
        }

        public AdoQueryProvider(DbConnection connection, bool pluralTableNames)
        {
            _connection = connection;
            _translator = new QueryTranslator(pluralTableNames);
        }

        public override object Execute(Expression expression)
        {
            TranslationResult translationResult = _translator.Translate(expression);

            DbCommand command = _connection.CreateCommand();
            command.CommandText = translationResult.CommandText;
            command.CommandType = CommandType.Text;

            IDataReader reader = command.ExecuteReader();

            Type elementType = GetExpressionType(expression);

            if (translationResult.Projector == null)
            {
                return _createModelsMethod.MakeGenericMethod(elementType).Invoke(
                    this,
                    new[] 
                    {
                        (object)reader,
                        command
                    });
            }

            Delegate projector = translationResult.Projector.Compile();
            Type readerType = typeof(ProjectionReader<>).MakeGenericType(elementType);

            return Activator.CreateInstance(
                readerType,
                reader,
                projector);
        }

        public override string GetQueryText(Expression expression)
        {
            return _translator.Translate(expression).CommandText;
        }

        private static IEnumerable<T> CreateModels<T>(IDataReader reader, DbCommand command)
            where T : class, new()
        {
            return reader.ToModels<T>(command);
        }
    }
}
