using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab7_2
{
    public class Cloakroom
    {
        public string NameObject = "";
        public string LastName = "";
        public DateTime DateGet;
        public float SavingTime = 0;
        public int InvertalNum = 0;
        public Cloakroom(string NameObject = "", string LastName = "", float SavingTime = 0, int InvertalNum = 0)
        {
            this.NameObject = NameObject;
            this.LastName = LastName;
            this.SavingTime = SavingTime;
            this.InvertalNum = InvertalNum;
        }
        public int CompareTo(Cloakroom p)
        {
            return this.InvertalNum.CompareTo(p.InvertalNum);
        }
        public class SortByInvertalNum : IComparer<Cloakroom>
        {
            
            public int Compare(Cloakroom p1, Cloakroom p2)
            {
                if (p1.InvertalNum > p2.InvertalNum)
                    return 1;
                else if (p1.InvertalNum < p2.InvertalNum)
                    return -1;
                else
                    return 0;
            }
        }
        public class SortByDateAndTime : IComparer<Cloakroom>
        {
            public int Compare(Cloakroom p1, Cloakroom p2)
            {
                if (p1.DateGet > p2.DateGet)
                {
                    return 1;
                }
                else if (p1.DateGet < p2.DateGet)
                {
                    return -1;
                }
                else if (p1.SavingTime > p2.SavingTime)
                {
                    return 1;
                }
                else if (p1.SavingTime < p2.SavingTime)
                {
                    return -1;
                }
                else
                {
                    return 0;
                }
            }
        }
    }
    class Program
    {
        static OpenFileDialog fl = new OpenFileDialog();
        [STAThread]
        static void Main(string[] args)
        {
            List<Cloakroom> data = new List<Cloakroom>();
            fl.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            if (fl.ShowDialog() == DialogResult.OK)
            {
                data = ReadDate(fl.FileName);
            }
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Головне меню: \nA) Додавання записiв\nR) Знищення записiв\nC) Редагування записiв\nE) Виведення даних з файла \nS) Збереження змiн у файл\nD) Сортування за iнвентарним номером\nT) Сортування за термiном здачi i за датою зберiгання");
                var k = Console.ReadKey().Key;
                if (k == ConsoleKey.A)
                {
                    Add(data);
                }
                if (k == ConsoleKey.R)
                {
                    Console.Clear();
                    Table(data);
                    Remove(data);
                }
                if (k == ConsoleKey.C)
                {
                    Console.Clear();
                    Table(data);
                    ChangeData(data);
                }
                if (k == ConsoleKey.E)
                {
                    Console.Clear();
                    Table(data);
                    Console.WriteLine("Натиснiть будьяку кнопку для повернення в головне меню");
                    Console.ReadKey();
                }
                if (k == ConsoleKey.D)
                {
                    Console.Clear();
                    Console.WriteLine("Посуртуванi записи за iнвентарним номером: ");
                    data.Sort(new Cloakroom.SortByInvertalNum());
                    Table(data);
                    Console.WriteLine("Натиснiть будьяку кнопку для повернення в головне меню");
                    Console.ReadKey();
                }
                if (k == ConsoleKey.T)
                {
                    Console.Clear();
                    Console.WriteLine("Посуртуванi записи за термiном здачi i за датою зберiгання: ");
                    data.Sort(new Cloakroom.SortByDateAndTime());
                    Table(data);
                    Console.WriteLine("Натиснiть будьяку кнопку для повернення в головне меню");
                    Console.ReadKey();
                }
                if (k == ConsoleKey.S)
                {
                    SaveDate(data, fl.FileName);
                }
            }
        }
        static void Table(List<Cloakroom> v)
        {
            string[] Texts = new string[5];
            Texts[0] = "    Назва придмета    ";
            Texts[1] = " Прiзвище ";
            Texts[2] = " Дата здачi ";
            Texts[3] = " Термiн зберiгання ";
            Texts[4] = " Iнвентарний номер ";
            Console.WriteLine($"|{Texts[0]}|{Texts[1]}|{Texts[2]}|{Texts[3]}|{Texts[4]}|");
            foreach (Cloakroom vg in v)
            {
                Console.WriteLine("|"+ vg.NameObject + s(Texts[0].Length - vg.NameObject.Length) + "|" +
                    vg.LastName + s(Texts[1].Length - vg.LastName.Length) + "|" +
                    vg.DateGet.Date.ToString("dd.MM.yyyy") + s(Texts[2].Length - vg.DateGet.Date.ToString("dd.MM.yyyy").Length) + "|" +
                    vg.SavingTime + s(Texts[3].Length - vg.SavingTime.ToString().Length) + "|" +
                    vg.InvertalNum + s(Texts[4].Length - vg.InvertalNum.ToString().Length) + "|"
                    );
            }
        }
        static void Pere(String s,int i,out int num,out float nums)//+
        {
            num = 0;
            nums = 0;
            bool True = true;
            while (True)
            {
                Console.WriteLine(s);
                try
                {
                    True = false;
                    if(i==0)
                    {
                        num = Convert.ToInt32(Console.ReadLine());
                    }
                    else if(i==1)
                    {
                        nums = Convert.ToInt64(Console.ReadLine());
                    }
                }
                catch
                {
                    True = true;
                }
            }
        }
        static void Add(List<Cloakroom> v)//+
        {
            Console.Clear();
            Console.WriteLine("Режим додавання: ");
            Cloakroom New = new Cloakroom(); 
            Console.WriteLine("Введiть назву предмета: ");
            New.NameObject = Console.ReadLine();
            Console.WriteLine("Введiть прiзвище: ");
            New.LastName = Console.ReadLine();
            bool True = true;
            while(True)
            {
                Console.WriteLine("Введiть дату здачi: ");
                try
                {
                    True = false;
                    New.DateGet = DateTime.ParseExact(Console.ReadLine(), "dd.MM.yyyy", CultureInfo.InvariantCulture);
                }
                catch
                {
                    True = true;
                }
            }
            Pere("Введiть термiн зберiгання: ", 1, out int gh,out New.SavingTime);
            Pere("Введiть iнвентарний номер: ", 0, out New.InvertalNum, out float hg);
            v.Add(New);
        }
        static void Remove(List<Cloakroom> v)//+
        {
            Pere("Введiть iнвентарний номер запис якого потрiбно видалити: ", 0, out int num, out float nums);
            v.RemoveAt(v.FindIndex(f => f.InvertalNum == num));
        }
        static void ChangeData(List<Cloakroom> v)//+
        {
            Pere("Введiть iнвентарний номер запис якого потрiбно редагувати: ", 0, out int num, out float nums);
            if ((v.FindIndex(f => f.InvertalNum == num) != -1))
            {
                Cloakroom Change = v[v.FindIndex(f => f.InvertalNum == num)];
                Console.WriteLine("1)Редагування назви предмета\n2)Редагування прiзвища\n3)Редагування дати здачi\n4)Редагування термiну зберiгання\n5)Редагування iнвентарного номера");
                var res = Console.ReadKey().KeyChar;
                if (res == '1')
                {
                    Console.WriteLine("\nВведiть нове значення: ");
                    Change.NameObject = Console.ReadLine();
                }
                if (res == '2')
                {
                    Console.WriteLine("\nВведiть нове значення: ");
                    Change.LastName = Console.ReadLine();
                }
                if (res == '3')
                {
                    Console.WriteLine("\nВведiть нове значення: ");
                    Change.DateGet = DateTime.ParseExact(Console.ReadLine(), "dd.MM.yyyy", CultureInfo.InvariantCulture);
                }
                if (res == '4')
                {
                    Pere("\nВведiть нове значення: ", 1, out num, out Change.SavingTime);
                }
                if (res == '5')
                {
                    Pere("\nВведiть нове значення: ", 0, out Change.InvertalNum, out float nams);
                }
            }
            else
            {
                Console.WriteLine("Введений iнвентарний номер не є дiйсний");
                Console.ReadKey();
            }

        }
        public static string s(int c)//+
        {
            try
            {
                return new String(' ', c);
            }
            catch
            {
                return "";
            }
        }
        public static void SaveDate(List<Cloakroom> Date, string path)//+
        {
            using (StreamWriter sw = new StreamWriter(path, false, System.Text.Encoding.Default))
            {
                foreach (Cloakroom g in Date)
                {

                    sw.WriteLine(g.NameObject.Trim() + "|" + g.LastName + "|" + g.DateGet.Date.ToString("dd.MM.yyyy") + "|" + g.SavingTime + "|" + g.InvertalNum + "/");

                }
            }
        }
        public static List<Cloakroom> ReadDate(string path)//+
        {
            List<Cloakroom> g = new List<Cloakroom>();
            string text = "";
            using (StreamReader sr = new StreamReader(path, Encoding.Default))
            {
                text = sr.ReadToEnd();
            }
            string[] Dates = text.Split('/');
            foreach (string s in Dates)
            {
                string[] MetaDete = s.Split('|');
                if (MetaDete.Length == 5)
                {
                    Cloakroom d = new Cloakroom
                    {
                        NameObject = MetaDete[0].Trim(),
                        LastName = MetaDete[1],
                        DateGet = DateTime.ParseExact(MetaDete[2], "dd.MM.yyyy", CultureInfo.InvariantCulture),
                        SavingTime = (float)Convert.ToDouble(MetaDete[3]),
                        InvertalNum = Convert.ToInt32(MetaDete[4])
                    };
                    g.Add(d);
                }
            }
            return g;
        }
    }
}
