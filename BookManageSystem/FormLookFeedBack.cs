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
    public partial class FormLookFeedBack : Form
    {
        public FormLookFeedBack()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormLookFeedBack_Load(object sender, EventArgs e)
        {
            //把反馈表中的内容显示到网格控件中
            Dao dao = new Dao();
            dao.connect();
            string sql = "select * from T_FeedBack";
            SqlDataReader reader = dao.read(sql);

            while(reader.Read())
            {
                dgv.Rows.Add(reader[0].ToString(), reader[1].ToString(), reader[2].ToString());
            }
            reader.Close();
            dao.DaoClose();
        }
    }
}
