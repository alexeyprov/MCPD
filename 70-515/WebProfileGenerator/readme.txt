Web Profile Generator
=====================
The Web Profile Generator is an add-in for Visual Studio 2005 that generates a
strongly typed class for accessing the ASP.NET profile in Web Application
Projects.  This add-in does not work in Visual Studio 2002 or 2003.  This
add-in also does not apply to Web site projects in Visual Studio 2005.

To install and use the add-in, run WebProfileGenerator.msi. Alternatively, you
can manually install the add-in by copying WebProfileGenerator.dll and
WebProfileGenerator.AddIn to your "My Documents\Visual Studio 2005\AddIns"
folder.

To use the generator, right click the Web.config file in a Web Application Project 
and then select "Generate WebProfile."  This will create a WebProfile class in your 
project based on the current profile settings in the Web.config file.  The WebProfile 
class is a wrapper class that has strongly typed accessors to profile properties. If 
you make a change to your profile setting, you must run the tool again to update 
the WebProfile class.  

To use the WebProfile class in a page, create a get accessor as shown in the 
following example:

    // C# accessor
    private WebProfile Profile
    {
        get { return new WebProfile(Context.Profile); }
    }

    ' Visual Basic accessor
    Private ReadOnly Property Profile() As WebProfile
        Get
            Return New WebProfile(Context.Profile)
        End Get
    End Property

To use the WebProfile class, use code like the following example:

    // Using C# accessor
    string s = Profile.MyProperty;
    Profile.MyGroup.MyProperty = "value";

    ' Using Visual Basic accessor
    Dim prop As String = Profile.MyProperty
    Profile.MyGroup.MyProperty = "value"

You can also access the current profile by using the static Current property, 
as in the following example:

    // Using the Current property in C#
    string s = WebProfile.Current.MyProperty;
    WebProfile.Current.MyGroup.MyProperty = "value";

    // Using the Current property in Visual Basic
    Dim s As String = WebProfile.Current.MyProperty
    WebProfile.Current.MyGroup.MyProperty = "value"
    
The WebProfile class has all the members of the BaseProfile class and can 
generally be used in the same way. For your own classes and application
layers, it is valid to pass an instance of the WebProfile class strongly typed. However,
if you must pass a WebProfile instance to something that expects a
BaseProfile class, casting will not work. Instead, pass the BaseProfile
property, which returns the inner BaseProfile object, as shown in the following 
example:

    // Using GetProfile and BaseProfile in C#
    WebProfile webProfile = WebProfile.GetProfile("username");
    webProfile.MyProperty = "value";
    PassToFunctionThatWantsBaseProfile(webProfile.BaseProfile);

    // Using GetProfile and BaseProfile in Visual Basic
    Dim webProfile As WebProfile = WebProfile.GetProfile("username")
    webProfile.MyProperty = "value"
    PassToFunctionThatWantsBaseProfile webProfile.BaseProfile

The source code for the add-in is in the WebProfileGenerator folder, and the
source code for the installer is in the WebProfileGeneratorInstall folder.
You can load and build them all in Visual Studio 2005 using the
WebProfileGenerator.sln file.