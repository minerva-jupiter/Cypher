using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cypher
{
    internal class Encrypt
    {
        public void encrypt()
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



            inNumber = 0;

            //100dyteを満たさない場合は残りのファイルにゼロを代入
            if (bs.Length < 100)
            {
                Console.WriteLine("File size is less than 100");
                int dateLength = bs.Length;
                while (dateLength == 100)
                {
                    bs[dateLength + 1] = 0;
                    dateLength = bs.Length;
                }
            }

            //date[]にビット単位で百分割したファイルを保存
            //参照したファイルのビット数が百の倍数か判定する。
            if (bs.Length % 100 == 0)
            {
                //100の倍数の場合
                Console.WriteLine("date.Length mod10 = 0");
                inNumber = 0;
                quotient = bs.Length / 100;
                //ファイルを百分割してdata配列に代入
                while (arrayNumber < 100)
                {
                    f = f + quotient + 1;
                    while (inNumber < f)
                    {
                        eByte[0] = bs[inNumber];
                        date[arrayNumber] = Encoding.GetEncoding("UTF-8").GetString(eByte);
                        inNumber++;
                    }
                    Console.WriteLine(arrayNumber + "まで代入完了");
                    arrayNumber++;
                }
            }
            else
            {
                Console.WriteLine("date.Length mod10 =! 0");
                quotient = bs.Length / 99;
                //100の倍数ではないとき
                f = 0;
                inNumber = 0;
                f = f + quotient - 1;
                while (arrayNumber < 99)
                {
                    while (inNumber < f)
                    {
                        eByte[0] = bs[inNumber];
                        date[arrayNumber] = date[arrayNumber] + Encoding.GetEncoding("UTF-8").GetString(eByte);
                        inNumber++;
                    }
                    Console.WriteLine(arrayNumber + "まで代入完了");
                    arrayNumber++;
                    f = f + quotient;
                }
                //余りをdate[100]に代入
                while (inNumber <= bs.Length - 1)
                {
                    eByte[0] = bs[inNumber];
                    date[arrayNumber] = date[arrayNumber] + Encoding.GetEncoding("UTF-8").GetString(eByte);
                    inNumber++;
                }
                Console.WriteLine(arrayNumber + "まで代入完了");
            }

            //encryptedファイルの生成を開始
            //配列の順に検索
            inNumber = 0;
            int b;
            StreamWriter outputFile = new StreamWriter(whereEncryptedFile);
            while (inNumber < date.Length)
            {
                //参照すべき行を検索
                b = order[inNumber];

                //参照して"written"に代入
                written = date[b];

                //"encrypted"に書き込み

                outputFile.WriteLine(written);


                Console.WriteLine(inNumber + "まで代入完了");
                inNumber++;
            }
            outputFile.Close();
            Console.WriteLine("Encrypt was done!");
            Console.WriteLine("The date key is " + bs.Length + ".");
            Console.WriteLine("You have to remamber it!");
        }

    }
}
