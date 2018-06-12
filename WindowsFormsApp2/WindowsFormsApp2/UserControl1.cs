using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace WindowsFormsApp2
{
    public partial class UserControl1 : UserControl
    {
        
        //bool Set_CtrlA = false; //select All?
        //bool status_Ctrl = false; //ไม่ได้กด ctrl
        int L_x, L_y;
        List<Control> SelectedControls = new List<Control>();

        public UserControl1()
        {
            InitializeComponent();
        }
     

        private void UserControl1_Load(object sender, EventArgs e)
        {
            this.MouseDown += UserControl1_MouseDown;
            this.MouseUp += UserControl1_MouseUp;
            this.MouseDown += UserControl1_MouseDown_1;
            //----------------------------------------
            this.Focus();

        }
        public void AddNewBox(int x, int y)
        {
    
            Panel myPanel = new Panel();
            myPanel.Size = new Size(10, 10);
            myPanel.Location = new Point(x, y);
            myPanel.BackColor = Color.Blue;

            myPanel.MouseDown += UserControl1_MouseDown;
            myPanel.MouseUp += UserControl1_MouseUp; 
            myPanel.MouseMove += UserControl1_MouseMove;        
            myPanel.KeyDown += UserControl1_KeyDown;
           // myPanel.MouseDown += UserControl1_MouseDown_1;

            

            this.Controls.Add(myPanel);
            this.Focus();

        }
       
      

        Point MouseDownLocation;    
        private void UserControl1_MouseDown(object sender, MouseEventArgs e)
        {
            
            MouseDownLocation = e.Location;
            //case press ctrl
            if (Control.ModifierKeys == Keys.Control || e.Button == MouseButtons.Left)
            {

                Control item = (Control)sender;
                item.BackColor = Color.Yellow;
                L_x = e.X;
                L_y = e.Y;
                if (!SelectedControls.Contains(item))
                {
                    SelectedControls.Add(item);
                }

                //กดctrlนะ

                //Console.WriteLine(SelecttedControls.Count);
            }


            //case don't press ctrl
            else if (e.Button == MouseButtons.Left)
            {

                Control item = (Control)sender;
                item.BackColor = Color.Yellow;
                L_x = e.X;
                L_y = e.Y;
                if (!SelectedControls.Contains(item))
                {
                    SelectedControls.Add(item);
                }


            }
           
       
        }

        private void UserControl1_MouseUp(object sender, MouseEventArgs e)
        {


            //case1 press ctrl
           
            if (Control.ModifierKeys == Keys.Control || SelectedControls.Count > 1)
            {
                //MessageBox.Show("Yes");
                Control item = (Control)sender;
                item.BackColor = Color.Yellow;
               


            }
            //case2 don't press ctrl
            else
            {
                //MessageBox.Show("No");
                Control item = (Control)sender;
                item.BackColor = Color.Blue;               
                
            }    
        }
        //Move
        private void UserControl1_MouseMove(object sender, MouseEventArgs e)
        {
          
          
            if (e.Button == MouseButtons.Left) 
            {
         
                int dx = e.X - L_x;
                int dy = e.Y - L_y;
                Control p = (Control)sender;

                foreach (Control item in SelectedControls)
                {
                    Control pp = (Control)item;
                    pp.Location = new Point(pp.Left + dx, pp.Top + dy);
                    item.BackColor = Color.Yellow;
                    //status_Ctrl = true;
                              
                }
            }
           
        }
        private void UserControl1_KeyDown(object sender, KeyEventArgs e)
        {          
            if (e.KeyCode == Keys.A && e.Modifiers == Keys.Control)
            {   
                 foreach (Control item in this.Controls)
                {
                    item.BackColor = Color.Yellow;
                 
                    if (!SelectedControls.Contains(item))
                    {
                        SelectedControls.Add(item);
                    }
                    item.BackColor = Color.Yellow;
                   
                    
                }
                Console.WriteLine(SelectedControls.Count);
            }
            
            else if (e.KeyCode == Keys.Delete)
            {
                Console.WriteLine(SelectedControls.Count);
                foreach (Control item in SelectedControls)
                {
                           
                    Controls.Remove(item);
                    
                }
                SelectedControls.Clear();
                this.Focus();
            }
        }

        private void UserControl1_MouseDown_1(object sender, MouseEventArgs e)
        {
            foreach (Control item in this.Controls)
            {

                item.Controls.Clear();
                if (item.BackColor == Color.Yellow)
                {
                    item.BackColor = Color.Blue;
                }

            }
            SelectedControls.Clear();
        }

        private void UserControl1_KeyUp(object sender, KeyEventArgs e)
        {
            //this.Focus();
        }

        private void UserControl1_KeyPress(object sender, KeyPressEventArgs e)
        {
            


        }       
    }
}
