using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace THK
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            SetCBBMH();
            setCBBSort();
            dataGridView1.DataSource = CSDL_OOP.Instance.getAllSP();
        }
        public void SetCBBMH()
        {
            cbb_MH.Items.Add(new CBBItem { Value = "0", Text = "All" });
            foreach (MatHang i in CSDL_OOP.Instance.getAllMH())
            {
                cbb_MH.Items.Add(new CBBItem
                {
                    Value = i.ID_MH,
                    Text = i.TenMH
                });
            }
            cbb_MH.SelectedIndex = 0;
        }
        public void setCBBSort()
        {
            for (int i = 0; i < CSDL_OOP.Instance.listName.Length; i++)
            {
                cbb_Sort.Items.Add(new CBBItem
                {
                    Text = CSDL_OOP.Instance.listName[i],
                    Value = i.ToString()
                });
            }
            cbb_Sort.SelectedIndex = 0;
        }
        private void btn_Add_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.d(-1);
            f2.ShowDialog();
            this.Hide();
        }
        private void btn_Edit_Click(object sender, EventArgs e)
        {

            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("Khong con ban ghi nao de edit", "Chu y", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            DataGridViewSelectedRowCollection r = dataGridView1.SelectedRows;
            if (r.Count == 1)
            {
                Form2 f2 = new Form2();
                DataGridViewRow dr = dataGridView1.CurrentRow;
                f2.d(CSDL_OOP.Instance.IndexOf(Convert.ToInt32(dr.Cells["ID_SP"].Value)));
                f2.ShowDialog();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Vui long chon chinh xac hang", "Chu y", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btn_Del_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection r = dataGridView1.SelectedRows;
            if (r.Count == 0)
            {
                MessageBox.Show("Khong con ban ghi!", "Chu y", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            foreach (DataGridViewRow i in r)
            {
                DialogResult d = MessageBox.Show("Ban co muon xoa khong?", "Chu y", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                switch(d)
                {
                    case DialogResult.Yes:
                        CSDL_OOP.Instance.DeleteSPInCSDL(CSDL_OOP.Instance.IndexOf(Convert.ToInt32(i.Cells["ID_SP"].Value)));
                        ReloadDatagrid();
                        break;
                    case DialogResult.No:
                        break;
                }
                
            }
        }
        private void btn_Sort_Click(object sender, EventArgs e)
        {
            List<SP> list = new List<SP>();
            if (((CBBItem)cbb_MH.SelectedItem).Value == "0")
            {
                list = CSDL_OOP.Instance.getAllSP();
            }
            else
            {
                list = CSDL_OOP.Instance.getSPByIDName(((CBBItem)cbb_MH.SelectedItem).Value, "");
            }
            switch (Convert.ToInt32(((CBBItem)cbb_Sort.SelectedItem).Value))
            {
                case 0:
                    CSDL_OOP.Instance.Sort(list, SP.CompareIDSP);
                    break;
                case 1:
                    CSDL_OOP.Instance.Sort(list, SP.CompareTen);
                    break;
                case 2:
                    CSDL_OOP.Instance.Sort(list, SP.CompareTT);
                    break;
                case 3:
                    CSDL_OOP.Instance.Sort(list, SP.CompareNSX);
                    break;
                case 4:
                    CSDL_OOP.Instance.Sort(list, SP.CompareIDMH);
                    break;
            }
            dataGridView1.DataSource = list;
        }
        private void cbb_MH_SelectedIndexChanged(object sender, EventArgs e)
        {
            ReloadDatagrid();
        }
        private void tb_Search_TextChanged(object sender, EventArgs e)
        {
            if (tb_Search.Text == "") ReloadDatagrid();
            else if (((CBBItem)cbb_MH.SelectedItem).Value == "0")
            {
                dataGridView1.DataSource = CSDL_OOP.Instance.getSPByName(tb_Search.Text);
                dataGridView1.Refresh();
            }
            else
            {
                dataGridView1.DataSource = CSDL_OOP.Instance.getSPByIDName(((CBBItem)cbb_MH.SelectedItem).Value, tb_Search.Text);
                dataGridView1.Refresh();
            }
        }
        public void ReloadDatagrid()
        {
            if (((CBBItem)cbb_MH.SelectedItem).Value == "0")
            {
                dataGridView1.DataSource = CSDL_OOP.Instance.getAllSP();
            }
            else
            {
                dataGridView1.DataSource = CSDL_OOP.Instance.getSPByIDName(((CBBItem)cbb_MH.SelectedItem).Value, "");
            }
            dataGridView1.Refresh();
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
