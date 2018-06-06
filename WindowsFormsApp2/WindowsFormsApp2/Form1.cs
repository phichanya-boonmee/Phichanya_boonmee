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
    public partial class Form1 : Form
    {
        int x, y;
      
      
        public Form1()
        {
            InitializeComponent();
        }
        //input number
        public void button1_Click(object sender, EventArgs e)
        {
            string textbox_text = this.textBox1.Text;
            this.Text = textbox_text;

            string txt2 = "hello\r\n";
        //loop show text
            int i;
            string ans = txt2;
            int count = Convert.ToInt32(textbox_text);
            for (i = 1; i <= count; i++)
            {

                this.textBox2.Text = ans.ToString();
                ans = ans + txt2;

            }
            MyWriteText(txt2);

        }
        //function write all text
        void MyWriteText(string txt2)
        {

            File.WriteAllText("file1.txt", txt2);
        }
        //listbox Folder
        private void button2_Click(object sender, EventArgs e)
        {
            string curDir = Directory.GetCurrentDirectory();
            curDir = "..\\..";
            string[] dirs = Directory.GetDirectories(curDir);
            string[] files = Directory.GetFiles(curDir);

            int j = dirs.Length;
            for (int i = 0; i < j; ++i)
            {
                listBox1.Items.Add(dirs[i]);
            }
        }
        //listbox Files
        private void button3_Click(object sender, EventArgs e)
        {
            string curDir = Directory.GetCurrentDirectory();
            curDir = "..\\..";
            string[] dirs = Directory.GetDirectories(curDir);
            string[] files = Directory.GetFiles(curDir);

            int j = dirs.Length;
            for (int i = 0; i < j; ++i)
            {

                listBox1.Items.Add(files[i]);
            }
        }
        //treeView
        private void button4_Click(object sender, EventArgs e)
        {
            ListDirectory(treeView1, "..//..");
        }
        private void ListDirectory(TreeView treeView, string path)
        {
            treeView.Nodes.Clear();
            var rootDirectory = new DirectoryInfo(path);
            treeView.Nodes.Add(CreateDirectoryNode(rootDirectory));
        }

        private static TreeNode CreateDirectoryNode(DirectoryInfo directoryInfo)
        {
            var directoryNode = new TreeNode(directoryInfo.Name);
            //Directory,Floder
            foreach (var directory in directoryInfo.GetDirectories())
            
                directoryNode.Nodes.Add(CreateDirectoryNode(directory));
            //File           
            foreach (var file in directoryInfo.GetFiles())
            
                directoryNode.Nodes.Add(new TreeNode(file.Name));

            return directoryNode;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
//Event input num & show Item/save location/show item from location
        private void button5_Click(object sender, EventArgs e)
        {
        // input number 
           
            int count = Int32.Parse(textBox3.Text);

        //Loop Show item in panel1
            for (int i = 0; i < count; ++i)
            {
                Panel myPanel = new Panel();
                myPanel.Size = new Size(10, 10);
                myPanel.Location = new Point(i * 10, i * 10);
                myPanel.BackColor = Color.Blue;

                this.panel1.Controls.Add(myPanel);

            }
         
        //Event        
            foreach(Control item in this.panel1.Controls)
            {            
                item.MouseDown += panel1_MouseDown;
                item.MouseUp += panel1_MouseUp;
                item.MouseMove += panel1_MouseMove;

            }

        }
        //MouseUp
        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {

            Control item = (Control)sender;
            item.BackColor = Color.Blue;
   

        }
        //MouseDown
        Point MouseDownLocation;
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {

            Control item = (Control)sender;
            if (e.Button == MouseButtons.Left) MouseDownLocation = e.Location;
            {    
                x = e.X;
                y = e.Y;
                item.BackColor = Color.Yellow;  
            }
        }
        //MouseMove
        
        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            Control item = (Control)sender;
            if (e.Button == MouseButtons.Left) 
            {
                item.Left += e.X - MouseDownLocation.X;

                item.Top += e.Y - MouseDownLocation.Y;

            }

        }
        //Write Binary
        private void button6_Click(object sender, EventArgs e)
        {
                int count = panel1.Controls.Count; //call panel1
                using (FileStream fs = new FileStream("test.txt", FileMode.Create))
                using (BinaryWriter w = new BinaryWriter(fs))
                {
                for (int i = 0; i < count; i++)
                {
                    //list,Directory, Queue,Stack

                    Panel panel = (Panel)panel1.Controls[i];
                    Point local = panel.Location;
                    int Location_x = local.X;
                    int Location_y = local.Y;

                    //write binary in text file
                    w.Write(Location_x);
                    w.Write(Location_y);



                }

            }
            //File.WriteAllText("file1.txt", txt3);
            //int 32
            //{int32, int 32y}
        }
        private void button7_Click(object sender, EventArgs e)
        {
            //clear panel
            panel1.Controls.Clear(); 
    
            using (FileStream fs = new FileStream("test.txt", FileMode.Open))
            using (BinaryReader w = new BinaryReader(fs))
            {
                //Loop create item
                while (w.BaseStream.Position != w.BaseStream.Length)
                {
                    //position_Location/convert Binary to string 
                    int Location_x = w.ReadInt32(); 
                    int Location_y = w.ReadInt32();
                    //Create item
                    Panel myPanel = new Panel();
                    myPanel.Size = new Size(10, 10);
                    myPanel.Location = new Point(Location_x, Location_y);
                    myPanel.BackColor = Color.Blue;

                    this.panel1.Controls.Add(myPanel);
                }
                //Event
                foreach (Control item in this.panel1.Controls)
                {
                    item.MouseDown += panel1_MouseDown;
                    item.MouseUp += panel1_MouseUp;
                    item.MouseMove += panel1_MouseMove;

                }
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }


    }
}
