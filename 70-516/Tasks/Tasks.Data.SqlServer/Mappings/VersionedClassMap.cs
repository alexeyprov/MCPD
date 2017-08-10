using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using Tasks.Data.Models;

namespace Tasks.Data.SqlServer.Mappings
{
    internal abstract class VersionedClassMap<T> : ClassMap<T>
        where T : IVersionedModel
    {
        protected VersionedClassMap()
        {
            Version(m => m.Version)
                .Column("ts")
                .CustomSqlType("rowversion")
                .Generated.Always()
                .UnsavedValue("null");
        }
    }
}
