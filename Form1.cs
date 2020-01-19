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
using Zadatak_01.References;

namespace Zadatak_01
{
    public partial class Form1 : Form
    {
        Form2 f2 = new Form2();
        public static int numberOfStars = 0;
        List<Zvijezda> listOfStars;
        public Form1()
        {
            InitializeComponent();
            using (hygEntities context = new hygEntities())
            {
                listOfStars = context.Zvijezdas.ToList<Zvijezda>();
            }
            label4.Text = "Number of stars to draw:\nMaximum stars: " + (listOfStars.Count - 1).ToString();
        }
        
        private String addslash(String filePath)
        {
            String return_string = "";
            for(int i = 0; i < filePath.Length; i++)
            {
                return_string += filePath[i];
                char backlash = (char)92;
                if(filePath[i] == backlash)
                {
                    return_string += backlash;
                }
            }
            return return_string;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string filePath = addslash(txtFilePath.Text);
            try
            {
                StreamReader sr = new StreamReader(filePath);
                sr.ReadLine();
                string linija;
                
                while ((linija = sr.ReadLine()) != null)
                {
                    string[] linija1 = new string[36];
                    linija1 = linija.Split(',');
                    if (linija1[6] != "")
                    {
                        using (hygEntities context = new hygEntities())
                        {
                            context.Zvijezdas.Add(new Zvijezda
                            {
                                proper = linija1[6],
                                dist = linija1[9],
                                mag = linija1[13]
                            });

                            context.SaveChanges();
                        }
                    }
                }

                this.Hide();
                Form2 f2 = new Form2();
                f2.Closed += (s, args) => this.Close();
                f2.Show();
            }
            
            
            catch(Exception ex)
            {
                MessageBox.Show("Wrong file type or file path!");
                Form1 f1 = new Form1();
                this.Hide();
                f1.Closed += (s, args) => this.Close();
                f1.Show();                 
            }
            
            


        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            txtFilePath.Text = "";
            txtFilePath.Text = openFileDialog1.FileName;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            f2.Show();
            this.Hide();
            f2.Closed += (s, args) => this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            numberOfStars = (int)float.Parse(number.Text);
            if (listOfStars.Count == 0)
            {
                MessageBox.Show("Database is empty!");
                InitializeComponent();
            }
            else if(numberOfStars > listOfStars.Count - 1)
            {
                MessageBox.Show("Too few stars in database!");
                InitializeComponent();
            }
            else {
                Form3 f3 = new Form3();
                this.Hide();
                f3.Show();
                f3.Closed += (s, args) => this.Close();
            }
        }
    }
    
}
