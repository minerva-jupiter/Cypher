using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cypher
{
    internal class Test
    {
        public void test()
        {
            //暗号化

            //変数宣言
            int[] order = new int[100];
            string written;
            string[] date = new string[100];
            int quotient; //商
            int amari = 0; //あまり
            int inNumber = 0; //入力データ番号
            int arrayNumber = 0; //配列番号
            int chapter = 0;
            int f = 0;
            byte[] eByte = new byte[1];

            //質問
            string whereDateFile;
            string whereKeyFile;
            string whereEncryptedFile;

            //自動テスト
            whereDateFile = @"h:\date.txt";
            whereEncryptedFile = @"h:\encrypted.txt";
            whereKeyFile = @"h:\key.txt";

            /*
            whereEncryptedFile = Question("Where will you create encrypted file?", false);
            whereDateFile = Question("Where is the file you want to encrypt?", true);
            whereKeyFile = Question("Where is keyFile?", true);
            */

            //順序ファイルの読み込み
            StreamReader sr = new StreamReader(whereKeyFile);
            Console.WriteLine("KeyFile was readed");


            //配列に順序を書き込み
            while (inNumber < 100)
            {
                order[inNumber] = int.Parse(sr.ReadLine());
                inNumber++;
            }
            Console.WriteLine("Order was written to int.");


            //ファイルの内容をすべて読み込む
            FileStream fs = new FileStream(whereDateFile, FileMode.Open);
            byte[] bs = new byte[fs.Length];
            fs.Read(bs, 0, bs.Length);
            fs.Close();
            Console.WriteLine("Date was readed to byte.");


        }
    }
}
