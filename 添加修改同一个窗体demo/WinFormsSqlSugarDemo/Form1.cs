using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsSqlSugarDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Helper.InitializeDatabase();
            LoadData();
        }

        // 加载数据到DataGridView
        private void LoadData()
        {
            try
            {
                var list = Helper.db.Queryable<Student>().ToList();
                dataGridView1.DataSource = list;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载数据失败: {ex.Message}");
            }
        }

        private void uiButton1_Click(object sender, EventArgs e)
        {
            var editForm = new EditForm
            {
                IsAddMode = true,
                CurrentStudent = null
            };

            if (editForm.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var id = Helper.db.Insertable(editForm.CurrentStudent).ExecuteReturnIdentity();
                    MessageBox.Show($"添加成功，ID: {id}");
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"添加失败: {ex.Message}");
                }
            }
        }

        private void uiButton3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("请选择要更新的行");
                return;
            }

            var selectedId = (int)dataGridView1.SelectedRows[0].Cells["Id"].Value;
            var student = Helper.db.Queryable<Student>().InSingle(selectedId);

            if (student != null)
            {
                var editForm = new EditForm
                {
                    IsAddMode = false,
                    CurrentStudent = student
                };

                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        var count = Helper.db.Updateable(editForm.CurrentStudent).ExecuteCommand();
                        if (count > 0)
                        {
                            MessageBox.Show("更新成功");
                            LoadData();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"更新失败: {ex.Message}");
                    }
                }
            }
        }

        private void uiButton2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("请选择要删除的行");
                return;
            }

            try
            {
                var selectedId = (int)dataGridView1.SelectedRows[0].Cells["Id"].Value;
                var count = Helper.db.Deleteable<Student>().Where(s => s.Id == selectedId).ExecuteCommand();

                if (count > 0)
                {
                    MessageBox.Show("删除成功");
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"删除失败: {ex.Message}");
            }
        }
        //查询
        private void uiButton4_Click(object sender, EventArgs e)
        {
            try
            {

                var list = Helper.db.Queryable<Student>().Where(s => s.Name.Contains(uiTextBox1.Text)).ToList();
                if (list.Count > 0)
                {
                    dataGridView1.DataSource = list;
                }
                else
                {
                    MessageBox.Show("没有查询到数据");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"查询失败: {ex.Message}");
            }

        }
    }

}
