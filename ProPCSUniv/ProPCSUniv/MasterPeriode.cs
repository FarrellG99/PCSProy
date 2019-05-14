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
    public partial class MasterPeriode : Form
    {
        OracleDataAdapter ADAP;
        DataTable DT;
        OracleConnection conn = Form1.conn;
        String searchtxt = "";

        public void rename_header()
        {
            DG.Columns[0].HeaderText = "KODE";
            DG.Columns[1].HeaderText = "TAHUN AJARAN";
        }
        private void buka_grid()
        {
            DT = new DataTable();
            ADAP = new OracleDataAdapter("select * from periode  " + searchtxt, conn);
            ADAP.Fill(DT);
            DG.DataSource = DT;
            rename_header();
        }
        public void siapkan_form_mode(Boolean bol) //patokan dari button insert
        {
            btnInsert.Enabled = bol;
            btnDelete.Enabled = !bol;
        }
        public void bersihkan_form()
        {
            txtKodeThnAjaran.Text = "";
        }
        public MasterPeriode()
        {
            InitializeComponent();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            //if (txtKodeThnAjaran.Text == "") MessageBox.Show("Isi Kode Tahun Ajaran");
            // else
            if (cmbTahun.Text == "") MessageBox.Show("Pilih Tahun Ajaran");
            else
            {
                try
                {
                    String ket = "",gagen="";
                    if (rbGasal.Checked) { ket = cmbTahun.Text + "1"; gagen = "Gasal"; }
                    else { ket = cmbTahun.Text + "2"; gagen = "Genap"; }
                    String ketket = "Tahun Ajaran " + cmbTahun.Text + "/" + (int.Parse(cmbTahun.Text) + 1) + gagen;
                    OracleCommand oins = new OracleCommand("insert into periode values(" +
                  "'" + ket + "'," +
                  "'" + ketket + "')"
                  , conn);
                    oins.ExecuteNonQuery();
                    MessageBox.Show("1 data periode terbuat");
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
            searchtxt = " WHERE LOWER(KODE_THN_AJARAN) LIKE '%" + txtSearch.Text.ToLower() + "%' OR lower(TAHUN_AJARAN) LIKE '%" + txtSearch.Text.ToLower() + "%' order by 1";
            buka_grid();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                OracleCommand oupd = new OracleCommand("delete periode where " +
                    "KODE_THN_AJARAN='" + txtKodeThnAjaran.Text + "'"
                    , conn);
                if (conn.State == ConnectionState.Closed) conn.Open();
                oupd.ExecuteNonQuery();
                MessageBox.Show("Data tahun ajaran terhapus");
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
            String tahun = DG.Rows[e.RowIndex].Cells[0].Value.ToString().Substring(0, 4);
            int posisi = -1;
            for (int i = 0; i < cmbTahun.Items.Count; i++)
            {
                if (cmbTahun.Items[i].ToString() == tahun) posisi = i;
            }
            int gangen = int.Parse(DG.Rows[e.RowIndex].Cells[0].Value.ToString().Substring(4));
            cmbTahun.SelectedIndex = posisi;
            siapkan_form_mode(false); cmbTahun.Enabled = false;
            if (gangen==1) rbGasal.Checked = true;
            else rbGenap.Checked = true;
            rbGasal.Enabled = false; rbGenap.Enabled = false;
            txtKodeThnAjaran.Text = DG.Rows[e.RowIndex].Cells[0].Value.ToString();
        }

        private void MasterTahunAjaran_Load(object sender, EventArgs e)
        {
            buka_grid(); siapkan_form_mode(true);
            cmbTahun.Items.Clear();
            cmbTahun.Enabled = true; rbGasal.Enabled = true; rbGenap.Enabled = true;
            rbGasal.Checked = true; txtKodeThnAjaran.Text = "";
            for (int i = 2010; i < 2037; i++)
            {
                cmbTahun.Items.Add(i);
            }
        }

        private void BCancel_Click(object sender, EventArgs e)
        {
            MasterTahunAjaran_Load(sender, e);
        }
    }
}
