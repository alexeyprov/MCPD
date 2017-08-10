Steps before deploying to Azure.

1.In the web project, Create a new folder named Startup.
2.Add the files: RegisterDLL.cmd and Windows6.0-KB974405-x64 from the Shared/aExpense/Startup folder

3.In the web role's Service Definition file, Include the following snippet

<Startup>
      <Task commandLine="Startup\RegisterDLL.cmd" executionContext="elevated" taskType="simple" />
</Startup>