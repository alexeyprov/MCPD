copy /y ..\..\ModularMath\bin\Debug\net46\ModularMath.dll .\ModularMath.dll
csc /out:ConsoleTest.exe /r:ModularMath.dll ..\Program.cs