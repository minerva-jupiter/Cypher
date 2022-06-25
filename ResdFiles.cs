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
            int[] order = new int[100];
            int inNumber = 0;
            //順序ファイルの読み込み
            StreamReader sr = new StreamReader(whereKeyFile);
            //配列に順序を読み込み
            while (inNumber < 100)
            {
                order[inNumber] = int.Parse(sr.ReadLine());
                inNumber++;
            }
            Console.WriteLine("key file was readed");
            return order;
        }
    }
}
