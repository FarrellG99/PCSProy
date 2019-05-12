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
    public partial class MasterDosen : Form
    {
        OracleDataAdapter ADAP;
        DataTable DT,DTAGAMA;
        OracleConnection conn = Form1.conn;
        String searchtxt = "", status;

        public MasterDosen()
        {
            InitializeComponent();
        }

        private void rbFemale_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rbMale_CheckedChanged(object sender, EventArgs e)
        {

        }
        private void rename_header()
        {
            DG.Columns[0].HeaderText = "NIP";
            DG.Columns[1].HeaderText = "Nama Dosen";
            DG.Columns[2].HeaderText = "Jenis Kelamin";
            DG.Columns[3].HeaderText = "Agama";
            DG.Columns[4].HeaderText = "Alamat Dosen";
            DG.Columns[5].HeaderText = "No.Telpon Dosen";
            DG.Columns[6].HeaderText = "Status Wali";
        }
        private void buka_grid()
        {
            DT = new DataTable();
            ADAP = new OracleDataAdapter("select nip,namadosen,case jkdosen when 'M' then " +
                "'Laki-Laki' when 'F' then 'Perempuan' end,agamadosen,alamatdosen," +
                "notelpdosen,case statuswali when 'N' then 'Bukan Dosen Wali'" +
                " when 'Y' then 'Dosen Wali' end from dosen order by 1", conn);
            ADAP.Fill(DT);
            DG.DataSource = DT;
            rename_header();
        }

        private void DG_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            allbutton(false);
            if (e.RowIndex>-1)
            {
                string nip = DG.Rows[e.RowIndex].Cells[0].Value.ToString();
                string nama = DG.Rows[e.RowIndex].Cells[1].Value.ToString();
                string jk = DG.Rows[e.RowIndex].Cells[2].Value.ToString();
                string agama = DG.Rows[e.RowIndex].Cells[3].Value.ToString();
                string alamat = DG.Rows[e.RowIndex].Cells[4].Value.ToString();
                string notelp = DG.Rows[e.RowIndex].Cells[5].Value.ToString();
                string stat = DG.Rows[e.RowIndex].Cells[6].Value.ToString();

                txtNip.Text = nip;
                txtNamadosen.Text = nama;
                if (jk == "Laki-Laki")
                {
                    rbMale.Checked = true;
                }
                else
                {
                    rbFemale.Checked = true;
                }
                comboBox1.Text = agama;
                txtAlamat.Text = alamat;
                TxtNotelp.Text = notelp;
                if (stat == "Dosen Wali")
                {
                    radioButton1.Checked = true;
                }
                else
                {
                    radioButton2.Checked = true;
                }
            }
        }
        bool flag = true;
        private void cek_smua()
        {
            if (txtNip.Text == "")
            {
                MessageBox.Show("Nip tidak boleh kosong!!");
                flag = false;
            }
            if (txtNamadosen.Text == "")
            {
                MessageBox.Show("Nama Dosen tidak boleh kosong!!");
                flag = false;
            }
            if (rbFemale.Checked == false && rbMale.Checked == false)
            {
                MessageBox.Show("Jenis Kelamin Harus Dipilih Salah Satu");
                flag = false;
            }
            if (txtAlamat.Text == "")
            {
                MessageBox.Show("Alamat tidak Boleh Kosong");
                flag = false;
            }
            if (TxtNotelp.Text == "")
            {
                MessageBox.Show("No.Telpon tidak boleh kosong");
                flag = false;
            }
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            cek_smua();
            string sama;
            if (flag == true)
            {
                if (conn.State == ConnectionState.Closed) conn.Open();
                string jk;
                if (rbFemale.Checked == true)
                {
                    jk = "F";
                }
                else
                {
                    jk = "M";
                }
                string stats;
                if (radioButton1.Checked == true)
                {
                    stats = "Y";
                }
                else
                {
                    stats = "N";
                }
                string qry = "insert into dosen values('"+txtNip.Text+"','"+txtNamadosen.Text+"'," +
                    "'"+jk+"','"+comboBox1.Text+"','"+txtAlamat.Text+"','"+TxtNotelp.Text+"','"+stats+"')";
                try
                {
                    OracleCommand ocap = new OracleCommand(qry, conn);
                    ocap.ExecuteNonQuery();
                    MessageBox.Show("1 Data Dosen telah Masuk ");
                    String qryins = "insert into pengguna values('" + txtNip.Text + "','" + txtNip.Text + "')";
                    OracleCommand ocappgn = new OracleCommand(qryins, conn);
                    ocappgn.ExecuteNonQuery();
                    MessageBox.Show("user pengguna masuk ");
                    buka_grid();
                    allbutton(true);
                    reset_data();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal Insert karena : " + ex);
                    txtNamadosen.Text = qry;
                }
                flag = true;
            }
        }
        private void reset_data()
        {
            txtNip.Text = "";
            txtNamadosen.Text = "";
            TxtNotelp.Text = "";
            txtAlamat.Text = "";
            txtSearch.Text = "";
            rbMale.Checked = false;
            rbFemale.Checked = false;
            radioButton2.Checked = true;
            radioButton1.Checked = false;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            cek_smua();
            if (flag == true)
            {
                if (conn.State == ConnectionState.Closed) conn.Open();
                string jk;
                if (rbFemale.Checked == true)
                {
                    jk = "F";
                }
                else
                {
                    jk = "M";
                }
                string stats;
                if (radioButton1.Checked == true)
                {
                    stats = "Y";
                }
                else
                {
                    stats = "N";
                }
                string qry = "update dosen set namadosen = '" + txtNamadosen.Text + "'," +
                    "jkdosen = '" + jk + "',agamadosen = '" + comboBox1.Text + "',alamatdosen = '" + txtAlamat.Text + "'" +
                    ",notelpdosen = '" + TxtNotelp.Text + "',statuswali = '" + stats + "' where nip = '"+txtNip.Text+"'";
                try
                {
                    OracleCommand ocap = new OracleCommand(qry, conn);
                    ocap.ExecuteNonQuery();
                    MessageBox.Show("1 Data Dosen telah terupdate ");
                    buka_grid();
                    allbutton(true);
                    reset_data();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal Update karena : " + ex);
                    txtNamadosen.Text = qry;
                }
                flag = true;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            cek_smua();
            if (flag == true)
            {
                if (conn.State == ConnectionState.Closed) conn.Open();
                string qry = "delete dosen where nip = '"+txtNip.Text+"'";
                try
                {
                    OracleCommand ocap = new OracleCommand(qry, conn);
                    ocap.ExecuteNonQuery();
                    MessageBox.Show("1 Data Dosen telah terhapus ");
                    buka_grid();
                    allbutton(true);
                    reset_data();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal Delete karena : " + ex);
                    txtNamadosen.Text = qry;
                }
                flag = true;
            }
        }

        private void btnCari_Click(object sender, EventArgs e)
        {
            string carii = txtSearch.ToString();
            DT = new DataTable();
            ADAP = new OracleDataAdapter("select nip,namadosen,case jkdosen when 'M' then " +
                "'Laki-Laki' when 'F' then 'Perempuan' end,agamadosen,alamatdosen," +
                "notelpdosen,case statuswali when 'N' then 'Bukan Dosen Wali'" +
                " when 'Y' then 'Dosen Wali' end from dosen where nip like '%"+carii+"%' or namadosen like " +
                "'%"+carii+"%' or statuswali like '%"+carii+"%'order by 1", conn);
            ADAP.Fill(DT);
            DG.DataSource = DT;
            rename_header();
        }

        private void allbutton(Boolean siapp)
        {
            btnInsert.Enabled = siapp;
            btnUpdate.Enabled = !siapp;
            btnDelete.Enabled = !siapp;
            txtNip.Enabled = siapp;
        }

        private void MasterDosen_Load(object sender, EventArgs e)
        {
            buka_grid();
        }
    }
}
