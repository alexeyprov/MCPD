using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Tasks.Data.Models;

namespace Tasks.Data.SqlServer.Mappings
{
    internal sealed class TaskMap : VersionedClassMap<Task>
    {
        public TaskMap()
        {
            Id(m => m.TaskId);

            Map(m => m.DateCompleted);
            Map(m => m.DueDate);
            Map(m => m.StartDate);
            Map(m => m.Subject).Not.Nullable();
            Map(m => m.CreatedDate).Not.Nullable();

            References(m => m.Priority, "PriorityId");
            References(m => m.Status, "StatusId");
            References(m => m.CreatedBy, "CreatedUserId");

            HasManyToMany(m => m.Assignees)
                .Access.ReadOnlyPropertyThroughCamelCaseField(Prefix.Underscore)
                .Table("TaskUser")
                .ParentKeyColumn("TaskId")
                .ChildKeyColumn("UserId");

            HasManyToMany(m => m.Categories)
                .Access.ReadOnlyPropertyThroughCamelCaseField(Prefix.Underscore)
                .Table("TaskCategory")
                .ParentKeyColumn("TaskId")
                .ChildKeyColumn("CategoryId");
        }
    }
}
