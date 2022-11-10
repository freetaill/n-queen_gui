using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace n_queen_gui
{
    public partial class Form2 : Form
    {
        int[] compare;
        Stack stack = new Stack();

        public Form2()
        {
            InitializeComponent();
            this.CenterToScreen();
        }

        // form2가 실행될때 동작하는 함수 모음
        private void Form2_Load(object sender, EventArgs e)
        {
            // form1에서 정보를 가져올 수 있게하는 역할을 수행
            Form1 form1 = (Form1)Owner;
            int num = form1.num;

            int[] col = new int[num+1];
            compare = new int[num];
            possibility_push(col, 0);
            DynamicButton(num);
        }

        // form2 위의 모든 컨트롤을 모두 지우고 재생성하여 화면에
        // 출력하는 동작을 시행한다.
        private void btn_Reset_Click(object sender, EventArgs e)
        {
            Form1 form1 = (Form1)Owner;
            int num = form1.num;
            this.Controls.Clear();
            this.Controls.Add(btn_Reset);
            this.Controls.Add(btn_end);
            this.Controls.Add(btn_compare);
            this.Controls.Add(btn_back);
            DynamicButton(num);
        }

        // 보드위에 배치된 여왕의 값과 정답이 저장된 스택을
        // 비교해 참인지 판단하는 역할을 수행
        private void btn_compare_Click(object sender, EventArgs e)
        {
            bool flag = false;
            Stack copy = new Stack();
            copy = (Stack)stack.Clone();
            for (int i = 0; i < stack.Count; i++)
            {
                // 스택에 들어가는 순간 "object?"형식으로 포장되듯이
                // 형변환되기 때문에 꺼낼 때 넣기 전 형식으로
                // 형변환 해주어야한다.
                int[] copy_file = (int[])copy.Pop();
                // SequenceEqual을 이용해 두 배열을 대조하여 일치할 경우
                // 아래 if문이 동작하도록 flag값을 true로 바꾼다. 
                if (copy_file.SequenceEqual(compare)) { flag = true; }
            }
            //flag값에 따라 알맞은 메세지를 출력하는 역할 수행
            if (flag==true) { MessageBox.Show("정답"); }
            else { MessageBox.Show("오답"); }
        }

        // 게임을 끝내는 역할을 수행
        // form2에서 이 버튼으로 종료하지 않으면 form1이 종료되지 않음
        private void btn_end_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // 다시 form1으로 돌아가게 만드는 역할을 수행한다.
        // form2가 닫히고 보이지않게 만든 form1을 보이게 만드는 역할을 수행
        // 숨겨둔 걸 불러오기에 form2를 불러오기 위해 입력한 값이 유지되어 있음
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
            btn_Reset.Location = new Point(50 , 50 + 90 * count);
            btn_compare.Location = new Point(150, 50 + 90 * count);
            btn_end.Location = new Point(250, 50 + 90 * count);

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
                    BTN[y, x].Location = new Point((50 + 90 * x), (50 + 90 * y));
                    //클릭을 했을 때 이벤트를 불러오는 역할을 수행
                    BTN[y, x].Click += new EventHandler(message_print);

                    if ((x + y) % 2 == 1) { BTN[y, x].BackColor = Color.White; }

                    // 위 이벤트와 정보들이 저장된 버튼 컨트롤을
                    // 컨트롤 컬랙션에 추가하는 역할 수행
                    this.Controls.Add(BTN[y, x]);

                }
            }

        }

        // 동적으로 생성된 버튼을 클릭했을 때 발생하는 이벤트 함수
        void message_print(object? sender, EventArgs e)
        {
            //버튼 정보가져오기
            Button btn = sender as Button;
            Form1 form1 = (Form1)Owner;

            int k = Convert.ToInt32(btn.Name);
            int num = form1.num;
            //보드판의 세로(열)
            int column = k / num;
            //보드판의 가로(행)
            int row = k % num;

            // 버튼을 눌렀을 때 해당 버튼의 값이 저장됬는지에 따라 
            // 그림의 출력과 삭제를 판별하는 역할 수행
            if (compare[0] == 0 && column == 0)
            {
                remove_action(column, num);
                Image? b = Properties.Resources.ResourceManager
                .GetObject("Queen_Picture") as Image;
                btn.Image = b;
                compare[column] = row + 1;
            }
            else if (compare[column] == (row + 1))
            {
                Image? b = Properties.Resources.ResourceManager
                .GetObject("") as Image;
                btn.Image = b;
                compare[column] = 0;
            }
            else
            {
                remove_action(column, num);
                Image? b = Properties.Resources.ResourceManager
                .GetObject("Queen_Picture") as Image;
                btn.Image = b;
                compare[column] = row + 1;
            }
        }

        // 이전에 선택했던 여왕이 존재할 때
        // 이전에 여왕을 배치한 버튼의 이름을 가져와서
        // 해당 버튼의 위치를 표시하는 이미지를 지우는 알고리즘 
        void remove_action(int column, int num)
        {
            Button btn = null;
            // 이전에 배치한 여왕이 존재하는지 판별하는 역할 수행
            if (compare[column] != 0)
            {
                //이전에 선택했던 버튼의 이름을 가져오는 역할을 하는 식
                string btnName = (column * num + compare[column] - 1).ToString();
                //가져오려는 버튼이 존재하는지 판별하는 역할을 수행함
                if (this.Controls.ContainsKey(btnName))
                {
                    // 가져온 버튼을 배정하는 식
                    btn = this.Controls[btnName] as Button;
                    // 이미 배치한 여왕이 존재할때 그 위치의 버튼에 출력된 이미지를 지우는 역할을 수행
                    Image? b = Properties.Resources.ResourceManager
                        .GetObject("") as Image;
                    btn.Image = b;
                    
                }
            }
           
        }

        //여왕이 서로 공격가능한 위치에 있는지를 판별하는 역할
        bool compare_queen(int[] col, int i)
        {
            int k = 1;
            bool flag = true;
            while (k < i && flag)
            {
                if (col[i] == col[k] || Math.Abs(col[i] - col[k]) == (i - k))
                    flag = false;
                k += 1;
            }
            return flag;
        }
        //조건이 참이 되는 모든 경우의 수를 col을 이용해 stack에 push하는 역할 수행
        void possibility_push(int[] col, int i)
        {
            int n = col.Length - 1;
            if (compare_queen(col, i))
            {
                if (i == n)
                {
                    int[] set = new int[n];
                    for(int x = 0; x < n; x++)
                    {
                        set[x] = col[x + 1];
                    }
                    stack.Push(set);
                }

                else
                {
                    for (int x = 0; x < n; x++)
                    {
                        col[i+1] = x + 1;
                        possibility_push(col, i + 1);
                    }
                }
            }
            
        }

    }
}
