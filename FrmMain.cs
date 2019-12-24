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
using System.Xml;
using System.Net;
using System.Net.Http;

namespace cshape_design
{
    public partial class FrmMain : Form
    {
        //double Version = 
        public OleDbConnection old = new OleDbConnection(String.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data source={0}", Application.StartupPath + "\\PlanRemind.mdb"));
        List<CalFlag> listSource = new List<CalFlag>
        { new CalFlag { DataValue = "1", DisplayText = "是" }, new CalFlag { DataValue = "0", DisplayText = "否" } };
        bool isEdit = false;
        OleDbDataAdapter oleda = null;
        public FrmMain()
        {
            InitializeComponent();
           
            
        }


        private void FrmMain_Load(object sender, EventArgs e)
        {
            panelHisSearch.Visible = false;
            panelPlanEdit.Visible = false;
            panelPlanSearch.Visible = false;
            panelPlanStat.Visible = false;
            panelSetting.Visible = false;

            OleDbDataAdapter oleDaLoad = new OleDbDataAdapter("Select IsAutoCheck,Days,IsTimeCue,TimeInterval from tb_CueSetting", old);
            DataTable dt = new DataTable();
            oleDaLoad.Fill(dt);
            StringBuilder sb = new StringBuilder(string.Empty);
            if (Convert.ToBoolean(dt.Rows[0][0]))
            {
                sb.Append("软件启动后将自动检测未来" + dt.Rows[0][1].ToString() + "天内要执行的计划， " + Environment.NewLine);
                //picPlanSearch_Click(sender, e); //触发计划查询图片按钮的Click事件
                DisplayWelcomePanel();
            }
            if (Convert.ToBoolean(dt.Rows[0][2]))
            {
                timer1.Interval = Convert.ToInt32((double)dt.Rows[0][3] * 3600 * 1000); //设置触发频率
                timer1.Enabled = true;                                                  //启动计时器,同timer1.Start()
                sb.Append("软件每隔" + dt.Rows[0][3].ToString() + "小时会自动提醒一次！");
            }
            else
            {
                timer1.Enabled = false; //禁用计时器,同timer1.Stop()
            }
            if (sb.ToString() != string.Empty)
            {
                MessageBox.Show(sb.ToString(), "启动提示");
            }

        }


        private void DisplayWelcomePanel()
        {
            panelHisSearch.Visible = false;
            panelPlanEdit.Visible = false;
            panelPlanSearch.Visible = false;
            panelPlanStat.Visible = false;
            panelSetting.Visible = false;
            panWelcome.Visible = true;
        }



       

        private void dgvPlanRegister_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        /*
         * 以下代码实现图标图片在鼠标上面时的图片的变换
         
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
        */
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
            //oleda = new OleDbDataAdapter("Select Days from tb_CueSetting",old);
            oleda = new OleDbDataAdapter("Select Days from tb_CueSetting",old);
            DataTable dt = new DataTable();//创建datatable实例,表示内存中的一个表
            oleda.Fill(dt);//数据填充进入dt实例表中
            txbPreDay.Text = Convert.ToString(dt.Rows[0][0]);//设置默认提前天数
            btn_ser_Click(sender, e);//执行查询事件按钮
        }
        public void btn_ser_Click(object sender, EventArgs e)
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
            oleda = new OleDbDataAdapter(sb.ToString(), old);//创建OleDbDataAdapter实例 
            DataTable dt = new DataTable();//
            oleda.Fill(dt);//把数据填充到DataTable实例中 
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
            oleda = new OleDbDataAdapter("select * from tb_Plan", old);
            DataTable dt = new DataTable();
            oleda.Fill(dt);
            dgvPlanRegister.DataSource = dt;
            dgvPlanRegister.AltColor(Color.LightYellow);

        }

        private void picHisSearch_Click_1(object sender, EventArgs e)
        {
            panelHisSearch.Visible = true;
            panelPlanEdit.Visible = false;
            panelPlanSearch.Visible = false;
            panelPlanStat.Visible = false;
            panelSetting.Visible = false;
            panWelcome.Visible = false;
            cbxYear.Items.Clear();

            for(int i=0;i<10;i++)
            {
                cbxYear.Items.Add(DateTime.Today.Year - i);
            }
            cbxYear.SelectedIndex = 0;
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

        private void button2_Click(object sender, EventArgs e)
        {
            
            XmlDocument remotedoc = new XmlDocument();
            string version = SystemInfo.SofewareVersion;
            //下载远程的版本号文件
            //string url = "https://raw.githubusercontent.com/Rphone/curriculum-design/master/bin/Debug/version.xml";
            //myWebClient.DownloadFile(url, "rversion.xml");
            try
            {
                
                remotedoc.Load("https://raw.githubusercontent.com/Rphone/curriculum-design/master/bin/Debug/version.xml");
                
                //remotedoc.Load("version.xml");
                XmlNode remoteversion = remotedoc.SelectSingleNode("body/version");
                string rv = remoteversion.InnerText;
                string messag = "当前版本 " + version + "\n远程版本 " + remoteversion.InnerText;

                if (rv == version)
                {
                    messag += "\n当前已经是最新版本";
                    MessageBox.Show(messag, "提示");
                   // Thread thread = new Thread(new ThreadStart(this.UpdateThread));
                   // thread.Start();
                }
                else
                {
                    messag += "\n发现新的版本,是否更新?";
                    if (MessageBox.Show(messag, "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        //MessageBox.Show(messag, "提示", MessageBoxButtons.y) == DialogResult.Yes
                        Thread thread = new Thread(new ThreadStart(this.UpdateThread));
                        thread.Start();
                        MessageBox.Show("等待更新完毕后会有窗口提醒!");

                    }
                }


                   








            }
            catch (XmlException Xmlex)
            {
                MessageBox.Show("检查更新失败");

            }
            catch (WebException Webex)
            {
                MessageBox.Show("检查更新失败,可能是网络问题");
            }

        }
        public void UpdateThread()
        {   /*
            WebClient myWebClient = new WebClient();
            myWebClient.Proxy = null;
            
            string url = "https://github.com/Rphone/curriculum-design/raw/master/bin/Debug/cshape%20design.exe";
            myWebClient.DownloadFile(url, "DesktopAssistant.exe");
            MessageBox.Show("更新成功!\n请在该程序所在文件夹寻找新版程序\n并切换到新版程序使用 ");
          
            */
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            System.Net.ServicePointManager.DefaultConnectionLimit = 512;
            string url = "https://github.com/Rphone/curriculum-design/raw/master/bin/Debug/cshape%20design.exe";
            XmlDocument remotedoc = new XmlDocument();
            remotedoc.Load("https://raw.githubusercontent.com/Rphone/curriculum-design/master/bin/Debug/version.xml");
            XmlNode remoteversion = remotedoc.SelectSingleNode("body/version");
            XmlNode remotefilename = remotedoc.SelectSingleNode("body/filename");
            string rv =remotefilename.InnerText + remoteversion.InnerText;
            string filename = rv;
            bool flag = false;
            long startPosition = 0; // 上次下载的文件起始位置
            FileStream writeStream; // 写入本地文件流对象

            // 判断要下载的文件夹是否存在
            if (File.Exists(filename))
            {

                writeStream = File.OpenWrite(filename);             // 存在则打开要下载的文件
                startPosition = writeStream.Length;                  // 获取已经下载的长度
                writeStream.Seek(startPosition, SeekOrigin.Current); // 本地文件写入位置定位
            }
            else
            {
                writeStream = new FileStream(filename, FileMode.Create);// 文件不保存创建一个文件
                startPosition = 0;
            }


            try
            {
                HttpWebRequest myRequest = (HttpWebRequest)HttpWebRequest.Create(url);// 打开网络连接
                myRequest.Proxy = null;
                if (startPosition > 0)
                {
                    myRequest.AddRange((int)startPosition);// 设置Range值,与上面的writeStream.Seek用意相同,是为了定义远程文件读取位置
                }


                Stream readStream = myRequest.GetResponse().GetResponseStream();// 向服务器请求,获得服务器的回应数据流


                byte[] btArray = new byte[8192];// 定义byte数组,向readStream读取内容和向writeStream写入内容
                int contentSize = readStream.Read(btArray, 0, btArray.Length);// 向远程文件读第一次

                while (contentSize > 0)// 如果读取长度大于零则继续读
                {
                    writeStream.Write(btArray, 0, contentSize);// 写入本地文件
                    contentSize = readStream.Read(btArray, 0, btArray.Length);// 继续向远程文件读取
                }

                //关闭流
                writeStream.Close();
                readStream.Close();

                MessageBox.Show("更新成功!\n请在该程序所在文件夹寻找新版程序\n并切换到新版程序使用 ");
            }
            catch (Exception)
            {
                writeStream.Close();
                 MessageBox.Show("下载失败");      //返回false下载失败
            }



        }

        private void chbDays_CheckedChanged(object sender, EventArgs e)
        {
            if (chbDays.Checked)
            {
                txbPreDay.Enabled = true;
                chb.Checked = false;
            }
            else
            {
                txbPreDay.Enabled = false;
            }
        }

        private void chb_CheckedChanged(object sender, EventArgs e)
        {
            if(chb.Checked)
            {
                txb_Key.Enabled = true;
                chbDays.Checked = false;
            }
            else
            {
                txb_Key.Enabled = false;
            }
        }

        private void but_cal_Click(object sender, EventArgs e)
        {
            txbPreDay.Text = "";
            txb_Key.Text = "";
        }

        private void btnSatr_Click(object sender, EventArgs e)
        {
            string strsql = "";

            if(rb1.Checked)
            {
                strsql = "select * from tb_Plan where DoFlag = '1'";
            }
            else
            {
                strsql = "select * from tb_Plan where DoFlag = '0'";
            }
            DoFlag2.ConvertValueToText("DataValue", "DisplayText", listSource);// 值转换  
            oleda = new OleDbDataAdapter(strsql, old); ;//创建Adapter实例
            DataTable dt = new DataTable();//创建DataTable实例
            oleda.Fill(dt);//把数据添加到DataTable实例中
            dataGridView2.DataSource = dt;//DataGridView控件绑定数据源 
            dataGridView2.AltColor(Color.LightYellow);//在DataGridView控件中隔行换色显示记录 

        }

        private void dgvPlanSearch_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvPlanSearch_MouseEnter(object sender, EventArgs e)
        {
            if (dgvPlanSearch.Rows.Count > 0)
                this.toolTip1.SetToolTip(dgvPlanSearch, "双击记录编辑计划");
        }

        private void dgvPlanSearch_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            this.toolTip1.SetToolTip(dgvPlanSearch, "双击记录编辑计划");
        }

        private void dgvPlanSearch_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            fromPlanProcess frmDo = new fromPlanProcess();
            frmDo.Owner = this;
            frmDo.ShowDialog();
        }

        private void btnHisQuery_Click(object sender, EventArgs e)
        {
            DoFlag3.ConvertValueToText("Datavalue", "DisplayText", listSource);
            string strSql = "select * from tb_Plan ";
            strSql += "where year(ExecuteTime) = " + Convert.ToInt32(cbxYear.Text) + " and PlanContent like '%" + txtHisContent.Text.Trim() + "%'";//设置sql语句的过滤条件
            oleda = new OleDbDataAdapter(strSql, old);
            DataTable dt = new DataTable();
            oleda.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.AltColor(Color.LightYellow);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            isEdit = false;
            //激活页面上的控件

            txtPlanTitle.Enabled = true;
            cbxPlanKind.Enabled = true;
            dtpExecuteTime.Enabled = true;
            rtbPlanContent.Enabled = true;

            txtPlanTitle.Text = "";
            cbxPlanKind.Text = "一般计划";
            dtpExecuteTime.Value = DateTime.Today;
            rtbPlanContent.Text = "";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
             string strSql = String.Empty;                           //定义存储SQL语句的字符串
            DataRow dr = null;                                      //定义数据行对象
            DataTable dt = (DataTable)dgvPlanRegister.DataSource; //获取数据源
            


            oleda.FillSchema(dt, SchemaType.Mapped);                //配置指定的数据架构
            string strCue = string.Empty;                           //定义提示字符串
            if (txtPlanTitle.Text.Trim() == string.Empty)
            {
                MessageBox.Show("标题不许为空！"); //提示标题不许为空
                txtPlanTitle.Focus();
                return;
            }
            if (isEdit) //若是修改操作状态
            {
                dr = dt.Rows.Find(dgvPlanRegister.CurrentRow.Cells["IndivNum"].Value); //查找要修改的行
                strCue = "修改";
            }
            else //若是新添加操作状态
            {
                dr = dt.NewRow(); //创建新行
                dt.Rows.Add(dr);  //在数据源中添加新创建的行
                strCue = "添加";
                dr["DoFlag"] = "0";
            }

            //给数据源的各个字段赋值
            dr["PlanTitle"] = txtPlanTitle.Text.Trim();
            dr["PlanKind"] = cbxPlanKind.Text;
            dr["ExecuteTime"] = dtpExecuteTime.Value;
            dr["PlanContent"] = rtbPlanContent.Text;
            OleDbCommandBuilder scb = new OleDbCommandBuilder(oleda); //关联数据库表单命令
            if (oleda.Update(dt) > 0)                                 //更新数据
            {
                MessageBox.Show(strCue + "成功！"); //放空界面
                txtPlanTitle.Text = "";             //清空标题输入框
                cbxPlanKind.Text = "一般计划";      //初始化计划种类
                dtpExecuteTime.Value = DateTime.Today;
                rtbPlanContent.Text = ""; //清空内容
                                          //禁用界面，等待下一次操作
                txtPlanTitle.Enabled = false;
                cbxPlanKind.Enabled = false;
                dtpExecuteTime.Enabled = false;
                rtbPlanContent.Enabled = false;
            }
            else
            {
                MessageBox.Show(strCue + "失败！");
            }
            dt.Clear();
            oleda.Fill(dt); //以助于更新IndivNum列
        }

        private void picSet_Click(object sender, EventArgs e)
        {
            //添加：检索提醒设置数据表,并显示在控件
            OleDbDataAdapter oleDaSet = new OleDbDataAdapter("Select top 1 * from tb_CueSetting", old);
            DataTable dt = new DataTable();
            oleDaSet.Fill(dt);//填充数据
            if(dt.Rows.Count >0)
            {
                DataRow dr = dt.Rows[0];
                nudDays.Value = Convert.ToDecimal(dr["Days"]);//获取提前的天数
                chbAutoCheck.Checked = Convert.ToBoolean(dr["IsAutoCheck"]);
                chbTimecue.Checked = Convert.ToBoolean(dr["IsTimeCue"]);
                nudTimerInterval.Value = Convert.ToDecimal(dr["TimeInterval"]);
            }
        }
        /*
         * 设置确定的按钮的事件代码
         */
        private void btnSetOK_Click(object sender, EventArgs e)
        {
            OleDbCommand olecmd = new OleDbCommand("select top 1 * from tb_CueSetting", old);
            if(old.State!=ConnectionState.Open)
            {
                old.Open();
            }
            OleDbDataReader oledr = olecmd.ExecuteReader();
            string strInsertSql = "INSERT INTO tb_CueSetting VALUES(" + Convert.ToInt32(nudDays.Value) + "," + chbAutoCheck.Checked + "," + chbTimecue.Checked + "," + Convert.ToDouble(nudTimerInterval.Value) + ")";             //定义更新SQL语句             
            string strUpdateSql = "UPDATE tb_CueSetting set Days = " + Convert.ToInt32(nudDays.Value) + ",IsAutoCheck = " + chbAutoCheck.Checked + ",IsTimeCue = " + chbTimecue.Checked + ",TimeInterval = " + Convert.ToDouble(nudTimerInterval.Value);            //获取本次要执行的SQL语句             
            string strSql = oledr.HasRows ? strUpdateSql : strInsertSql;
            oledr.Close();
            olecmd.CommandType = CommandType.Text;//设置命令类型             
            olecmd.CommandText = strSql;//设置SQL语句             
            if (olecmd.ExecuteNonQuery() > 0)//若执行SQL语句成功 
            {
                MessageBox.Show("设置成功！");
                if (chbTimecue.Checked)
                {
                    timer1.Interval = Convert.ToInt32(nudTimerInterval.Value * 3600 * 1000);//设置 Timer控件的触发频率                     
                    timer1.Enabled = true;//启动计时器                 
                }
                else
                {
                    timer1.Enabled = false;//禁用计时器 
                }
            }
            else
            {
                MessageBox.Show("设置失败!");
            }
            old.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //创建一个新的子线程，用于检索和提示数据，以减轻主线程的压力             
            Thread th = new Thread(
                () =>
                {
                    Invoke(
                        (MethodInvoker)(() => 
                        {

                            int intDays;//存储提前天数                             
                            OleDbDataAdapter oleDaTime = new OleDbDataAdapter("Select Days from tb_CueSetting", old);//创建OleDbDataAdapter实例，读取提前天数                             
                            DataTable dt = new DataTable();//创建DataTable实例                             
                            oleDaTime.Fill(dt);//把数据写入DataTable实例中                             
                            intDays = Convert.ToInt32(dt.Rows[0][0]);//获取提前天数                             
                                                                     //读取需要提醒的计划                             
                            StringBuilder sb = new StringBuilder(" Select PlanTitle from tb_Plan Where ");//创建动态字符串 
                            string strSql = " DoFlag = '0' and   (format(ExecuteTime,'yyyy-mm-dd') >= '" + DateTime.Today.ToString("yyyy-MM-dd") 
                            + "' and format(ExecuteTime,'yyyy-mm-dd') <= '" + DateTime.Today.AddDays(intDays).ToString("yyyy-MM-dd") + "')";//过滤日期符合查询条件的记录 
                            sb.Append(strSql);
                            oleDaTime = new OleDbDataAdapter(sb.ToString(), old);//得到新的 OleDbDataAdapter实例   
                            oleDaTime.Fill(dt);//把数据写入DataTable实例中 
                            sb.Clear();//清空动态字符串
                             foreach (DataRow dr in dt.Rows)//遍历数据行                             
                            {                                 
                                sb.Append(dr["PlanTitle"].ToString() + Environment.NewLine);//追加字 符串                            
                             }
                            if (!String.IsNullOrEmpty(sb.ToString().Trim())) //若存在相应记录
                            {
                                string strTemp = string.Empty;
                                if (intDays == 0)
                                {
                                    strTemp = "今天有以下未执行的计划任务:";
                                }
                                else
                                {
                                    strTemp = "未来" + intDays + "天内有以下未执行的计划任务:";
                                }
                                this.notifyIcon1.ShowBalloonTip(1000, "计划提示：", strTemp + sb.ToString() + "详细情况请单击托盘图标！", ToolTipIcon.Info);
                            }
                            else //若不存在对应的记录
                            {
                                string strTemp = string.Empty;
                                if (intDays == 0)
                                {
                                    strTemp = "今天无未执行的计划任务:";
                                }
                                else
                                {
                                    strTemp = "未来" + intDays + "天内无未执行的计划任务:";
                                }
                                this.notifyIcon1.ShowBalloonTip(1000, "计划提示：", strTemp + "\n详 细情况请单击托盘图标！", ToolTipIcon.Info);
                            }
                        }));
                });
            th.IsBackground = true; //设置新的子线程在后台执行
            th.Start();             //启动新的子线程
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
           
            
                if (e.Button == System.Windows.Forms.MouseButtons.Left) //当点击的是鼠标左键
                {
                    this.Show();
                    this.WindowState = FormWindowState.Normal;
                //picPlanSearch_Click(sender, e); //调用计划查询图片框的点击事件

                    DisplayWelcomePanel();
                }

        }

        private void FrmMain_Deactivate(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)  //监控到窗体被最小化时             
            {                 
                this.Hide();  //隐藏窗体            
            }   
        }

        private void tsmiOpen_Click(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            DisplayWelcomePanel();
        }

        private void dgvPlanRegister_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            isEdit = true; //处于修改状态
                           //激活当前界面上的控件
            txtPlanTitle.Enabled = true;
            cbxPlanKind.Enabled = true;
            dtpExecuteTime.Enabled = true;
            rtbPlanContent.Enabled = true; //将数据表中点击行的记录放到四个控件中
            txtPlanTitle.Text = Convert.ToString(dgvPlanRegister.CurrentRow.Cells["PlanTitle"].Value);
            cbxPlanKind.Text = Convert.ToString(dgvPlanRegister.CurrentRow.Cells["PlanKind"].Value);

            dtpExecuteTime.Value = Convert.ToDateTime(dgvPlanRegister.CurrentRow.Cells["ExecuteTime"].Value);
            rtbPlanContent.Text = Convert.ToString(dgvPlanRegister.CurrentRow.Cells["PlanContent"].Value);
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (dgvPlanRegister.CurrentRow != null) //若当前行不为空
            {                                       //若确定要删除
                if (MessageBox.Show("确定要删除吗？", "软件提示", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    DataTable dt = dgvPlanRegister.DataSource as DataTable;                                //获取数据源
                    oleda.FillSchema(dt, SchemaType.Mapped);                                               //配置指定的数据架构
                    int intIndivNum = Convert.ToInt32(dgvPlanRegister.CurrentRow.Cells["IndivNum"].Value); //获取人员唯一编号
                    DataRow dr = dt.Rows.Find(intIndivNum);                                                //查找指定数据行
                    dr.Delete();                                                                           //删除数据行
                    OleDbCommandBuilder scb = new OleDbCommandBuilder(oleda);                              //关联数据库表单命令
                    try
                    {
                        if (oleda.Update(dt) > 0) //提交数据
                        {
                            if (old.State != ConnectionState.Open) //弱连接为打开
                            {
                                old.Open(); //打开连接
                            }
                            MessageBox.Show("删除成功！"); //放空界面
                            txtPlanTitle.Text = "";        //清空标题输入框
                            cbxPlanKind.Text = "一般计划"; //初始化计划种类
                            dtpExecuteTime.Value = DateTime.Today;
                            rtbPlanContent.Text = ""; //清空内容                 //禁用界面，等待下一次操作
                            txtPlanTitle.Enabled = false;
                            cbxPlanKind.Enabled = false;
                            dtpExecuteTime.Enabled = false;
                            rtbPlanContent.Enabled = false;
                        }
                        else //若删除失败
                        {
                            MessageBox.Show("删除失败！");
                        }
                    }
                    catch (Exception ex) //处理异常
                    {
                        MessageBox.Show(ex.Message, "软件提示");
                    }
                    finally //finally语句
                    {
                        if (old.State == ConnectionState.Open) //若连接打开
                        {
                            old.Close(); //关闭连接
                        }
                    }
                }
            }
        }

        private void label29_Click(object sender, EventArgs e)
        {

        }

        private void label31_Click(object sender, EventArgs e)
        {

        }

        private void 检查更新ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button2_Click(sender,e);
        }
    }

    public partial class SystemInfo
    {
        public static string SofewareVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
    }
        

}
