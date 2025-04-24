using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace WinFormsSqlSugarDemo
{
    public partial class EditForm : Form
    {
        // 当前编辑的学生对象
        public Student CurrentStudent;

        // 是否是新增模式
        public bool IsAddMode;
        public EditForm()
        {
            InitializeComponent();
        }

        private void EditForm_Load(object sender, EventArgs e)
        {
            if (!IsAddMode && CurrentStudent != null)
            {
                txtName.Text = CurrentStudent.Name;
                txtAge.Text = CurrentStudent.Age.ToString();
                txtGender.Text = CurrentStudent.Gender;
                Text = "编辑学生信息";
            }
            else
            {
                Text = "添加新学生";
            }
        }

        private void uiButton1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("请输入姓名");
                return;
            }

            if (!int.TryParse(txtAge.Text, out int age) || age <= 0)
            {
                MessageBox.Show("请输入有效的年龄");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtGender.Text))
            {
                MessageBox.Show("请输入性别");
                return;
            }

            // 设置学生信息
            if (CurrentStudent == null)
            {
                CurrentStudent = new Student();
            }

            CurrentStudent.Name = txtName.Text.Trim();
            CurrentStudent.Age = age;
            CurrentStudent.Gender = txtGender.Text.Trim();
            CurrentStudent.CreateTime = DateTime.Now;

            DialogResult = DialogResult.OK;
            Close();
        }

        private void uiButton2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
