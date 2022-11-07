using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.DataFormats;

namespace n_queen_gui
{
    public partial class Form2 : Form
    {
        Form1 form1;

        public Form2()
        {
            InitializeComponent();
        }

        private void btn_Reset_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 버튼 컨트롤 동적으로 생성하기
        /// </summary>
        public void DynamicButton(int count)
        {
            Control[,] BTN = new Control[count, count];

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
                    BTN[y, x].Text = "Dynamic_" + num.ToString();
                    BTN[y, x].Location = new Point((50 + 95 * x), (50 + 95 * y));
                    this.Controls.Add(BTN[y, x]);

                    //WIDTH += 80;
                }
                //HEIGHT += 90;
            }

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            //form1.DynamicButton(form1.num);
        }
    }
}
