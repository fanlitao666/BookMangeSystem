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
    public partial class FormLookUser : Form
    {
        public FormLookUser()
        {
            InitializeComponent();
        }
        private void LoadUserInfo()
        {
            dgv.Rows.Clear();

            Dao dao = new Dao();
            dao.connect();
            string sql = $"select Uid, Uname, Sex, IDCard, Tel, Used from T_User";
            SqlDataReader reader = dao.read(sql);
            while (reader.Read())
            {
                dgv.Rows.Add(reader[0].ToString(), reader[1].ToString(),
                    reader[2].ToString(), reader[3].ToString(),
                    reader[4].ToString(), reader[5].ToString());
            }

            reader.Close();
            dao.DaoClose();
        }
        private void FormLookUser_Load(object sender, EventArgs e)
        {
            //显示所有的用户信息 密码除外
            LoadUserInfo();
            if(dgv.Rows.Count > 1)
            {
                lblID.Text = dgv.CurrentRow.Cells[0].Value.ToString();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            LoadUserInfo();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lblID_Click(object sender, EventArgs e)
        {

        }

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgv.CurrentRow == null || dgv.CurrentRow.Cells[0].Value == null)
            {
                MessageBox.Show("选中无效的区域！", "消息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            lblID.Text = dgv.CurrentRow.Cells[0].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //把用户表中的used ->0
            if(lblID.Text == "NULL")
            {
                MessageBox.Show("未选中用户！", "消息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Dao dao = new Dao();
            dao.connect();
            string sql = $"update T_User set Used = 0 where [Uid] = {int.Parse(lblID.Text)}";
            if(dao.Execute(sql) > 0)
            {
                //用户被停止登录系统
                dao.DaoClose();
                MessageBox.Show("用户失去登录权限！！", "消息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadUserInfo();
            }
            else
            {
                dao.DaoClose();
                MessageBox.Show("操作失败！", "消息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //把用户表中的used ->0
            if (lblID.Text == "NULL")
            {
                MessageBox.Show("未选中用户！", "消息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Dao dao = new Dao();
            dao.connect();
            string sql = $"update T_User set Used = 1 where [Uid] = {int.Parse(lblID.Text)}";
            if (dao.Execute(sql) > 0)
            {
                //用户被停止登录系统
                dao.DaoClose();
                MessageBox.Show("用户获得登录权限！！", "消息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadUserInfo() ;
            }
            else
            {
                dao.DaoClose();
                MessageBox.Show("操作失败！", "消息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //拿到关键字
            string key = txtKey.Text.Trim();
            //清空网格数据
            dgv.Rows.Clear();
            //以关键字对图书信息进行一个模糊查询
            Dao dao = new Dao();
            dao.connect();
            string sql = $"select Uid, Uname, Sex, IDCard, Tel, Used from T_User where Uname like '%{key}%' or Sex like '%{key}%' or Used like '%{key}%'";
            SqlDataReader reader = dao.read(sql);
            //清空表格
            dgv.Rows.Clear();
            //结果显示到表格控件中
            while (reader.Read())
            {
                dgv.Rows.Add(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(),
                    reader[3].ToString(), reader[4].ToString(), reader[5].ToString());
            }
            reader.Close();
            dao.DaoClose();
        }
    }
}
