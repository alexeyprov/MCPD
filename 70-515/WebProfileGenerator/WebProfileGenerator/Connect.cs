using System;
using Extensibility;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.CommandBars;
using System.Resources;
using System.Reflection;
using System.Globalization;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Windows.Forms;

namespace WebProfileGenerator
{
	/// <summary>The object for implementing an Add-in.</summary>
	/// <seealso class='IDTExtensibility2' />
    [PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
    [ComVisible(true)]
    public sealed class Connect : IDTExtensibility2, IDTCommandTarget
	{
		/// <summary>Implements the constructor for the Add-in object. Place your initialization code within this method.</summary>
		public Connect()
		{
		}

		/// <summary>Implements the OnConnection method of the IDTExtensibility2 interface. Receives notification that the Add-in is being loaded.</summary>
		/// <param term='application'>Root object of the host application.</param>
		/// <param term='connectMode'>Describes how the Add-in is being loaded.</param>
		/// <param term='addInInst'>Object representing this Add-in.</param>
		/// <seealso class='IDTExtensibility2' />
        void IDTExtensibility2.OnConnection(object application, ext_ConnectMode connectMode, object addInInst, ref Array custom)
		{
			_applicationObject = (DTE2)application;
			_addInInstance = (AddIn)addInInst;
			if(connectMode == ext_ConnectMode.ext_cm_UISetup)
			{
				object []contextGUIDS = new object[] { };
				Commands2 commands = (Commands2)_applicationObject.Commands;
				string toolsMenuName;

				try
				{
					//If you would like to move the command to a different menu, change the word "Tools" to the 
					//  English version of the menu. This code will take the culture, append on the name of the menu
					//  then add the command to that menu. You can find a list of all the top-level menus in the file
					//  CommandBar.resx.
					ResourceManager resourceManager = new ResourceManager("WebProfileGenerator.CommandBar", Assembly.GetExecutingAssembly());
					CultureInfo cultureInfo = new System.Globalization.CultureInfo(_applicationObject.LocaleID);
					string resourceName = String.Concat(cultureInfo.TwoLetterISOLanguageName, "Tools");
					toolsMenuName = resourceManager.GetString(resourceName);
				}
				catch (Exception)
				{
					//We tried to find a localized version of the word Tools, but one was not found.
					//  Default to the en-US word, which may work for the current culture.
					toolsMenuName = "Tools";
				}

				//Place the command on the tools menu.
				//Find the MenuBar command bar, which is the top-level command bar holding all the main menu items:
				Microsoft.VisualStudio.CommandBars.CommandBar menuBarCommandBar = ((Microsoft.VisualStudio.CommandBars.CommandBars)_applicationObject.CommandBars)["MenuBar"];

				//Find the Tools command bar on the MenuBar command bar:
                CommandBarControl toolsControl = menuBarCommandBar.Controls[toolsMenuName];
				CommandBarPopup toolsPopup = (CommandBarPopup)toolsControl;

                CommandBar itemCmdBar = ((CommandBars)_applicationObject.CommandBars)["Item"];
 
				//This try/catch block can be duplicated if you wish to add multiple commands to be handled by your Add-in,
				//  just make sure you also update the QueryStatus/Exec method to include the new command names.
				try
				{
					//Add a command to the Commands collection:
                    Command command = commands.AddNamedCommand2(_addInInstance, "GenerateWebProfile", "Generate WebProfile", "Generates WebProfile class for strongly typed access to web profile.", true, 0, ref contextGUIDS, (int)vsCommandStatus.vsCommandStatusSupported  | (int)vsCommandStatus.vsCommandStatusInvisible, 0, vsCommandControlType.vsCommandControlTypeButton);
                    
					//Add a control for the command to the tools menu:
					if((command != null) && (toolsPopup != null))
					{
						command.AddControl(toolsPopup.CommandBar, 1);
                        command.AddControl(itemCmdBar, 1);
					}
				}
				catch(System.ArgumentException)
				{
					//If we are here, then the exception is probably because a command with that name
					//  already exists. If so there is no need to recreate the command and we can 
                    //  safely ignore the exception.
				}
			}
		}

		/// <summary>Implements the OnDisconnection method of the IDTExtensibility2 interface. Receives notification that the Add-in is being unloaded.</summary>
		/// <param term='disconnectMode'>Describes how the Add-in is being unloaded.</param>
		/// <param term='custom'>Array of parameters that are host application specific.</param>
		/// <seealso class='IDTExtensibility2' />
        void IDTExtensibility2.OnDisconnection(ext_DisconnectMode disconnectMode, ref Array custom)
		{
		}

		/// <summary>Implements the OnAddInsUpdate method of the IDTExtensibility2 interface. Receives notification when the collection of Add-ins has changed.</summary>
		/// <param term='custom'>Array of parameters that are host application specific.</param>
		/// <seealso class='IDTExtensibility2' />		
        void IDTExtensibility2.OnAddInsUpdate(ref Array custom)
		{
		}

		/// <summary>Implements the OnStartupComplete method of the IDTExtensibility2 interface. Receives notification that the host application has completed loading.</summary>
		/// <param term='custom'>Array of parameters that are host application specific.</param>
		/// <seealso class='IDTExtensibility2' />
        void IDTExtensibility2.OnStartupComplete(ref Array custom)
		{
		}

		/// <summary>Implements the OnBeginShutdown method of the IDTExtensibility2 interface. Receives notification that the host application is being unloaded.</summary>
		/// <param term='custom'>Array of parameters that are host application specific.</param>
		/// <seealso class='IDTExtensibility2' />
        void IDTExtensibility2.OnBeginShutdown(ref Array custom)
		{
		}
		
		/// <summary>Implements the QueryStatus method of the IDTCommandTarget interface. This is called when the command's availability is updated</summary>
		/// <param term='commandName'>The name of the command to determine state for.</param>
		/// <param term='neededText'>Text that is needed for the command.</param>
		/// <param term='status'>The state of the command in the user interface.</param>
		/// <param term='commandText'>Text requested by the neededText parameter.</param>
		/// <seealso class='Exec' />
        void IDTCommandTarget.QueryStatus(string commandName, vsCommandStatusTextWanted neededText, ref vsCommandStatus status, ref object commandText)
		{
			if(neededText == vsCommandStatusTextWanted.vsCommandStatusTextWantedNone)
			{
				if(commandName == "WebProfileGenerator.Connect.GenerateWebProfile")
				{
                    ProjectItem selectedItem = GetSelectedProjectItem();
                    if (IsWebConfig(selectedItem) && IsWebApplicationProject(selectedItem.ContainingProject))
                    {
                        status = (vsCommandStatus)vsCommandStatus.vsCommandStatusSupported | vsCommandStatus.vsCommandStatusEnabled;
                    }
                    else
                    {
                        status = (vsCommandStatus)vsCommandStatus.vsCommandStatusSupported | vsCommandStatus.vsCommandStatusInvisible;
                    }
					return;
				}
			}
		}

		/// <summary>Implements the Exec method of the IDTCommandTarget interface. This is called when the command is invoked.</summary>
		/// <param term='commandName'>The name of the command to execute.</param>
		/// <param term='executeOption'>Describes how the command should be run.</param>
		/// <param term='varIn'>Parameters passed from the caller to the command handler.</param>
		/// <param term='varOut'>Parameters passed from the command handler to the caller.</param>
		/// <param term='handled'>Informs the caller if the command was handled or not.</param>
		/// <seealso class='Exec' />
        void IDTCommandTarget.Exec(string commandName, vsCommandExecOption executeOption, ref object varIn, ref object varOut, ref bool handled)
		{
			handled = false;
			if(executeOption == vsCommandExecOption.vsCommandExecOptionDoDefault)
			{
				if(commandName == "WebProfileGenerator.Connect.GenerateWebProfile")
				{
                    ProjectItem selectedItem = GetSelectedProjectItem();
                    if (IsWebConfig(selectedItem) && IsWebApplicationProject(selectedItem.ContainingProject))
                    {
                        handled = true;
                        try
                        {
                            WebProfileGenerator.Generate(selectedItem);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Web Profile Generation Failed");
                        }
                    }
					return;
				}
			}
		}
		private DTE2 _applicationObject;
		private AddIn _addInInstance;

        /// <summary>
        ///    Gets the currently selected project item in Visual Studio.
        ///    Returns null if more than one item is selected or no items are selected.
        /// </summary>
        private ProjectItem GetSelectedProjectItem()
        {
            if (_applicationObject != null)
            {
                SelectedItems selectedItems = _applicationObject.SelectedItems;
                if (selectedItems != null && !selectedItems.MultiSelect && selectedItems.Count == 1)
                {
                    SelectedItem selectedItem = selectedItems.Item(1);
                    if (selectedItem != null)
                    {
                        return selectedItem.ProjectItem;
                    }
                }
            }
            return null;
        }

        /// <summary>
        ///    Determines if the provided project item is web.config.
        /// </summary>
        private static bool IsWebConfig(ProjectItem item)
        {
            if (item != null)
            {
                string name = item.Name;
                if (!string.IsNullOrEmpty(name))
                {
                    const string webConfig = "web.config";
                    if (name.Length == webConfig.Length
                        && string.Compare(name, webConfig, StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        ///    Determines if the provided project is a Web Application Project.
        /// </summary>
        private static bool IsWebApplicationProject(Project project)
        {
            if (project != null)
            {
                object extender = null;
                try
                {
                    extender = project.get_Extender("WebApplication");
                }
                catch
                {
                }
                if (extender != null)
                {
                    return true;
                }
            }
            return false;
        }
    }
}