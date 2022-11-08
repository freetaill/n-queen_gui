namespace n_queen_gui
{
    public partial class Form1 : Form
    {
        public int num;

        public Form1()
        {
            InitializeComponent();
            this.CenterToScreen();
        }

        private void start_Click(object? sender, EventArgs e)
        {
            //����ó�� �Ǵ� ���� �������� �������� �Էµǰ� �����
            bool flag = true;
            try { num = Convert.ToInt32(textBox.Text); }

            catch (FormatException)
            { 
                MessageBox.Show("������ �Է����ּ���"); 
                flag = false;
            }

            if (flag)
            {
                this.Hide();
                Form2 form2 = new Form2();
                form2.StartPosition = FormStartPosition.CenterScreen;
                form2.Owner = this;
                //form2.Show()�� ������ �ÿ��� �����Ӱ� ȭ����ȯ ����
                form2.ShowDialog();
            }

        }
    }
}