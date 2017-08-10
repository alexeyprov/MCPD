rem Compiling and running v1
csc /t:library /out:MyLib.dll MyLibV1.cs
csc /r:MyLib.dll TestApp.cs
TestApp

rem Compiling and running v2
csc /t:library MyOtherLib.cs
csc /t:library /r:MyOtherLib.dll /out:MyLib.dll MyLibV2.cs
TestApp