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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }
  
        private void button1_Click(object sender, EventArgs e)
        {
            int count = Int32.Parse(textBox1.Text);
            for (int i = 0; i < count; ++i)
            {                
                userControl1.AddNewBox(i*10,i*10);
            }
            
        }


        private void userControl1_Load(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int count = userControl1.Controls.Count; //call panel1
            using (FileStream fs = new FileStream("Binary.txt", FileMode.Create))
            using (BinaryWriter w = new BinaryWriter(fs))
            {
                for (int i = 0; i < count; i++)
                {
                    //list,Directory, Queue,Stack

                    Panel panel = (Panel)userControl1.Controls[i];
                    Point local = panel.Location;
                    int Location_x = local.X;
                    int Location_y = local.Y;

                    //write binary in text file
                    w.Write(Location_x);
                    w.Write(Location_y);
                }
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            //clear panel
            userControl1.Controls.Clear();

            using (FileStream fs = new FileStream("Binary.txt", FileMode.Open))
            using (BinaryReader w = new BinaryReader(fs))
            {
                //Loop create item
                while (w.BaseStream.Position != w.BaseStream.Length)
                {
                    int Location_x = w.ReadInt32();
                    int Location_y = w.ReadInt32();
                    userControl1.AddNewBox(Location_x,Location_y);
                }
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
           
            


        }

        private void button6_Click(object sender, EventArgs e)
        {
          
            this.textBox2.Text = userControl1.RefreshHxBox();

        }
    }
}
