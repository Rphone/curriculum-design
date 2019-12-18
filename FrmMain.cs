using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.IO;
using System.Threading;
using System.Collections;

namespace cshape_design
{
    public partial class FrmMain : Form
    {
        public OleDbConnection old = new OleDbConnection(String.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data source={0}", Application.StartupPath + "\\PlanRemind.mdb"));
        List<CalFlag> listSource = new List<CalFlag>
        { new CalFlag { DataValue = "1", DisplayText = "是" }, new CalFlag { DataValue = "0", DisplayText = "否" } };

        public FrmMain()
        {
            InitializeComponent();
            //链接数据
            
        }


        private void FrmMain_Load(object sender, EventArgs e)
        {
            panelHisSearch.Visible = false;
            panelPlanEdit.Visible = false;
            panelPlanSearch.Visible = false;
            panelPlanStat.Visible = false;
            panelSetting.Visible = false;

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
        /*
         * 以下代码实现图标图片在鼠标上面时的图片的变换
         */
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
            picStat.Image = (Image)Properties.Resources.ResourceManager.GetObject("计划统计");
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
        /*
         *以下代码为点击图标时面板的切换的事件代码
         *
         */

        private void picPlanSearch_Click(object sender, EventArgs e)
        {
            panelHisSearch.Visible = false;
            panelPlanEdit.Visible = false;
            panelPlanSearch.Visible = true;
            panelPlanStat.Visible = false;
            panelSetting.Visible = false;
            panWelcome.Visible = false;
            //DataGridView中绑定数据
            DoFlag1.ConvertValueToText("DataValue", "DisplayText", listSource);//转换为是否
            chbDays.Checked = true;
            txb_Key.Clear();
            OleDbDataAdapter oleda = new OleDbDataAdapter("Select Days from tb_CueSetting",old);
            DataTable dt = new DataTable();//创建datatable实例,表示内存中的一个表
            oleda.Fill(dt);//数据填充进入dt实例表中
            txbPreDay.Text = Convert.ToString(dt.Rows[0][0]);//设置默认提前天数
            btn_ser_Click(sender, e);//执行查询事件按钮
        }
        private void btn_ser_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder("select * from tb_Plan where ");//创建SQl语句
            if(chbDays.Checked)
            {
                if(String.IsNullOrEmpty(txbPreDay.Text.Trim()))//如果天数为空
                {
                    MessageBox.Show("天数不能为空", "提示");
                    return;
                }
                string strSql = "(format(ExecuteTime,'yyyy-mm-dd') >= '" +
                DateTime.Today.ToString("yyyy-MM-dd") +
                "' and format(ExecuteTime,'yyyy-mm-dd') <= '" +
                DateTime.Today.AddDays(Convert.ToInt32(txbPreDay.Text)).ToString("yyyy-MM-dd") + "')";             //过滤提前天数符合查询条件的数据       
                sb.Append(strSql);//连接查询字符串 
            }
            else
            {
                string strContentSql = " PlanContent like '%" + txb_Key.Text.Trim() + "%'";//过滤符合查询条 件的计划内容   
                sb.Append(strContentSql);//连接查询字符串 
            }
            DoFlag1.ConvertValueToText("DataValue", "DisplayText", listSource);
            OleDbDataAdapter oleDa = new OleDbDataAdapter(sb.ToString(), old);//创建OleDbDataAdapter实例 
            DataTable dt = new DataTable();//
            oleDa.Fill(dt);//把数据填充到DataTable实例中 
            dgvPlanSearch.DataSource = dt;//DataGridView控件绑定数据源 
            dgvPlanSearch.AltColor(Color.LightYellow);
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
            panWelcome.Visible = false;
        }

        private void picAddPlan_Click(object sender, EventArgs e)
        {

            panelHisSearch.Visible = false;
            panelPlanEdit.Visible = true;
            panelPlanSearch.Visible = false;
            panelPlanStat.Visible = false;
            panelSetting.Visible = false;
            panWelcome.Visible = false;
        }

        private void picHisSearch_Click_1(object sender, EventArgs e)
        {
            panelHisSearch.Visible = true;
            panelPlanEdit.Visible = false;
            panelPlanSearch.Visible = false;
            panelPlanStat.Visible = false;
            panelSetting.Visible = false;
            panWelcome.Visible = false;
        }

        private void picStat_Click(object sender, EventArgs e)
        {
            panelHisSearch.Visible = false;
            panelPlanEdit.Visible = false;
            panelPlanSearch.Visible = false;
            panelPlanStat.Visible = true;
            panelSetting.Visible = false;
            panWelcome.Visible = false;
        }

        private void picExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void tsmiExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void checkcode_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/Rphone/curriculum-design");
        }
    }
        

}
