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
            foreach(var directory in directoryInfo.GetDirectories())
            
                directoryNode.Nodes.Add(CreateDirectoryNode(directory));
           
            foreach (var file in directoryInfo.GetFiles())
            
                directoryNode.Nodes.Add(new TreeNode(file.Name));

            return directoryNode;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            string textbox_text = this.textBox1.Text;
            this.Text = textbox_text;

            int count = Convert.ToInt32(textbox_text);
            for (int i = 0; i < count; ++i)
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
