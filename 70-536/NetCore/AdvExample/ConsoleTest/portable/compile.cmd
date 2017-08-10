copy /y "..\..\ModularMath\bin\Debug\portable40-net40+sl5+win8+wp8+wpa81\ModularMath.dll" .\ModularMath.dll
csc /out:ConsoleTest.exe /r:ModularMath.dll ..\Program.cs