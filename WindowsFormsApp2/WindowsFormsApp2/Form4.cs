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
using Newtonsoft.Json.Linq;
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
                userControl1.AddNewBox(i * 10, i * 10, i);
            }

        }

        public class Position
        {
            public int X;
            public int Y;
        }
        List<Position> List_location = new List<Position>();

        private void userControl1_Load(object sender, EventArgs e)
        {
            
        }

        //Save location

        private void button2_Click(object sender, EventArgs e)
        {
            //Clear List after click button Save again

         
            //userControl1.Write_Json();
            
          
            List_location.Clear();
            using (FileStream fs = new FileStream("Json.json", FileMode.Create))
            using (StreamWriter file = new StreamWriter(fs))
            using (JsonWriter w = new JsonTextWriter(file))
            {
                JsonSerializer Position = new JsonSerializer();

                foreach (Control item in userControl1.Controls)
                {     
                    Position po = new Position();
                    po.X = item.Left;
                    po.Y = item.Top;              
                    List_location.Add(po);
               
                    Console.WriteLine(List_location.Count);
                   
                }
                Position.Serialize(w, List_location);              
            }
                         
        }
        //Load file Json
        private void button3_Click(object sender, EventArgs e)
        {

            //clear panel

            userControl1.Controls.Clear();
            using (FileStream fs = new FileStream("Json.json", FileMode.Open))
            using (StreamReader file = new StreamReader(fs))
            using (JsonReader w = new JsonTextReader(file))
            {
                //Position po = new Position();
                JsonSerializer Position = new JsonSerializer();              
                List<Position> positions = JsonConvert.DeserializeObject<List<Position>>(file.ReadToEnd());
                Console.WriteLine(positions[0].X);
                //----------------------------------------------------------------------------------------
                Console.WriteLine(List_location.Count);
                for (int i=0; i<=positions.Count-1;i++)
                {                
                    int Location_x = positions[i].X;
                    int Location_y = positions[i].Y;
                    userControl1.AddNewBox(Location_x, Location_y,0);

                }               
                Console.WriteLine(List_location.Count);
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
