using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApp2
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for(int i=0; i< 10;++i)
            {
                Panel myPanel1 = new Panel();
                myPanel1.Size = new Size(10, 10);
                myPanel1.Location = new Point(i * 10, i * 10);
                myPanel1.BackColor = Color.Blue;

                this.panel1.Controls.Add(myPanel1);
            }
        }
    }
}
