using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

using System.Security.Cryptography;
using System.IO;
using System.Text;

namespace PublicKeyEncryptor
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.TextBox messageTextBox;
		private System.Windows.Forms.Label messageLabel;
		private System.Windows.Forms.MenuItem sendMessagesMenu;
		private System.Windows.Forms.MenuItem exportKeyMenuItem;
		private System.Windows.Forms.MenuItem importKeyMenuItem;
		private System.Windows.Forms.MenuItem receiveMessagesMenu;
		private System.Windows.Forms.MenuItem keysMenu;
		private System.Windows.Forms.MenuItem loadKeyMenuItem;
		private System.Windows.Forms.MenuItem saveKeyMenuItem;
		private System.Windows.Forms.MenuItem saveMessageMenuItem;
		private System.Windows.Forms.MenuItem loadMessageMenuItem;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private RSACryptoServiceProvider _myRSA;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox myPrivateKeyTextBox;
		private System.Windows.Forms.TextBox myPublicKeyTextBox;
		private System.Windows.Forms.TextBox otherPublicKeyTextBox;
		private RSACryptoServiceProvider _otherRSA;

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			_myRSA = new RSACryptoServiceProvider();
			_otherRSA = new RSACryptoServiceProvider();
			DisplayKeys();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.sendMessagesMenu = new System.Windows.Forms.MenuItem();
			this.exportKeyMenuItem = new System.Windows.Forms.MenuItem();
			this.importKeyMenuItem = new System.Windows.Forms.MenuItem();
			this.receiveMessagesMenu = new System.Windows.Forms.MenuItem();
			this.keysMenu = new System.Windows.Forms.MenuItem();
			this.loadKeyMenuItem = new System.Windows.Forms.MenuItem();
			this.saveKeyMenuItem = new System.Windows.Forms.MenuItem();
			this.saveMessageMenuItem = new System.Windows.Forms.MenuItem();
			this.loadMessageMenuItem = new System.Windows.Forms.MenuItem();
			this.messageTextBox = new System.Windows.Forms.TextBox();
			this.messageLabel = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.myPrivateKeyTextBox = new System.Windows.Forms.TextBox();
			this.myPublicKeyTextBox = new System.Windows.Forms.TextBox();
			this.otherPublicKeyTextBox = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                      this.keysMenu,
                                                                                      this.sendMessagesMenu,
                                                                                      this.receiveMessagesMenu});
			// 
			// sendMessagesMenu
			// 
			this.sendMessagesMenu.Index = 1;
			this.sendMessagesMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                             this.importKeyMenuItem,
                                                                                             this.saveMessageMenuItem});
			this.sendMessagesMenu.Text = "Send Messages";
			// 
			// exportKeyMenuItem
			// 
			this.exportKeyMenuItem.Index = 0;
			this.exportKeyMenuItem.Text = "Export public key";
			this.exportKeyMenuItem.Click += new System.EventHandler(this.exportKeyMenuItem_Click);
			// 
			// importKeyMenuItem
			// 
			this.importKeyMenuItem.Index = 0;
			this.importKeyMenuItem.Text = "Import public key";
			this.importKeyMenuItem.Click += new System.EventHandler(this.importKeyMenuItem_Click);
			// 
			// receiveMessagesMenu
			// 
			this.receiveMessagesMenu.Index = 2;
			this.receiveMessagesMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                                this.exportKeyMenuItem,
                                                                                                this.loadMessageMenuItem});
			this.receiveMessagesMenu.Text = "Receive Messages";
			// 
			// keysMenu
			// 
			this.keysMenu.Index = 0;
			this.keysMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                     this.loadKeyMenuItem,
                                                                                     this.saveKeyMenuItem});
			this.keysMenu.Text = "Keys";
			// 
			// loadKeyMenuItem
			// 
			this.loadKeyMenuItem.Index = 0;
			this.loadKeyMenuItem.Text = "Load private key";
			this.loadKeyMenuItem.Click += new System.EventHandler(this.loadKeyMenuItem_Click);
			// 
			// saveKeyMenuItem
			// 
			this.saveKeyMenuItem.Index = 1;
			this.saveKeyMenuItem.Text = "Save private key";
			this.saveKeyMenuItem.Click += new System.EventHandler(this.saveKeyMenuItem_Click);
			// 
			// saveMessageMenuItem
			// 
			this.saveMessageMenuItem.Index = 1;
			this.saveMessageMenuItem.Text = "Save encrypted message";
			this.saveMessageMenuItem.Click += new System.EventHandler(this.saveMessageMenuItem_Click);
			// 
			// loadMessageMenuItem
			// 
			this.loadMessageMenuItem.Index = 1;
			this.loadMessageMenuItem.Text = "Load encrypted message";
			this.loadMessageMenuItem.Click += new System.EventHandler(this.loadMessageMenuItem_Click);
			// 
			// messageTextBox
			// 
			this.messageTextBox.Location = new System.Drawing.Point(112, 80);
			this.messageTextBox.Multiline = true;
			this.messageTextBox.Name = "messageTextBox";
			this.messageTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.messageTextBox.Size = new System.Drawing.Size(200, 96);
			this.messageTextBox.TabIndex = 0;
			this.messageTextBox.Text = "";
			// 
			// messageLabel
			// 
			this.messageLabel.Location = new System.Drawing.Point(8, 80);
			this.messageLabel.Name = "messageLabel";
			this.messageLabel.Size = new System.Drawing.Size(100, 16);
			this.messageLabel.TabIndex = 1;
			this.messageLabel.Text = "Message:";
			this.messageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(100, 16);
			this.label1.TabIndex = 1;
			this.label1.Text = "My private key: ";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 32);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(100, 16);
			this.label2.TabIndex = 1;
			this.label2.Text = "My public key:";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(8, 56);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(100, 16);
			this.label3.TabIndex = 1;
			this.label3.Text = "Other public key:";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// myPrivateKeyTextBox
			// 
			this.myPrivateKeyTextBox.Location = new System.Drawing.Point(112, 8);
			this.myPrivateKeyTextBox.Name = "myPrivateKeyTextBox";
			this.myPrivateKeyTextBox.ReadOnly = true;
			this.myPrivateKeyTextBox.Size = new System.Drawing.Size(200, 20);
			this.myPrivateKeyTextBox.TabIndex = 2;
			this.myPrivateKeyTextBox.Text = "";
			// 
			// myPublicKeyTextBox
			// 
			this.myPublicKeyTextBox.Location = new System.Drawing.Point(112, 32);
			this.myPublicKeyTextBox.Name = "myPublicKeyTextBox";
			this.myPublicKeyTextBox.ReadOnly = true;
			this.myPublicKeyTextBox.Size = new System.Drawing.Size(200, 20);
			this.myPublicKeyTextBox.TabIndex = 3;
			this.myPublicKeyTextBox.Text = "";
			// 
			// otherPublicKeyTextBox
			// 
			this.otherPublicKeyTextBox.Location = new System.Drawing.Point(112, 56);
			this.otherPublicKeyTextBox.Name = "otherPublicKeyTextBox";
			this.otherPublicKeyTextBox.ReadOnly = true;
			this.otherPublicKeyTextBox.Size = new System.Drawing.Size(200, 20);
			this.otherPublicKeyTextBox.TabIndex = 4;
			this.otherPublicKeyTextBox.Text = "";
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(320, 185);
			this.Controls.Add(this.otherPublicKeyTextBox);
			this.Controls.Add(this.myPublicKeyTextBox);
			this.Controls.Add(this.myPrivateKeyTextBox);
			this.Controls.Add(this.messageLabel);
			this.Controls.Add(this.messageTextBox);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label3);
			this.Menu = this.mainMenu1;
			this.Name = "Form1";
			this.Text = "Public Key Encryptor";
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.Run(new Form1());
		}

		private void loadKeyMenuItem_Click(object sender, System.EventArgs e)
		{
			ImportKey(false);
		}

		private void saveKeyMenuItem_Click(object sender, System.EventArgs e)
		{
			ExportKey(false);
		}

		private void importKeyMenuItem_Click(object sender, System.EventArgs e)
		{
			ImportKey(true);
		}

		private void saveMessageMenuItem_Click(object sender, System.EventArgs e)
		{
			try
			{
				string outfile = GetOutputFileName("Save Message");
				byte[] data = _otherRSA.Encrypt(
					Encoding.UTF8.GetBytes(messageTextBox.Text),
					true);

				File.WriteAllBytes(outfile, data);
			}
			catch (UserAbortedException)
			{
			}
		}

		private void exportKeyMenuItem_Click(object sender, System.EventArgs e)
		{
			ExportKey(true);
		}

		private void loadMessageMenuItem_Click(object sender, System.EventArgs e)
		{
			try
			{
				string infile = GetInputFileName("Open Message");
				byte[] data = File.ReadAllBytes(infile);
				string msg = Encoding.UTF8.GetString(_myRSA.Decrypt(data, true));
				messageTextBox.Text = msg;
			}
			catch (UserAbortedException)
			{
			}
		}

		private void DisplayKeys()
		{
			RSAParameters pars = _myRSA.ExportParameters(true);
			myPrivateKeyTextBox.Text = Convert.ToBase64String(pars.P);
			myPublicKeyTextBox.Text = Convert.ToBase64String(pars.Modulus);

			pars = _otherRSA.ExportParameters(false);
			otherPublicKeyTextBox.Text = Convert.ToBase64String(pars.Modulus);
		}

		private string GetOutputFileName(string title)
		{
			return GetFileName<SaveFileDialog>(title);
		}

		private string GetInputFileName(string title)
		{
			return GetFileName<OpenFileDialog>(title);
		}

		private string GetFileName<T>(string title) where T : FileDialog, new()
		{
			using (T fd = new T())
			{
				fd.Title = title;
				fd.Filter = "All files (*.*)|*.*";
				if (DialogResult.OK == fd.ShowDialog(this))
				{
					return fd.FileName;
				}
				throw new UserAbortedException();
			}
		}

		private void ExportKey(bool publicOnly)
		{
			string title = (publicOnly) ? "Export Public Key" :
				"Export Private Key";
			try
			{
				string outfile = GetOutputFileName(title);
				File.WriteAllText(outfile, _myRSA.ToXmlString(!publicOnly));
			}
			catch (UserAbortedException)
			{
			}
		}

		private void ImportKey(bool publicOnly)
		{
			string title;
			AsymmetricAlgorithm alg;

			if (publicOnly)
			{
				title = "Import Public Key";
				alg = _otherRSA;
			}
			else
			{
				title = "Import Private Key";
				alg = _myRSA;
			}
				
			try
			{
				string infile = GetInputFileName(title);
				string keyXml = File.ReadAllText(infile);
				alg.FromXmlString(keyXml);
				DisplayKeys();
			}
			catch (UserAbortedException)
			{
			}
		}
	}
}
