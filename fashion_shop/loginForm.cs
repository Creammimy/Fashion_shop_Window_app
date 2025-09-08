using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace fashion_shop
{
    public partial class loginForm : Form
    {
        public loginForm()
        {
            InitializeComponent();
        }

        private void employeesBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.employeesBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.fashion_shopDataSet);

        }

        private void loginForm_Load(object sender, EventArgs e)
        {
           

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult dlrResult;
            dlrResult = MessageBox.Show("ต้องการออกจากโปรแกรมหรือไม่","แจ้งเตือน",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if (dlrResult == DialogResult.Yes)
            {
                this.Close(); 
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            int intCheck;
            if (txtUserName.Text !="" && txtPassword.Text != "")
            {
                intCheck = Convert.ToInt32(this.employeesTableAdapter.checkLogin(txtUserName.Text, txtPassword.Text));
                if (intCheck != 0) 
                {
                    Form frmMaminMenu = new MainMenu();
                    frmMaminMenu.Show();
                    frmMaminMenu.Activate();
                    this.Hide(); //ซ่อนฟอร์ม login
                }
                else //ไม่พบ username กับ password
                {
                    MessageBox.Show("ชื่อผู้ใช้หรือรหัสผ่านไม่ถูกต้อง", 
                        "แจ้งเตือน", MessageBoxButtons.OK, 
                        MessageBoxIcon.Warning);
                }
            }
            else //ไม่มีค่า username
            {
                MessageBox.Show("กรูณากรอก username และ password", 
                    "แจ้งเตือน", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Warning);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
    }
}
