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

namespace ProPCSUniv
{
    public partial class Form1 : Form
    {
        public static OracleConnection conn = new OracleConnection("user id=pcs;password=pcs;data source=orcl");
        public Form1()
        {
            InitializeComponent();
        }

        public void buka_koneksi()
        {
            try
            {
                conn.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tidak bisa membuat koneksi dengan database\n" + "Karena : " + ex.Message);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BLogin_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "") MessageBox.Show("Username masih kosong");
            else if (textBox2.Text == "") MessageBox.Show("Password masih kosong");
            else
            {
                OracleDataAdapter findpeng = new OracleDataAdapter("select * from pengguna where userid='" + textBox1.Text + "' and password='" + textBox2.Text + "'", conn);
                DataTable dt = new DataTable();
                findpeng.Fill(dt);
                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Invalid username or password");
                }
                else
                {
                    if (dt.Rows[0].ItemArray[2].ToString() == "admin")
                    {
                        FMenuAdmin fadmin = new FMenuAdmin();
                        fadmin.Show();
                    }
                    else if (dt.Rows[0].ItemArray[2].ToString() == "DOSEN")
                    {
                        FMenuDosen fdosen = new FMenuDosen();
                        fdosen.Show();
                    }
                    else if (dt.Rows[0].ItemArray[2].ToString() == "MAHASISWA")
                    {
                        FMenuMhs fmhs = new FMenuMhs();
                        fmhs.Show();
                    }

                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            textBox1.Focus();
        }
    }
}
