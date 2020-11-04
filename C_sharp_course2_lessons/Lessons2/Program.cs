using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lessons2
{
    class Program
    {
       

        static void Main(string[] args)
        {
           
            Hourly_Pay hp = new Hourly_Pay { Name = "Сергей", Position = "Слесарь", Payment = 203 };
            Hourly_Pay hp1 = new Hourly_Pay { Name = "Вячеслав", Position = "Маляр", Payment = 253 };
            Hourly_Pay hp2 = new Hourly_Pay { Name = "Игорь", Position = "Гибщик", Payment = 290 };

            Fixed_Pay fp = new Fixed_Pay { Name = "Алишер", Position = "Инженер", Payment = 24100 };
            Fixed_Pay fp2 = new Fixed_Pay { Name = "Дарья", Position = "Финансист", Payment = 25400 };
            Fixed_Pay fp3 = new Fixed_Pay { Name = "Талгат", Position = "Комплектовщик", Payment = 25100 };

            hp.Wages();
            hp2.Wages();
            hp1.Wages();

            fp.Wages();
            fp2.Wages();
            fp3.Wages();

            ListWorker[] jobteam = new ListWorker[6] { hp,hp1,hp2,fp,fp2,fp3 };
            Array.Sort(jobteam);

            foreach (ListWorker p in jobteam)
            {
                Console.WriteLine($"{p.Mounthly_Pay}");
                
            }
            Console.WriteLine();
            Console.ReadKey();
        }
    }
}
