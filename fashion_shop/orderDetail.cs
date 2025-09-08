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
    public partial class orderDetail : Form
    {
        int intCount;
        public orderDetail()
        {
            InitializeComponent();
        }

        private void countRecord()
        {
            intCount = this.orderDetailsTableAdapter.FillByOrderID(this.fashion_shopDataSet.OrderDetails, Convert.ToInt32(txtOrderID.Text));
            lblCount.Text = intCount.ToString();
        }

        private void displayTotalPrice()
        {
            Double dblTotalPrice;
            dblTotalPrice = Convert.ToDouble(this.orderDetailsTableAdapter.sumTotalPrice(Convert.ToInt32(txtOrderID.Text)));
            lblTotalPrice.Text = dblTotalPrice.ToString();
        }
        private void setStock()
        {
            int intStock, intCount, intNewStock;
            //รับค่า stockของสินค้า
            intStock = Convert.ToInt32(txtStock.Text);
            // รับค่าจำนวนสินค้า
            intCount = Convert.ToInt32(quantityTextBox.Text);

            //จำนวนสินค้าใหม่
            intNewStock = intStock - intCount;


            txtStock.Text = intNewStock.ToString(); // แสดงผลที่หน้าฟอร์ม

            this.productsTableAdapter.updateStockQuantity(Convert.ToInt32(txtStock.Text), Convert.ToInt32(productIDTextBox.Text));
        }
        

        private void orderDetail_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'fashion_shopDataSet.Products' table. You can move, or remove it, as needed.
            this.productsTableAdapter.Fill(this.fashion_shopDataSet.Products);
            // TODO: This line of code loads data into the 'fashion_shopDataSet.Orders' table. You can move, or remove it, as needed.
            this.ordersTableAdapter.Fill(this.fashion_shopDataSet.Orders);
            // TODO: This line of code loads data into the 'fashion_shopDataSet.OrderDetails' table. You can move, or remove it, as needed.
            this.orderDetailsTableAdapter.Fill2(this.fashion_shopDataSet.OrderDetails);
            btnSave.Enabled = false; //ปิดปุ่มเซฟไม่ให้ใช้งาน

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            int intOrderDetail;
            this.orderDetailsBindingSource.AddNew();

            intOrderDetail = Convert.ToInt32(this.orderDetailsTableAdapter.getOrderDetailID());
            orderDetailIDTextBox.Text = (intOrderDetail+1).ToString();
            orderIDTextBox.Text = txtOrderID.Text;
            btnSave.Enabled = true;
        }

        private void btnBrowseProduct_Click(object sender, EventArgs e)
        {
            browseProduct frmBrowseProduct = new browseProduct();
            frmBrowseProduct.ShowDialog();
            productIDTextBox.Text = frmBrowseProduct.productIDTextBox.Text;
            productNameTextBox.Text = frmBrowseProduct.productNameTextBox.Text;
            unitPriceTextBox.Text = frmBrowseProduct.unitPriceTextBox.Text;
            txtStock.Text = frmBrowseProduct.stockQuantityTextBox.Text;
            frmBrowseProduct.Activate();
        }

        private void quantityTextBox_TextChanged(object sender, EventArgs e)
        {
            // จำนวนและราคาต้องไม่เป็นค่าว่าง
            if (quantityTextBox.Text != "" && unitPriceTextBox.Text != "")
            {
                int intValue;
                int intStock;
                intStock = Convert.ToInt32(txtStock.Text);
                // จำนวนต้องเป็นตัวเลขเท่านั้น
                if (int.TryParse(quantityTextBox.Text, out intValue))
                {
                   
                    //คำนวนหาค่าราคา
                    int intCount;
                    double dblPrice, dblProductPrice;

                    intCount = Convert.ToInt32(quantityTextBox.Text);
                    if (intCount > intStock)
                    {
                        MessageBox.Show("ไม่สามารถใส่จำนวนสินค้ามากกว่าจำนวนสินค้าที่คงเหลืออยู่ในสต็อก", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        quantityTextBox.Clear();
                        totalPriceTextBox.Clear();
                        quantityTextBox.Focus();
                    }
                    else
                    {
                        dblPrice = Convert.ToDouble(unitPriceTextBox.Text);
                        dblProductPrice = intCount * dblPrice;
                        totalPriceTextBox.Text = dblProductPrice.ToString();
                    }
                }
                else
                {
                    MessageBox.Show("จำนวนสินค้าต้องเป็นตัวเลขเท่านั้น",
                        "แจ้งเตือน",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    quantityTextBox.Clear();
                    quantityTextBox.Focus();
                }
               
            }
        }

        private void txtOrderID_TextChanged(object sender, EventArgs e)
        {
            countRecord();
            displayTotalPrice();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.orderDetailsBindingSource.EndEdit();
            this.orderDetailsTableAdapter.Update(this.fashion_shopDataSet.OrderDetails);
            // แสดง totalPrice
            displayTotalPrice();

            //update totalprice ที่ orderinfo
            this.ordersTableAdapter.updateTotalPrice(Convert.ToDecimal(lblTotalPrice.Text), Convert.ToInt32(txtOrderID.Text));

            //อัพเดตจำนวนสินค้าที่ productinfo
            setStock();
            countRecord();
            btnSave.Enabled = false;
        }

        private void orderIDTextBox_TextChanged(object sender, EventArgs e)
        {
            //ถ้ามีค่า productID
            if(productIDTextBox.Text != "")
            {
                productNameTextBox.Text = this.productsTableAdapter.getProductName(Convert.ToInt32(productIDTextBox.Text));
                txtStock.Text = this.productsTableAdapter.getStcokQuantity(Convert.ToInt16(productIDTextBox.Text)).ToString();
            }
            else //ถ้าไม่มี
            {
                productNameTextBox.Clear();
                unitPriceTextBox.Clear();
                txtStock.Clear();
            }
            
            
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            DialogResult dlrResult;
            dlrResult = MessageBox.Show("ต้องการลบข้อมูลหรือไม่", "แจ้งเตือน", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlrResult == DialogResult.Yes)
            {
                int intStock, intCount, intNewStock;
                intStock = Convert.ToInt32(txtStock.Text);
                // รับค่าจำนวนสินค้า
                intCount = Convert.ToInt32(quantityTextBox.Text);

                //จำนวนสินค้าใหม่
                intNewStock = intStock + intCount;
                txtStock.Text = intNewStock.ToString();

                this.productsTableAdapter.updateStockQuantity(Convert.ToInt32(txtStock.Text), Convert.ToInt32(productIDTextBox.Text));
                this.orderDetailsBindingSource.RemoveCurrent();
                this.Validate();
                this.orderDetailsBindingSource.EndEdit();
                this.orderDetailsTableAdapter.Update(this.fashion_shopDataSet.OrderDetails);

                countRecord();
                displayTotalPrice();
                this.ordersTableAdapter.updateTotalPrice(Convert.ToDecimal(lblTotalPrice.Text),Convert.ToInt32(orderIDTextBox.Text));
                
                
            }
        }

        private void orderDetail_Leave(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
