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
    public partial class orderInfo : Form
    {
        int intCount;
        int intCheckButton;
        int intCheck;
        public orderInfo()
        {
            InitializeComponent();
        }
        private void countRecord()
        {
            intCount = this.ordersTableAdapter.Fill(this.fashion_shopDataSet.Orders);
            lblCount.Text = intCount.ToString();
        }
        private void ordersBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.ordersBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.fashion_shopDataSet);

        }

        private void orderInfo_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'fashion_shopDataSet.OrderDetails' table. You can move, or remove it, as needed.
            this.orderDetailsTableAdapter.Fill(this.fashion_shopDataSet.OrderDetails);
            // TODO: This line of code loads data into the 'fashion_shopDataSet.Employees' table. You can move, or remove it, as needed.
            this.employeesTableAdapter.Fill(this.fashion_shopDataSet.Employees);
            // TODO: This line of code loads data into the 'fashion_shopDataSet.Customers' table. You can move, or remove it, as needed.
            this.customersTableAdapter.Fill(this.fashion_shopDataSet.Customers);
            countRecord();

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
           /* string strOrderID;
            int intCount;
            int intOrderID; */
            this.ordersBindingSource.AddNew();
            intCount = Convert.ToInt16(this.ordersTableAdapter.countOrderID());
           /* if (intCount != 0) // order มีข้อมูล
            {
                strOrderID = this.ordersTableAdapter.getOrderID().ToString();
                string[] strCode = strOrderID.Split('O'); //[PO , 000X]
                intOrderID = Convert.ToInt16(strCode[1]);
                intOrderID = intOrderID + 1;
                if (intOrderID > 0 && intOrderID <= 9) // เลข 1 หลัก
                {
                    strOrderID = "PO000" + intOrderID;
                }
                else if (intOrderID > 9 && intOrderID <= 99) // เลข 2 หลัก
                {
                    strOrderID = "PO00" + intOrderID;
                }
                else if (intOrderID > 99 && intOrderID <= 999) // เลข 3 หลัก
                {
                    strOrderID = "PO0" + intOrderID;
                }
                else if (intOrderID > 999) // เลข 3 หลัก
                {
                    strOrderID = "PO" + intOrderID;
                }
            }
            else
            {
                strOrderID = "PO0001"; //กำหนดค่า orderID
            }
            orderIDTextBox.Text = strOrderID;*/
            
            totalAmountTextBox.Text = "0";
            customerIDTextBox.Clear();
            customerNameTextBox.Clear();
            employeeIDTextBox.Clear();
            employeeNameTextBox.Clear();
            intCheckButton = 1;
            }

        

        private void btnDel_Click(object sender, EventArgs e)
        {
            DialogResult dlrResult;
            dlrResult = MessageBox.Show("ต้องการลบข้อมูลหรือไม่","แจ้งเตือน",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if(dlrResult == DialogResult.Yes) //ยืนยันการลบข้อมูล
            {
                int intCountDetail;
                intCountDetail = Convert.ToInt16(this.orderDetailsTableAdapter.countOrderDetail(Convert.ToInt16(orderIDTextBox.Text)));
                if (intCountDetail == 0) //ไม่มี orderDetail ของ orderID นี้
                {
                    this.ordersBindingSource.RemoveCurrent();
                    btnSave_Click(sender, e);
                }
                else //มี orderDetail ของ orderID นี้
                {
                    MessageBox.Show("ไม่สามารถลบการสั่งซื้อนี้ได้ เนื่องจากยังมีรายการสั่งซื้ออยู่",
                        "แจ้งเตือน", 
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            
            if (intCheckButton == 0) //แก้ไขข้อมูล แล้วบันทึก
            {
                this.Validate();
                this.ordersBindingSource.EndEdit();
                this.ordersTableAdapter.Update(this.fashion_shopDataSet.Orders);
                countRecord();
            }
            else //เพิ่มข้อมูลแล้วบันทึก
            {
                int intCheck;
                intCheck = Convert.ToInt16(this.ordersTableAdapter.checkOrderID(Convert.ToInt16(orderIDTextBox.Text)));
               
                if(intCheck == 0) //ไม่พบข้อมูล orderID
                {
                    this.Validate();
                    this.ordersBindingSource.EndEdit();
                    this.ordersTableAdapter.Update(this.fashion_shopDataSet.Orders);
                    countRecord();
                }
                else
                {
                    MessageBox.Show("ไม่สามารถบันทึกข้อมูลที่มีรหัสคำสั่งซื้อซ้ำได้",
                        "แจ้งเตือน",
                        MessageBoxButtons.OK, 
                        MessageBoxIcon.Warning);
                }
                intCheckButton = 0;
            }
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            this.ordersBindingSource.MoveFirst();
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            this.ordersBindingSource.MovePrevious();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            this.ordersBindingSource.MoveNext();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            this.ordersBindingSource.MoveLast();
        }

        private void btnBrowseCustomer_Click(object sender, EventArgs e)
        {
            browseCustomer frmbrowseCustomer = new browseCustomer();
            frmbrowseCustomer.ShowDialog();
            customerIDTextBox.Text = frmbrowseCustomer.customerIDTextBox.Text;
            customerNameTextBox.Text = frmbrowseCustomer.customerNameTextBox.Text;
        }

        private void btnBrowseEmployee_Click(object sender, EventArgs e)
        {
            browseEmployee frmbrowseEmployee = new browseEmployee();
            frmbrowseEmployee.ShowDialog();
            employeeIDTextBox.Text = frmbrowseEmployee.employeeIDTextBox.Text;
            employeeNameTextBox.Text = frmbrowseEmployee.employeeNameTextBox.Text;

        }

        private void customerIDTextBox_TextChanged(object sender, EventArgs e)
        {

            if (customerIDTextBox.Text != "" && int.TryParse((customerIDTextBox.Text), out intCheck))
            {
                customerNameTextBox.Text = this.customersTableAdapter.getCustomerName(Convert.ToInt16(customerIDTextBox.Text));
            }
            else
            {
                customerNameTextBox.Clear();
            }
        }

        private void employeeIDTextBox_TextChanged(object sender, EventArgs e)
        {

            if (employeeIDTextBox.Text != "" && int.TryParse((employeeIDTextBox.Text), out intCheck))
            {
                employeeNameTextBox.Text = this.employeesTableAdapter.getEmployeeName(Convert.ToInt16(employeeIDTextBox.Text));
            }
            else
            {
                employeeNameTextBox.Clear();
            }
        }

        private void btnOrdrDetail_Click(object sender, EventArgs e)
        {
            orderDetail frmOrderDetail = new orderDetail();
            frmOrderDetail.Show();
            frmOrderDetail.Activate();
            frmOrderDetail.txtOrderID.Text = orderIDTextBox.Text;
           
        }

        private void btnBill_Click(object sender, EventArgs e)
        {
            frmOrderBill frmOrderBill = new frmOrderBill();
            //ส่งค่า orderid ไปที่ report
            frmOrderBill.txtOrderID.Text= orderIDTextBox.Text;
            frmOrderBill.Show();
            frmOrderBill.Activate();
            frmOrderBill.Show();
        }
    }
}
