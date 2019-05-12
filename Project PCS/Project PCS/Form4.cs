using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.DataAccess.Client;

namespace Project_PCS
{
    public partial class Form4 : Form
    {
        OracleConnection conn;
        public Form4()
        {
            InitializeComponent();
            try
            {
                conn = new OracleConnection();
                conn.ConnectionString = "Data Source= orcl ;User ID=pcs ;password=pcs";
                refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void refresh()
        {
            conn.Open();
            //load data grid view
            DataSet dsTab = new DataSet();
            OracleDataAdapter da = new OracleDataAdapter("SELECT * FROM MAHASISWA ", conn);
            da.Fill(dsTab);
            dataGridView1.DataSource = dsTab.Tables[0];
            //dataGridView1.Sort(this.dataGridView1.Columns[]);
            conn.Close();
        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }

        int index;
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            index = e.RowIndex;
            textBox1.Text = dataGridView1.Rows[index].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.Rows[index].Cells[1].Value.ToString();
            textBox6.Text = dataGridView1.Rows[index].Cells[2].Value.ToString();
            comboBox1.SelectedItem = dataGridView1.Rows[index].Cells[3].Value.ToString();
            textBox3.Text = dataGridView1.Rows[index].Cells[4].Value.ToString();
            string[] pecah = dataGridView1.Rows[index].Cells[5].Value.ToString().Split(' ');
            string[] pecah2 = pecah[0].Split('/');
            DateTime tanggal = new DateTime(Convert.ToInt32(pecah2[2]), Convert.ToInt32(pecah2[1]), Convert.ToInt32(pecah2[0]));
            dateTimePicker1.Value = tanggal;
            if (dataGridView1.Rows[index].Cells[6].Value.ToString() == "L")
            {
                radioButton1.Checked = true;
            }
            else
            {
                radioButton2.Checked = true;
            }
            textBox4.Text = dataGridView1.Rows[index].Cells[7].Value.ToString();
            textBox5.Text = dataGridView1.Rows[index].Cells[8].Value.ToString();
            textBox7.Text = dataGridView1.Rows[index].Cells[9].Value.ToString();
            textBox8.Text = dataGridView1.Rows[index].Cells[10].Value.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string message = "Apakah Anda Yakin??";
            string title = "Delete Confirmation";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, title, buttons);
            if (result == DialogResult.Yes)
            {
                try
                {
                    conn.Open();
                    OracleCommand cmd = new OracleCommand();
                    cmd.Connection = conn;
                    //delete dan update tinggal ubah CommandText 
                    cmd.CommandText = "DELETE FROM MAHASISWA WHERE NRP = '" + textBox1.Text + "'";
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    refresh();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    throw;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                //delete dan update tinggal ubah CommandText 
                char jk;
                if (radioButton1.Checked == true) { jk = 'L'; }
                else { jk = 'P'; }
                string[] pecah = dataGridView1.Rows[index].Cells[5].Value.ToString().Split(' ');
                cmd.CommandText = "INSERT INTO MAHASISWA VALUES('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox6.Text + "','" + comboBox1.SelectedItem.ToString() + "','" + textBox3.Text + "',to_date('" + pecah[0] + "','dd-mm-yyyy'),'" + jk + "','" + textBox4.Text + "','" + textBox5.Text + "','" + textBox7.Text + "','" + textBox8.Text + "')";
                cmd.ExecuteNonQuery();
                conn.Close();
                refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
