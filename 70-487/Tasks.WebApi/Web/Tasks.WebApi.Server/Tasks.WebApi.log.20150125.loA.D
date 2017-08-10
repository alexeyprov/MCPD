2015-01-25 00:07:21,761 DEBUG [7] TasksWebApiServer - ENTERING Tasks.WebApi.Server.Controllers.UsersController::Post
2015-01-25 00:09:47,650 ERROR [7] TasksWebApiServer - Unhandled API controller exception
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
2015-01-25 00:09:47,654 DEBUG [7] TasksWebApiServer - EXITING Tasks.WebApi.Server.Controllers.UsersController::Post
2015-01-25 00:10:21,794 DEBUG [7] TasksWebApiServer - ENTERING Tasks.WebApi.Server.Controllers.UsersController::Post
2015-01-25 00:10:21,859 ERROR [7] TasksWebApiServer - Unhandled API controller exception
System.InvalidCastException: The SqlParameterCollection only accepts non-null SqlParameter type objects, not Guid objects.
   at System.Data.SqlClient.SqlParameterCollection.ValidateType(Object value)
   at System.Data.SqlClient.SqlParameterCollection.Add(Object value)
   at Tasks.Data.SqlServer.UserRepository.AddParameter(IDbCommand command, String name, Object value) in c:\Projects\MCPD\70-516\Tasks\Tasks.Data.SqlServer\UserRepository.cs:line 50
   at Tasks.Data.SqlServer.UserRepository.SaveUser(Guid userId, String firstname, String lastname) in c:\Projects\MCPD\70-516\Tasks\Tasks.Data.SqlServer\UserRepository.cs:line 36
   at Tasks.WebApi.Server.UserManager.Tasks.WebApi.Server.IUserManager.CreateUser(String username, String password, String firstname, String lastname, String email) in c:\Projects\MCPD\70-487\Tasks.WebApi\Web\Tasks.WebApi.Server\UserManager.cs:line 33
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
2015-01-25 00:10:21,861 DEBUG [7] TasksWebApiServer - EXITING Tasks.WebApi.Server.Controllers.UsersController::Post
2015-01-25 00:38:25,990 DEBUG [9] TasksWebApiServer - ENTERING Tasks.WebApi.Server.Controllers.UsersController::Post
2015-01-25 00:38:26,078 ERROR [9] TasksWebApiServer - Unhandled API controller exception
System.Web.Security.MembershipCreateUserException: The username is already in use.
   at System.Web.Security.Membership.CreateUser(String username, String password, String email)
   at Tasks.WebApi.Common.Security.MembershipAdapter.Tasks.WebApi.Common.Interface.IMembershipInfoProvider.CreateUser(String username, String password, String email) in c:\Projects\MCPD\70-487\Tasks.WebApi\Web\Tasks.WebApi.Common\Security\MembershipAdapter.cs:line 32
   at Tasks.WebApi.Server.UserManager.Tasks.WebApi.Server.IUserManager.CreateUser(String username, String password, String firstname, String lastname, String email) in c:\Projects\MCPD\70-487\Tasks.WebApi\Web\Tasks.WebApi.Server\UserManager.cs:line 30
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
2015-01-25 00:38:26,082 DEBUG [9] TasksWebApiServer - EXITING Tasks.WebApi.Server.Controllers.UsersController::Post
2015-01-25 00:44:01,407 DEBUG [13] TasksWebApiServer - ENTERING Tasks.WebApi.Server.Controllers.UsersController::Post
2015-01-25 00:44:01,503 ERROR [13] TasksWebApiServer - Unhandled API controller exception
System.Data.SqlClient.SqlException (0x80131904): Could not find stored procedure 'dbo.SaveUser'.
   at System.Data.SqlClient.SqlConnection.OnError(SqlException exception, Boolean breakConnection, Action`1 wrapCloseInAction)
   at System.Data.SqlClient.SqlInternalConnection.OnError(SqlException exception, Boolean breakConnection, Action`1 wrapCloseInAction)
   at System.Data.SqlClient.TdsParser.ThrowExceptionAndWarning(TdsParserStateObject stateObj, Boolean callerHasConnectionLock, Boolean asyncClose)
   at System.Data.SqlClient.TdsParser.TryRun(RunBehavior runBehavior, SqlCommand cmdHandler, SqlDataReader dataStream, BulkCopySimpleResultSet bulkCopyHandler, TdsParserStateObject stateObj, Boolean& dataReady)
   at System.Data.SqlClient.SqlCommand.FinishExecuteReader(SqlDataReader ds, RunBehavior runBehavior, String resetOptionsString)
   at System.Data.SqlClient.SqlCommand.RunExecuteReaderTds(CommandBehavior cmdBehavior, RunBehavior runBehavior, Boolean returnStream, Boolean async, Int32 timeout, Task& task, Boolean asyncWrite, SqlDataReader ds)
   at System.Data.SqlClient.SqlCommand.RunExecuteReader(CommandBehavior cmdBehavior, RunBehavior runBehavior, Boolean returnStream, String method, TaskCompletionSource`1 completion, Int32 timeout, Task& task, Boolean asyncWrite)
   at System.Data.SqlClient.SqlCommand.InternalExecuteNonQuery(TaskCompletionSource`1 completion, String methodName, Boolean sendToPipe, Int32 timeout, Boolean asyncWrite)
   at System.Data.SqlClient.SqlCommand.ExecuteNonQuery()
   at Tasks.Data.SqlServer.UserRepository.SaveUser(Guid userId, String firstname, String lastname) in c:\Projects\MCPD\70-516\Tasks\Tasks.Data.SqlServer\UserRepository.cs:line 40
   at Tasks.WebApi.Server.UserManager.Tasks.WebApi.Server.IUserManager.CreateUser(String username, String password, String firstname, String lastname, String email) in c:\Projects\MCPD\70-487\Tasks.WebApi\Web\Tasks.WebApi.Server\UserManager.cs:line 33
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
ClientConnectionId:f2edbbae-1e31-4f04-80ee-00a93531197f
Error Number:2812,State:62,Class:16
2015-01-25 00:44:01,507 DEBUG [13] TasksWebApiServer - EXITING Tasks.WebApi.Server.Controllers.UsersController::Post
2015-01-25 00:55:18,561 DEBUG [7] TasksWebApiServer - ENTERING Tasks.WebApi.Server.Controllers.UsersController::Post
2015-01-25 00:55:18,611 DEBUG [7] TasksWebApiServer - EXITING Tasks.WebApi.Server.Controllers.UsersController::Post
2015-01-25 15:05:34,982 DEBUG [10] TasksWebApiServer - ENTERING Tasks.WebApi.Server.Controllers.TasksController::Get
2015-01-25 15:05:35,204 DEBUG [10] TasksWebApiServer - EXITING Tasks.WebApi.Server.Controllers.TasksController::Get
2015-01-25 15:18:37,080 DEBUG [26] TasksWebApiServer - ENTERING Tasks.WebApi.Server.Controllers.TasksController::Post
2015-01-25 15:18:37,131 DEBUG [26] TasksWebApiServer - EXITING Tasks.WebApi.Server.Controllers.TasksController::Post
2015-01-25 15:19:27,536 DEBUG [13] TasksWebApiServer - ENTERING Tasks.WebApi.Server.Controllers.TasksController::Get
2015-01-25 15:19:27,545 DEBUG [13] TasksWebApiServer - EXITING Tasks.WebApi.Server.Controllers.TasksController::Get
2015-01-25 15:19:52,898 DEBUG [25] TasksWebApiServer - ENTERING Tasks.WebApi.Server.Controllers.TaskPriorityController::Get
2015-01-25 15:19:52,903 DEBUG [25] TasksWebApiServer - EXITING Tasks.WebApi.Server.Controllers.TaskPriorityController::Get
2015-01-25 15:22:03,009 DEBUG [19] TasksWebApiServer - ENTERING Tasks.WebApi.Server.Controllers.TaskPriorityController::Put
2015-01-25 15:22:03,044 DEBUG [19] TasksWebApiServer - EXITING Tasks.WebApi.Server.Controllers.TaskPriorityController::Put
2015-01-25 15:22:14,798 DEBUG [25] TasksWebApiServer - ENTERING Tasks.WebApi.Server.Controllers.TaskPriorityController::Put
2015-01-25 15:22:14,800 DEBUG [25] TasksWebApiServer - EXITING Tasks.WebApi.Server.Controllers.TaskPriorityController::Put
2015-01-25 15:23:19,567 DEBUG [28] TasksWebApiServer - ENTERING Tasks.WebApi.Server.Controllers.TaskPriorityController::Put
2015-01-25 15:23:19,569 DEBUG [28] TasksWebApiServer - EXITING Tasks.WebApi.Server.Controllers.TaskPriorityController::Put
2015-01-25 15:23:32,084 DEBUG [29] TasksWebApiServer - ENTERING Tasks.WebApi.Server.Controllers.TaskPriorityController::Put
2015-01-25 15:23:32,086 DEBUG [29] TasksWebApiServer - EXITING Tasks.WebApi.Server.Controllers.TaskPriorityController::Put
2015-01-25 15:23:52,650 DEBUG [23] TasksWebApiServer - ENTERING Tasks.WebApi.Server.Controllers.TaskPriorityController::Put
2015-01-25 15:23:52,651 DEBUG [23] TasksWebApiServer - EXITING Tasks.WebApi.Server.Controllers.TaskPriorityController::Put
2015-01-25 15:24:08,054 DEBUG [13] TasksWebApiServer - ENTERING Tasks.WebApi.Server.Controllers.TaskPriorityController::Put
2015-01-25 15:24:55,175 DEBUG [13] TasksWebApiServer - EXITING Tasks.WebApi.Server.Controllers.TaskPriorityController::Put
2015-01-25 15:26:02,009 DEBUG [34] TasksWebApiServer - ENTERING Tasks.WebApi.Server.Controllers.TaskPriorityController::Get
2015-01-25 15:26:02,011 DEBUG [34] TasksWebApiServer - EXITING Tasks.WebApi.Server.Controllers.TaskPriorityController::Get
2015-01-25 15:26:17,499 DEBUG [32] TasksWebApiServer - ENTERING Tasks.WebApi.Server.Controllers.TaskCategoriesController::Get
2015-01-25 15:26:17,510 DEBUG [32] TasksWebApiServer - EXITING Tasks.WebApi.Server.Controllers.TaskCategoriesController::Get
2015-01-25 15:26:42,021 DEBUG [32] TasksWebApiServer - ENTERING Tasks.WebApi.Server.Controllers.TaskUsersController::Get
2015-01-25 15:26:42,032 DEBUG [32] TasksWebApiServer - EXITING Tasks.WebApi.Server.Controllers.TaskUsersController::Get
2015-01-25 15:27:47,213 DEBUG [35] TasksWebApiServer - ENTERING Tasks.WebApi.Server.Controllers.TaskUsersController::Put
2015-01-25 15:27:47,237 DEBUG [35] TasksWebApiServer - EXITING Tasks.WebApi.Server.Controllers.TaskUsersController::Put
2015-01-25 15:27:58,104 DEBUG [36] TasksWebApiServer - ENTERING Tasks.WebApi.Server.Controllers.TaskUsersController::Get
2015-01-25 15:27:58,107 DEBUG [36] TasksWebApiServer - EXITING Tasks.WebApi.Server.Controllers.TaskUsersController::Get
2015-01-25 15:28:14,930 DEBUG [33] TasksWebApiServer - ENTERING Tasks.WebApi.Server.Controllers.TaskUsersController::Delete
2015-01-25 15:28:14,939 DEBUG [33] TasksWebApiServer - EXITING Tasks.WebApi.Server.Controllers.TaskUsersController::Delete
2015-01-25 15:28:21,243 DEBUG [36] TasksWebApiServer - ENTERING Tasks.WebApi.Server.Controllers.TaskUsersController::Get
2015-01-25 15:28:21,245 DEBUG [36] TasksWebApiServer - EXITING Tasks.WebApi.Server.Controllers.TaskUsersController::Get
2015-01-25 15:29:41,030 DEBUG [37] TasksWebApiServer - ENTERING Tasks.WebApi.Server.Controllers.TasksController::Get
2015-01-25 15:29:41,309 DEBUG [37] TasksWebApiServer - EXITING Tasks.WebApi.Server.Controllers.TasksController::Get
2015-01-25 15:30:34,517 DEBUG [38] TasksWebApiServer - ENTERING Tasks.WebApi.Server.Controllers.TasksController::Delete
2015-01-25 15:30:34,539 DEBUG [38] TasksWebApiServer - EXITING Tasks.WebApi.Server.Controllers.TasksController::Delete
2015-01-25 15:30:43,480 DEBUG [33] TasksWebApiServer - ENTERING Tasks.WebApi.Server.Controllers.TasksController::Get
2015-01-25 15:30:43,487 DEBUG [33] TasksWebApiServer - EXITING Tasks.WebApi.Server.Controllers.TasksController::Get
