using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Messanger
{
    
    public partial class LogInForm : Form
    {
        string Name;
        public string GetData
        {
            get { return Name; }
        }
        public LogInForm()
        {
            
            InitializeComponent();
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            Name = NameTextBox.Text;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
