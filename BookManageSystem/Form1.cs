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
    public partial class Form1 : Form
    {
        //用户的账号和姓名
        public static int id;
        public static string name;
        public Form1()
        {
            InitializeComponent();
        }
        //管理员登陆的方法
        private void AdminLogin()
        {
            //获取账号和密码，与数据库进行对比
            //用户输入的账号和密码
            int id = int.Parse(txtId.Text);
            string pwd = txtPassword.Text;
            Dao dao = new Dao();
            dao.connect();
            string sql = $"select * from T_Admin where AdminID = {id} and Pwd = '{pwd}'";
            SqlDataReader reader = dao.read(sql);
            if(reader.Read() == true)
            {
                //表里面有该用户的信息
                Form1.id = id;
                sql = $"select [Name] from T_Admin where AdminID = {id}";
                reader = dao.read(sql);
                reader.Read();
                Form1.name = reader[0].ToString();

                txtId.Text = "";
                txtPassword.Text = "";

                reader.Close();
                dao.DaoClose();
                //登陆成功
                FormAdmin form = new FormAdmin();
                form.ShowDialog();
            }
            else
            {
                //登陆失败
                reader.Close();
                dao.DaoClose();
                MessageBox.Show("用户名或密码输入错误！", "消息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void UserLogin()
        {
            // 获取账号和密码，与数据库进行对比
            //用户输入的账号和密码
            int id = int.Parse(txtId.Text);
            string pwd = txtPassword.Text;
            //
            Dao dao = new Dao();
            dao.connect();
            string sql = $"select * from T_User where Uid = {id} and Pwd = '{pwd}'";
            SqlDataReader reader = dao.read(sql);
            if (reader.Read() == true)
            {
                //再做一个判断，看该用户是否有登录权限
                string sqlStatus = $"select used from T_User where [Uid] = {int.Parse(txtId.Text)}";
                reader = dao.read(sqlStatus);
                reader.Read();
                if ((bool)reader[0] != true)
                {
                    //没有登录权限
                    MessageBox.Show("该用户没有登录权限！\n\r请联系管理员", "消息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                //表里面有该用户的信息
                Form1.id = id;
                sql = $"select Uname from T_User where [Uid] = {id}";
                reader = dao.read(sql);
                reader.Read();
                Form1.name = reader[0].ToString();

                txtId.Text = "";
                txtPassword.Text = "";

                reader.Close();
                dao.DaoClose();
                //登陆成功
                FormUser form = new FormUser();
                form.ShowDialog();
            }
            else
            {
                reader.Close();
                dao.DaoClose();
                //登陆失败
                MessageBox.Show("用户名或密码输入错误！", "消息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnLogon_Click(object sender, EventArgs e)
        {
            FormLogon form = new FormLogon();
            form.ShowDialog();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
           if(DialogResult.Yes == MessageBox.Show("确认退出吗？", "消息", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                //退出
                this.Close();
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            //判断文本框是否有内容
            if(txtId.Text == "" || txtPassword.Text == "")
            {
                MessageBox.Show("有空项", "消息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            //判断登陆的对象
            if(rbtnAdmin.Checked == true)   //管理员在登录
            {
                AdminLogin();
            }
            if (rbtnUser.Checked == true)   //用户在登录
            {
                UserLogin();
            }
        }
        private void rbtnUser_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rbtnAdmin_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
