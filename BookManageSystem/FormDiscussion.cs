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
    public partial class FormDiscussion : Form
    {
        public FormDiscussion()
        {
            InitializeComponent();
        }
        private void LoadDiscussion()
        {
            dgv.Rows.Clear();

            Dao dao = new Dao();
            dao.connect();
            string sql = $"select * from T_Discussion";
            SqlDataReader reader = dao.read(sql);
            while (reader.Read())
            {
                dgv.Rows.Add(reader[0].ToString(), reader[1].ToString(),
                    reader[2].ToString(), reader[3].ToString(),
                    reader[4].ToString(), reader[5].ToString(),
                    reader[6].ToString(), reader[7].ToString());
            }

            reader.Close();
            dao.DaoClose();
        }
        private void LoadMyBook()
        {

            Dao dao = new Dao();
            dao.connect();
            string sql = $"select Bid from T_Borrow where [Uid] = {Form1.id}";
            SqlDataReader reader = dao.read(sql);
            while (reader.Read())
            {
                cobId.Items.Add(reader[0].ToString());
            }

            reader.Close();
            dao.DaoClose();
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void FormDiscussion_Load(object sender, EventArgs e)
        {
            //考虑 登录者的类型(用户->删除功能没有 管理员->发表没有)
            if(Form1.id.ToString().Length == 5)
            {
                //用户在登录
                btnDelete.Visible = false;
            }
            else
            {
                //管理员
                btnSend.Visible = false;
            }
            //把评论信息全部显示到网格
            LoadDiscussion();
            //自己借的书才能评论 id加载到组合框的item
            LoadMyBook();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            //检查控件的内容
            if(lblName.Text == "NULL" || txtWords.Text == "" || cobId.Text == "" || cobScore.Text == "")
            {
                //有空项
                MessageBox.Show("有空项！", "消息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //添加评论
            int Bid = int.Parse(cobId.Text);
            string Bname = lblName.Text;
            int id = Form1.id;
            string name = Form1.name;
            string words = txtWords.Text;
            DateTime date = DateTime.Now;
            string score = cobScore.Text;
            int key = 0;

            Dao dao = new Dao();
            dao.connect();
            string sql = $"select [key] from T_Discussion where [key] = {key}";

            SqlDataReader reader = dao.read(sql);

            while (true)
            {
                key++;
                sql = $"select [key] from T_Discussion where [key] = {key}";
                reader = dao.read(sql);
                reader.Read();
                if (!reader.HasRows)
                {
                    break;
                }
            }
            //添加评论
            string sqlInsert = $"insert into T_Discussion values('{key}', '{Bid}', '{Bname}', '{id}', '{name}', '{words}', '{date}', '{score}')";
            if(dao.Execute(sqlInsert) > 0)
            {
                //发表成功
                MessageBox.Show("发表成功！", "消息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                //发表失败
                MessageBox.Show("发表失败！", "消息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            LoadDiscussion();
            reader.Close();
            dao.DaoClose();
        }

        private void cobId_TextChanged(object sender, EventArgs e)
        {
            //获取当前选中的图书编号
            int id = int.Parse(cobId.Text);
            //通过编号获取书名
            Dao dao = new Dao();
            dao.connect();
            string sql = $"select Bname from T_Book where Bid = {id}";
            SqlDataReader reader = dao.read(sql);
            reader.Read();
            string name = reader[0].ToString();
            lblName.Text = name;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //判断选中的内容是否有效
            if (dgv.CurrentRow.Cells[0].Value == null)
            {
                MessageBox.Show("选中无效的区域！", "消息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //删除操作
            Dao dao = new Dao() ;
            dao.connect();
            string sql = $"delete T_Discussion where [key] = {int.Parse(dgv.CurrentRow.Cells[0].Value.ToString())}";
            if(dao.Execute(sql) > 0)
            {
                //删除成功
                MessageBox.Show("删除成功！", "消息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                //删除失败
                MessageBox.Show("删除失败！", "消息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            LoadDiscussion();
            dao.DaoClose();
        }

        private void cobId_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
