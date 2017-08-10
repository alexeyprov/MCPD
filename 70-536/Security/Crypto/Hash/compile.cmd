csc /t:library HashHelper.cs
csc /r:HashHelper.dll ComputeHash.cs
csc /r:HashHelper.dll VerifyHash.cs