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
        /*private void listBox1_SelectIndexChanged(object sender, EventArgs e)
        {
            if(listBox1.SelectedIndex != -1)
            {
                String item = listBox1.SelectedItem.ToString();
                MessageBox.Show(item + " selected!");
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
        /*private void btnAdd_Click(object sender, EventArgs e)
        {
            string title = tbTitle.Text;
            string description = tbDesc.Text;
            Date.Time now = 
        }*/
    }
}
