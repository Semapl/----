using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace змейка
{
  public  class Участник:Обьект
    {
        public void передвинуться(int x, int y)
        {

            координатаX = координатаX + x*скорость;
            координатаY = координатаY + y*скорость;
            if (координатаX > 900) координатаX = 900;
            if (координатаY > 300) координатаY = 300;
            if (координатаX < 0) координатаX = 0;
            if (координатаY < 0) координатаY = 0;
        }
        public int hp;
        public int жизки;
       // public void огонь(ссылка на список);
    }
}
