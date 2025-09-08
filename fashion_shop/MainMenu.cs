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
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
        }



        private void ขอมลสนคาToolStripMenuItem_Click(object sender, EventArgs e)
        {
            productInfo frmProductInfo = new productInfo();
            frmProductInfo.MdiParent = this;
            frmProductInfo.Show();
            frmProductInfo.Activate();
        }

        private void ขอมลพนกงานToolStripMenuItem_Click(object sender, EventArgs e)
        {
            employeeInfo frmEmployeeInfo = new employeeInfo();
            frmEmployeeInfo.MdiParent = this; 
            frmEmployeeInfo.Show();
            frmEmployeeInfo.Activate();
        }

        private void ออกจากระบบToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ขอมลประเภทสนคาToolStripMenuItem_Click(object sender, EventArgs e)
        {
            categoryInfo frmCategoryInfo = new categoryInfo();
            frmCategoryInfo.MdiParent = this;
            frmCategoryInfo.Show();
            frmCategoryInfo.Activate();
        }

        private void ขอมลผจดจำหนายToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SupplierInfo frmSupplierInfo = new SupplierInfo();
            frmSupplierInfo.MdiParent = this;
            frmSupplierInfo.Show();
            frmSupplierInfo.Activate();
        }

        private void ขอมลลกคาToolStripMenuItem_Click(object sender, EventArgs e)
        {
            customerInfo frmCustomerInfo = new customerInfo();
            frmCustomerInfo.MdiParent = this;
            frmCustomerInfo.Show();
            frmCustomerInfo.Activate();
        }

        private void ขอมลการสงซอToolStripMenuItem_Click(object sender, EventArgs e)
        {
            orderInfo frmOrderInfo = new orderInfo();
            frmOrderInfo.MdiParent = this;
            frmOrderInfo.Show();
            frmOrderInfo.Activate();
        }

        private void รายงานประเภทสนคาToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCategoryInfo frmCategoryInfo = new frmCategoryInfo();
            frmCategoryInfo.MdiParent =this; 
            frmCategoryInfo.Show();
            frmCategoryInfo.Activate();
        }

        private void รายงานขอมลลกคาToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCustomer frmCustomer = new frmCustomer();
            frmCustomer.MdiParent = this;
            frmCustomer.Show();
            frmCustomer.Activate();
        }

        private void รายงานขอมลพนกงานToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmEmployee frmEmployee = new frmEmployee();
            frmEmployee.MdiParent = this;
            frmEmployee.Show();
            frmEmployee.Activate();
        }

        private void รายงานขอมลสนคาToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmProduct frmProduct = new frmProduct();
            frmProduct.MdiParent = this;
            frmProduct.Show();
            frmProduct.Activate();
        }

        private void MainMenu_Load(object sender, EventArgs e)
        {
            DashBoard dashBoard = new DashBoard();
            dashBoard.MdiParent = this;
            dashBoard.Show();
            dashBoard.Activate();
        }

        private void รายงานขอมลสงซอสนคาประจำวนToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmOrderByDate frmOrderByDate = new frmOrderByDate();
            frmOrderByDate.MdiParent = this;
            frmOrderByDate.Show();
            frmOrderByDate.Activate();
        }
    }
}
