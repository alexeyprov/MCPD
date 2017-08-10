using System;
using System.IO;
using System.Reflection;
using System.Security;
using System.Security.Permissions;
using System.Security.Policy;
using System.Windows.Forms;

namespace SecHostApp
{
	public partial class MainForm : Form
	{
		#region Construction/Destruction
		public MainForm()
		{
			InstallAppDomainPolicy();
			LoadAssemblies();
			InitializeComponent();
		}
		#endregion

		#region Implementation
		private void LoadAssemblies()
		{
			this._fullTrustAssembly = LoadAssembly(GetAssemblyPath("FullTrust"));
			this._untrustedAssembly = LoadAssembly(GetAssemblyPath("UnTrusted"));
		}

		private string GetAssemblyPath(string subFolder)
		{
			const string DLLNAME = "SecureModule.dll";

			string tempPath = Path.GetDirectoryName(Application.ExecutablePath);
			tempPath = Path.Combine(tempPath, subFolder);
			return Path.Combine(tempPath, DLLNAME);
		}

		private Assembly LoadAssembly(string location)
		{
			Evidence e = new Evidence();
			e.AddHost(new Url(location));

			return Assembly.LoadFile(location, e);
		}


		private void RunTest(Assembly asm)
		{
			const string TEST_CLASS = "SecureModule.ChildForm";
			const string TEST_METHOD = "RunTest";

			// This code does not work :(
			//if (!this.chkCustomPerm.Checked)
			//{
			//    new CustomPermission(PermissionState.Unrestricted).Deny();
			//}

			using (IDisposable testObj = (IDisposable) asm.CreateInstance(TEST_CLASS))
			{
				MethodInfo testMethod = testObj.GetType().GetMethod(TEST_METHOD);
				testMethod.Invoke(testObj, new object[] {new MethodInvoker(CallbackFunction)});
			}
		}

		private void CallbackFunction()
		{
			try
			{
				if (this.chkCustomPerm.Checked)
				{
					new CustomPermission(PermissionState.Unrestricted).Demand();
				}
				WriteEvent("CallbackFunction", "OK");
			}
			catch (SecurityException e)
			{
				WriteEvent("CallbackFunction", e.Message);
				//throw;
			}
		}

		private void WriteEvent(string evt, string status)
		{
			ListViewItem newItem = lvwEventLog.Items.Add(evt);
			newItem.SubItems.Add(status);
		}

		private static void InstallAppDomainPolicy()
		{
			string basePath = AppDomain.CurrentDomain.BaseDirectory;
			PolicyLevel appDomainLevel = PolicyLevel.CreateAppDomainLevel();
			
			// Create restricted permission set
			PermissionSet restrictedSet = new PermissionSet(PermissionState.None);
			
			// The only mandatory permission is permission to execute
			restrictedSet.AddPermission(new SecurityPermission(SecurityPermissionFlag.Execution));

			// UI permissions should be restricted
			restrictedSet.AddPermission(new UIPermission(UIPermissionWindow.SafeTopLevelWindows));

			// Create root code group w/out any permissions
			CodeGroup rootGroup = new UnionCodeGroup(new AllMembershipCondition(),
				new PolicyStatement(new PermissionSet(PermissionState.None)));

			// Create a child group for "My Computer" zone with unrestricted permissions
			CodeGroup myCompGroup = new UnionCodeGroup(new ZoneMembershipCondition(SecurityZone.MyComputer),
				new PolicyStatement(new PermissionSet(PermissionState.Unrestricted)));
			rootGroup.AddChild(myCompGroup);

			// Create a child group with limited permissions
			// This group should use be configured as exclusive to override "My Computer" zone group
			// This group should be bound to the limited permission set (see 2.1)
			// This group should be bound to the "Untrusted" folder
			CodeGroup limitedGroup = new UnionCodeGroup(new UrlMembershipCondition(Path.Combine(basePath, @"Untrusted\*")),
				new PolicyStatement(restrictedSet, PolicyStatementAttribute.Exclusive));
			rootGroup.AddChild(limitedGroup);

			// Create an optional child group to provide full explicit permissions
			// This group should be bound to the "FullTrust" folder
			CodeGroup explicitTrustGroup = new UnionCodeGroup(new UrlMembershipCondition(Path.Combine(basePath, @"FullTrust\*")),
				new PolicyStatement(new PermissionSet(PermissionState.Unrestricted)));
			rootGroup.AddChild(explicitTrustGroup);

			// Setup policy level and apply it to the current domain
			appDomainLevel.RootCodeGroup = rootGroup;
			AppDomain.CurrentDomain.SetAppDomainPolicy(appDomainLevel);
		}

		#endregion

		#region Event Handlers
		private void btnTrusted_Click(object sender, EventArgs e)
		{
			RunTest(this._fullTrustAssembly);
		}

		private void btnUntrusted_Click(object sender, EventArgs e)
		{
			RunTest(this._untrustedAssembly);
		}
		#endregion

		#region Data Members
		Assembly _untrustedAssembly;
		Assembly _fullTrustAssembly;
		#endregion
	}
}