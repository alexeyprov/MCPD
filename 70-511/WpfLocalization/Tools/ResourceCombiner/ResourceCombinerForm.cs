using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Westwind.Utilities;


namespace ResourceCombiner
{
    public partial class ResourceCombinerForm : Form
    {
        public ResourceCombiner Combiner = new ResourceCombiner();

        public ResourceCombinerForm()
        {
            InitializeComponent();
        }

        private void ResourceCombinerForm_Load(object sender, EventArgs e)
        {
            const string STR_ResourceCombinerFile = "ResourceCombiner.saved";
            
            if (File.Exists(STR_ResourceCombinerFile))
                this.Combiner = ResourceCombiner.LoadFromFile(STR_ResourceCombinerFile);

            this.propertyGrid.SelectedObject = this.Combiner;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (!this.Combiner.CombineResources())
                MessageBox.Show(this.Combiner.ErrorMessage);
            else
                MessageBox.Show("Completed without errors");
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }

        private void MenuItemClicked(object sender, EventArgs e)
        {
            if (sender == mnuSave)
            {
                SaveFileDialog diag = new SaveFileDialog();
                diag.AddExtension = true;
                diag.CheckFileExists = false;
                diag.Filter = "Saved files (*.saved)|*.saved";
                diag.SupportMultiDottedExtensions = true;
                diag.ValidateNames = true;
                diag.RestoreDirectory = true;
                diag.InitialDirectory = Directory.GetCurrentDirectory();

                DialogResult res = diag.ShowDialog();

                if (res == DialogResult.OK)
                {
                    SerializationUtils.SerializeObject(this.Combiner, diag.FileName, false);
                    return;
                }
            }
            else if (sender == mnuLoad)
            {
                OpenFileDialog diag = new OpenFileDialog();
                diag.AddExtension = true;
                diag.CheckFileExists = false;
                diag.Filter = "Saved files (*.saved)|*.saved";
                diag.SupportMultiDottedExtensions = true;
                diag.ValidateNames = true;
                diag.RestoreDirectory = true;
                diag.InitialDirectory = Directory.GetCurrentDirectory();

                DialogResult res = diag.ShowDialog();

                if (res == DialogResult.OK)
                {
                    this.Combiner = SerializationUtils.DeSerializeObject(diag.FileName, typeof(ResourceCombiner), false) as ResourceCombiner;
                    this.propertyGrid.SelectedObject = Combiner;
                    return;
                }

            }
            else if (sender == mnuExit)
                this.Close();

        }


    }
}
