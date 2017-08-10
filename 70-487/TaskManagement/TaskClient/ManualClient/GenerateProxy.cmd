svcutil /language:VB /namespace:*,TaskWcfService /async /out:ManualProxy.vb /config:app.config net.tcp://localhost/WasHost/TaskService.svc
SvcUtil.exe http://localhost:8080/TaskService /out:Tasks30.cs /noconfig /async /tcv:Version30
SvcUtil.exe http://localhost:8080/TaskService /out:Tasks35.cs /noconfig /async /tcv:Version35
SvcUtil.exe http://localhost:8080/TaskService /out:Tasks40.cs /noconfig /async
SvcUtil.exe http://localhost:8080/TaskService /out:Tasks45.cs /noconfig
