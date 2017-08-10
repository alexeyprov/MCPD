using System;
using System.Windows.Forms;

namespace CustomUI.Interop
{
    public partial class WinFormsInputBox : Form, IInputBox
    {
        #region Constructor

        public WinFormsInputBox()
        {
            InitializeComponent();

            InitializeWrappers();
        }

        #endregion

        #region IInputBox Members

        public event EventHandler DataUpdated;

        string IInputBox.Label
        {
            get
            {
                return lblLabel.Text;
            }
            set
            {
                lblLabel.Text = value;
            }
        }

        string IInputBox.Header
        {
            get
            {
                return Text;
            }
            set
            {
                Text = value;
            }
        }

        string IInputBox.Text
        {
            get
            {
                return txtText.Text;
            }
            set
            {
                txtText.Text = value;
            }
        }

        #endregion

        #region Event Handlers

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (DataUpdated != null)
            {
                DataUpdated(this, EventArgs.Empty);
            }
        }
        
        #endregion

        #region Implementation

        private void InitializeWrappers()
        {
            buttonWrapper.Text = "_Update";
            buttonWrapper.Clicked += btnUpdate_Click;
        }

        #endregion
    }
}
