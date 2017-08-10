2015-01-24 17:32:59,653 DEBUG [8] TasksWebApiServer - ENTERING Tasks.WebApi.Server.Controllers.TasksController::Get
2015-01-24 17:32:59,707 DEBUG [8] TasksWebApiServer - EXITING Tasks.WebApi.Server.Controllers.TasksController::Get
2015-01-24 17:37:00,626 DEBUG [15] TasksWebApiServer - ENTERING Tasks.WebApi.Server.Controllers.TasksController::Get
2015-01-24 17:38:21,698 DEBUG [15] TasksWebApiServer - EXITING Tasks.WebApi.Server.Controllers.TasksController::Get
2015-01-24 17:41:51,533 DEBUG [9] TasksWebApiServer - ENTERING Tasks.WebApi.Server.Controllers.TasksController::Get
2015-01-24 17:41:52,269 ERROR [9] TasksWebApiServer - Unhandled API controller exception
System.NullReferenceException: Object reference not set to an instance of an object.
   at Tasks.WebApi.Server.Mappers.UserMapper.Tasks.WebApi.Server.Mappers.IUserMapper.Create(String username, String firstname, String lastname, String email, Guid userId) in c:\Projects\MCPD\70-487\Tasks.WebApi\Web\Tasks.WebApi.Server\Mappers\UserMapper.cs:line 13
   at Tasks.WebApi.Server.Mappers.UserMapper.Tasks.WebApi.Server.Mappers.ITypeMapper<Tasks.Data.Models.User,Tasks.WebApi.Models.User>.Create(User from) in c:\Projects\MCPD\70-487\Tasks.WebApi\Web\Tasks.WebApi.Server\Mappers\UserMapper.cs:line 44
   at Tasks.WebApi.Server.Mappers.TaskMapper.<Tasks.WebApi.Server.Mappers.ITypeMapper<Tasks.Data.Models.Task,Tasks.WebApi.Models.Task>.Create>b__5(User a) in c:\Projects\MCPD\70-487\Tasks.WebApi\Web\Tasks.WebApi.Server\Mappers\TaskMapper.cs:line 33
   at System.Linq.Enumerable.WhereSelectEnumerableIterator`2.MoveNext()
   at System.Collections.Generic.List`1..ctor(IEnumerable`1 collection)
   at System.Linq.Enumerable.ToList[TSource](IEnumerable`1 source)
   at Tasks.WebApi.Server.Mappers.TaskMapper.Tasks.WebApi.Server.Mappers.ITypeMapper<Tasks.Data.Models.Task,Tasks.WebApi.Models.Task>.Create(Task from) in c:\Projects\MCPD\70-487\Tasks.WebApi\Web\Tasks.WebApi.Server\Mappers\TaskMapper.cs:line 30
   at System.Linq.Enumerable.WhereSelectEnumerableIterator`2.MoveNext()
   at System.Linq.Buffer`1..ctor(IEnumerable`1 source)
   at System.Linq.Enumerable.ToArray[TSource](IEnumerable`1 source)
   at Tasks.WebApi.Server.Controllers.TasksController.Get() in c:\Projects\MCPD\70-487\Tasks.WebApi\Web\Tasks.WebApi.Server\Controllers\TasksController.cs:line 44
   at lambda_method(Closure , Object , Object[] )
   at System.Web.Http.Controllers.ReflectedHttpActionDescriptor.ActionExecutor.<>c__DisplayClass10.<GetExecutor>b__9(Object instance, Object[] methodParameters)
   at System.Web.Http.Controllers.ReflectedHttpActionDescriptor.ActionExecutor.Execute(Object instance, Object[] arguments)
   at System.Web.Http.Controllers.ReflectedHttpActionDescriptor.ExecuteAsync(HttpControllerContext controllerContext, IDictionary`2 arguments, CancellationToken cancellationToken)
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter`1.GetResult()
   at System.Web.Http.Controllers.ApiControllerActionInvoker.<InvokeActionAsyncCore>d__0.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter`1.GetResult()
   at System.Web.Http.Filters.ActionFilterAttribute.<CallOnActionExecutedAsync>d__5.MoveNext()
2015-01-24 17:41:52,278 DEBUG [9] TasksWebApiServer - EXITING Tasks.WebApi.Server.Controllers.TasksController::Get
2015-01-24 17:42:05,526 DEBUG [9] TasksWebApiServer - ENTERING Tasks.WebApi.Server.Controllers.TasksController::Get
2015-01-24 17:42:05,579 ERROR [9] TasksWebApiServer - Unhandled API controller exception
System.NullReferenceException: Object reference not set to an instance of an object.
   at Tasks.WebApi.Server.Mappers.UserMapper.Tasks.WebApi.Server.Mappers.IUserMapper.Create(String username, String firstname, String lastname, String email, Guid userId) in c:\Projects\MCPD\70-487\Tasks.WebApi\Web\Tasks.WebApi.Server\Mappers\UserMapper.cs:line 13
   at Tasks.WebApi.Server.Mappers.UserMapper.Tasks.WebApi.Server.Mappers.ITypeMapper<Tasks.Data.Models.User,Tasks.WebApi.Models.User>.Create(User from) in c:\Projects\MCPD\70-487\Tasks.WebApi\Web\Tasks.WebApi.Server\Mappers\UserMapper.cs:line 44
   at Tasks.WebApi.Server.Mappers.TaskMapper.<Tasks.WebApi.Server.Mappers.ITypeMapper<Tasks.Data.Models.Task,Tasks.WebApi.Models.Task>.Create>b__5(User a) in c:\Projects\MCPD\70-487\Tasks.WebApi\Web\Tasks.WebApi.Server\Mappers\TaskMapper.cs:line 33
   at System.Linq.Enumerable.WhereSelectEnumerableIterator`2.MoveNext()
   at System.Collections.Generic.List`1..ctor(IEnumerable`1 collection)
   at System.Linq.Enumerable.ToList[TSource](IEnumerable`1 source)
   at Tasks.WebApi.Server.Mappers.TaskMapper.Tasks.WebApi.Server.Mappers.ITypeMapper<Tasks.Data.Models.Task,Tasks.WebApi.Models.Task>.Create(Task from) in c:\Projects\MCPD\70-487\Tasks.WebApi\Web\Tasks.WebApi.Server\Mappers\TaskMapper.cs:line 30
   at System.Linq.Enumerable.WhereSelectEnumerableIterator`2.MoveNext()
   at System.Linq.Buffer`1..ctor(IEnumerable`1 source)
   at System.Linq.Enumerable.ToArray[TSource](IEnumerable`1 source)
   at Tasks.WebApi.Server.Controllers.TasksController.Get() in c:\Projects\MCPD\70-487\Tasks.WebApi\Web\Tasks.WebApi.Server\Controllers\TasksController.cs:line 44
   at lambda_method(Closure , Object , Object[] )
   at System.Web.Http.Controllers.ReflectedHttpActionDescriptor.ActionExecutor.<>c__DisplayClass10.<GetExecutor>b__9(Object instance, Object[] methodParameters)
   at System.Web.Http.Controllers.ReflectedHttpActionDescriptor.ActionExecutor.Execute(Object instance, Object[] arguments)
   at System.Web.Http.Controllers.ReflectedHttpActionDescriptor.ExecuteAsync(HttpControllerContext controllerContext, IDictionary`2 arguments, CancellationToken cancellationToken)
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter`1.GetResult()
   at System.Web.Http.Controllers.ApiControllerActionInvoker.<InvokeActionAsyncCore>d__0.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter`1.GetResult()
   at System.Web.Http.Filters.ActionFilterAttribute.<CallOnActionExecutedAsync>d__5.MoveNext()
2015-01-24 17:42:05,579 DEBUG [9] TasksWebApiServer - EXITING Tasks.WebApi.Server.Controllers.TasksController::Get
2015-01-24 17:42:20,010 DEBUG [6] TasksWebApiServer - ENTERING Tasks.WebApi.Server.Controllers.CategoriesController::Get
2015-01-24 17:42:20,091 DEBUG [6] TasksWebApiServer - EXITING Tasks.WebApi.Server.Controllers.CategoriesController::Get
2015-01-24 17:43:36,961 DEBUG [9] TasksWebApiServer - ENTERING Tasks.WebApi.Server.Controllers.CategoriesController::Get
2015-01-24 17:46:04,164 DEBUG [9] TasksWebApiServer - EXITING Tasks.WebApi.Server.Controllers.CategoriesController::Get
2015-01-24 17:47:00,747 DEBUG [9] TasksWebApiServer - ENTERING Tasks.WebApi.Server.Controllers.CategoriesController::Get
2015-01-24 17:47:08,407 DEBUG [9] TasksWebApiServer - EXITING Tasks.WebApi.Server.Controllers.CategoriesController::Get
2015-01-24 17:49:42,442 DEBUG [10] TasksWebApiServer - ENTERING Tasks.WebApi.Server.Controllers.CategoriesController::Get
2015-01-24 17:49:42,539 DEBUG [10] TasksWebApiServer - EXITING Tasks.WebApi.Server.Controllers.CategoriesController::Get
2015-01-24 17:50:25,214 DEBUG [5] TasksWebApiServer - ENTERING Tasks.WebApi.Server.Controllers.UsersController::Get
2015-01-24 17:50:25,221 DEBUG [5] TasksWebApiServer - EXITING Tasks.WebApi.Server.Controllers.UsersController::Get
2015-01-24 17:50:55,609 DEBUG [7] TasksWebApiServer - ENTERING Tasks.WebApi.Server.Controllers.CategoriesController::Get
2015-01-24 17:50:55,615 DEBUG [7] TasksWebApiServer - EXITING Tasks.WebApi.Server.Controllers.CategoriesController::Get
2015-01-24 17:51:03,941 DEBUG [8] TasksWebApiServer - ENTERING Tasks.WebApi.Server.Controllers.PrioritiesController::Get
2015-01-24 17:51:03,950 DEBUG [8] TasksWebApiServer - EXITING Tasks.WebApi.Server.Controllers.PrioritiesController::Get
2015-01-24 17:53:17,147 DEBUG [9] TasksWebApiServer - ENTERING Tasks.WebApi.Server.Controllers.PrioritiesController::Get
2015-01-24 17:53:17,254 DEBUG [9] TasksWebApiServer - EXITING Tasks.WebApi.Server.Controllers.PrioritiesController::Get
2015-01-24 17:53:39,534 DEBUG [10] TasksWebApiServer - ENTERING Tasks.WebApi.Server.Controllers.PrioritiesController::Get
2015-01-24 17:53:39,540 DEBUG [10] TasksWebApiServer - EXITING Tasks.WebApi.Server.Controllers.PrioritiesController::Get
2015-01-24 17:53:45,833 DEBUG [10] TasksWebApiServer - ENTERING Tasks.WebApi.Server.Controllers.PrioritiesController::Get
2015-01-24 17:53:45,904 DEBUG [10] TasksWebApiServer - EXITING Tasks.WebApi.Server.Controllers.PrioritiesController::Get
2015-01-24 17:54:07,362 DEBUG [9] TasksWebApiServer - ENTERING Tasks.WebApi.Server.Controllers.PrioritiesController::Get
2015-01-24 17:54:07,398 DEBUG [9] TasksWebApiServer - EXITING Tasks.WebApi.Server.Controllers.PrioritiesController::Get
2015-01-24 17:55:02,157 DEBUG [9] TasksWebApiServer - ENTERING Tasks.WebApi.Server.Controllers.TasksController::Get
2015-01-24 17:55:02,668 DEBUG [9] TasksWebApiServer - EXITING Tasks.WebApi.Server.Controllers.TasksController::Get
2015-01-24 17:56:01,881 DEBUG [11] TasksWebApiServer - ENTERING Tasks.WebApi.Server.Controllers.TasksController::Get
2015-01-24 17:56:01,887 DEBUG [11] TasksWebApiServer - EXITING Tasks.WebApi.Server.Controllers.TasksController::Get
2015-01-24 17:56:22,066 DEBUG [9] TasksWebApiServer - ENTERING Tasks.WebApi.Server.Controllers.UsersController::Get
2015-01-24 17:56:22,069 DEBUG [9] TasksWebApiServer - EXITING Tasks.WebApi.Server.Controllers.UsersController::Get
2015-01-24 20:49:58,094 DEBUG [10] TasksWebApiServer - ENTERING Tasks.WebApi.Server.Controllers.StatusesController::Get
2015-01-24 20:49:58,214 DEBUG [10] TasksWebApiServer - EXITING Tasks.WebApi.Server.Controllers.StatusesController::Get
2015-01-24 20:50:16,883 DEBUG [9] TasksWebApiServer - ENTERING Tasks.WebApi.Server.Controllers.StatusesController::Get
2015-01-24 20:50:16,888 DEBUG [9] TasksWebApiServer - EXITING Tasks.WebApi.Server.Controllers.StatusesController::Get
2015-01-24 22:40:35,686 DEBUG [65] TasksWebApiServer - ENTERING Tasks.WebApi.Server.Controllers.CategoriesController::Get
2015-01-24 22:40:35,692 DEBUG [65] TasksWebApiServer - EXITING Tasks.WebApi.Server.Controllers.CategoriesController::Get
2015-01-24 22:43:59,692 DEBUG [64] TasksWebApiServer - ENTERING Tasks.WebApi.Server.Controllers.CategoriesController::Post
2015-01-24 22:43:59,713 DEBUG [64] TasksWebApiServer - EXITING Tasks.WebApi.Server.Controllers.CategoriesController::Post
2015-01-24 22:44:32,853 DEBUG [64] TasksWebApiServer - ENTERING Tasks.WebApi.Server.Controllers.CategoriesController::Get
2015-01-24 22:44:32,853 DEBUG [64] TasksWebApiServer - EXITING Tasks.WebApi.Server.Controllers.CategoriesController::Get
2015-01-24 22:45:28,617 DEBUG [66] TasksWebApiServer - ENTERING Tasks.WebApi.Server.Controllers.CategoriesController::Put
2015-01-24 22:45:28,646 DEBUG [66] TasksWebApiServer - EXITING Tasks.WebApi.Server.Controllers.CategoriesController::Put
2015-01-24 22:45:47,819 DEBUG [68] TasksWebApiServer - ENTERING Tasks.WebApi.Server.Controllers.CategoriesController::Get
2015-01-24 22:45:47,821 DEBUG [68] TasksWebApiServer - EXITING Tasks.WebApi.Server.Controllers.CategoriesController::Get
2015-01-24 22:47:11,655 DEBUG [60] TasksWebApiServer - ENTERING Tasks.WebApi.Server.Controllers.CategoriesController::Delete
2015-01-24 22:47:11,680 DEBUG [60] TasksWebApiServer - EXITING Tasks.WebApi.Server.Controllers.CategoriesController::Delete
2015-01-24 22:47:23,212 DEBUG [60] TasksWebApiServer - ENTERING Tasks.WebApi.Server.Controllers.CategoriesController::Get
2015-01-24 22:47:23,247 DEBUG [60] TasksWebApiServer - EXITING Tasks.WebApi.Server.Controllers.CategoriesController::Get
2015-01-24 22:47:51,192 DEBUG [64] TasksWebApiServer - ENTERING Tasks.WebApi.Server.Controllers.CategoriesController::Get
2015-01-24 22:47:51,194 DEBUG [64] TasksWebApiServer - EXITING Tasks.WebApi.Server.Controllers.CategoriesController::Get
2015-01-24 22:51:50,897 DEBUG [70] TasksWebApiServer - ENTERING Tasks.WebApi.Server.Controllers.UsersController::Post
2015-01-24 22:52:16,130 ERROR [70] TasksWebApiServer - Unhandled API controller exception
System.NullReferenceException: Object reference not set to an instance of an object.
   at Tasks.WebApi.Server.Controllers.UsersController.Post(HttpRequestMessage request, User user) in c:\Projects\MCPD\70-487\Tasks.WebApi\Web\Tasks.WebApi.Server\Controllers\UsersController.cs:line 51
   at lambda_method(Closure , Object , Object[] )
   at System.Web.Http.Controllers.ReflectedHttpActionDescriptor.ActionExecutor.<>c__DisplayClass10.<GetExecutor>b__9(Object instance, Object[] methodParameters)
   at System.Web.Http.Controllers.ReflectedHttpActionDescriptor.ActionExecutor.Execute(Object instance, Object[] arguments)
   at System.Web.Http.Controllers.ReflectedHttpActionDescriptor.ExecuteAsync(HttpControllerContext controllerContext, IDictionary`2 arguments, CancellationToken cancellationToken)
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter`1.GetResult()
   at System.Web.Http.Controllers.ApiControllerActionInvoker.<InvokeActionAsyncCore>d__0.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter`1.GetResult()
   at System.Web.Http.Filters.ActionFilterAttribute.<CallOnActionExecutedAsync>d__5.MoveNext()
2015-01-24 22:52:16,147 DEBUG [70] TasksWebApiServer - EXITING Tasks.WebApi.Server.Controllers.UsersController::Post
