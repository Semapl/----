using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace змейка
{
   public class Элемент_списка_ботов
    {
        public Элемент_списка_ботов()
        {
            бот = new бот();
        }
       public Элемент_списка_ботов следущий;
       public бот бот;
    }
}
