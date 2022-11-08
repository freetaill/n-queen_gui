using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace n_queen_gui
{
    public partial class Form2 : Form
    {
        public int WIDTH = 0;
        public int HEIGHT = 0;
        int[] compare = { 0, 0, 0, 0, 0 };
        int[] prablem = { 1, 3, 5, 2, 4 };
        int[] prablem1 = { 1, 4, 2, 5, 3 };
        Stack stack = new Stack();

        public Form2()
        {
            InitializeComponent();
            this.CenterToScreen();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            Form1 form1 = (Form1)Owner;
            int num = form1.num;
            DynamicButton(num);
            stack.Push(prablem);
            stack.Push(prablem1);
        }

        private void btn_Reset_Click(object sender, EventArgs e)
        {

        }

        private void btn_compare_Click(object sender, EventArgs e)
        {
            // 스택에 들어가는 순간 "object?"형식으로 포장되듯이
            // 형변환되기 때문에 꺼낼 때 넣기 전 형식으로
            // 현변환 해주어야한다.
            int[] kk = (int[])stack.Pop();
            if (kk.SequenceEqual(compare)) { MessageBox.Show("정답"); }

            else { MessageBox.Show("오답"); }
        }

        private void btn_end_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            this.Close();
            Form1 form1 = (Form1)Owner;
            form1.Show();
        }

        /// <summary>
        /// 버튼 컨트롤 동적으로 생성하기
        /// </summary>
        public void DynamicButton(int count)
        {
            Control[,] BTN = new Control[count, count];
            Size = new Size(120 + 90 * count, 150 + 90 * count);

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

                    if ((x + y) % 2 == 1) { BTN[y, x].BackColor = Color.White; }

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
            Form1 form1 = (Form1)Owner;

            int k = Convert.ToInt32(btn.Name);
            int num = form1.num;
            
            if (compare[0] == 0 && k / num == 0)
            {
                compare[k / num] = k % num + 1;
                //MessageBox.Show(compare[k / num].ToString());
                btn.Text = num.ToString();
            }
            else if (compare[k / num] == (k % num + 1))
            {
                compare[k / num] = 0;
                //MessageBox.Show(compare[k / num].ToString());
                btn.Text = "";
            }
            else
            {
                compare[k / num] = k % num + 1;
                //MessageBox.Show(compare[k / num].ToString());
                btn.Text = num.ToString();
            }

        }

    }
}
