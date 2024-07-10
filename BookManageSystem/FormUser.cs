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
    public partial class FormUser : Form
    {
        public FormUser()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void 图书评价ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 退出登录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("确认退出吗？", "消息", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                //退出
                this.Close();
            }
        }

        private void FormUser_Load(object sender, EventArgs e)
        {
            this.label1.Text = $"用户：{Form1.name}   {Form1.id}";
        }

        private void 注销账号ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //删除数据库中当前账号信息
            if (DialogResult.Yes == MessageBox.Show("确定注销当前账号吗？", "消息", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                //删除操作
                //获取到要注销的账号
                int id = Form1.id;
                Dao dao = new Dao();
                dao.connect();
                string sql = $"delete T_User where [Uid] = {id}";
                if (dao.Execute(sql) > 0)
                {
                    //注销账号成功
                    MessageBox.Show("注销账号成功", "消息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dao.DaoClose();
                    //返回到登录窗口
                    this.Close();
                }
                else
                {
                    //注销失败
                    MessageBox.Show("注销账号失败", "消息", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    dao.DaoClose();
                }
            }
        }

        private void 修改密码ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormUpdatePwd_User form = new FormUpdatePwd_User();
            form.ShowDialog();
        }

        private void 租借图书ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormBorrowBook form = new FormBorrowBook();
            form.ShowDialog();
        }

        private void 归还图书ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormReturnBook form = new FormReturnBook();
            form.ShowDialog();
        }

        private void 查看评价ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormDiscussion form = new FormDiscussion();
            form.ShowDialog();
        }

        private void 反馈到管理员ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormFeedBack form = new FormFeedBack();
            form.ShowDialog();
        }
    }
}
