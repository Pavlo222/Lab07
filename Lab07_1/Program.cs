using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab07_1
{
    public class Worker
    {
        public string Name = "";
        public float Payment = 0;
        public int Years = 0;
        public float Weight = 0;
        public float WorkTime = 0;
        public Worker(string Name = "", float Payment = 0, int Years = 0, float Weight = 0, float WorkTime = 0)
        {
            this.Name = Name;
            this.Payment = Payment;
            this.Years = Years;
            this.Weight = Weight;
            this.WorkTime = WorkTime;
        }
        public int CompareTo(Worker p)
        {
            return this.Years.CompareTo(p.Years);
        }
        public class SortByPaymanet : IComparer
        {

            public int Compare(object pp1, object pp2)
            {
                Worker p1 = (Worker)pp1;
                Worker p2 = (Worker)pp2;
                if (p1.Payment < p2.Payment)
                    return 1;
                else if (p1.Payment > p2.Payment)
                    return -1;
                else
                    return 0;
            }
        }
        public class SortByYears : IComparer
        {
            public int Compare(object pp1, object pp2)
            {
                Worker p1 = (Worker)pp1;
                Worker p2 = (Worker)pp2;
                if (p1.Years > p2.Years)
                    return 1;
                else if (p1.Years < p2.Years)
                    return -1;
                else
                    return 0;
            }
        }
        public class SortByYearsAndPayment : IComparer
        {
            public int Compare(object pp1, object pp2)
            {
                Worker p1 = (Worker)pp1;
                Worker p2 = (Worker)pp2;
                if (p1.Years > p2.Years)
                    return 1;
                else if (p1.Years < p2.Years)
                    return -1;
                else if (p1.Payment > p2.Payment)
                    return 1;
                else if (p1.Payment < p2.Payment)
                    return -1;
                else
                    return 0;
            }
        }
    }
    public class Workers : IEnumerable
    {
        int cnt = 0;
        Worker[] mas;
        public Workers(int count = 10)
        {
            mas = new Worker[count];
        }
        public void Add(Worker o)
        {
            if (cnt >= 10)
            {
                return;
            }
            mas[cnt] = o;
            cnt++;
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            for (int i = 0; i < cnt; ++i) yield return mas[i];
        }
        public void Sort()
        {
            Array.Sort(mas, new Worker.SortByPaymanet());
        }
    }
    class Program
    {
        public static void Table(Workers w)
        {
            Console.WriteLine("|    Назва     | Зарплата |Рокiв | Вага | Години роботи |");
            int MaxN = 14;
            int MaxP = 10;
            int MaxY = 6;
            int MaxW = 6;
            int MaxWT = 15;
            foreach (Worker x in w)
            {
                int nn = MaxN - Convert.ToString(x.Name).Length;
                int np = MaxP - Convert.ToString(x.Payment).Length;
                int ny = MaxY - Convert.ToString(x.Years).Length;
                int nw = MaxW - Convert.ToString(x.Weight).Length;
                int nwt = MaxWT - Convert.ToString(x.WorkTime).Length;
                Console.WriteLine("|" + Convert.ToString(x.Name) + PS(nn) + "|" + x.Payment + PS(np) + "|" +
                 Convert.ToString(x.Years) + PS(ny) + "|" + Convert.ToString(x.Weight) + PS(nw) + "|" + Convert.ToString(x.WorkTime) + PS(nwt) + "|");
            }
        }
        public static string PS(int count)
        {
            string s = "";
            for (int i = 0; i < count; i++)
            {
                s += " ";
            }
            return s;
        }
        static void Main(string[] args)
        {
            Workers workers = new Workers(10);
            Random r = new Random();

            for (int i = 0; i < 10; i++)
            {
                int rnd = r.Next(1, 20);
                workers.Add(new Worker("Робiтник №" + (i+1),  rnd * 1000, 22 + rnd, 3f * rnd+50f, rnd%20+2));
            }
            Console.WriteLine("Не впорядкований");
            Table(workers);
            workers.Sort();
            Console.WriteLine("\nВпорядкований за зарплатою");
            Table(workers);
            Console.ReadKey();
        }
    }
}
