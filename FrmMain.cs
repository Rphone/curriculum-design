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

        private void picPlanSearch_Click(object sender, EventArgs e)
        {
            panelHisSearch.Visible = false;
            panelPlanEdit.Visible = false;
            panelPlanSearch.Visible = true;
            panelPlanStat.Visible = false;
            panelSetting.Visible = false;
        }
        private void picHisSearch_Click(object sender, EventArgs e)
        {
            
        }
        private void picSet_MouseClick(object sender, MouseEventArgs e)
        {

            panelHisSearch.Visible = false;
            panelPlanEdit.Visible = false;
            panelPlanSearch.Visible = false;
            panelPlanStat.Visible = false;
            panelSetting.Visible = true;
        }

        private void picAddPlan_Click(object sender, EventArgs e)
        {

            panelHisSearch.Visible = false;
            panelPlanEdit.Visible = true;
            panelPlanSearch.Visible = false;
            panelPlanStat.Visible = false;
            panelSetting.Visible = false;
        }

        private void picHisSearch_Click_1(object sender, EventArgs e)
        {
            panelHisSearch.Visible = true;
            panelPlanEdit.Visible = false;
            panelPlanSearch.Visible = false;
            panelPlanStat.Visible = false;
            panelSetting.Visible = false;
        }

        private void picStat_Click(object sender, EventArgs e)
        {
            panelHisSearch.Visible = false;
            panelPlanEdit.Visible = false;
            panelPlanSearch.Visible = false;
            panelPlanStat.Visible = true;
            panelSetting.Visible = false;
        }

        private void picExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
