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
using System.Drawing.Drawing2D;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace WindowsFormsApp2
{
    public partial class UserControl1 : UserControl
    {


        int L_x, L_y;
        bool mouseDown;
        //-------------------------------------------------------------------------------------
        Point selectionStart; //Point Start Mouse Drag
        Point selectionEnd;   //Point End Mouse Drag
        Rectangle selection;    
        List<Rectangle> rectangles = new List<Rectangle>(); //list rectangles Mouse Drag
        //-------------------------------------------------------------------------------------
        List<PanelObjectHistory> _undoList = new List<PanelObjectHistory>();  // list Undo
        List<PanelObjectHistory> _redoList = new List<PanelObjectHistory>();  // list redo
        List<PanelObjectHistory> _objHxList = new List<PanelObjectHistory>(); // list history
        //-------------------------------------------------------------------------------------
        List<int> _selectCount = new List<int>();  // list count Undo
        List<int> _selectCount_redo = new List<int>();   // list count Redo
        //-------------------------------------------------------------------------------------
        List<Control> SelectedControls = new List<Control>(); //list count select Yellow box
        //-------------------------------------------------------------------------------------
        

        public UserControl1()
        {
            InitializeComponent();
        }
        private void UserControl1_Load(object sender, EventArgs e)
        {
            this.MouseDown += UserControl1_MouseDown;
            this.MouseUp += UserControl1_MouseUp;
            this.MouseDown += UserControl1_MouseDown_1;
            //----------------------------------------------
            this.Focus();

        }
        public void AddNewBox(int x, int y, int t)
        {            
            Panel myPanel = new Panel();
            myPanel.Size = new Size(10, 10);
            myPanel.Location = new Point(x, y);
            myPanel.BackColor = Color.Blue;
            myPanel.Tag = t;
            //-------------------------------------------------------
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
        private void SetSelectionRect()
        {
            int x, y;
            int width, height;
            
            x = selectionStart.X > selectionEnd.X ? selectionEnd.X : selectionStart.X;
            y = selectionStart.Y > selectionEnd.Y ? selectionEnd.Y : selectionStart.Y;

            width = selectionStart.X > selectionEnd.X ? selectionStart.X - selectionEnd.X : selectionEnd.X - selectionStart.X;
            height = selectionStart.Y > selectionEnd.Y ? selectionStart.Y - selectionEnd.Y : selectionEnd.Y - selectionStart.Y;

            selection = new Rectangle(x, y, width, height);
            

            Console.WriteLine(selection.Location);
            //-------------------------------------------------------------------------------------------------------------------

            foreach (Control item in this.Controls)
            {               
                if (selection.Contains(item.Bounds))
                {
                    item.BackColor = Color.Yellow;
                    SelectedControls.Add(item);
                    PanelObjectHistory hx = new PanelObjectHistory(item.Left, item.Top, item);
                    hx.targetPanel.Location = item.Location;
                    
                    _undoList.Add(hx);
                    _objHxList.Add(hx);
                    //Write_Json();

                }
                RefreshHxBox();
            }
        }     
        Point MouseDownLocation;
        private void UserControl1_MouseDown(object sender, MouseEventArgs e)
        {          
            MouseDownLocation = e.Location;
            Control item = (Control)sender;
            //case press ctrl
            if (Control.ModifierKeys == Keys.Control || e.Button == MouseButtons.Left)
            {

                L_x = e.X;
                L_y = e.Y;
                item.BackColor = Color.Yellow;
               
                if (!SelectedControls.Contains(item))
                {
                    SelectedControls.Add(item);
                    PanelObjectHistory hx = new PanelObjectHistory(item.Left, item.Top, item);                   
                    hx.targetPanel.Location = item.Location;
                 
                    _undoList.Add(hx);
                    _objHxList.Add(hx);
                    //Write_Json();

                }
                RefreshHxBox();
            }
            //case don't press ctrl
            else if (e.Button == MouseButtons.Left)
            {             
                item.BackColor = Color.Yellow;
                L_x = e.X;
                L_y = e.Y;

                //----------------------------------------------------------------------------
                if (!SelectedControls.Contains(item))
                {                   
                    SelectedControls.Add(item);
                    PanelObjectHistory hx = new PanelObjectHistory(item.Left, item.Top, item);
                    hx.targetPanel.Location = item.Location;

                    _undoList.Add(hx);
                    _objHxList.Add(hx);
                    //Write_Json();

                }
                RefreshHxBox();
            }
        }
        private void UserControl1_MouseUp(object sender, MouseEventArgs e)
        {          
            //case1 press ctrl
            Control item = (Control)sender;

           if (Control.ModifierKeys == Keys.Control || SelectedControls.Count > 1)
            {
                //MessageBox.Show("CtrlA");          
                item.BackColor = Color.Yellow;
                if(Control.ModifierKeys != Keys.Control)
                {
                    _selectCount.Add(SelectedControls.Count);
                    //SelectCount();
                }                
            }
            //case2 don't press ctrl
            else
            {                            
                if (Control.ModifierKeys != Keys.Control)
                {
                   
                    _selectCount.Add(SelectedControls.Count);
                    //SelectCount();
                }
                //MessageBox.Show("No");              
                item.BackColor = Color.Blue;
                SelectedControls.Clear();             
            }
            this.Focus();          
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
                }
            }          
        }
        private void UserControl1_KeyDown(object sender, KeyEventArgs e)
        {          
            if (e.KeyCode == Keys.A && e.Modifiers == Keys.Control)
            {
                Console.WriteLine("A");
                if (this.Controls.Count > 0)
                {
                    foreach (Control item in this.Controls)
                    {
                        item.BackColor = Color.Yellow;

                        if (!SelectedControls.Contains(item))
                        {
                            PanelObjectHistory hx = new PanelObjectHistory(item.Left, item.Top, item);
                            SelectedControls.Add(item);
                            hx.targetPanel.Location = item.Location;
                            _undoList.Add(hx);
                            //Write_Json();
                        }                    

                    }
                }              
                this.Focus();
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
                Console.WriteLine("history ={0}", _undoList.Count);

                if (_undoList.Count > 0)
                {
                    for (int i =0; i < _selectCount[_selectCount.Count - 1]; i++)
                    {
                        Console.WriteLine("Undo" + _undoList[_undoList.Count - 1].targetPanel.Location);
                        Control thisPo = _undoList[_undoList.Count - 1].targetPanel;
                        //----------------------------------------------------------------------------------

                        PanelObjectHistory hx = new PanelObjectHistory(thisPo.Left, thisPo.Top, thisPo);
                        PanelObjectHistory lastestHx = _undoList[_undoList.Count - 1];                
                        _redoList.Add(hx);                        
                        lastestHx.targetPanel.Location = new Point(lastestHx.x, lastestHx.y);                       
                        _undoList.RemoveAt(_undoList.Count - 1);
                        //-----------------------------------------------------------------------------------

                        Console.WriteLine("Redo" + _redoList[_redoList.Count - 1].targetPanel.Location);
                    }
                    _selectCount_redo.Add(_selectCount[_selectCount.Count - 1]);
                    _selectCount.RemoveAt(_selectCount.Count - 1);
                }
               
                //RefreshHxBox();


            }
            //Redo
            else if(e.KeyCode == Keys.Y && e.Modifiers == Keys.Control)
            {
                Console.WriteLine("Y");
                Console.WriteLine("history ={0}", _redoList.Count);
                if (_redoList.Count > 0)
                {
                    for (int i = 0; i < _selectCount_redo[_selectCount_redo.Count - 1]; i++)
                    {                    
                        Console.WriteLine("Redo" + _redoList[_redoList.Count - 1].targetPanel.Location);
                        Control thisPo = _redoList[_redoList.Count - 1].targetPanel;
                        //--------------------------------------------------------------------------------------

                        PanelObjectHistory hx = new PanelObjectHistory(thisPo.Left, thisPo.Top, thisPo);
                        PanelObjectHistory lastestHx = _redoList[_redoList.Count - 1];
                        //_undoList.RemoveAt(_undoList.Count - 1);
                        _undoList.Add(hx);
                        ////lastestHx.targetPanel.Location = new Point(lastestHx.x, lastestHx.y);
                        lastestHx.targetPanel.Location = new Point(lastestHx.x, lastestHx.y);
                        //_redoList.Add(lastestHx);
                        _redoList.RemoveAt(_redoList.Count - 1);
                        //---------------------------------------------------------------------------------------

                        Console.WriteLine("Undo" + _undoList[_undoList.Count - 1].targetPanel.Location);
                    }            
                    _selectCount.Add(_selectCount_redo[_selectCount_redo.Count - 1]);
                    _selectCount_redo.RemoveAt(_selectCount_redo.Count - 1);
                }
            }
        }
        private void UserControl1_MouseDown_1(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                selectionStart = e.Location;
                mouseDown = true;
               
            }
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
        public void Write_Json()
        {
            //Clear List after click button Save again
            List_UndoLocation.Clear();
            //_undoList.Clear();
            using (FileStream fs = new FileStream("Undo_Json.json", FileMode.Create))
            using (StreamWriter file = new StreamWriter(fs))
            using (JsonWriter w = new JsonTextWriter(file))
            {
                JsonSerializer Position = new JsonSerializer();

                for (int i = 0; i <= _undoList.Count - 1; i++)
                {
                    
                    Position po = new Position();
                    po.X = _undoList[i].x;
                    po.Y = _undoList[i].y;
                    po.T = (int)_undoList[i].targetPanel.Tag;
                    List_UndoLocation.Add(po);
                    
                }
                Position.Serialize(w, List_UndoLocation);
                
                
            }
          
        }
        //read undo
        public void Read_Json()
        {
            List_UndoLocation.Clear();
            
            using (FileStream fs = new FileStream("Undo_Json.json", FileMode.Open))
            using (StreamReader file = new StreamReader(fs))
            using (JsonReader w = new JsonTextReader(file))
            {
                
                JsonSerializer Position = new JsonSerializer();
                List<Position> positions = JsonConvert.DeserializeObject<List<Position>>(file.ReadToEnd());
                //Console.WriteLine(positions[0].X);
                //----------------------------------------------------------------------------------------
                for (int i = 0; i <= positions.Count - 1; i++)
                {
                    Position po = new Position();
                    int myTag = positions[i].T;
                    int Location_x = positions[i].X;
                    int Location_y = positions[i].Y;
    
                    foreach (Control item in this.Controls)
                    {
                       
                       PanelObjectHistory hx = new PanelObjectHistory(Location_x, Location_y, item);
                       if(myTag == (int)item.Tag)
                        {
                            _undoList.Add(hx);
                        }
                                              
                    }

                }

            }
        }
        public void SelectCount()
        {
            using (FileStream fs = new FileStream("Tag_Json.json", FileMode.Create))
            using (StreamWriter file = new StreamWriter(fs))
            using (JsonWriter w = new JsonTextWriter(file))
            {
                //JArray array = new JArray();
                JsonSerializer Position = new JsonSerializer();

                Position.Serialize(w, _selectCount);
            }
        }
        public void ReadSelectCount()
        {
            _selectCount.Clear();
            
            using (FileStream fs = new FileStream("Tag_Json.json", FileMode.Open))
            using (StreamReader file = new StreamReader(fs))
            using (JsonReader w = new JsonTextReader(file))
            {

                //JsonSerializer Position = new JsonSerializer();
                string json = file.ReadToEnd();
                JArray array = JArray.Parse(json);
                foreach(JValue item in array)
                {     
                    _selectCount.Add((int)item);
                }
            }
        }
        private void UserControl1_KeyUp(object sender, KeyEventArgs e)
        {
            //this.Focus();
        }
        private void UserControl1_KeyPress(object sender, KeyPressEventArgs e)
        {
            


        }
        public class Position
        {
            public int T;
            public int X;
            public int Y;
            
        }
        List<Position> List_UndoLocation = new List<Position>();
        
        enum CommandKind
        {
            Unknown,
            Position,
            BgColor
        }

        private void UserControl1_MouseUp_1(object sender, MouseEventArgs e)
        {
            mouseDown = false;
            selectionEnd = e.Location;
            SetSelectionRect();
        }
        class PanelObjectHistory
        {           
            public readonly int x;
            public readonly int y;
            public readonly Control targetPanel;
            public Color color;
            public CommandKind command;
            public PanelObjectHistory(int x, int y, Control targetPanel)
            {
                this.x = x;
                this.y = y;
                this.targetPanel = targetPanel;
            }
            public override string ToString()
            {
                if (targetPanel.Tag != null)
                {
                    return command + "," + (int)targetPanel.Tag + ":" + x + "," + y + "," + color;
                }
                else
                {
                    return command + "," + x + "," + y + "," + color;
                }
            }
        }
    }
}
