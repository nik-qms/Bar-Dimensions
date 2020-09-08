using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Wefa
{
    public partial class Form2 : Form
    {
        Form1 opener;
        public Form2(Form1 parentForm)
        {
            InitializeComponent();
            opener = parentForm;

            
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            //opener.FillFormWithHistorie_rmn(textBox1.Text);
            //opener.tabControl1.TabPages.Insert(2, opener.tabPage2);
            try
            {
                opener._MAiT_MM_MITTELSTANDTableAdapter.FillBy(opener.wefaDataSet._MAiT_MM_MITTELSTAND, textBox1.Text);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            this.Close();

        }
    }
}
