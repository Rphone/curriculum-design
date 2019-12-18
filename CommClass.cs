using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.Drawing;
namespace cshape_design
{
    class CalFlag
    {
        public string DisplayText
        {
            get;
            set;
        }
        public string DataValue
        {
            get;
            set;

        }

    }
    static class ExtendDataGridView//数据绑定和设置dgv的隔行换颜色的功能
    {
        /// <summary>        
       ///  转换DataGridViewComboBoxColumn列的数据值为显示值 
        /// </summary> 
         /// <param name="dgvcbxColumn">DataGridViewComboBoxColumn列</param>
         /// <param name="strValueMemberName">数据值</param>  
         /// <param name="strDisplayMemberName">显示值</param>   
        ///<param name="items">集合</param> 
        ///


        public static void ConvertValueToText(this DataGridViewComboBoxColumn dgvbxcom,string strValueMemberName,string strDislayMemberName,ICollection items)
        {
            dgvbxcom.DataSource = items;
            dgvbxcom.DisplayMember = strDislayMemberName;
            dgvbxcom.ValueMember = strValueMemberName;
        }
        ///<summary>
        ///在DataGridView控件中隔行换色显示数据的记录
        ///</summary>
        ///<param name="dgv">DataGridView控件</param>
        ///<param name="color">偶数行的颜色</param>
        
        public static void AltColor(this DataGridView dgv,Color color)
        {
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;//设置选定模式为整行
            foreach(DataGridViewRow sdgv in dgv.Rows)
            {
                if(sdgv.Index%2==0)
                {
                    sdgv.DefaultCellStyle.BackColor = color;//设置偶数行的背景颜色
                }
            }
        }

    }

}
