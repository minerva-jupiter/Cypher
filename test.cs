﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cypher;

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
            ResdFiles resdFiles = new ResdFiles();
            order = resdFiles.ReadKeyFile(whereKeyFile);


            //ファイルの内容をすべて読み込む
            FileStream fs = new FileStream(whereDateFile, FileMode.Open);
            byte[] bs = new byte[fs.Length];
            fs.Read(bs, 0, bs.Length);
            fs.Close();
            Console.WriteLine("Date was readed to byte.");

            byte[] encrypted = new byte[bs.Length];

            //ここまでテスト完了
            int indexNumuber;
            int indexEnd;
            int indexStart;
            int outNumber = 0;
            if (bs.Length % 0 == 0)
            {
                quotient = bs.Length / 100; 
                //百で割り切れる場合
                while(arrayNumber < 100)
                {
                    indexNumuber = Array.IndexOf(order, arrayNumber);
                    indexStart = quotient * indexNumuber;
                    indexEnd = quotient * (indexNumuber+1);
                    inNumber = indexStart;
                    while (inNumber < indexEnd)
                    {
                        encrypted[outNumber] = bs[inNumber];
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
                {
                    while(arrayNumber < 100)
                    {
                        indexNumuber = Array.IndexOf(order, arrayNumber);
                        if(indexNumuber < where99)
                        {
                            indexStart = quotient * indexNumuber;
                            indexEnd = quotient * (indexNumuber + 1);
                        }else if(indexNumuber == where99){
                            indexStart = where99 * quotient;
                            indexEnd = (where99+1) * quotient;
                        }
                        else
                        {
                            indexStart = (indexNumuber-1) * quotient + amari;
                            indexEnd = indexNumuber * quotient + amari;
                        }
                        inNumber = indexStart;
                        while(inNumber < indexEnd)
                        {
                            encrypted[outNumber] = bs[inNumber];
                            outNumber++;
                        }
                        arrayNumber++;
                    }
                }
            }
            



        }


        public void Writedate(byte[] date)
        {
            int inNumber = 0;
            while (inNumber < date.Length)
            {
                Console.WriteLine(date[inNumber]);
                inNumber++;
            }
        }

        public void CreateTestDate()
        {
            Question question = new Question();
            string whereCreateDateFile = question.Questions("where do you want to create test date file?", false);
            int[] date = new int[100];
            for (int i = 0; i < date.Length; i++)
            {
                date[i] = i;
            }
            StreamWriter streamWriter = new StreamWriter(whereCreateDateFile);
            for (int i = 0; i < date.Length; i++)
            {
                streamWriter.WriteLine(date[i]);
            }
            streamWriter.Close();
        }

        public void Writedate(string[] date)
        {
            int inNumber = 0;
            while (inNumber < date.Length)
            {
                Console.WriteLine(date[inNumber]);
                inNumber++;
            }
        }
    }
}
