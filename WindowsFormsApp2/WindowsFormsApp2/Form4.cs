using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
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
                userControl1.AddNewBox(i * 10, i * 10);
            }

        }

        public class Position
        {

            public int x;
            public int y;
        }
        List<Position> List_location = new List<Position>();

        private void userControl1_Load(object sender, EventArgs e)
        {
            
        }


      
        //SAVE locatio
        private void button2_Click(object sender, EventArgs e)
        {
            JsonSerializer Position = new JsonSerializer();

            using (FileStream fs = new FileStream("Json.json", FileMode.Create))
            using (StreamWriter file = new StreamWriter(fs))
            using (JsonWriter w = new JsonTextWriter(file))
            {
                
                foreach(Control item in userControl1.Controls)
                {     
                    Position po = new Position();
                    po.x = item.Left;
                    po.y = item.Top;              
                    List_location.Add(po);
               
                    Console.WriteLine(List_location.Count);
                   
                }
                Position.Serialize(w, List_location);

            }
                         
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //clear panel
            //userControl1.Controls.Clear();

            //using (FileStream fs = new FileStream("textJson.json", FileMode.Open))

            //using (BinaryReader w = new BinaryReader(fs))
            //{
            //    //Loop create item
            //    while (w.BaseStream.Position != w.BaseStream.Length)
            //    {
            //        int Location_x = w.ReadInt32();
            //        int Location_y = w.ReadInt32();
            //        userControl1.AddNewBox(Location_x, Location_y);
            //    }
            //}
            userControl1.Controls.Clear();
            JsonSerializer Position = new JsonSerializer();

            using (FileStream fs = new FileStream("Json.json", FileMode.Open))
            using (StreamReader file = new StreamReader(fs))
            using (JsonReader w = new JsonTextReader(file))
            {              
                while (file.BaseStream.Position != file.BaseStream.Length)
                {
                    Position po = new Position();
                    //List<int> location_D = JsonConvert.DeserializeObject<List<int>>(w);
                    //Console.WriteLine(string.Join(", ", location_D.ToArray()));

                    //int Location_x = w.ReadInt32();
                    //int Location_y = w.ReadInt32();
                    //userControl1.AddNewBox(Location_x, Location_y);

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
