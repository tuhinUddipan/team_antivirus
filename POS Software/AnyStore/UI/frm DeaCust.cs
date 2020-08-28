﻿using AnyStore.BLL;
using AnyStore.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnyStore.UI
{
    public partial class frm_DeaCust : Form
    {
        public frm_DeaCust()
        {
            InitializeComponent();
        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        DeaCustBLL dc = new DeaCustBLL();
        DeaCustDAL dcDal = new DeaCustDAL();
        userDAL uDal = new userDAL();

        private void btnAdd_Click(object sender, EventArgs e)
        {
            dc.type = cmbDeaCust.Text;
            dc.name = txtName.Text;
            dc.email = txtEmail.Text;
            dc.contact = txtContact.Text;
            dc.address = txtAddress.Text;
            dc.added_date = DateTime.Now;

            string loggedUser = frmLogin.loggedIn;
            userBLL user = uDal.GetIdFromUsername(loggedUser);
            dc.added_by = user.id;

            bool success = dcDal.Insert(dc);

            if (success == true)
            {
                MessageBox.Show("Added Successfully");
                Clear();

                DataTable dt = dcDal.Select();
                dgvDeaCust.DataSource = dt;
            }
            else
            {
                MessageBox.Show("Failed to Added");
            }
        }
        public void Clear()
        {
            txtDeaCustID.Text = "";
            txtName.Text = "";
            txtEmail.Text = "";
            txtContact.Text = "";
            txtAddress.Text = "";
            txtSearch.Text = "";

        }

        private void frm_DeaCust_Load(object sender, EventArgs e)
        {
            DataTable dt = dcDal.Select();
            dgvDeaCust.DataSource = dt;
        }

        private void dgvDeaCust_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int RowIndex = e.RowIndex;
            txtDeaCustID.Text = dgvDeaCust.Rows[RowIndex].Cells[0].Value.ToString();
            cmbDeaCust.Text = dgvDeaCust.Rows[RowIndex].Cells[1].Value.ToString();
            txtName.Text = dgvDeaCust.Rows[RowIndex].Cells[2].Value.ToString();
            txtEmail.Text = dgvDeaCust.Rows[RowIndex].Cells[3].Value.ToString();
            txtContact.Text = dgvDeaCust.Rows[RowIndex].Cells[4].Value.ToString();
            txtAddress.Text = dgvDeaCust.Rows[RowIndex].Cells[5].Value.ToString();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            dc.id = int.Parse(txtDeaCustID.Text);
            dc.type = cmbDeaCust.Text;
            dc.name = txtName.Text;
            dc.email = txtEmail.Text;
            dc.contact = txtContact.Text;
            dc.address = txtAddress.Text;
            dc.added_date = DateTime.Now;

            string loggedUser = frmLogin.loggedIn;
            userBLL user = uDal.GetIdFromUsername(loggedUser);
            dc.added_by = user.id;

            bool success = dcDal.Update(dc);

            if (success == true)
            {
                MessageBox.Show("Successfully Updated");
                Clear();
                DataTable dt = dcDal.Select();
                dgvDeaCust.DataSource = dt;
            }
            else
            {
                MessageBox.Show("Failed To Updated");
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            dc.id = int.Parse(txtDeaCustID.Text);

            bool success = dcDal.Delete(dc);
            if (success == true)
            {
                MessageBox.Show("Delete successfully");
                Clear();
                DataTable dt = dcDal.Select();
                dgvDeaCust.DataSource = dt;
            }
            else
            {
                MessageBox.Show("Failed To Delete");
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text;

            if (keyword != null)
            {
                DataTable dt = dcDal.Search(keyword);
                dgvDeaCust.DataSource = dt;
            }
            else
            {
                DataTable dt = dcDal.Select();
                dgvDeaCust.DataSource = dt;
            }
        }
    }
}
