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
    public partial class FormReturnBook : Form
    {
        public FormReturnBook()
        {
            InitializeComponent();
        }
        private void LoadBorrowInfo()
        {
            dgv.Rows.Clear();

            Dao dao = new Dao();
            dao.connect();
            string sql = $"select * from T_Borrow where [Uid] = '{Form1.id}'";
            SqlDataReader reader = dao.read(sql);
            while (reader.Read())
            {
                dgv.Rows.Add(reader[0].ToString(), reader[1].ToString(),
                    reader[2].ToString(), reader[3].ToString(),
                    reader[4].ToString(), reader[5].ToString(), reader[6].ToString());
            }

            reader.Close();
            dao.DaoClose();
        }
        private void FormReturnBook_Load(object sender, EventArgs e)
        {
            //把当前账号租借的图书显示到表格控件中
            LoadBorrowInfo();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //检查当前选中的数据是否为空
            if (dgv.CurrentRow.Cells[0].Value == null)
            {
                MessageBox.Show("未选中数据！", "消息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //1.图书表中对应库存增加； 2.租借表   删除该条租借信息
            int key = int.Parse(dgv.CurrentRow.Cells[0].Value.ToString());
            Dao dao = new Dao();
            dao.connect();
            string sqlDelete = $"delete T_Borrow where [key] = '{key}'";
            string sqlUpdate = $"update T_Book set Num = Num + {int.Parse(dgv.CurrentRow.Cells[6].Value.ToString())}" +
                $" where Bid = {int.Parse(dgv.CurrentRow.Cells[3].Value.ToString())};" +
                $" update T_Book set BorrowCount = BorrowCount - {int.Parse(dgv.CurrentRow.Cells[6].Value.ToString())}" +
                $" where Bid = {int.Parse(dgv.CurrentRow.Cells[3].Value.ToString())};";
            if(dao.Execute(sqlUpdate) + dao.Execute(sqlDelete) >= 2)
            {
                //归还成功
                dao.DaoClose();
                MessageBox.Show("归还成功！", "消息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadBorrowInfo();

            }
            else
            {
                //归还失败
                dao.DaoClose();
                MessageBox.Show("归还失败！", "消息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
    }
}
