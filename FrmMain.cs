using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cshape_design
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }


        private void FrmMain_Load(object sender, EventArgs e)
        {

        }

        private void btn_ser_Click(object sender, EventArgs e)
        {

        }

        private void panelPlanStat_Paint(object sender, PaintEventArgs e)
        {

        }

        private void rb2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label21_Click(object sender, EventArgs e)
        {

        }

        private void label22_Click(object sender, EventArgs e)
        {

        }

        private void toolStripSeparator1ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void dgvPlanRegister_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void picPlanSearch_MouseEnter(object sender, EventArgs e)
        {
            picPlanSearch.Image = (Image)Properties.Resources.ResourceManager.GetObject("计划查询1");
        }

        private void picPlanSearch_MouseLeave(object sender, EventArgs e)
        {
            picPlanSearch.Image = (Image)Properties.Resources.ResourceManager.GetObject("计划查询");
        }
        private void picStat_MouseEnter(object sender, EventArgs e)
        {
            picStat.Image = (Image)Properties.Resources.ResourceManager.GetObject("计划统计1");
        }

        private void picStat_MouseLeave(object sender, EventArgs e)
        {
            picStat.Image = (Image)Properties.Resources.ResourceManager.GetObject("计划查询");
        }
        private void picHisSearch_MouseEnter(object sender, EventArgs e)
        {
            picHisSearch.Image = (Image)Properties.Resources.ResourceManager.GetObject("历史查询1");
        }

        private void picHisSearch_MouseLeave(object sender, EventArgs e)
        {
            picHisSearch.Image = (Image)Properties.Resources.ResourceManager.GetObject("历史查询");
        }
        private void picAddPlan_MouseEnter(object sender, EventArgs e)
        {
            picAddPlan.Image = (Image)Properties.Resources.ResourceManager.GetObject("计划录入1");
        }

        private void picAddPlan_MouseLeave(object sender, EventArgs e)
        {
            picAddPlan.Image = (Image)Properties.Resources.ResourceManager.GetObject("计划录入");
        }
        private void picSet_MouseEnter(object sender, EventArgs e)
        {
            picSet.Image = (Image)Properties.Resources.ResourceManager.GetObject("提醒设置1");
        }

        private void picSet_MouseLeave(object sender, EventArgs e)
        {
            picSet.Image = (Image)Properties.Resources.ResourceManager.GetObject("提醒设置");
        }
        private void picExit_MouseEnter(object sender, EventArgs e)
        {
            picExit.Image = (Image)Properties.Resources.ResourceManager.GetObject("退出1");
        }

        private void picExit_MouseLeave(object sender, EventArgs e)
        {
            picExit.Image = (Image)Properties.Resources.ResourceManager.GetObject("退出");


        }
    }
}
