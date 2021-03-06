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
            string[] date = new string[100];
            int quotient; //商
            int amari = 0; //あまり
            int inNumber = 0; //入力データ番号
            int arrayNumber = 0; //配列番号
            byte[] eByte = new byte[1];


            //質問
            string whereDateFile;
            string whereKeyFile;
            string whereEncryptedFilefolder;
            string whereEncryptedfileName;
            string whereEncryptedFile;

            /*
            //自動テスト
            whereDateFile = @"h:\date.txt";
            whereEncryptedFile = @"h:\encrypted.txt";
            whereKeyFile = @"h:\key.txt";
            */

            Question question = new Question();
            whereEncryptedFilefolder = question.Questions("Where will you create encrypted file? (folder Name)", false);
            whereEncryptedfileName = question.Questions("What will you create encrypted file name?",false);
            whereEncryptedFile = whereEncryptedFilefolder + whereEncryptedfileName + ".cypher";
            whereDateFile = question.Questions("Where is the file you want to encrypt?", true);
            whereKeyFile = question.Questions("Where is keyFile?", true);
            

            //順序ファイルの読み込み
            StreamReader sr = new StreamReader(whereKeyFile);


            //配列に順序を書き込み
            ResdFiles resdFiles = new ResdFiles();
            order = resdFiles.ReadKeyFile(whereKeyFile);


            //ファイルの内容をすべて読み込む
            FileStream fs = new FileStream(whereDateFile, FileMode.Open);
            byte[] bs = new byte[fs.Length];
            fs.Read(bs, 0, bs.Length);
            fs.Close();

            byte[] encrypted = new byte[bs.Length];

            int indexNumuber;
            int indexEnd;
            int indexStart;
            int outNumber = 0;
            inNumber = 0;
            if (bs.Length % 100 == 0)
            {
                quotient = bs.Length / 100;
                //百で割り切れる場合
                while (arrayNumber < 100)
                {
                    indexNumuber = order[arrayNumber];
                    indexStart = quotient * indexNumuber;
                    indexEnd = quotient * (indexNumuber + 1);
                    outNumber = indexStart;
                    while (outNumber < indexEnd)
                    {
                        encrypted[outNumber] = bs[inNumber];
                        inNumber++;
                        outNumber++;
                    }
                    arrayNumber++;
                }
            }
            else
            {
                //割り切れない場合
                quotient = bs.Length / 99;
                amari = bs.Length - quotient * 99;
                int where99 = Array.IndexOf(order, 99);

                while (arrayNumber < 100)
                {
                    indexNumuber = Array.IndexOf(order, arrayNumber);
                    if (indexNumuber < where99)
                    {
                        indexStart = quotient * indexNumuber;
                        indexEnd = quotient * (indexNumuber + 1);
                    }
                    else if (indexNumuber == where99)
                    {
                        indexStart = indexNumuber * quotient;
                        indexEnd = indexNumuber * quotient + amari;
                    }
                    else
                    {
                        indexStart = (indexNumuber - 1) * quotient + amari;
                        indexEnd = indexNumuber * quotient + amari;
                    }


                    outNumber = indexStart;
                    while (outNumber < indexEnd)
                    {
                        encrypted[outNumber] = bs[inNumber];
                        outNumber++;
                        inNumber++;
                    }

                    arrayNumber++;
                }

            }
            //書きこ
            File.WriteAllBytes(whereEncryptedFile, encrypted);
            Console.WriteLine("Encrypt was ended.");
            Console.WriteLine("The date key is " + encrypted.Length + ".");
            Console.WriteLine("You have to remember it!");
        }

    }
}
