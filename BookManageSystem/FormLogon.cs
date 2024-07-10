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
    public partial class FormLogon : Form
    {
        public FormLogon()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void FormLogon_Load(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void cobSex_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLogon_Click(object sender, EventArgs e)
        {
            //先判断文本框是否为空
            if(txtAgainPwd.Text == "" || txtIDCard.Text == "" || txtName.Text == "" || 
                txtPwd.Text == "" || txtTel.Text == "" || cobSex.Text == "")
            {
                //提示有空项
                MessageBox.Show("有空项", "消息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if(txtPwd.Text.Trim() != txtAgainPwd.Text.Trim())
            {
                //密码不匹配
                MessageBox.Show("密码不匹配", "消息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //注册 获取数据库中账号的最大值，然后加1
            Dao dao = new Dao();
            dao.connect();  //连接并打开数据库
            string sql = "select MAX(Uid) from T_User";
            SqlDataReader reader = dao.read(sql);
            reader.Read();
            int id = int.Parse(reader[0].ToString()) + 1;
            string name = txtName.Text;
            string idCard = txtIDCard.Text;
            string tel = txtTel.Text;
            string sex = cobSex.Text;
            string pwd = txtPwd.Text;

            //进行添加
            sql = $"insert into T_User values('{id}', '{name}', '{pwd}', '{sex}', '{idCard}', '{tel}', '1')";
            if (dao.Execute(sql) > 0)
            {
                //注册成功
                MessageBox.Show($"注册成功, 您的账号是{id}", "消息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                //注册失败
                MessageBox.Show("注册失败", "消息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            reader.Close();
            dao.DaoClose();
        }
    }
}
