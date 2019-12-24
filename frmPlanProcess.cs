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

namespace cshape_design
{
    public partial class fromPlanProcess : Form
    {
        int intIndivNum;
        OleDbConnection oleCon = null;//空的数据库连接引用
        public fromPlanProcess()
        {
            InitializeComponent();
        }
        private void fromPlanProcess_Load(object sender, EventArgs e)
        {
            FrmMain fm = (FrmMain)this.Owner;
            intIndivNum = Convert.ToInt32(fm.dgvPlanSearch.CurrentRow.Cells["IndivNum"].Value);
            oleCon = fm.old;
            OleDbCommand oldcom = new OleDbCommand("SELECT PlanTitle,DoFlag,Explain FROM tb_Plan where IndivNum = "
                                    + intIndivNum, oleCon);

            if(oleCon.State!=ConnectionState.Open)
            {
                oleCon.Open();
            }
            OleDbDataReader oleDr = oldcom.ExecuteReader();

            oleDr.Read();
            if(oleDr.HasRows)
            {
                
                txtTile.Text = Convert.ToString(oleDr["PlanTitle"]);
                chbComplete.CheckState = Convert.ToString(oleDr["DoFlag"]) == "1" ? CheckState.Checked : CheckState.Unchecked;
                richTextBox1.Text = Convert.ToString(oleDr["Explain"]);
            }
            oleCon.Close();//关闭数据库的链接
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            string strDoFlag = String.Empty;//定义描述计划执行的标记 
            if(chbComplete.CheckState==CheckState.Checked)
            {
                strDoFlag = "1";
            }
            else
            {
                strDoFlag = "0";
            }
            string strSql = "Update tb_Plan set DoFlag = '" + strDoFlag + "',Explain='" + richTextBox1.Text
                + "' where IndivNum = " + intIndivNum;//修改处理信息 
            OleDbCommand oleCmd = new OleDbCommand(strSql, oleCon);//创建命令对象 
            if (oleCon.State != ConnectionState.Open)//若连接未打开 
            {
                oleCon.Open();
            }
            if (oleCmd.ExecuteNonQuery() > 0)//执行SQL语句 
            {

                MessageBox.Show("完成！", "软件提示");//提示完成 
                FrmMain frm = (FrmMain)this.Owner;
                frm.chbDays.Checked = false;
                frm.btn_ser_Click(sender, e);

            }
            else
            {

                MessageBox.Show("失败！", "软件提示");//提示还未完成 
            }
            oleCon.Close();//关闭连接             
            this.Close();//关闭当前窗体 
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }
        
        private void btnExit_Click(object sender, EventArgs e)
        {
            
            this.Close();
        }

        
    }
}
