using Microsoft.OData.Edm;
using Microsoft.VisualBasic;
using System.Windows.Forms;

namespace TodoListApp
{
    public partial class Form1 : Form
    {
        private TodoList todoList;
        private string filePath = "todolist.json";

        public Form1()
        {
            InitializeComponent();
        }
        /*private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                TodoItem selectedItem = listBox1.SelectedItem as TodoItem;
                String description = selectedItem.Description;
                MessageBox.Show(description);
            }
        }*/
        private void Form1_Load(object sender, EventArgs e)
        {
            todoList = new TodoList();
            todoList.LoadFromFile(filePath);
            listBox1.DisplayMember = "Title"; // 设置 ListBox 显示的字段为 Title
            listBox1.DataSource = todoList.GetItems();
        }
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            todoList.SaveToFile(filePath);
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            string title = tbTitle.Text;
            string description = tbDesc.Text;
            DateTime now = DateTime.Now;
            DateTime dueDate = dtPickerDue.Value;
            //
            //
            if (string.IsNullOrEmpty(tbTitle.Text))
            {
                MessageBox.Show("請輸入標題");
                return;
            }
            if (string.IsNullOrEmpty(tbDesc.Text))
            {
                MessageBox.Show("請輸入内容説明");
                return;
            }
            //
            TodoItem newItem = new TodoItem
            {
                Title = title,
                Description = description,
                CreatedDate = now,
                DueDate = dueDate,
                Status = rbStatus1.Checked ? 1 : (rbStatus2.Checked ? 2 : 0)
            };
            //
            todoList.AddItem(newItem);

            // 清空输入框内容
            tbTitle.Text = "";
            tbDesc.Text = "";
            tbCreatedDate.Text = "";
            dtPickerDue.Value = DateTime.Now; // 重置日期选择器
            rbStatus0.Checked = true;
            //
            listBox1.ClearSelected();
            // 顯示信息
            MessageBox.Show("新增代辦清單項目成功！");
        }
        private void listbox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex >= 0)
            {
                TodoItem item = (TodoItem)listBox1.SelectedItem;
                //
                tbTitle.Text = item.Title;
                tbDesc.Text = item.Description;
                tbCreatedDate.Text = item.CreatedDate.ToString();
                dtPickerDue.Value = item.DueDate;
                //
                rbStatus0.Checked = true;
                rbStatus1.Checked = item.Status == 1;
                rbStatus2.Checked = item.Status == 2;

                //控制按鈕的狀態
                btnAdd.Enabled = false;
                btnModify.Enabled = true;
                btnDelete.Enabled = true;
                btnCancel.Enabled = true;
            }

        }
        private void btnModify_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex >= 0)
            {
                TodoItem item = (TodoItem)listBox1.SelectedItem;
                //
                item.Title = tbTitle.Text;
                item.Description = tbDesc.Text;
                item.CreatedDate = DateTime.Now;
                item.DueDate = DateTime.Now;
                item.Status = rbStatus1.Checked ? 1 : (rbStatus2.Checked ? 2 : 0);
                //
                // 刷新
                listBox1.DataSource = null;
                listBox1.DataSource = todoList.GetItems();

                //
                // 顯示信息
                MessageBox.Show("修改代辦清單項目成功！");
                //
                resetUI();
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex >= 0)
            {
                TodoItem item = (TodoItem)listBox1.SelectedItem;
                //
                DialogResult confirm = MessageBox.Show("確定要刪除嗎？", "刪除代辦清單項目", MessageBoxButtons.YesNo);
                if (confirm == DialogResult.Yes)
                {
                    todoList.RemoveItem(item);
                    //
                    // 顯示信息
                    MessageBox.Show("刪除待辦清單項目成功！");
                    //
                    resetUI();
                }
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            resetUI();
        }
        private void resetUI()
        {
            //清空輸入欄
            tbTitle.Text = "";
            tbDesc.Text = "";
            tbCreatedDate.Text = "";
            dtPickerDue.Value = DateTime.Now;
            rbStatus0.Checked = true;
            //
            listBox1.ClearSelected();
            //控制按鈕的狀態
            btnAdd.Enabled = true;
            btnModify.Enabled = false;
            btnDelete.Enabled = false;
            btnCancel.Enabled = false;
        }
    }
}
