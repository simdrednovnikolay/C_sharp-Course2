using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Николай Реднов;
namespace Lessons2
{
    class ListWorker : IComparable
    {
        public string Name;
        public string Position;
        public int Payment;
        public int Mounthly_Pay;

        
        public int CompareTo(object obj)
        {
            if (Mounthly_Pay > ((ListWorker)obj).Mounthly_Pay) return 1;
            if (Mounthly_Pay < ((ListWorker)obj).Mounthly_Pay) return -1;
            return 0;
        }
    }

    abstract class BaseWorker: ListWorker
    {
      
        public abstract void Wages();

    }
    // почасовая оплата
    class Hourly_Pay : BaseWorker
    {
        
        public override void Wages()
        {
            Mounthly_Pay = (int)(20.8 * 8 * Payment);
            Console.WriteLine(Name);
            Console.WriteLine(Position);
            Console.WriteLine(Mounthly_Pay);
            Console.ReadKey();

        }
    }
    // фиксированная оплата
    class Fixed_Pay : BaseWorker
    {
        
        public override void Wages()
        {
            Mounthly_Pay = Payment;
            Console.WriteLine(Name);
            Console.WriteLine(Position);
            Console.WriteLine(Mounthly_Pay);
            Console.ReadKey();

        }
    }
}
