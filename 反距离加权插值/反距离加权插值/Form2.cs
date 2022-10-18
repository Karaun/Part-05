using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 反距离加权插值
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private static List<Coordinate> QList;
        public List<string> naem = new List<string>();
        public List<double> coord = new List<double>();
        public int Count = 0;

        private void button1_Click(object sender, EventArgs e)
        {
            string[] txt = textBox1.Text.Split('\n');
            QList = new List<Coordinate>();
            naem = new List<string>();
            coord = new List<double>();

            for (int i = 0; i < txt.Length; i++)
            {
                string[] temp = txt[i].Split(',');
                Coordinate cd = new Coordinate(temp[0], Convert.ToDouble(temp[1]), Convert.ToDouble(temp[2]), Convert.ToDouble(temp[3]));
                QList.Add(cd);
            }

            foreach (var i in QList)
            {
                naem.Add(i.mark);
                coord.Add(i.X_point);
                coord.Add(i.Y_point);
                coord.Add(i.altitude);
            }
            Count = Convert.ToInt32(textBox2.Text);

            MessageBox.Show("设置成功");
        }


        private void Form2_Load(object sender, EventArgs e)
        {
            string[] txt = textBox1.Text.Split('\n');
            QList = new List<Coordinate>();
            naem = new List<string>();
            coord = new List<double>();

            for (int i = 0; i < txt.Length; i++)
            {
                string[] temp = txt[i].Split(',');
                Coordinate cd = new Coordinate(temp[0], Convert.ToDouble(temp[1]), Convert.ToDouble(temp[2]), Convert.ToDouble(temp[3]));
                QList.Add(cd);
            }
            foreach (var i in QList)
            {
                naem.Add(i.mark);
                coord.Add(i.X_point);
                coord.Add(i.Y_point);
                coord.Add(i.altitude);
            }
            Count = Convert.ToInt32(textBox2.Text);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
        }
    }
}
