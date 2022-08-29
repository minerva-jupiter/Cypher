using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Cypher
{
    internal class SortProgram
    {
        public void sort()
        {
            //復号プログラム

            //変数宣言
            int[] order = new int[100];
            int inNumber = 0;

            SHA512 shaM = new SHA512Managed(); //インスタンス

            //質問
            string whereEncrypted;
            string whereKeyFile;
            string whereSortedFile;
            string howLongDate;

            /*
            //自動テスト
            whereEncrypted = @"h:\encrypted.txt";
            whereKeyFile = @"h:\key.txt";
            whereSortedFile = @"h:\sorted.txt";
            howLongDate = "390";
            */

            Question question = new Question();
            whereEncrypted = question.Questions("Where is encrypted file?", false);
            while (Directory.Exists(whereEncrypted) == false)
            {
                Console.WriteLine("The folder not exists.");
                Console.WriteLine("Plese try another folder.");
            }
            whereKeyFile = question.Questions("Where is KeyFile?", true);
            whereSortedFile = question.Questions("Where will you create sorted file?", false);
            howLongDate = question.Questions("How Long the date file?", false);

            int howLongDateInt = int.Parse(howLongDate);

            //ハッシュ関数を用いてデータサイズを知らないかどうかで判断
            //元のハッシュ値を持ってくる
            byte[] hashOriginal = File.ReadAllBytes(whereEncrypted + @"\" + "Number");
            byte[] hashNumber;
            byte[] LengthByte = BitConverter.GetBytes(howLongDateInt);
            hashNumber = shaM.ComputeHash(LengthByte);

            if (BitConverter.ToInt32(hashOriginal) != BitConverter.ToInt32(hashNumber))
            {
                Console.WriteLine("You mistaken!");
                Console.WriteLine("The date Number is wrong!");
                Console.WriteLine("This program will stop by 10seconds.");
                Thread.Sleep(10000);
                Environment.Exit(0);
            }

            //byteのデータを長さの数だけ用意
            byte[] dates = new byte[howLongDateInt];
            byte[] sorded = new byte[howLongDateInt];

            int quotient;
            int amari;
            bool multipleJudgment;
            //データサイズに適した数ずつ引っこ抜く
            if (howLongDateInt % 100 == 0)
            {
                quotient = howLongDateInt / 100;
                amari = howLongDateInt % 100;
                multipleJudgment = true;
            }
            else
            {
                //100で割り切れない場合は、99で割って、その余りをすべてamariにぶち込む
                quotient = howLongDateInt / 99;
                amari = howLongDateInt - quotient * 99;
                multipleJudgment = false;
            }

            //ReadkeyFile
            try
            {
                ResdFiles resdFiles = new ResdFiles();
                order = resdFiles.ReadKeyFile(whereKeyFile);
            }
            catch (Exception e)
            {
                Console.WriteLine(e + "was happend.");
                Console.WriteLine("try again.");
                Task.Delay(10000);
                Environment.Exit(0);
            }


            //Read Encrypted File
            inNumber = 0;
            FileStream fs = new FileStream(whereEncrypted + @"\" + "Encrypted", FileMode.Open);
            byte[] encrypted = new byte[fs.Length];
            try
            {
                fs.Read(encrypted, 0, encrypted.Length);
                fs.Close();
            }
            catch(Exception e)
            {
                Console.WriteLine(e + "was happend.");
                Console.WriteLine("try again.");
                Task.Delay(10000);
                Environment.Exit(0);
            }
            Console.WriteLine("Encrypted date was readed");



            //sort
            inNumber = 0;
            int arreyNumber = 0;
            int indexNumber = 0;
            int indexStartNumber = 0;
            int indexEndNumber = 0;
            int outNumber = 0;
            if (multipleJudgment)
            {
                //100で割り切れないとき
                while (arreyNumber < 100)
                {
                    //参照すべき番号を検索
                    indexNumber = Array.IndexOf(order, arreyNumber);
                    //参照始めの行
                    indexStartNumber = indexNumber * quotient;
                    //参照終わり+1の行
                    indexEndNumber = (indexNumber + 1) * quotient;

                    //参照して"written"に代入
                    outNumber = indexStartNumber;
                    while (indexNumber < indexEndNumber)
                    {
                        sorded[inNumber] = encrypted[outNumber];
                        indexNumber++;
                        outNumber++;
                    }
                    arreyNumber++;
                }
            }
            else
            {
                //100で割り切れない数の場合
                //余りの位置を特定
                int whereAmari = Array.IndexOf(order, 99);

                while (arreyNumber < 100)
                {
                    indexNumber = Array.IndexOf(order, arreyNumber); //inNumber 80
                    //indexNumberとamariの関係で場合分け
                    if (indexNumber < whereAmari)
                    {
                        //indexNumberがamariの方が小さいとき。
                        indexStartNumber = indexNumber * quotient;
                        indexEndNumber = (indexNumber + 1) * quotient;
                    }
                    else if (indexNumber == whereAmari)
                    {
                        indexStartNumber = indexNumber * quotient;
                        indexEndNumber = indexNumber * quotient + amari;
                    }
                    else
                    {
                        indexStartNumber = (indexNumber - 1) * quotient + amari;
                        indexEndNumber = indexNumber * quotient + amari;
                    }
                    outNumber = indexStartNumber;
                    while (outNumber < indexEndNumber)
                    {
                        sorded[inNumber] = encrypted[outNumber];
                        outNumber++;
                        inNumber++;
                    }
                    arreyNumber++;
                }
            }
            Console.WriteLine("The date was sorted.");


            //データを元の長さに整理
            //ファイルサイズが100以下の場合
            if (howLongDateInt < 100)
            {
                Array.Copy(sorded, 0, dates, 0, 100 - howLongDateInt);
            }
            else
            {
                dates = sorded;
            }

            //元のデータのハッシュと突き合わせ
            byte[] hashdate;
            hashdate = shaM.ComputeHash(dates);
            byte[] hashOriginalDate;
            hashOriginalDate = File.ReadAllBytes(whereEncrypted + @"\" + "Date");
            if (BitConverter.ToInt32(hashdate) != BitConverter.ToInt32(hashOriginalDate))
            {
                Console.WriteLine("The sorted file was not Original File.");
                Console.WriteLine("Something wrong. ex,key.");
            }
            else
            {
                //あっていたら、書きこみ。
                File.WriteAllBytes(whereSortedFile, dates);
            }

        }
    }
}