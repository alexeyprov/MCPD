using System;
using System.Windows;
using System.Windows.Forms;

using BusinessEntities;
using CustomUI.Interop;

namespace CustomBinding
{
    public partial class MainForm : Form
    {
        #region Private Fields

        private IInputBox _inputBox; 

        #endregion

        #region Construction

        public MainForm()
        {
            InitializeComponent();
        }

        #endregion

        #region Event Handlers

        private void MainForm_Load(object sender, EventArgs e)
        {
            bsBooks.DataSource = ObjectFactory.GetListOfBooks();
        }

        private void btnAddPage_Click(object sender, EventArgs e)
        {
            ((BookInfo)bsBooks.Current).PageCount++;
        }

        private void btnRemovePage_Click(object sender, EventArgs e)
        {
            ((BookInfo)bsBooks.Current).PageCount--;
        }

        private void btnWpfInterop_Click(object sender, EventArgs e)
        {
            if (_inputBox == null)
            {
                WpfInputBox inputBox = InputBoxFactory.CreateInputBox<WpfInputBox>(
                    "What is your name?",
                    "Name:");

                inputBox.Closed += OnInputBoxClosed;

                inputBox.Show();

                _inputBox = inputBox;
            }
            else
            {
                _inputBox.Activate();
            }
        }

        private void OnInputBoxClosed(object sender, EventArgs e)
        {
            ((Window)_inputBox).Closed -= OnInputBoxClosed;
            _inputBox = null;
        }

        #endregion
    }
}