using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Cypher;

namespace Cypher
{
    internal class CypherChecker
    {
        public void Inspection()
        {
            int countChecker = 0;
            bool complete = false;
            //鍵の読み込み
            Question question = new Question();
            ResdFiles resdFiles = new ResdFiles();
            //int[] key;
            //string whereKeyFile = question.Questions("Where key file", true);
            //key = resdFiles.ReadKeyFile(whereKeyFile);
            double[] key2Double;
            Key key1 = new Key();

            //鍵を作って比較
            Console.WriteLine("Please wait a minuits");
            Console.WriteLine("Starting the inspection...");
            var sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            Task.Run(() =>
            {
                Console.ReadKey();
                Console.WriteLine("Number of verifications is " + countChecker);
                Console.WriteLine("Time taken is " + sw.ElapsedMilliseconds);
            });
            key2Double = key1.GeneratKey();
            //復号プログラム

            //変数宣言
            double[] order = new double[1000];
            int inNumber = 0;
            SHA512 shaM = new SHA512Managed(); //インスタンス

            //質問
            string whereEncrypted;
            string whereSortedFile;
            string howLongDate;


            //自動テスト
            whereEncrypted = @"e:\enc";
            whereSortedFile = @"e:\sorted.txt";
            howLongDate = "390";


            /*
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
            */

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
            if (howLongDateInt % 1000 == 0)
            {
                quotient = howLongDateInt / 1000;
                amari = howLongDateInt % 1000;
                multipleJudgment = true;
            }
            else
            {
                //1000で割り切れない場合は、999で割って、その余りをすべてamariにぶち込む
                quotient = howLongDateInt / 999;
                amari = howLongDateInt - quotient * 999;
                multipleJudgment = false;
            }

            //ReadkeyFile
            order = key2Double;

            //Read Encrypted File
            inNumber = 0;
            FileStream fs = new FileStream(whereEncrypted + @"\" + "Encrypted", FileMode.Open);
            byte[] encrypted = new byte[fs.Length];
            fs.Read(encrypted, 0, encrypted.Length);
            fs.Close();
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
                //1000で割り切れないとき
                while (arreyNumber < 1000)
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
                //1000で割り切れない数の場合
                //余りの位置を特定
                int whereAmari = Array.IndexOf(order, 999);

                while (arreyNumber < 1000)
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


            //データを元の長さに整理
            //ファイルサイズが1000以下の場合
            if (howLongDateInt < 1000)
            {
                Array.Copy(sorded, 0, dates, 0, 1000 - howLongDateInt);
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
            if (BitConverter.ToInt32(hashdate) == BitConverter.ToInt32(hashOriginalDate))
            {
                complete = true;
            }
            countChecker++;

            //繰り返し
            while (complete == false)
            {
                Task.Run(() =>
                {
                    key2Double = key1.GeneratKey();

                    //復号プログラム

                    //変数宣言
                    double[] order = new double[1000];
                    int inNumber = 0;
                    SHA512 shaM = new SHA512Managed(); //インスタンス

                    //質問
                    string whereEncrypted;
                    string whereSortedFile;
                    string howLongDate;


                    //自動テスト
                    whereEncrypted = @"e:\enc";
                    whereSortedFile = @"e:\sorted.txt";
                    howLongDate = "390";


                    /*
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
                    */

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
                    if (howLongDateInt % 1000 == 0)
                    {
                        quotient = howLongDateInt / 1000;
                        amari = howLongDateInt % 1000;
                        multipleJudgment = true;
                    }
                    else
                    {
                        //1000で割り切れない場合は、999で割って、その余りをすべてamariにぶち込む
                        quotient = howLongDateInt / 999;
                        amari = howLongDateInt - quotient * 999;
                        multipleJudgment = false;
                    }

                    //ReadkeyFile
                    ResdFiles resdFiles = new ResdFiles();
                    order = key2Double;






                    //sort
                    inNumber = 0;
                    int arreyNumber = 0;
                    int indexNumber = 0;
                    int indexStartNumber = 0;
                    int indexEndNumber = 0;
                    int outNumber = 0;
                    if (multipleJudgment)
                    {
                        //1000で割り切れないとき
                        while (arreyNumber < 1000)
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
                        //1000で割り切れない数の場合
                        //余りの位置を特定
                        int whereAmari = Array.IndexOf(order, 999);

                        while (arreyNumber < 1000)
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


                    //データを元の長さに整理
                    //ファイルサイズが1000以下の場合
                    if (howLongDateInt < 1000)
                    {
                        Array.Copy(sorded, 0, dates, 0, 1000 - howLongDateInt);
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
                    if (BitConverter.ToInt32(hashdate) == BitConverter.ToInt32(hashOriginalDate))
                    {
                        complete = true;
                    }
                    countChecker++;
                });

            }
            sw.Stop();
            Console.WriteLine("Number of verifications is " + countChecker);
            Console.WriteLine("Time taken is " + sw.ElapsedMilliseconds);
        }
    }
}

