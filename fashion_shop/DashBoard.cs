using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace fashion_shop
{
    public partial class DashBoard : Form
    {
        public DashBoard()
        {
            InitializeComponent();
        }

        private void DashBoard_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'fashion_shopDataSet.Products' table. You can move, or remove it, as needed.
            this.productsTableAdapter.Fill(this.fashion_shopDataSet.Products);

            lblEmploee.Text = this.employeesTableAdapter.countEmployee().ToString();
            lblCustomer.Text = this.customersTableAdapter.countCustomer().ToString();
            lblOrder.Text = this.ordersTableAdapter.countOrder().ToString();
            lblPrice.Text = this.ordersTableAdapter.getTotalMonth().ToString();

            lbltop1.Text = this.orderDetailsTableAdapter.top1().ToString();
            lbltop2.Text = this.orderDetailsTableAdapter.top2();
            lbltop3.Text = this.orderDetailsTableAdapter.top3();

            dataGridView1.DataSource = this.productsTableAdapter.GetDataBy3();

            // TODO: This line of code loads data into the 'fashion_shopDataSet.Orders' table. You can move, or remove it, as needed.
            this.ordersTableAdapter.FillByOrderYear(this.fashion_shopDataSet.Orders);
            chart1.Series.Clear();

            Series series = new Series("ยอดขายรายเดือน");
            series.ChartType = SeriesChartType.Column;
            series.XValueType = ChartValueType.String;

            foreach (DataRow row in fashion_shopDataSet.Orders.Rows)
            {
                // เช็ค DBNull ก่อนแปลง
                int year = row["Year"] != DBNull.Value ? Convert.ToInt32(row["Year"]) : 0;
                int month = row["Month"] != DBNull.Value ? Convert.ToInt32(row["Month"]) : 0;
                decimal sales = row["TotalAmount"] != DBNull.Value ? Convert.ToDecimal(row["TotalAmount"]) : 0;

                string monthLabel = $"{year}-{month:D2}";
                series.Points.AddXY(monthLabel, sales);
            }


            // ใส่ Series ลง Chart
            chart1.Series.Add(series);

            // ปรับแกน X ให้แสดงทุกเดือน
            chart1.ChartAreas[0].AxisX.Interval = 1;
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {

        }

        private void lblPrice_Click(object sender, EventArgs e)
        {

        }
    }
}
