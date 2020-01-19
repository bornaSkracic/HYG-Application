using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Zadatak_01.References;
using System.Threading;

namespace Zadatak_01
{
    public partial class Form3 : Form
    {

        public int xMax = 1160+25;
        public int yMax = 588+25;
        List<Zvijezda> listOfSTar; 

        public int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }

        public void SetStar(Point tempLocation, float mag, string proper)
        {
            int magC;
            if (mag < 0)
            {
                mag = Math.Abs(mag);
                magC = (int)mag;
            }
            else
            {
                magC = (int)mag / 10;

            }
            int size = magC + 35;
            PictureBox star = new PictureBox
            {

                Size = new Size(size, size),
                Location = new Point(tempLocation.X - magC, tempLocation.Y - magC), 
                BackgroundImage = Zadatak_01.Properties.Resources.starWhite,
                BackgroundImageLayout = ImageLayout.Stretch,
                Visible = true,
                BackColor = Color.Transparent
            };
            
            this.Controls.Add(star);

            Label starLabel = new Label
            {
                Size = new Size(46, 17),
                Location = new Point(star.Location.X, star.Location.Y + size),
                Text = proper,
                ForeColor = Color.White,
                BackColor = Color.Transparent,
            };

            this.Controls.Add(starLabel);

        }

        public void Main1()
        {
           
            int solX = xMax / 2;
            int solY = yMax / 2;

            int yPrevious = 0;
            for (int i = 1; i < Form1.numberOfStars; i++)
            {
                Zvijezda star = new Zvijezda();
                star = listOfSTar[i];

                int distance = (int)float.Parse(star.dist) + 200;

                int tempY;
                int tempX;
                do
                {
                    tempY = RandomNumber(-distance, +distance);
                    tempX = (int)(Math.Pow(distance, 2) - Math.Pow(tempY, 2));
                } while (tempX < 0);

                tempX = (int)Math.Sqrt(tempX);
                
                if (i % 2 == 0)
                {
                    tempY = yPrevious - solY + 50;
                    tempX += solX;
                }
                else
                {
                    tempX = -tempX + solX;
                
                }
                
                tempY += solY;
                
                Point Location = new Point(tempX, tempY);
                SetStar(Location, float.Parse(star.mag), star.proper);
                yPrevious = tempY;
            }

        }

        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            using (hygEntities context = new hygEntities())
            {
                listOfSTar = context.Zvijezdas.ToList<Zvijezda>();
            }
            
            Zvijezda sol = listOfSTar[0];
            
            Point solLocation = new Point(xMax/2 -25, yMax/2 -25); //center of the map
            SetStar(solLocation, float.Parse(sol.mag), sol.proper);
            
            Main1();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }
    }
}
