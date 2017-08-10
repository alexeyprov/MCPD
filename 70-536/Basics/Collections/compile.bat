rem Compile CollectionHelper library
cd CollectionHelper
csc /debug /out:CollectionHelperLib.dll /t:library CollectionHelper.cs GenericCollectionHelper.cs
copy CollectionHelperLib.* ..\NonGeneric
copy CollectionHelperLib.* ..\Generic
copy CollectionHelperLib.* ..\Specialized

rem Compile TestModel library
cd ..\TestModel
csc /debug /out:TestModel.dll /t:library Employee.cs IEmployeeGetter.cs
copy TestModel.* ..\NonGeneric
copy TestModel.* ..\Generic
copy TestModel.* ..\Specialized

rem Compile non-generic samples
cd ..\NonGeneric
csc /debug /out:CollectionTest.exe /r:TestModel.dll /r:CollectionHelperLib.dll CollectionTest.cs EnumerableEmployees.cs Employees.cs RoEmployees.cs EmployeesByID.cs

rem Compile generic samples
cd ..\Generic
csc /debug /out:GenericTest.exe /r:TestModel.dll /r:CollectionHelperLib.dll GenericTest.cs

rem Compile specialized collections samples
cd ..\Specialized
csc /debug /r:TestModel.dll /r:CollectionHelperLib.dll SpecializedTest.cs

cd..