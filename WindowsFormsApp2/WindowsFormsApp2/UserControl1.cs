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
        
        
        int L_x, L_y;
        List<PanelObjectHistory> _undoList = new List<PanelObjectHistory>();
        List<PanelObjectHistory> _redoList = new List<PanelObjectHistory>();
        List<PanelObjectHistory> _objHxList = new List<PanelObjectHistory>();
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
       
        public string RefreshHxBox()
        {
            StringBuilder stbuilder = new StringBuilder();
            int j = _objHxList.Count;
            for (int i = 0; i < j; ++i)
            {
                stbuilder.AppendLine(_objHxList[i].ToString());
            }
            return stbuilder.ToString();

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
                //------------------------------------------------------
                int j = SelectedControls.Count;
                for (int i = 0; i < j; ++i)
                {
                   
                    PanelObjectHistory hx = new PanelObjectHistory();
                    hx.targetPanel = SelectedControls[i];
                    hx.color = SelectedControls[i].BackColor;
                    hx.X = SelectedControls[i].Location.X;
                    hx.Y = SelectedControls[i].Location.Y;
                    hx.command = CommandKind.BgColor;
                    _objHxList.Add(hx);
                    _undoList.Add(hx);

                }
                RefreshHxBox();
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
                //---------------------------------------------
                int j = SelectedControls.Count;
                for (int i = 0; i < j; ++i)
                {

                    PanelObjectHistory hx = new PanelObjectHistory();
                    hx.targetPanel = SelectedControls[i];
                    //hx.color = SelectedControls[i].BackColor;
                    hx.X = SelectedControls[i].Location.X;
                    hx.Y = SelectedControls[i].Location.Y;
                    hx.command = CommandKind.BgColor;
                    _objHxList.Add(hx);
                    _undoList.Add(hx);

                 

                }
                RefreshHxBox();


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
                SelectedControls.Clear();

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
                Console.WriteLine("A");
                
                foreach (Control item in this.Controls)
                {
                    item.BackColor = Color.Yellow;
                 
                    if (!SelectedControls.Contains(item))
                    {
                        SelectedControls.Add(item);
                    }
                    item.BackColor = Color.Yellow;
                   
                    
                }
                //Console.WriteLine(SelectedControls.Count);
            }

            else if (e.KeyCode == Keys.Delete)
            {
                //Console.WriteLine(SelectedControls.Count);
                foreach (Control item in SelectedControls)
                {
                           
                    Controls.Remove(item);
                    
                }
                SelectedControls.Clear();
                this.Focus();
            }
            //Undo
            else if (e.KeyCode == Keys.Z && e.Modifiers == Keys.Control)
            {

                Console.WriteLine("Z");
                int j = _undoList.Count;
                if (_undoList.Count > 0)
                {
                                    
                        PanelObjectHistory latestHx = _undoList[_undoList.Count - 1];
                        _undoList.RemoveAt(_undoList.Count - 1);
                        latestHx.targetPanel.Location = new Point(latestHx.X, latestHx.Y);
                        _redoList.Add(latestHx);

                }
                //this.Focus();



            }
            //Redo
            else if(e.KeyCode == Keys.Y || e.Modifiers == Keys.Control)
            {

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
        enum CommandKind
        {
            Unknown,
            Position,
            BgColor
        }
        class PanelObjectHistory
        {
            public Control targetPanel;
            public int X;
            public int Y;
            public Color color;
            public CommandKind command;

            public override string ToString()
            {
                if (targetPanel.Tag != null)
                {
                    return command + "," + (int)targetPanel.Tag + ":" + X + "," + Y + "," + color;
                }
                else
                {
                    return command + "," + X + "," + Y + "," + color;
                }

            }
        }
    }
}
