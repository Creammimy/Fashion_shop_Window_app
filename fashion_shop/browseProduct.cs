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
    public partial class browseProduct : Form
    {
        int intCount;
        public browseProduct()
        {
            InitializeComponent();
        }

        private void countRecord()
        {
            intCount = this.productsTableAdapter.Fill(this.fashion_shopDataSet.Products);
            lblCount.Text = intCount.ToString();
        }

        private void productsBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.productsBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.fashion_shopDataSet);

        }

        private void browseProduct_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'fashion_shopDataSet.Products' table. You can move, or remove it, as needed.
            this.productsTableAdapter.Fill(this.fashion_shopDataSet.Products);

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            int MyInt;
            if (txtKeyword.Text != "") //ถ้า keyword ไม่เท่ากับค่าว่าง
            {
                if (rbdID.Checked == true) //ค้นหาด้วยรหัส
                {
                    if (int.TryParse(txtKeyword.Text, out MyInt)) //ถ้ากรอกเป็นตัวเลข
                    {
                        intCount = this.productsTableAdapter.
                            FillByProductID(this.fashion_shopDataSet.Products, Convert.ToInt16(txtKeyword.Text));
                        lblCount.Text = intCount.ToString();
                        if (intCount == 0) //ไม่พบข้อมูล
                        {
                            MessageBox.Show("ไม่พบข้อมูล",
                            "แจ้งเตือน",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        }
                    }
                    else //ถ้าไม่กรอกเป็นตัวเลข
                    {
                        MessageBox.Show("กรุณากรอกรหัสเป็นตัวเลข",
                            "แจ้งเตือน",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                    }

                }
                else if (rdbName.Checked == true) //ค้นหาด้วยชื่อ
                {
                    string strKeyword;
                    strKeyword = "%" + txtKeyword.Text + "%"; //%ค่าที่กรอก%
                    intCount = this.productsTableAdapter.FillByProductName(this.fashion_shopDataSet.Products, strKeyword);
                    lblCount.Text = intCount.ToString();
                    if (intCount == 0) //ไม่พบข้อมูล
                    {
                        MessageBox.Show("ไม่พบข้อมูล",
                        "แจ้งเตือน",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    }

                }
                else if (rbdCategoryName.Checked == true)
                {
                    string strKeyword;
                    strKeyword = "%" + txtKeyword.Text + "%"; //%ค่าที่กรอก%
                    intCount = this.productsTableAdapter.FillByCategoryname(this.fashion_shopDataSet.Products, strKeyword);
                    lblCount.Text = intCount.ToString();
                    if (intCount == 0) //ไม่พบข้อมูล
                    {
                        MessageBox.Show("ไม่พบข้อมูล",
                        "แจ้งเตือน",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    }
                }
                else   //ไม่ติ๊กอะไรเลย
                {
                    MessageBox.Show("กรุณาระบุเงื่อนไขในการค้นหา",
                        "แจ้งเตือน",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }
            else //ถ้า keyword  =  ค่าว่าง
            {
                MessageBox.Show("กรุณากรอกคีเวิร์ดที่ต้องการค้นหา",
                    "แจ้งเตือน",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtKeyword.Text = "";
            countRecord();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
