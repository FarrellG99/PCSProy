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
    public partial class MasterRuangan : Form
    {
        OracleDataAdapter ADAP;
        DataTable DT;
        OracleConnection conn = Form1.conn;
        String searchtxt = "";
        public MasterRuangan()
        {
            InitializeComponent();
        }
        public void rename_header()
        {
            DG.Columns[0].HeaderText = "KODE";
            DG.Columns[1].HeaderText = "JUMLAH KURSI";
            DG.Columns[2].HeaderText = "KETERANGAN";
        }
        private void buka_grid()
        {
            DT = new DataTable();
            ADAP = new OracleDataAdapter("select * from ruangan  " + searchtxt, conn);
            ADAP.Fill(DT);
            DG.DataSource = DT;
            rename_header();
        }
        public void siapkan_form_mode(Boolean bol) //patokan dari button insert
        {
            btnInsert.Enabled = bol;
            btnUpdate.Enabled = !bol;
            btnDelete.Enabled = !bol;
            txtKodeRuang.Enabled = bol;
        }
        public void bersihkan_form()
        {
            txtKodeRuang.Text = ""; txtDeskripsi.Text = ""; cmbJKursi.SelectedIndex = -1;
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {

            if (txtKodeRuang.Text == "") MessageBox.Show("Isi Kode Ruangan");
            else if (txtDeskripsi.Text == "") MessageBox.Show("Isi Peruntukan Ruangan");
            else if (cmbJKursi.Text =="") MessageBox.Show("Pilih Jumlah Kursi");
            else
            {
                try
                {
                    OracleCommand oins = new OracleCommand("insert into ruangan values(" +
                  "'" + txtKodeRuang.Text + "'," +
                  "'" + cmbJKursi.Text+ "'," +
                  "'" + txtDeskripsi.Text + "')"
                  , conn);
                    oins.ExecuteNonQuery();
                    MessageBox.Show("1 data ruangan terbuat");
                    siapkan_form_mode(true);
                    buka_grid(); bersihkan_form();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal Insert : " + ex.Message);
                }

            }
            
        }

        private void btnCari_Click(object sender, EventArgs e)
        {
            searchtxt = " WHERE LOWER(KODE_RUANGAN) LIKE '%"+txtSearch.Text.ToLower()+"%' OR JUMLAH_KURSI LIKE '%" + txtSearch.Text.ToLower() + "%' OR  LOWER(PERUNTUKAN) LIKE '%"+ txtSearch.Text.ToLower() + "%' order by 1";
            buka_grid();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

            if (txtKodeRuang.Text == "") MessageBox.Show("Isi Kode Ruangan");
            else if (txtDeskripsi.Text == "") MessageBox.Show("Isi Peruntukan Ruangan");
            else if (cmbJKursi.Text == "") MessageBox.Show("Pilih Jumlah Kursi");
            else
            {
                try
                {
                    OracleCommand oupd = new OracleCommand("update ruangan set " +
                     "jumlah_kursi='" +cmbJKursi.Text + "'," +
                     "peruntukan='" + txtDeskripsi.Text + "' where " +
                     "kode_ruangan='" + txtKodeRuang.Text + "'"
                     , conn);
                    if (conn.State == ConnectionState.Closed) conn.Open();
                    oupd.ExecuteNonQuery();
                    MessageBox.Show("Data ruangan terubah");
                    siapkan_form_mode(true);
                    buka_grid(); bersihkan_form();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal Update : " + ex.Message);
                }

            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                OracleCommand oupd = new OracleCommand("delete ruangan where " +
                    "kode_ruangan='" + txtKodeRuang.Text + "'"
                    , conn);
                if (conn.State == ConnectionState.Closed) conn.Open();
                oupd.ExecuteNonQuery();
                MessageBox.Show("Data ruangan terhapus");
                siapkan_form_mode(true);
                buka_grid(); bersihkan_form();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal Delete : " + ex.Message);
            }
        }

        private void DG_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtKodeRuang.Text = DT.Rows[e.RowIndex].ItemArray[0].ToString();
            cmbJKursi.Text = DT.Rows[e.RowIndex].ItemArray[1].ToString();
            txtDeskripsi.Text = DT.Rows[e.RowIndex].ItemArray[2].ToString();
            siapkan_form_mode(false);
        }

        private void MasterRuangan_Load(object sender, EventArgs e)
        {
            buka_grid(); siapkan_form_mode(true);
        }
    }
}
