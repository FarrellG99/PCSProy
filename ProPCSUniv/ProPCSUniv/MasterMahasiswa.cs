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
    public partial class MasterMahasiswa : Form
    {
        public MasterMahasiswa()
        {
            InitializeComponent();
        }
        OracleDataAdapter ADAP;
        DataTable DT, DTJURUSAN, DTDOSENWALI, DTAGAMA;
        OracleConnection conn = Form1.conn;

        private void DG_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            allbutton(false);
            if (e.RowIndex > -1)
            {
                string nrp = DG.Rows[e.RowIndex].Cells[0].Value.ToString();
                string dosenwali = DG.Rows[e.RowIndex].Cells[1].Value.ToString();
                string jurusan = DG.Rows[e.RowIndex].Cells[2].Value.ToString();
                string nama = DG.Rows[e.RowIndex].Cells[3].Value.ToString();
                string njk = DG.Rows[e.RowIndex].Cells[4].Value.ToString();
                string agama = DG.Rows[e.RowIndex].Cells[5].Value.ToString();
                string tmptlhr = DG.Rows[e.RowIndex].Cells[6].Value.ToString();
                DateTime tgllahir = Convert.ToDateTime(DG.Rows[e.RowIndex].Cells[7].Value.ToString());
                string alamat = DG.Rows[e.RowIndex].Cells[8].Value.ToString();
                string notelp = DG.Rows[e.RowIndex].Cells[9].Value.ToString();
                string namaortu = DG.Rows[e.RowIndex].Cells[10].Value.ToString();
                string notelportu = DG.Rows[e.RowIndex].Cells[11].Value.ToString();

                txtnrp.Text = nrp;
                txtnama.Text = nama;
                if (njk == "Laki-Laki")
                {
                    rbMale.Checked = true;
                }
                else
                {
                    rbFemale.Checked = true;
                }
                txttmptlhr.Text = tmptlhr;
                txtalamat.Text = alamat;
                txtnotelp.Text = notelp;
                txtnamaortu.Text = namaortu;
                txttelportu.Text = notelportu;
                dateTimePicker1.Value = tgllahir;
                comboBox1.Text = jurusan;
                comboBox2.Text = dosenwali;
                comboBox3.Text = agama;
            }
        }

        private void btnCari_Click(object sender, EventArgs e)
        {
            if (txtSearch.Text != "")
            {
                string carii = txtSearch.Text;
                DT = new DataTable();
                ADAP = new OracleDataAdapter(" select m.nrp ,d.nama_dosen ,j.nama_jurusan," +
                    "m.nama_mahasiswa ,case m.jenis_kelamin when 'L' " +
                    "then 'Laki-Laki' when 'P' then 'Perempuan' end " +
                    ",m.agama_mahasiswa ,m.tempatlahir_mhs ,m.tgllahir_mhs " +
                    ",m.alamat_mhs ,m.notelp_mhs ,m.namaortu_mhs ,m.notelportu_mhs from mahasiswa m " +
                    ",dosen d , jurusan j where  m.nip = d.nip and j.kode_jurusan = m.kode_jurusan and " +
                    "(m.nrp like '%"+carii+ "%' " +
                    "or d.namadosen like '%" + carii + "%' or m.namamhs like '%"+ carii +"%' " +
                    "or j.namajurusan like '%"+ carii +"%') order by 1", conn);
                ADAP.Fill(DT);
                DG.DataSource = DT;
                rename_header();
            }
        }

        private void ceksmua()
        {
            if (txtnrp.Text == "")
            {
                MessageBox.Show("Nrp Harus diisi!!");
                flag = false;
            }
            if (txtnama.Text == "")
            {
                MessageBox.Show("Nama Mahasiswa Harus Diisi!!");
                flag = false;
            }
            if (rbFemale.Checked == false && rbMale.Checked == false)
            {
                MessageBox.Show("Pilih salah satu gender terlebih dahulu!!");
                flag = false;
            }
            if (txtalamat.Text == "")
            {
                MessageBox.Show("Alamat Harus diisi!!");
                flag = false;
            }
            if (txtnotelp.Text == "" || txtnotelp.TextLength < 12)
            {
                MessageBox.Show("No.telp mahasiswa harus diisi / angka pada No.Telp kurang");
                flag = false;
            }
            if (txttmptlhr.Text == "")
            {
                MessageBox.Show("Tempat lahir harus diisi!!");
                flag = false;
            }
            if (txtnamaortu.Text == "")
            {
                MessageBox.Show("Nama Orang tua harus diisi!!");
                flag = false;
            }
            if (txttelportu.Text == "" || txttelportu.TextLength < 12)
            {
                MessageBox.Show("No.telp orang tua kosong / jumlah Nomor kurang");
                flag = false;
            }
        }
        private void allbutton(Boolean siapp)
        {
            btnInsert.Enabled = siapp;
            btnUpdate.Enabled = !siapp;
            btnDelete.Enabled = !siapp;
            txtnrp.Enabled = siapp;
        }
        bool flag = true;
        private void btnInsert_Click(object sender, EventArgs e)
        {
            ceksmua();
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
                string qry = "insert into mahasiswa values " +
                        "('" + txtnrp.Text + "','" + comboBox2.SelectedValue.ToString() + "','" + comboBox1.SelectedValue.ToString() + "'" +
                        ",'" + txtnama.Text + "','" + comboBox3.Text.ToString() + "','" + txttmptlhr.Text + "'"+
                        ",to_date('" + dateTimePicker1.Value.Day.ToString().PadLeft(2, '0') + "/" + dateTimePicker1.Value.Month.ToString().PadLeft(2, '0') + "/" + dateTimePicker1.Value.Year.ToString().PadLeft(4, '0') + "','dd/mm/yyyy')" +
                        ",'"+jk+"','" + txtalamat.Text + "','" + txtnotelp.Text + "','" + txtnamaortu.Text + "'" +
                        ",'" + txttelportu.Text + "')";
                try
                {
                    OracleCommand ocap = new OracleCommand(qry, conn);
                    ocap.ExecuteNonQuery();
                    String qryins = "insert into pengguna values('" + txtnrp.Text + "','123')";
                    OracleCommand ocappgn = new OracleCommand(qryins, conn);
                    ocappgn.ExecuteNonQuery();
                    MessageBox.Show("user pengguna masuk ");
                    buka_grid();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal Insert karena : " + ex);
                    txtnama.Text = qry;
                }
                flag = true;
            }
            
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            ceksmua();
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
                string qry = "update mahasiswa set nip = '" + comboBox2.SelectedValue.ToString() + "', kode_jurusan = '" + comboBox1.SelectedValue.ToString() + "'" +
                        ",nama_mahasiswa = '" + txtnama.Text + "', agama_mahasiswa = '" + comboBox3.Text.ToString() + "', tempatlahir_mhs = '" + txttmptlhr.Text + "'" +
                        ",tgllahir_mhs = to_date('" + dateTimePicker1.Value.Day.ToString().PadLeft(2, '0') + "/" + dateTimePicker1.Value.Month.ToString().PadLeft(2, '0') + "/" + dateTimePicker1.Value.Year.ToString().PadLeft(4, '0') + "','dd/mm/yyyy')" +
                        ", jenis_kelamin = '" + jk + "',alamat_mhs = '" + txtalamat.Text + "',notelp_mhs = '" + txtnotelp.Text + "', nama_ortu = '" + txtnamaortu.Text + "'" +
                        ",notelportu_mhs = '" + txttelportu.Text + "' where nrp = '"+ txtnrp.Text +"'";
                try
                {
                    OracleCommand ocap = new OracleCommand(qry, conn);
                    ocap.ExecuteNonQuery();
                    MessageBox.Show("1 Data Mahasiswa telah terupdate ");
                    buka_grid();
                    allbutton(true);
                    reset_data();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal Update karena : " + ex);
                    txtnama.Text = qry;
                }
                flag = true;
            }
            
        }
        private void reset_data()
        {
            txtnrp.Text = "";
            txtnama.Text = "";
            txtnamaortu.Text = "";
            txtnotelp.Text = "";
            txtalamat.Text = "";
            txtSearch.Text = "";
            rbFemale.Checked = false;
            rbMale.Checked = false;
            txttelportu.Text = "";
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            string qry = "delete mahasiswa where nrp = '" + txtnrp.Text + "'";
            try
            {
                if (conn.State == ConnectionState.Closed) conn.Open();
                OracleCommand ocap = new OracleCommand(qry, conn);
                ocap.ExecuteNonQuery();
                MessageBox.Show("1 Data Mahasiswa telah terhapus ");
                buka_grid();
                allbutton(true);
                reset_data();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal Delete karena : " + ex);
                txtnama.Text = qry;
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void rename_header()
        {
            DG.Columns[0].HeaderText = "NRP";
            DG.Columns[1].HeaderText = "Dosen Wali";
            DG.Columns[2].HeaderText = "Nama Jurusan";
            DG.Columns[3].HeaderText = "Nama Mahasiswa";
            DG.Columns[4].HeaderText = "Jenis Kelamin";
            DG.Columns[5].HeaderText = "Agama";
            DG.Columns[6].HeaderText = "Tempat Lahir";
            DG.Columns[7].HeaderText = "Tanggal Lahir";
            DG.Columns[8].HeaderText = "Alamat Mahasiswa";
            DG.Columns[9].HeaderText = "No.Telpon Mahasiswa";
            DG.Columns[10].HeaderText = "Nama Orang Tua";
            DG.Columns[11].HeaderText = "No.Telpon Orang Tua";
        }
        private void buka_grid()
        {
            DT = new DataTable();         
            ADAP = new OracleDataAdapter(" select m.nrp ,d.nama_dosen ,j.nama_jurusan,m.nama_mahasiswa ,case m.jenis_kelamin when 'L' then 'Laki-Laki' when 'P' then 'Perempuan' end ,m.agama_mahasiswa ,m.tempatlahir_mhs ,m.tgllahir_mhs ,m.alamat_mhs ,m.notelp_mhs ,m.namaortu_mhs ,m.notelportu_mhs from mahasiswa m , dosen d , jurusan j where  m.nip = d.nip and j.kode_jurusan = m.kode_jurusan order by 1", conn);
            ADAP.Fill(DT);
            DG.DataSource = DT;
            rename_header();
        }
        private void buka_combo()
        {
            DTJURUSAN = new DataTable();
            ADAP = new OracleDataAdapter("select * from jurusan", conn);
            ADAP.Fill(DTJURUSAN);
            comboBox1.Items.Clear();
            comboBox1.ValueMember = "kode_jurusan" ;
            comboBox1.DisplayMember = "nama_jurusan" ;
            comboBox1.DataSource = DTJURUSAN;

            DTDOSENWALI = new DataTable();
            ADAP = new OracleDataAdapter("select * from dosen where status_wali = 'Y'", conn);
            ADAP.Fill(DTDOSENWALI);
            comboBox2.Items.Clear();
            comboBox2.ValueMember = "nip";
            comboBox2.DisplayMember = "nama_dosen";
            comboBox2.DataSource = DTDOSENWALI;


        }
        private void MasterMahasiswa_Load(object sender, EventArgs e)
        {
            buka_grid();buka_combo();allbutton(true);
        }
    }
}
