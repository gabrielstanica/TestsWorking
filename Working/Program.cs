using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace Working
{
    //6 - Race Condition - http://resources.infosecinstitute.com/multithreading/
    public class Test
    {
        //public void Calculation()
        //{
        //    for (int i = 0; i < 10; i++)
        //    {
        //        Thread.Sleep(new Random().Next(5));
        //        Console.Write(" {0},", i);
        //    }
        //    Console.WriteLine();
        //}


        //7 - Locks
        public object tLock = new object();

        public void Calculation()
        {
            lock (tLock)
            {
                Console.Write(" {0} is Executing", Thread.CurrentThread.Name);

                for (int i = 0; i < 10; i++)
                {
                    Thread.Sleep(new Random().Next(5));
                    Console.Write(" {0},", i);
                }
                Console.WriteLine();
            }
        }

    }


    class Program
    {
        /// <summary>
        /// Method to return the description from an Enum class
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string GetDescription(Enum value)
        {
            FieldInfo field = value.GetType().GetField(value.ToString());
            DescriptionAttribute attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
            return attribute == null ? value.ToString() : attribute.Description;
        }

        //public string ParseExcelFile(string pathToFile)
        //{
        //    Excel.Application myApp;
        //    Excel.Workbook xlWorkBook;
        //    Excel.Worksheet xlWorkSheet;
        //    Excel.Range range;

        //    myApp = new Excel.Application();
        //    myApp.Visible = false;
        //    xlWorkBook = myApp.Workbooks.Open(pathToFile);
        //    xlWorkSheet = (Excel.Worksheet)xlWorkBook.Sheets[1];
        //    int lastRow = xlWorkSheet.Cells.SpecialCells(Excel.XlCellType.xlCellTypeLastCell).Row;

        //    xlWorkSheet.PivotTables

        //    string gs = null;

        //    return gs;
        //}

        //2
        static public Dictionary<int, string> GetLine(string pathToFile)
        {
            string[] read = File.ReadAllLines(pathToFile);
            var contentLines = new Dictionary<int, string>();


            foreach (string line in read)
            {
                var dot = line.IndexOf(".");
                var number = line.Substring(0, dot);
                var text = line.Substring(dot + 1);

                contentLines.Add(int.Parse(number), text);
            }

            return contentLines;
        }

        //3
        static public void StartADB(string deviceId, string command)
        {
           Process process = new System.Diagnostics.Process();
           ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
           startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
           startInfo.FileName = "adb.exe ";
           startInfo.Arguments = "-s" + deviceId + " shell" + command;
           startInfo.UseShellExecute = false;
           process.StartInfo = startInfo;
           process.Start();
           process.WaitForExit();

        }

        //3
        //public void ThreadsADB()
        //{
        //    Thread thread1 = new Thread(new ThreadStart(StartADB));
        //    Thread thread2 = new Thread(new ThreadStart(StartADB));
        //    thread1.Start();
        //    thread2.Start();
        //    thread1.Join();
        //    thread2.Join();
        //}


        static void myFun()
        {
            //4
            //Console.WriteLine("Running other Thread");

            //5
            Console.WriteLine("Thread {0} started", Thread.CurrentThread.Name);
            Thread.Sleep(2000);
            Console.WriteLine("Thread {0} completed", Thread.CurrentThread.Name);

        }


        static void Main(string[] args)
        {
            //Console.WriteLine(attribute.Description);
            //Console.ReadLine();
            //Console.WriteLine(@"../TextFile1.txt");
            //string dir = Directory.GetCurrentDirectory();
            //Console.WriteLine(@dir + "/TextFile1.txt");

            //2
            //to read from a text file and memo all lines in a dictionary
            //var lines = new Dictionary<int, string>();
            //lines = GetLine("TextFile1.txt");
            //foreach(var number in lines.Keys)
            //{
            //    Console.WriteLine(String.Format("Keys {0} - Value {1}", number, lines[number]));
            //}

            //4
            //Thread t = new Thread(myFun);
            //t.Start();
            //Console.WriteLine("Main thread Running");

            //5
            //Thread t = new Thread(myFun);
            //t.Name = "Thread1";
            //t.IsBackground = true;
            //t.Start();
            //Console.WriteLine("Main thread Running");
            //Thread.Sleep(1000);
            //Console.WriteLine("Main thread finished");

            //6
            //Test t = new Test();
            //Thread[] tr = new Thread[5];

            //for (int i = 0; i < 5; i++)
            //{
            //    tr[i] = new Thread(new ThreadStart(t.Calculation));
            //    tr[i].Name = String.Format("Working Thread: {0}", i);
            //    Console.WriteLine(tr[i].Name);
            //}
            ////Start each thread
            //foreach (Thread x in tr)
            //{
            //    x.Start();
            //}


            //7 - Locks
            Test t = new Test();
            Thread[] tr = new Thread[5];

            for (int i = 0; i < 5; i++)
            {
                tr[i] = new Thread(new ThreadStart(t.Calculation));
                tr[i].Name = String.Format("Working Thread: {0}", i);
            }
            //Start each thread
            foreach (Thread x in tr)
            {
                x.Start();
            }

            Console.ReadLine();


        }


    }
}
