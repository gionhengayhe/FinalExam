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
using BUS;

namespace GUI
{
    public partial class Form2 : Form
    {
        private readonly BenhNhanDAO benhNhanDAO = new BenhNhanDAO();
        private readonly TinhTrangDAO tinhTrangDAO = new TinhTrangDAO();
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            try
            {
                var dsbn = benhNhanDAO.GetAll();
                FillComboBox(dsbn);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    

        private void BindGrid(List<BenhNhan> dsbn)
        {
            dgvCaBenh.Rows.Clear();
            foreach(var item in dsbn)
            {
                int i = dgvCaBenh.Rows.Add();
                if (item != null)
                {
                    dgvCaBenh.Rows[i].Cells[0].Value = item.MaBN;
                    dgvCaBenh.Rows[i].Cells[1].Value = item.TenBN;
                    dgvCaBenh.Rows[i].Cells[2].Value = item.TinhTrang.TenTT;
                    dgvCaBenh.Rows[i].Cells[3].Value = "F" + benhNhanDAO.TinhF(item.MaBN).ToString();
                }
            }
        }

        private void FillComboBox(List<BenhNhan> dsbn)
        {
            cmbMaBN.DataSource = dsbn;
            cmbMaBN.ValueMember = "MaBN";
            cmbMaBN.DisplayMember = "Display";
        }

        private void cmbMaBN_SelectedIndexChanged(object sender, EventArgs e)
        {
            BenhNhan benhNhan = cmbMaBN.SelectedItem as BenhNhan;
            if (benhNhan != null)
            {
                var dsbn = benhNhanDAO.TruyVet(benhNhan.MaBN);
                BindGrid(dsbn);
            }
        }
    }
}
