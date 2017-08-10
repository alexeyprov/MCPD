using System;
using System.Windows.Forms;


partial class CallsCounterForm : Form
{
   public CallsCounterForm()
   {
      InitializeComponent();
   }

   public int Counter
   {
      get
      {
         return Convert.ToInt32(m_CounterLabel.Text);
      }
      set
      {
         m_CounterLabel.Text = value.ToString();
      }
   }
}
