cd Client
csc /debug AuthenticatedClient.cs
cd ..\Server
csc /debug AuthenticatingServer.cs ClientState.cs
cd ..