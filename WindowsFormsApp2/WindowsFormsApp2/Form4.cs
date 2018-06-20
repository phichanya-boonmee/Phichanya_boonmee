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

            List_location.Clear();
            using (FileStream fs = new FileStream("Json.json", FileMode.Create))
            using (StreamWriter file = new StreamWriter(fs))
            using (JsonWriter w = new JsonTextWriter(file))
            {
                JsonSerializer Position = new JsonSerializer();

                foreach (Control item in userControl1.Controls)
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
            userControl1.Controls.Clear();

            using (FileStream fs = new FileStream("Json.json", FileMode.Open))
            using (StreamReader file = new StreamReader(fs))
            using (JsonReader w = new JsonTextReader(file))
            {
                //Position po = new Position();
                JsonSerializer Position = new JsonSerializer();              
                List<Position> positions = JsonConvert.DeserializeObject<List<Position>>(file.ReadToEnd());

                //----------------------------------------------------------------------------------------

                for(int i=0; i<=List_location.Count-1;i++)
                {
                  
                    int Location_x = positions[i].x;
                    int Location_y = positions[i].y;

                    userControl1.AddNewBox(Location_x, Location_y);
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
