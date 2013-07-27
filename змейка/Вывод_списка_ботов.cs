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
    public partial class Вывод_списка_ботов : Form
    {
        Список_ботов список = new Список_ботов();
        public Вывод_списка_ботов(Список_ботов боты_)
        {
            InitializeComponent();
            список = боты_;
        }

        private void Вывод_списка_ботов_Load(object sender, EventArgs e)
        {
            if (!список.проверка_на_пустоту())
            {
                список.встать_в_начало();
                dataGridView1.Rows.Clear();
                while (true)
                {
                    dataGridView1.Rows.Add(список.текущий.бот.id,
                                          список.текущий.бот.координатаX,
                                          список.текущий.бот.координатаY
                                         ); 
                    if (список.проверка_на_конец_очереди())
                    {
                        break;
                    }
                    список.Передвинуться();
                }
            }
            
        }
    }
}
