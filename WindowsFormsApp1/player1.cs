using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
namespace WindowsFormsApp1
{
    public class player1
    {
        public Color[,] p1 = new Color[12,12];
        public Color[,] p2 = new Color[12,12];

        public void LoadData(DataGridView ng)
        {
            foreach (DataGridViewRow row in ng.Rows)
            {
                int i=row.Index;
                foreach (DataGridViewColumn col in ng.Columns)
                {
                    int x = col.Index;

                    p2[i, x] = Color.Aquamarine;
                    p1[i, x] = row.Cells[x].Style.BackColor;

                    x++;
                }

            }

        }
        public void bobrData(DataGridView ng)
        {
            foreach (DataGridViewRow row in ng.Rows)
            {
                int i = row.Index;
                foreach (DataGridViewColumn col in ng.Columns)
                {
                    int x = col.Index;

                    
                    p2[i, x] = row.Cells[x].Style.BackColor;

                    x++;
                }

            }

        }



        public void getData(DataGridView rw, DataGridView sssr )
        {
           for(int q = 0; q < 12; q++)
            {
                for (int w = 0; w < 12; w++)
                {

                    rw.Rows[q].Cells[w].Style.BackColor = p1[q,w];
                    sssr.Rows[q].Cells[w].Style.BackColor = p2[q,w];
                }
            }

        }
    }
}
