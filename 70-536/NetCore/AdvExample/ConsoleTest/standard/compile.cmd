copy /y ..\..\ModularMath\bin\Debug\netstandard1.0\ModularMath.dll .\ModularMath.dll
csc /out:ConsoleTest.exe /r:ModularMath.dll ..\Program.cs