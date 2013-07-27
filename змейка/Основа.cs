using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace змейка
{
    public partial class Основа : Form
    {
        public Основа()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // спросить сохранить ли текущюю игру
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Основная_форма_игры frm = new Основная_форма_игры();
            frm.Owner = this;
            frm.Show();
        }

        private void Основа_Load(object sender, EventArgs e)
        {

        }
    }
}
