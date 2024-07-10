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
    public partial class FormLookBorrow : Form
    {
        public FormLookBorrow()
        {
            InitializeComponent();
        }
        private void LoadBorrowInfo()
        {
            dgv.Rows.Clear();

            Dao dao = new Dao();
            dao.connect();
            string sql = $"select * from T_Borrow";
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
        private void FormLookBorrow_Load(object sender, EventArgs e)
        {
            //加载租借表中所有信息到网格控件中
            LoadBorrowInfo();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //姓名    书名
            //获取关键字
            string key = txtKey.Text;

            Dao dao = new Dao();
            dao.connect();
            string sql = $"select * from T_Borrow where Bname like '%{key}%' or Uname like '%{key}%'";
            dgv.Rows.Clear();
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
    }
}
