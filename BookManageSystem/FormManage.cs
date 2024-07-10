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
    public partial class FormManage : Form
    {
        //图书信息  修改图书功能
        public static int Bid;
        public static string Bname;
        public static string Author;
        public static string Publisher;
        public static string PubDate;
        public static string Type;
        public static int Num;
        public static float Price;
        public static string Introduce;
        public FormManage()
        {
            InitializeComponent();
        }
        private void LoadBooks()
        {
            dgv.Rows.Clear();
            Dao dao = new Dao();
            dao.connect();
            string sql = "select * from T_Book";
            SqlDataReader reader = dao.read(sql);
            while (reader.Read())
            {
                dgv.Rows.Add(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(),
                    reader[3].ToString(), reader[4].ToString(), reader[5].ToString(),
                    reader[6].ToString(), reader[7].ToString(), reader[8].ToString(), reader[9].ToString());
            }
            reader.Close();
            dao.DaoClose();
        }
        private void FormManage_Load(object sender, EventArgs e)
        {
            //在窗体加载的同时，将数据库中的图书消息显示到网格控件当中
            LoadBooks();
        }

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(dgv.CurrentRow == null || dgv.CurrentRow.Cells[0].Value == null)
            {
                MessageBox.Show("选中无效的区域！", "消息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string id = dgv.CurrentRow.Cells[0].Value.ToString();   //当前选中的图书编号
            string name = dgv.CurrentRow.Cells[1].Value.ToString(); //当前选中的图书名
            lblID.Text = id;
            lblName.Text = name;

            Bid = int.Parse(dgv.CurrentRow.Cells[0].Value.ToString());
            Bname = dgv.CurrentRow.Cells[1].Value.ToString();
            Author = dgv.CurrentRow.Cells[2].Value.ToString();
            Publisher = dgv.CurrentRow.Cells[3].Value.ToString();
            PubDate = dgv.CurrentRow.Cells[4].Value.ToString();
            Type = dgv.CurrentRow.Cells[5].Value.ToString();
            Price = float.Parse(dgv.CurrentRow.Cells[6].Value.ToString());
            Num = int.Parse(dgv.CurrentRow.Cells[7].Value.ToString());
            Introduce = dgv.CurrentRow.Cells[8].Value.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            LoadBooks();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if(lblName.Text == "NULL")
            {
                MessageBox.Show("未选中图书！", "消息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //获取到图书的编号
            Dao dao = new Dao();
            dao.connect();
            string sql = $"select Introduce from T_Book where Bid = {int.Parse(lblID.Text)}";
            SqlDataReader reader = dao.read(sql);
            reader.Read();
            string introduce = reader[0].ToString();

            //通过对话框显示出图书的简介
            MessageBox.Show(introduce, $"{lblName.Text}的简介", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //拿到关键字
            string key = txtKey.Text.Trim();
            //以关键字对图书信息进行一个模糊查询
            Dao dao = new Dao();
            dao.connect();
            string sql = $"select * from T_Book where Bname like '%{key}%' or Author like '%{key}%' or Publisher like '%{key}%'";
            SqlDataReader reader = dao.read(sql);
            //清空表格
            dgv.Rows.Clear();
            //结果显示到表格控件中
            while (reader.Read())
            {
                dgv.Rows.Add(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(),
                    reader[3].ToString(), reader[4].ToString(), reader[5].ToString(),
                    reader[6].ToString(), reader[7].ToString(), reader[8].ToString(), reader[9].ToString());
            }
            reader.Close();
            dao.DaoClose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //获取到当前选中的图书编号
            if(lblID.Text == "NULL")
            {
                MessageBox.Show("未选中图书", "消息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //删除对应的数据
            Dao dao = new Dao();
            dao.connect();
            string sql = $"delete T_Book where Bid = {int.Parse(lblID.Text)}";
            if(dao.Execute(sql) > 0)
            {
                //删除成功
                //两个对应label数据的修改 NULL
                lblID.Text = "NULL";
                lblName.Text = "NULL";
                //更新表格数据
                LoadBooks();
                dao.DaoClose();
                MessageBox.Show("下架成功！", "消息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                dao.DaoClose();
                MessageBox.Show("下架失败！", "消息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            FormUpdateBook form = new FormUpdateBook();
            form.ShowDialog();
        }

        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
