using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookManageSystem
{
    public partial class FormFeedBack : Form
    {
        public FormFeedBack()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(txtFeedBack.Text.Trim() != "") 
            {
                //添加
                Dao dao = new Dao();
                dao.connect();

                string sql = $"insert into T_FeedBack values('{Form1.id}', '{txtFeedBack.Text}', '{DateTime.Now}')";
                if(dao.Execute(sql) > 0)
                {
                    //提交反馈成功
                    MessageBox.Show("提交成功！", "消息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("操作失败！", "消息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                dao.DaoClose();
            }
            else
            {
                MessageBox.Show("反馈不能为空！", "消息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
