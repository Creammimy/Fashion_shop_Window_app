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
    public partial class frmOrderBill : Form
    {
        public frmOrderBill()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {

        }

        private void frmOrderBill_Load(object sender, EventArgs e)
        {
            rptOrderBill rptOrderBill = new rptOrderBill();
            rptOrderBill.SetParameterValue("orderID",txtOrderID.Text);
            this.crystalReportViewer1.ReportSource = rptOrderBill;
            this.crystalReportViewer1.Refresh();
        }
    }
}
