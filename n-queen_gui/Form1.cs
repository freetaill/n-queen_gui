using System.Security.Cryptography.X509Certificates;
using System.Collections;

namespace n_queen_gui
{
    public partial class Form1 : Form
    {
        public int WIDTH = 0;
        public int HEIGHT = 0;
        int[] compare = { 0, 0, 0, 0, 0 };
        int[] prablem = { 1, 3, 5, 2, 4 };
        int[] prablem1 = { 1, 4, 2, 5, 3 };
        Stack stack  = new Stack();
        
        public Form1()
        {
            InitializeComponent();

            this.Load += Form1_Load;
            this.start.Click += start_Click; 
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //DynamicButton();
        }

        /// <summary>
        /// 버튼 컨트롤 동적으로 생성하기
        /// </summary>
        public void DynamicButton(int count)
        {
            Control[,] BTN = new Control[count, count];
            Size = new Size(100 + 90 * count, 100 + 90 * count);

            for (int y = 0; y < count; y++)
            {
                for (int x = 0; x < count; x++)
                {
                    int idx = y * count + x;
                    BTN[y, x] = new Button();

                    BTN[y, x].Name = idx.ToString();
                    BTN[y, x].Parent = this;
                    BTN[y, x].Size = new Size(90, 90);
                    int num = idx + 1;
                    //BTN[y, x].Text = "Dynamic_" + num.ToString();
                    BTN[y, x].Location = new Point((50 + 90 * x), (50 + 90 * y));
                    BTN[y, x].Click += new EventHandler(message_print);

                    if ((x+y)%2==1) { BTN[y, x].BackColor = Color.White; }

                    this.Controls.Add(BTN[y, x]);

                    WIDTH += 80;
                }
                HEIGHT += 90;
            }
            
        }

        void message_print(object? sender, EventArgs e)
        {
            //버튼 정보가져오기
            Button btn = sender as Button;

            int k = Convert.ToInt32(btn.Name);
            int num = Convert.ToInt32(textBox.Text);
            int a = k % num;
            //MessageBox.Show(a.ToString());
            //MessageBox.Show(num.ToString());
            stack.Push(prablem);
            stack.Push(prablem1);
            

            if (compare[0] == 0 && k/num == 0)
            {
                compare[k / num] = k % num + 1;
                MessageBox.Show(compare[k/num].ToString());
                btn.Text = num.ToString();
            }
            else if (compare[k / num] == (k % num + 1))
            {
                compare[k / num] = 0;
                MessageBox.Show(compare[k / num].ToString());
                btn.Text = "";
            }
            else
            {
                compare[k / num] = k % num + 1;
                MessageBox.Show(compare[k / num].ToString());
                btn.Text = num.ToString();
            }

            if (k/num == num - 1)
            {
                object? kk = stack.Pop();
                var intersection = compare.Intersect((int[])kk);
                foreach (var item in intersection)
                {
                    MessageBox.Show(item.ToString() + ",");
                }
                
            }

            //MessageBox.Show(a[0].ToString());
        }

        private void start_Click(object? sender, EventArgs e)
        {
            //예외처리 또는 범위 지정으로 정수값만 입력되게 만들기
            int num = Convert.ToInt32(textBox.Text);
            //this.Visible = false;
            //Form2 form2 = new Form2();
            //form2.Show();
            DynamicButton(num);
        }
    }
}