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
    public partial class FormBorrowBook : Form
    {
        public FormBorrowBook()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
        private void LoadBook()
        {
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
        private void FormBorrowBook_Load(object sender, EventArgs e)
        {
            //把书籍数据显示到网格控件当中
            LoadBook();
            cobNum.Text = "1";
            if (dgv.Rows.Count == 1)
            {
                return;
            }
            else
            {
                lblName.Text = dgv.CurrentRow.Cells[1].Value.ToString();
            }
        }

        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(dgv.CurrentRow == null || dgv.CurrentRow.Cells[0].Value == null)
            {
                MessageBox.Show("选中无效区域！", "消息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            lblName.Text = dgv.CurrentRow.Cells[1].Value.ToString();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //有一张表，保存借书信息
            //key   书名  编号  数量  借书时间
            string name = dgv.CurrentRow.Cells[1].Value.ToString();
            int id = int.Parse(dgv.CurrentRow.Cells[0].Value.ToString());
            int num = int.Parse(cobNum.Text);
            DateTime date = DateTime.Now;
            int key = 0;

            Dao dao = new Dao();
            dao.connect();
            string sql = $"select [Key] from T_Borrow where [Key] = {key}";
            SqlDataReader reader = dao.read(sql);
            reader.Read();

            //循环读取租借表中是否有为key的数据
            while (true)
            {
                key++;
                sql = $"select [Key] from T_Borrow where [Key] = {key}";
                reader = dao.read(sql);
                reader.Read();
                if (!reader.HasRows)
                {
                    break;
                }
            }
            reader.Close();

            //判断库存是否充足
            string sqlFlag = $"select Bid from T_Book where Num - '{num}' >= 0 and Bid = '{id}'";
            SqlDataReader reader1 = dao.read(sqlFlag);
            if(!reader1.Read())
            {
                MessageBox.Show("库存不足！", "消息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                reader1.Close();
                dao.DaoClose();
                return;
            }

            //租借    T_Book  T_Borrow
            string sqlInsert = $"insert into T_Borrow values('{key}', '{Form1.id}', '{Form1.name}', '{id}', '{name}', '{date}', '{num}')";
            string sqlUpdate = $"update T_Book set Num = Num - '{num}', BorrowCount = BorrowCount + '{num}' where Bid = '{id}'";
            if(dao.Execute(sqlInsert) + dao.Execute(sqlUpdate) >= 2)
            {
                //租借成功
                reader1.Close();
                dao.DaoClose();
                MessageBox.Show("租借成功！", "消息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgv.Rows.Clear();
                LoadBook(); //更新表格
            }
            else
            {
                //租借失败
                reader1.Close();
                dao.DaoClose();
                MessageBox.Show("租借失败！", "消息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }
    }
}
