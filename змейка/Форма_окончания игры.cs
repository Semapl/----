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
    public partial class Форма_окончания_игры : Form
    {
        int ii = 0;
        public Форма_окончания_игры(int i)
        {
            InitializeComponent();
            ii = i;
        }

        private void Форма_окончания_игры_Load(object sender, EventArgs e)
        {
            label1.Text = "Поздравляем вы продержались " + ii + " секунд";
        }
    }
}
