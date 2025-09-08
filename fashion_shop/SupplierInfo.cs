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
    public partial class SupplierInfo : Form
    {
        int intCount;
        public SupplierInfo()
        {
            InitializeComponent();
        }

        private void countRecord()
        {
            intCount = this.suppliersTableAdapter.Fill(this.fashion_shopDataSet.Suppliers);
            lblCount.Text = intCount.ToString();
        }

        

        private void SupplierInfo_Load(object sender, EventArgs e)
        {
            countRecord();

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            this.suppliersBindingSource.AddNew();
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            DialogResult dlrResult;
            dlrResult = MessageBox.Show("ต้องการลบข้อมูลหรือไม่", "แจ้งเตือน",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button1);
            if (dlrResult == DialogResult.Yes)
            {
                this.suppliersBindingSource.RemoveCurrent(); //ลบข้อมูล
                btnSave_Click(sender, e); //เรียกใช้ฟังก์ชันปุ่ม save

            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.suppliersBindingSource.EndEdit();
            this.suppliersTableAdapter.Update(this.fashion_shopDataSet.Suppliers);
            countRecord();
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            this.suppliersBindingSource.MoveFirst();
        }
        

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            this.suppliersBindingSource.MovePrevious();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            this.suppliersBindingSource.MoveNext();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            this.suppliersBindingSource.MoveLast();
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
                        intCount = this.suppliersTableAdapter.
                            FillBySupplierID(this.fashion_shopDataSet.Suppliers, Convert.ToInt16(txtKeyword.Text));
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
                    intCount = this.suppliersTableAdapter.FillBySupplierName(this.fashion_shopDataSet.Suppliers, strKeyword);
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
    }
}
