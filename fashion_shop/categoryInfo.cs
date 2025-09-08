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
    public partial class categoryInfo: Form
    {
        int intCount;

        public categoryInfo()
        {
            InitializeComponent();
        }

        private void countRecord()
        {
            intCount = this.categoriesTableAdapter.Fill(this.fashion_shopDataSet.Categories);
            lblCount.Text = intCount.ToString();
        }


        private void categoryInfo_Load(object sender, EventArgs e)
        {
           countRecord();

        }

        private void categoryInfo_Leave(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            this.categoriesBindingSource.AddNew();

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.categoriesBindingSource.EndEdit();
            this.categoriesTableAdapter.Update(this.fashion_shopDataSet.Categories);
            countRecord();
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
                this.categoriesBindingSource.RemoveCurrent(); //ลบข้อมูล
                btnSave_Click(sender, e); //เรียกใช้ฟังก์ชันปุ่ม save

            }
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            this.categoriesBindingSource.MoveFirst();
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            this.categoriesBindingSource.MovePrevious();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            this.categoriesBindingSource.MoveNext();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            this.categoriesBindingSource.MoveLast();
        }

        private void label2_Click(object sender, EventArgs e)
        {

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
                        intCount = this.categoriesTableAdapter.
                            FillByCategoryID(this.fashion_shopDataSet.Categories, Convert.ToInt16(txtKeyword.Text));
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

                }else if (rdbName.Checked == true) //ค้นหาด้วยชื่อ
                {
                    string strKeyword;
                    strKeyword = "%" + txtKeyword.Text + "%"; //%ค่าที่กรอก%
                    intCount = this.categoriesTableAdapter.FillByCategoryName(this.fashion_shopDataSet.Categories , strKeyword);
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
                        "แจ้งเตือน" , 
                        MessageBoxButtons.OK, 
                        MessageBoxIcon.Warning);
                }
            }
            else //ถ้า keyword  =  ค่าว่าง
            {
                MessageBox.Show("กรุณากรอกคีเวิร์ดที่ต้องการค้นหา", 
                    "แจ้งเตือน" ,
                    MessageBoxButtons.OK , 
                    MessageBoxIcon.Warning);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtKeyword.Text = "";
            countRecord();
        }

        private void rbdID_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
