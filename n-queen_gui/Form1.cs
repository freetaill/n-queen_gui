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
            bool flag = true;
            // textbox에 정수가 입력되는지 판별하는 역할 수행
            // 정수가 입력되지 않으면 메세지 출력 실행
            try { num = Convert.ToInt32(textBox.Text); }

            catch (FormatException)
            { 
                MessageBox.Show("정수를 입력해주세요"); 
                flag = false;
            }

            // textbox에 정수가 입력됬을 때 실행되는 함수 
            if (flag)
            {
                // form1을 숨기는 역할 수행
                this.Hide();
                Form2 form2 = new Form2();
                form2.StartPosition = FormStartPosition.CenterScreen;
                form2.Owner = this;
                //form2.Show()로 구현할 시에는 자유롭게 화면전환 못함
                form2.ShowDialog();
            }

        }
    }
}