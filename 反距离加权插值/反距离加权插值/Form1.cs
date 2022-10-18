using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 反距离加权插值
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private List<Coordinate> Qlist = new List<Coordinate>();
        public int n = 5;

        private void 打开ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "请选择打开的文件";
            ofd.Filter = "txt|*.txt|All|*.*";
            ofd.ShowDialog();

            string path = ofd.FileName;
            if (path == "")
            {
                return;
            }
            using (FileStream FsRead = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Read))
            {
                byte[] buffer = new byte[1024 * 1024 * 5];
                int r = FsRead.Read(buffer, 0, buffer.Length);
                txtSource.Text = Encoding.UTF8.GetString(buffer, 0, r);
            }
        }

        private void 导出结果ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog ofd = new SaveFileDialog();
            ofd.Title = "请保存路径";
            ofd.Filter = "txt|*.txt|All|*.*";
            ofd.ShowDialog();

            string path = ofd.FileName;
            if (path == "")
            {
                return;
            }
            using (FileStream FsWrite = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write))
            {
                byte[] buffer = Encoding.UTF8.GetBytes(txtResult.Text);
                FsWrite.Write(buffer, 0, buffer.Length);
            }
        }



        private void 计算ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string[] txt = txtSource.Text.Split('\n');
                List<Coordinate> coordList = new List<Coordinate>();

                for (int i = 0; i < txt.Length; i++)
                {
                    string[] temp = txt[i].Split(',');
                    Coordinate cd = new Coordinate(temp[0], Convert.ToDouble(temp[1]), Convert.ToDouble(temp[2]), Convert.ToDouble(temp[3]));
                    coordList.Add(cd);
                }

                int dex = Qlist.Count;
                List<result>[] dist = new List<result>[dex];

                for (int i = 0; i < dex; i++)
                {
                    dist[i] = Coordinate.distList(coordList, Qlist[i]);
                }



                for (int i = 0; i < dex; i++)
                {
                    Qlist[i].altitude = Coordinate.Get_Altu(dist[i], n);
                }

                int v = 0;

                txtResult.Text = "点名     X(m)       Y(m)       H(m)       参与插值点列表 " + "\r\n";
                foreach (var i in Qlist)
                {
                    txtResult.Text += i.mark + "   " + i.X_point.ToString("#0.000") + "    " + i.Y_point.ToString("#0.000") + "     " + i.altitude.ToString("#0.000") + "    " + Coordinate.Ptxt(dist[v], n) + "\r\n";
                    v++;
                }
            }
            catch
            {
                MessageBox.Show("操作错误","提示");
            }
        }



        private void 参数设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Qlist = new List<Coordinate>();
            Form2 f2 = new Form2();
            f2.ShowDialog();

            List<string> name = f2.naem;
            List<double> coord = f2.coord;
            int dex = 0;
            n = f2.Count;

            for (int j = 0; j < coord.Count; j+=3)
            {
                string t1 = name[dex];
                double t2 = coord[j];
                double t3 = coord[j + 1];
                double t4 = coord[j + 2];
                Coordinate cd = new Coordinate(t1, t2, t3, t4);
                Qlist.Add(cd);
                dex++;

            }
            
        }

        private void 帮助ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("请先进行参数设置后\r\n再进行计算\r\n制作者:KUST_CZY", "提示");
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            Coordinate cd1 = new Coordinate("Q1", 4310.000, 3600.000, 0);
            Coordinate cd2 = new Coordinate("Q2", 4330.000, 3600.000, 0);
            Coordinate cd3 = new Coordinate("Q3", 4310.000, 3620.000, 0);
            Coordinate cd4 = new Coordinate("Q4", 4330.000, 3620.000, 0);
            Qlist.Add(cd1);
            Qlist.Add(cd2);
            Qlist.Add(cd3);
            Qlist.Add(cd4);
        }
    }
}
