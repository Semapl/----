using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Threading;

namespace змейка
{   
    
    public partial class Основная_форма_игры : Form
    {
        bool игразакончена = false; int сек = 0;
        Thread Поток_переодичной_отрисовки;
        Thread поток_ботов;
        Thread врем;
        Очередь_нажатых_клавиш Очередь_клавиш = new Очередь_нажатых_клавиш();
        Главный_герой Герой = new Главный_герой();
        Список_ботов боты = new Список_ботов();
        public Основная_форма_игры()
        {
            InitializeComponent();
        }
        #region Магия для доступа к панели не из того потока
        private void SetPicture(Image img)
        {
            if (pictureBox1.InvokeRequired)
            {
                pictureBox1.Invoke(new MethodInvoker(
                delegate()
                {
                    pictureBox1.Refresh();
                }));
            }
            else
            {
                pictureBox1.Refresh();
            }
        }
        #endregion
        private void Основная_форма_игры_Load(object sender, EventArgs e)
        {
           // создаём поток отсчета 3 2 1 игра началась...после отсчета запускаем главный игровой потокциклом
            Thread Поток_остчета = new Thread(Отсчет);
            Поток_остчета.Start();
        }
        #region Обработка клавиш в потоке формы
        private void ПоймалиКлавишу(object sender, KeyEventArgs e)
                        {
                            if (игразакончена)
                            {
                                Форма_окончания_игры frm3 = new Форма_окончания_игры(сек);
                                frm3.Owner = this;
                                frm3.Show();
                            }
                            if (e.KeyValue == 87) { Герой.передвинуться(0, -1); }
                            if (e.KeyValue == 83) { Герой.передвинуться(0, 1); }
                            if (e.KeyValue == 68) { Герой.передвинуться(1, 0); }
                            if (e.KeyValue == 65) { Герой.передвинуться(-1, 0); }
                         
                        }
        #endregion
        #region Мои функции для потоков
        private void Отсчет()
        {
             Поток_переодичной_отрисовки = new Thread(Отрисовка);
            Поток_переодичной_отрисовки.Start();  
            //создаём ботов
            for (int i = 0; i < 5; i++)
            {
                боты.Дабавить_в_список(i);
                боты.текущий.бот.координатаX = боты.текущий.бот.координатаX + 200 * i;
            }
            label3.Invoke(new MethodInvoker(
               delegate()
               {
                   label3.Text="3";
               }));
          
            Thread.Sleep(TimeSpan.FromSeconds(1));
            label3.Invoke(new MethodInvoker(
               delegate()
               {
                   label3.Text = "2";
               }));
          
            Thread.Sleep(TimeSpan.FromSeconds(1));
            label3.Invoke(new MethodInvoker(
               delegate()
               {
                   label3.Text = "1";
               }));
          
            Thread.Sleep(TimeSpan.FromSeconds(1));
            label3.Invoke(new MethodInvoker(
                   delegate()
                   {
                       label3.Text = "GO";
                   }));
            Thread.Sleep(TimeSpan.FromSeconds(1));
           
            //создаём поток игры
            //
            врем = new Thread(время);
            врем.Start();
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ПоймалиКлавишу);//подпишемся на ловлю клавиш
            поток_ботов = new Thread(ботыДвижуха);
            поток_ботов.Start();
            
        }
        int секунды = 0;
        private void время()
        {
            while (true)
            {
                секунды++;
                Thread.Sleep(TimeSpan.FromSeconds(1));
                label4.Invoke(new MethodInvoker(
                  delegate()
                  {
                      label4.Text = секунды.ToString();
                  }));

            }
        }
        private void ботыДвижуха()
        {
            
            while (true)
            {
                
                боты.Движение();
                if (!боты.проверка_на_пустоту())
                {
                    боты.встать_в_начало();
                    while (true)
                    {
                        if (боты.текущий.бот.координатаX >= Герой.координатаX && боты.текущий.бот.координатаX <= Герой.координатаX + 40 &&
                            боты.текущий.бот.координатаY >= Герой.координатаY && боты.текущий.бот.координатаY <= Герой.координатаY + 40
                            || боты.текущий.бот.координатаX + 40 >= Герой.координатаX && боты.текущий.бот.координатаX + 40 <= Герой.координатаX + 40 &&
                            боты.текущий.бот.координатаY >= Герой.координатаY && боты.текущий.бот.координатаY <= Герой.координатаY + 40
                            || боты.текущий.бот.координатаX >= Герой.координатаX && боты.текущий.бот.координатаX <= Герой.координатаX + 40 &&
                            боты.текущий.бот.координатаY + 40 >= Герой.координатаY && боты.текущий.бот.координатаY + 40 <= Герой.координатаY + 40
                            || боты.текущий.бот.координатаX + 40 >= Герой.координатаX && боты.текущий.бот.координатаX + 40 <= Герой.координатаX + 40 &&
                            боты.текущий.бот.координатаY + 40 >= Герой.координатаY && боты.текущий.бот.координатаY + 40 <= Герой.координатаY + 40)
                        {
                            сек = секунды;
                            игразакончена = true;
                            label3.Invoke(new MethodInvoker(
                                             delegate()
                                             {
                                                 label3.Text = "end";
                                             }));
                            поток_ботов.Abort();
                            поток_ботов.Join(500);
                            Поток_переодичной_отрисовки.Abort();
                            Поток_переодичной_отрисовки.Join(500);
                            врем.Abort();
                            врем.Join(500);
                           

                        }
                        if (боты.проверка_на_конец_очереди())
                        {
                            break;
                        }
                        боты.Передвинуться();
                    }
                }
            }
       }
       
        #endregion
        #region Рисуем
        private void Отрисовка()
        {
            while (true)
            {
                SetPicture(null);
            }
        }
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Pen p = new Pen(Color.Coral, 4);
            e.Graphics.DrawRectangle(p,Герой.координатаX,Герой.координатаY, 40, 40);
            
            //нарисуем панель жизек
            //рисуем большой прямоугольник с уникальным дизайном DX
            //рисуем сверху данный о главном герои жизки и там хпешки
            //рисуем главного героя в тех координатах в которых он есть
            if (!боты.проверка_на_пустоту())
            {
                боты.встать_в_начало();
                while (true)
                {
                    if(боты.текущий != null)
                    e.Graphics.DrawRectangle(System.Drawing.Pens.Green, боты.текущий.бот.координатаX, боты.текущий.бот.координатаY, 40, 40);
                    if (боты.проверка_на_конец_очереди())
                    {
                        break;
                    }
                    боты.Передвинуться();
                }
            }
            SolidBrush br = new SolidBrush(Color.Red);
            label1.Text = Герой.координатаX.ToString();
            label2.Text = Герой.координатаY.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
          
        }

        private void списокБотовToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Вывод_списка_ботов frm3 = new Вывод_списка_ботов(боты);
            frm3.Owner = this;
            frm3.Show();
        }

        private void Основная_форма_игры_FormClosing(object sender, FormClosingEventArgs e)
        {
            поток_ботов.Abort();
            поток_ботов.Join(500);
            Поток_переодичной_отрисовки.Abort();
            Поток_переодичной_отрисовки.Join(500);
            врем.Abort();
            врем.Join(500);
        }
    }
}
        #endregion
