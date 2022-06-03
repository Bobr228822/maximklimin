using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.Threading;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public int agle = 0;
        public int ship = 0;
        public int logs = 0;
        public int step = 0;
        public bool isNext = false;
       
        public Dictionary<int, int[]> direction = new Dictionary<int, int[]>()
        {
            { 270, new int[]{1,0} },
            { 0,new int[]{0,1 } },
            { 90, new int[]{-1,0 } },
            { 180, new int[]{0,-1 } }
        };
        Dictionary<int, int> massivWhite = new Dictionary<int, int>() {
            {4,1 },
            {3,2 },
            {2,3 },
            {1,4 }
        };
        Dictionary<int, int> massivBlack = new Dictionary<int, int>() {
            {4,1 },
            {3,2 },
            {2,3 },
            {1,4 }
        };
        int[,] q1 = new int[,] { { 0, 1 }, { -1, 1 }, { -1, 0 }, { -1, -1 }, { 0, -1 }, { 1, -1 }, { 1, 0 }, { 1, 1 }, { 0, 0 } };
        public player1 white = new player1();
        public player1 black = new player1();

        public Form1()
        {
            InitializeComponent();
        }

        private void dt_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dt = sender as DataGridView;
            Stack<int[]> coords = new Stack<int[]>();

            int x = dt.CurrentCell.RowIndex;
            int y = dt.CurrentCell.ColumnIndex;

            int stepx = direction[agle][0];
            int stepy = direction[agle][1];

            for (int i = 0; i < ship; i++)
            {

                if (x < 0 || x > 11 || y < 0 || y > 11)
                {

                    coords.Clear();
                    break;
                }

                bool error = false;

                for (int d = 0; d < 9; d++)
                {
                    int tempx = x + q1[d, 0];
                    int tempy = y + q1[d, 1];
                    if (!(tempx < 0 || tempx > 11 || tempy < 0 || tempy > 11))
                    {
                        if (dt.Rows[tempx].Cells[tempy].Style.BackColor == Color.Red)
                        {
                            error = true;
                        }
                    }
                }
                if (error)
                {
                    coords.Clear();
                    break;
                }

                coords.Push(new int[] { x, y });
                x += stepx;
                y += stepy;


            }
            //создать ассоциативный массив в c# 
            //
            if (logs == 0)
            {
                if (coords.Count == 0 || massivWhite[ship] == 0) { coords.Clear(); } else { massivWhite[ship]--; }
            }
            if (logs == 1)
            {
                if (coords.Count == 0 || massivBlack[ship] == 0) { coords.Clear(); } else { massivBlack[ship]--; }
            }
            while (coords.Count > 0)
            {
                int[] z = coords.Pop();
                int z_x = z[0];
                int z_y = z[1];
                dt.Rows[z_x].Cells[z_y].Style.BackColor = Color.Red;
            }



            //////////////Here Govnokod
            if (step != 0 && dt.Name == "Player2")
            {
                if (step % 2 != 0)
                {
                    white.p2[x, y] = black.p1[x, y];
                    dt.Rows[x].Cells[y].Style.BackColor = white.p2[x, y];


                }
                else
                {
                    black.p2[x, y] = white.p1[x, y];
                    dt.Rows[x].Cells[y].Style.BackColor = black.p2[x, y];

                }
                isNext = dt.Rows[x].Cells[y].Style.BackColor != Color.Red;
                if (isNext)
                {
                    step++;
                    

                    //Сохранить значения второй тб в матрицу игрока
                    //Загрузить матрицы  другого игрока
                    if (step % 2 != 0)
                    {
                        black.bobrData(Player2);
                        white.getData(Player1, Player2);
                    }
                    else
                    {
                        white.bobrData(Player2);
                        black.getData(Player1, Player2);
                    }

                    //ns ghjbuhfk
                    if (gameover(white.p1, black.p2)){
                        MessageBox.Show(
        "победили черные",
        "Сообщение");
                    }
                    if (gameover(black.p1, white.p2))
                    {
                        MessageBox.Show(
        "победили белые",
        "Сообщение");
                    }




                }
            }




        }
        public bool gameover(Color[,] p1, Color[,] p2) {
            for (int i = 0; i < 12; i++)
            {
                for (int j = 0; j < 12; j++)
                {
                    if (p1[i, j] == Color.Red&&p2[i,j]!=Color.Red) { return false;  }
                }
            }
            return true;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Player1.AllowUserToAddRows = false;
            Player1.AllowUserToDeleteRows = false;
            Player1.ReadOnly = true;


            Player2.AllowUserToAddRows = false;
            Player2.AllowUserToDeleteRows = false;
            Player2.ReadOnly = true;
            groupBox2.Hide();
            button2.Hide();
            pictureBox2.Hide();
            for (int i = 0; i < 12; i++)
            {
                Player1.Rows.Add();
                Player2.Rows.Add();
            }
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;

        }

        private void Player_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var grid = sender as DataGridView;
            var rowIdx = (e.RowIndex + 1).ToString();
            var centerformat = new StringFormat()
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };
            var headerBounds = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, grid.RowHeadersWidth, e.RowBounds.Height);
            e.Graphics.DrawString(rowIdx, this.Font, SystemBrushes.ControlText, headerBounds, centerformat);
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Bitmap default_image = new Bitmap(pictureBox1.Image);

            default_image.RotateFlip(RotateFlipType.Rotate90FlipXY);
            pictureBox1.Image = default_image;
            agle = (agle + 90) % 360;


        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ship = listBox1.SelectedIndex + 1;
            MessageBox.Show(ship.ToString());
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

            groupBox1.Hide();

            groupBox2.Visible = true;
            button2.Visible = true;
            button1.Hide();
            logs++;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            groupBox2.Visible = false;
            white.LoadData(Player1);
            black.LoadData(Player2);
            MessageBox.Show("добро пожаловать на сервер безумные зомби");
            white.getData(Player1, Player2);
            button2.Hide();
            groupBox1.Visible = true;
            groupBox2.Visible = true;
            pictureBox1.Hide();
            listBox1.Hide();
            ship = 0;
            step = 1;
            logs++;
        }



       

        private void timer1_Tick(object sender, EventArgs e)
        {
                pictureBox2.Show();
            
             
              //pictureBox2.Image = Image.FromFile("C:/Users/User/Saved Games/Desktop/52ab7a98287239d9282a74e0abcd4835.jpg");
               
            }
        }
    }

