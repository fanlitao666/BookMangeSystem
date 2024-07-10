using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookManageSystem
{
    public partial class FormUpdateBook : Form
    {
        public FormUpdateBook()
        {
            InitializeComponent();
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormUpdateBook_Load(object sender, EventArgs e)
        {
            txtAuthor.Text = FormManage.Author;
            txtID.Text = FormManage.Bid.ToString();
            txtIntroduce.Text = FormManage.Introduce;
            txtName.Text = FormManage.Bname;
            txtNum.Text = FormManage.Num.ToString();
            txtPrice.Text = FormManage.Price.ToString();
            txtPublisher.Text = FormManage.Publisher;
            txtType.Text = FormManage.Type;
            dtpDate.Value = DateTime.Parse(FormManage.PubDate.ToString());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //判断文本框中内容
            if (txtType.Text == "" || txtAuthor.Text == "" || txtID.Text == "" || 
                txtIntroduce.Text == "" || txtName.Text == "" || txtNum.Text == "" || 
                txtPrice.Text == "" || txtPublisher.Text == "" || dtpDate.Text == "")
            {
                MessageBox.Show("有空项！", "消息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //修改图书信息
            Dao dao = new Dao();
            dao.connect();
            string sql = $"update T_Book set Bid = {txtID.Text}, Bname = '{txtName.Text}', Type = '{txtType.Text}', " + 
                $"Num = {txtNum.Text}, Price = {txtPrice.Text}, Introduce = '{txtIntroduce.Text}', " + 
                $"Author = '{txtAuthor.Text}', Publisher = '{txtPublisher.Text}', PBDate = '{dtpDate.Value}' where Bid = {FormManage.Bid}";
            if(dao.Execute(sql) > 0)
            {
                //修改成功
                dao.DaoClose();
                MessageBox.Show("修改成功！", "消息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                //修改失败
                dao.DaoClose();
                MessageBox.Show("修改失败！", "消息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
