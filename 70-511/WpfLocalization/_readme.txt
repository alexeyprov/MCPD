WPF Localization Examples
-------------------------
See http://wpflocalization.codeplex.com for associate whitepaper and updates to code.


The WpfLocalization Solution contains three projects:

* WpfLocalizationLocBaml
* WpfLocalizationResx
* WpfControls

To run these examples mark either one of them as the startup project and run from within Visual Studio or execute the binaries out of the Debug folder.


LocBaml Example Notes:
----------------------
The LocBaml Example requires that LocBaml /generate is called on each compilation step. The project is set up with the msbuild script described in the article and automatically calls LocBaml /generate. You can examine the build script (LocBamlCsv.target.xml) that is merged into the .csproj file.

There are also several batch files provided that facilitate running the one time command line utilities as well as running LocBaml resource generation via batch file.

 MakeUids.bat - generates x:Uids for the project
 LocBaml_CreateCsv.bat - creates the en-US.csv file for localization 
 LocBaml_CreateResourceAssembly.bat - merges resources direct to a resource assembly
   (use when no Resx resources are use in combination with LocBaml)
 LocBaml_CreateResourceAssembly_MixedBamlResx.bat - merges both Resx and LocBaml
   resources in the project

One of the latter two batch files can be called as a Post BUild task in lieu of the msbuild task.

LocBaml /generate is called on every compile and it merges resources from the project's \RES folder.

The application is localized only in German. Hebrew is also provided but only to demonstrate left to right operation. No Hebrew translation is provided.

Note: If the project fails to run the first time you access it make sure you do a complete recompile to ensure resources get created properly.

WpfLocalizationResx Example Notes:
----------------------------------
This example is straight forward and demonstrates x:Static, ResExtension and Attache Property Resx binding. There are no special notes for this project. Just run and examine the source to see how resource bindings are attached.


WpfControls Project:
--------------------
This project holds the ResExtension and TranslateExtension classes plus a few small utility classes. The WpfLocalizationResx project has a dependency on this project.



