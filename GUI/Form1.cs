using BUS;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class Form1 : Form
    {
        private readonly BenhNhanDAO benhNhanDAO = new BenhNhanDAO();
        private readonly TinhTrangDAO tinhTrangDAO = new TinhTrangDAO();

        public Form1()
        {
            InitializeComponent();
        }

        

        private void FillComboBoxTinhTrang(List<TinhTrang> dstinhtrang)
        {
            cmbTinhtrang.DataSource = dstinhtrang;
            cmbTinhtrang.DisplayMember = "TenTT";
            cmbTinhtrang.ValueMember = "MaTT";
        }

        private void FillComboBoxBNTXG(List<BenhNhan> dsbenhnhan)
        {
            cmbLaynhiemtu.DataSource = dsbenhnhan;
            cmbLaynhiemtu.DisplayMember = "MaBN";
            cmbLaynhiemtu.ValueMember = "MaBN";
            cmbLaynhiemtu.SelectedItem = null;
        }

        private void BindGrid(List<BenhNhan> a)
        {
            dgvCaBenh.Rows.Clear();
            foreach(var item in a)
            {
                int index = dgvCaBenh.Rows.Add();
                dgvCaBenh.Rows[index].Cells[0].Value = item.MaBN;
                dgvCaBenh.Rows[index].Cells[1].Value = item.TenBN;
                dgvCaBenh.Rows[index].Cells[2].Value = item.TinhTrang.TenTT;
                dgvCaBenh.Rows[index].Cells[3].Value = "F" + benhNhanDAO.TinhF(item.MaBN).ToString();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                var dsbn = benhNhanDAO.GetAll();
                var dstt = tinhTrangDAO.GetAll();
                FillComboBoxTinhTrang(dstt);
                FillComboBoxBNTXG(dsbn);
                BindGrid(dsbn);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

            private void dgvCaBenh_CellClick(object sender, DataGridViewCellEventArgs e)
            {
                try
                {
                    BenhNhan a = benhNhanDAO.FindByID(dgvCaBenh.SelectedRows[0].Cells[0].Value.ToString());
                    if (a != null)
                    {
                        txtMaBN.Text = a.MaBN;
                        txtTenBN.Text = a.TenBN;
                        cmbTinhtrang.SelectedValue = a.MaTT;
                        if (a.BNTXG == null)
                            cmbLaynhiemtu.SelectedItem = null;
                        else
                            cmbLaynhiemtu.SelectedValue = a.BNTXG;
                        if (a.GhiChu != null)
                            txtGhichu.Text = a.GhiChu;
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        private bool CheckData()
        {
            if (txtMaBN.Text == "" && txtTenBN.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin bệnh nhân!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (txtMaBN.TextLength != 6)
            {
                MessageBox.Show("Mã bệnh nhân phải có 6 ký tự!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (txtMaBN.Text == cmbLaynhiemtu.SelectedValue.ToString())
            {
                MessageBox.Show("Bệnh nhân không thể lây nhiễm từ chính mình!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (CheckData())
                {
                    BenhNhan a = new BenhNhan { MaBN = txtMaBN.Text, TenBN = txtTenBN.Text, GhiChu = txtGhichu.Text, MaTT = int.Parse(cmbTinhtrang.SelectedValue.ToString()), BNTXG = (cmbLaynhiemtu.SelectedItem != null) ? cmbTinhtrang.SelectedValue.ToString() : null };
                    benhNhanDAO.InsertUpdate(a);
                    List<BenhNhan> dsbn = benhNhanDAO.GetAll();
                    BindGrid(dsbn);
                    MessageBox.Show("Cập nhật dữ liệu thành công!", "Thông báo", MessageBoxButtons.OK);
                    FillComboBoxBNTXG(dsbn);
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void truyVếtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 frm2 = new Form2();
            frm2.Show();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult rs = MessageBox.Show("Bạn muốn thoát?", "Thông báo",MessageBoxButtons.YesNo);
            if (rs == DialogResult.Yes)
                this.Close();
        }
    }

}
