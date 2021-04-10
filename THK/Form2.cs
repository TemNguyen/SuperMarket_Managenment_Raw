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
    public partial class Form2 : Form
    {
        public delegate void MyDel(int index);
        public MyDel d;
        static int index = 0;
        static bool status = true;
        public Form2()
        {
            d = new MyDel(getIDSP);
            InitializeComponent();
            SetCBBMH();
            rbtn_Con.Checked = true;
        }
        public void SetCBBMH()
        {
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
        public void AddSP()
        {
            SP s = new SP();
            s.ID_SP = Convert.ToInt32(tb_IDSP.Text);
            s.Ten = tb_Ten.Text;
            if (rbtn_Con.Checked == true)
            {
                s.TrangThai = true;
            }
            else
            {
                s.TrangThai = false;
            }
            s.NSX = dateTimePicker1.Value;
            s.ID_MH = ((CBBItem)cbb_MH.SelectedItem).Value;
            CSDL_OOP.Instance.AddSPToCSDL(s);
            status = true;
        }
        public void EditSP()
        {
            SP s = new SP();
            s.ID_SP = Convert.ToInt32(tb_IDSP.Text);
            s.Ten = tb_Ten.Text;
            if (rbtn_Con.Checked == true)
            {
                s.TrangThai = true;
            }
            else
            {
                s.TrangThai = false;
            }
            s.NSX = dateTimePicker1.Value;
            s.ID_MH = ((CBBItem)cbb_MH.SelectedItem).Value;
            CSDL_OOP.Instance.EditStudentInCSDL(s, index);
            status = true;
        }
        private void btn_Ok_Click(object sender, EventArgs e)
        {
            if (index == -1)
            {
                if(tb_IDSP.Text == "" || tb_Ten.Text == "")
                {
                    MessageBox.Show("Vui long nhap du thong tin!", "Chu y", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (CSDL_OOP.Instance.isExist(tb_IDSP.Text))
                {
                    MessageBox.Show("ID San pham " + tb_IDSP.Text + " da ton tai.", "Chu y", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                foreach(char i in tb_IDSP.Text)
                {
                    if (i > '9' || i < '0')
                    {
                        MessageBox.Show("ID san pham chi chua ki tu so!", "Chu y", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                AddSP();
            }
            else
            {
                EditSP();
            }
            if (status == true)
            {
                Dispose();
                Form1 f1 = new Form1();
                f1.ShowDialog();
            }
        }
        public void setSP()
        {
            SP s = CSDL_OOP.Instance.getAllSP()[index];
            tb_IDSP.Text = s.ID_SP.ToString();
            tb_Ten.Text = s.Ten;
            if (s.TrangThai == true)
            {
                rbtn_Con.Checked = true;
            }
            else
            {
                rbtn_Het.Checked = true;
            }
            dateTimePicker1.Value = s.NSX;
            for (int i = 0; i < cbb_MH.Items.Count; i++)
            {
                if (s.ID_MH == ((CBBItem)cbb_MH.Items[i]).Value)
                {
                    cbb_MH.SelectedIndex = i;
                    break;
                }
            }
        }
        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            Dispose();
            Form1 f1 = new Form1();
            f1.ShowDialog();
        }
        public void getIDSP(int index)
        {
            Form2.index = index;
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            if (index != -1)
            {
                tb_IDSP.Enabled = false;
                setSP();
            }
        }
    }
}
