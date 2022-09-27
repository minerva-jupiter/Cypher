using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cypher
{
    internal class ResdFiles
    {
        public int[] ReadKeyFile(string whereKeyFile)
        {
            int[] order = new int[1000];
            int inNumber = 0;
            //keyファイルが1000行か確認する
            string[] lines = File.ReadAllLines(whereKeyFile);
            if (lines.Length != 1000)
            {
                Console.WriteLine("The key file you selected is not key file");
                Console.WriteLine("We can't continu this program!");
                Console.WriteLine("We will stop by 10seconds.");
                Thread.Sleep(10000);
                Environment.Exit(0);
            }
            //順序ファイルの読み込み
            StreamReader sr = new StreamReader(whereKeyFile);
            //配列に順序を読み込み
            while (inNumber < 1000)
            {
                order[inNumber] = int.Parse(sr.ReadLine());
                inNumber++;
            }
            sr.Close();
            Console.WriteLine("key file was readed");
            return order;
        }
    }
}
