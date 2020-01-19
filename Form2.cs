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
using System.Data.SqlClient;
using System.Data.Entity;

namespace Zadatak_01
{
    public partial class Form2 : Form
    {
        string connectionString = @"Data Source=(local)\SQLEXPRESS;Initial Catalog = hyg; Integrated Security = True;";
       
        public Form2()
        {
            InitializeComponent();
            
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'hygDataSet.Zvijezda' table. You can move, or remove it, as needed.
            // this.zvijezdaTableAdapter.Fill(this.hygDataSet.Zvijezda);
            using (hygEntities context = new hygEntities())
            {
                dataGridView1.DataSource = context.Zvijezdas.ToList<Zvijezda>();
                
            }
        }
        
        

       private void back_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            this.Hide();
            f1.Closed += (s, args) => this.Close();
            f1.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (hygEntities context = new hygEntities())
            {
                string properData = dataGridView1.CurrentCell.Value.ToString();
                int index = dataGridView1.CurrentCell.RowIndex;
                Zvijezda star = context.Zvijezdas.FirstOrDefault(s => (s.proper == properData) && (s.id -1 == index));
                try
                {
                    star.proper = txtProper.Text;
                    context.SaveChanges();
                    dataGridView1.DataSource = context.Zvijezdas.ToList<Zvijezda>();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Can only edit 'proper'!");
                    txtProper.Text = "";
                } 
            }
        
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
           
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("You are about to delete the database.\nProceed?", "Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if(result == DialogResult.Yes)
            {
                using (SqlConnection sqlCon = new SqlConnection(connectionString))
                {
                    sqlCon.Open();
                    string querry = "DELETE FROM Zvijezda\n DBCC CHECKIDENT(Zvijezda, RESEED, 0)";
                    SqlCommand cmd = new SqlCommand(querry, sqlCon);
                    cmd.ExecuteNonQuery();
                    using (hygEntities context = new hygEntities())
                    {
                        dataGridView1.DataSource = context.Zvijezdas.ToList<Zvijezda>();
                    }
                }
            }
        }

        private void zvijezdaBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }
    }
}

