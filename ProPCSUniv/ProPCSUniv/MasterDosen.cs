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
            DG.Columns[5].HeaderText = "Status Wali";
            DG.Columns[6].HeaderText = "No. Telpon Dosen";
        }
        private void buka_grid()
        {
            DT = new DataTable();
            ADAP = new OracleDataAdapter("select nip,nama_dosen,case jkdosen when 'L' then " +
                "'Laki-Laki' when 'P' then 'Perempuan' end,agama_dosen,tempatlhr_dosen,tanggal_dosen,alamat_dosen," +
                "case status_wali when '0' then 'Bukan Dosen Wali'" +
                " when '1' then 'Dosen Wali' end, notelp_dosen from dosen order by 1", conn);
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
                string tmptlhr = DG.Rows[e.RowIndex].Cells[4].Value.ToString();
                DateTime tgllhr = Convert.ToDateTime(DG.Rows[e.RowIndex].Cells[5].Value.ToString());
                string alamat = DG.Rows[e.RowIndex].Cells[6].Value.ToString();
                string stat = DG.Rows[e.RowIndex].Cells[7].Value.ToString();
                string notelp = DG.Rows[e.RowIndex].Cells[8].Value.ToString();

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
                dateTimePicker1.Value = tgllhr;
                txtTmptlhr.Text = tmptlhr;
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
                    jk = "P";
                }
                else
                {
                    jk = "L";
                }
                string stats;
                if (radioButton1.Checked == true)
                {
                    stats = "1";
                }
                else
                {
                    stats = "0";
                }
                string qry = "insert into dosen values('"+txtNip.Text+"','"+txtNamadosen.Text+"'," +
                    "'"+jk+"','"+comboBox1.Text+"','"+txtTmptlhr.Text+ "',to_date('" + dateTimePicker1.Value.Day.ToString().PadLeft(2, '0') + "" +
                    "/" + dateTimePicker1.Value.Month.ToString().PadLeft(2, '0') + "" +
                    "/" + dateTimePicker1.Value.Year.ToString().PadLeft(4, '0') + "','dd/mm/yyyy'),'" +
                    "" + txtAlamat.Text+"','"+stats+"','"+TxtNotelp.Text+"')";
                try
                {
                    OracleCommand ocap = new OracleCommand(qry, conn);
                    ocap.ExecuteNonQuery();
                    MessageBox.Show("1 Data Dosen telah Masuk ");
                    String qryins = "insert into pengguna values('" + txtNip.Text + "','123')";
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
                    jk = "P";
                }
                else
                {
                    jk = "L";
                }
                string stats;
                if (radioButton1.Checked == true)
                {
                    stats = "1";
                }
                else
                {
                    stats = "0";
                }
                string qry = "update dosen set nama_dosen = '" + txtNamadosen.Text + "'," +
                    "jkdosen = '" + jk + "',agama_dosen = '" + comboBox1.Text + "',tempatlhr_dosen = '" + txtTmptlhr.Text + "', tanggal_dosen = to_date('" + dateTimePicker1.Value.Day.ToString().PadLeft(2, '0') + "" +
                    "/" + dateTimePicker1.Value.Month.ToString().PadLeft(2, '0') + "" +
                    "/" + dateTimePicker1.Value.Year.ToString().PadLeft(4, '0') + "','dd/mm/yyyy'), alamat_dosen = '" +
                    "" + txtAlamat.Text + "', status_wali = '" + stats + "', notelp_dosen  = '" + TxtNotelp.Text + "' where nip = '"+txtNip.Text+"'";
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
