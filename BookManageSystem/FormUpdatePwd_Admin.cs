using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookManageSystem
{
    public partial class FormUpdatePwd_Admin : Form
    {
        public FormUpdatePwd_Admin()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //需要返回的可能
            if(txtNewPwd.Text == "" || txtOldPwd.Text == "" || txtOkPwd.Text == "")
            {
                //有空项的返回
                MessageBox.Show("有空项！", "消息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if(txtOldPwd.Text == txtNewPwd.Text)
            {
                MessageBox.Show("新旧密码不能一样！", "消息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if(txtNewPwd.Text != txtOkPwd.Text)
            {
                //新密码与确认密码不匹配
                MessageBox.Show("密码不匹配！", "消息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //原密码与数据库中的比较
            Dao dao = new Dao();
            dao.connect();
            //读取数据库中的原密码
            string sql = $"select Pwd from T_Admin where AdminID = '{Form1.id}'";
            SqlDataReader reader = dao.read(sql);

            reader.Read();
            if (reader[0].ToString() != txtOldPwd.Text)
            {
                //密码输入不正确
                MessageBox.Show("密码不正确！", "消息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                reader.Close();
                dao.DaoClose();
                return;
            }
            try
            {
                //修改
                sql = $"update T_Admin set Pwd = '{txtOkPwd.Text}' where AdminID = {Form1.id}";
                if (dao.Execute(sql) > 0)
                {
                    //修改成功
                    MessageBox.Show("修改成功！", "消息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    reader.Close();
                    dao.DaoClose();
                    this.Close();
                }
                else
                {
                    //修改失败
                    MessageBox.Show("修改失败！", "消息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    reader.Close();
                    dao.DaoClose();
                }
            }
            catch
            {
                MessageBox.Show("ERROR!", "消息", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
