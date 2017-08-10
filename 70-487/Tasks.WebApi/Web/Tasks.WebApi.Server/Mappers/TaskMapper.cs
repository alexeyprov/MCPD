using System.Collections.Generic;
using System.Linq;
using Tasks.WebApi.Models;

namespace Tasks.WebApi.Server.Mappers
{
    internal sealed class TaskMapper : ITypeMapper<Data.Models.Task, Task>
    {
        private readonly ITypeMapper<Data.Models.Category, Category> _categoryMapper;
        private readonly IUserMapper _userMapper;
        private readonly ITypeMapper<Data.Models.Priority, Priority> _priorityMapper;
        private readonly ITypeMapper<Data.Models.Status, Status> _statusMapper;

        public TaskMapper(
            ITypeMapper<Data.Models.Category, Category> categoryMapper,
            IUserMapper userMapper,
            ITypeMapper<Data.Models.Priority, Priority> priorityMapper,
            ITypeMapper<Data.Models.Status, Status> statusMapper)
        {
            _categoryMapper = categoryMapper;
            _userMapper = userMapper;
            _priorityMapper = priorityMapper;
            _statusMapper = statusMapper;
        }

        #region ITypeMapper<Task,Task> Members

        Task ITypeMapper<Data.Models.Task, Task>.Create(Data.Models.Task from)
        {
            return new Task
            {
                Assignees = from.Assignees
                    .Select(a => _userMapper.Create(a))
                    .ToList(),
                Categories = from.Categories
                    .Select(c => _categoryMapper.Create(c))
                    .ToList(),
                DateCompleted = from.DateCompleted,
                DueDate = from.DueDate,
                Priority = _priorityMapper.Create(from.Priority),
                StartDate = from.StartDate,
                Status = _statusMapper.Create(from.Status),
                Subject = from.Subject,
                TaskId = from.TaskId,
                Links = new List<Link>
                {
                    new Link
                    {
                        Rel = "self",
                        Title = "self",
                        Href = "/api/Tasks/" + from.TaskId
                    },
                    new Link
                    {
                        Rel = "all",
                        Title = "All Tasks",
                        Href = "/api/Tasks"
                    },
                    new Link
                    {
                        Rel = "categories",
                        Title = "Categories",
                        Href = "/api/Tasks/" + from.TaskId + "/categories"
                    },
                    new Link
                    {
                        Rel = "users",
                        Title = "Assignees",
                        Href = "/api/Tasks/" + from.TaskId + "/users"
                    }
                }
            };
        }

        #endregion
    }
}