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
    public partial class FormAddBook : Form
    {
        public FormAddBook()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //如果文本框内容有空，则返回
            if(txtAuthor.Text.Trim() == "" || txtID.Text.Trim() == "" || txtIntroduce.Text.Trim() == "" ||
                txtName.Text.Trim() == "" || txtNum.Text.Trim() == "" || txtPrice.Text.Trim() == "" ||
                txtType.Text.Trim() == "" || txtPublisher.Text.Trim() == "" || dtpPBDate.Text == "")
            {
                MessageBox.Show("有空项！", "消息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //添加图书
            Dao dao = new Dao();
            dao.connect();
            String sql = $"insert into T_Book values({int.Parse(txtID.Text)}, '{txtName.Text}', '{txtAuthor.Text}'," +
                $"'{txtPublisher.Text}', '{dtpPBDate.Value}', '{txtType.Text}', '{float.Parse(txtPrice.Text)}', " +
                $"'{int.Parse(txtNum.Text)}', '{txtIntroduce.Text}', 0)";
            try
            {
                if (dao.Execute(sql) > 0)
                {
                    //添加成功
                    dao.DaoClose();
                    MessageBox.Show("添加成功！", "消息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    //添加成功
                    dao.DaoClose();
                    MessageBox.Show("添加失败！", "消息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch
            {
                dao.DaoClose();
                MessageBox.Show("ERROR！", "消息", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
