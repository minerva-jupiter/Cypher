using System.Text;
using System;
using System.IO;

string answers;
Console.WriteLine("Hello world. I woke up.");
do{
    answers = Control();
}while (answers != "end") ;

static string Control()
{
    string answer;
    answer = Question("What do you want to do?",false);
    if(answer == "help")
    {
        Console.WriteLine("createKey is creating keyFile for Cryptography.");
        Console.WriteLine("encrypt is encrypting the date file you want with created keyFile.");
        Console.WriteLine("sort is sorting the encrypted file with keyFile.");
    }

    if(answer == "createKey")
    {
        createKey();
    }
    else
    {
        if(answer == "encrypt")
        {
            encrypt();
        }
        else
        {
            if(answer == "sort")
            {
                sort();
            }
            else
            {
                if (answer == "createTestDate")
                {
                    CreateDate();
                }
            }
        }
    }
    return answer;
}


static string Question(string text,bool exsis)
{
    Console.WriteLine(text);
    string answer = Console.ReadLine();
    if (exsis)
    {
        while(answer == null | File.Exists(answer) == false)
        {
            Console.WriteLine("You have not typed anything or you are specifying a file that does not exist");
            answer = Console.ReadLine();
        }
    }
    else
    {
        while (answer == null)
        {
            Console.WriteLine("Plese type anything");
            answer = Console.ReadLine();
        }
    }
    return answer;
}

static void sort()
{
    //復号プログラム

    //変数宣言
    int[] order = new int[100];
    string[] written = new string[100];
    string writtens;
    int inNumber = 0;

    //質問
    string whereEncrypted;
    string whereKeyFile;
    string whereSortedFile;
    string howLongDate;

    //自動テスト
    whereEncrypted = @"h:\encrypted.txt";
    whereKeyFile = @"h:\key.txt";
    whereSortedFile = @"h:\sorted.txt";
    howLongDate = "390";

    /*
    whereEncrypted = Question("Where is encrypted file?",true);
    whereKeyFile = Question("Where is KeyFile?",true);
    whereSortedFile = Question("Where will you create sorted file?",false);
    howLongDate = Question("How Long the date file?",false);
    */
    int howLongDateInt = int.Parse(howLongDate);

    //byteのデータを長さの数だけ用意
    byte[] dates = new byte[howLongDateInt];
    string[] encrypted = new string[howLongDateInt];
    string[] sorded = new string[howLongDateInt];

    int quotient;
    int amari;
    bool multipleJudgment;
    //データサイズに適した数ずつ引っこ抜く
    if (howLongDateInt%100 == 0)
    {
        quotient = howLongDateInt/100;
        amari = howLongDateInt%100;
        multipleJudgment = true;
    }
    else
    {
        //100で割り切れない場合は、99で割って、その余りをすべてamariにぶち込む
        quotient = howLongDateInt / 99;
        amari = howLongDateInt - quotient * 99;
        multipleJudgment = false;
    }

    //順序ファイルの読み込み
    StreamReader sr = new StreamReader(whereKeyFile);

    inNumber = 0;
    //配列に順序を読み込み
    while (inNumber < 100)
    {
        order[inNumber] = int.Parse(sr.ReadLine());
        Console.WriteLine(inNumber);
        inNumber++;
    }
    Console.WriteLine("key file was readed");


    //順番にデータを移行
    inNumber = 0;
    //encrypted の読み込み
    StreamReader streamReader = new StreamReader(whereEncrypted);
    inNumber = 0;
    while(inNumber <= howLongDateInt)
    {
        encrypted[inNumber] = streamReader.ReadLine();
        Console.WriteLine(inNumber);
        inNumber++;
    }
    streamReader.Close();

    inNumber = 0;
    int arreyNumber = 0;
    int indexNumber = 0;
    int indexStartNumber;
    int indexEndNumber;
    if (multipleJudgment)
    {
        //100で割り切れないとき
        while (arreyNumber < 100)
        {
            //参照すべき行を検索
            indexNumber = Array.IndexOf(order, arreyNumber);
            //参照始めの行
            indexStartNumber = indexNumber * quotient;
            //参照終わり+1の行
            indexEndNumber = (indexNumber + 1) * quotient;

            //参照して"written"に代入
            indexNumber = indexStartNumber;
            while(indexNumber < indexEndNumber)
            {
                sorded[inNumber] = encrypted[indexNumber];
                indexNumber++;
                inNumber++;
            }
            arreyNumber++;
        }
    }
    else
    {
        //100で割り切れない数の場合
        int whereAmari = Array.IndexOf(order, 99);
        while(arreyNumber < 100)
        {
            indexNumber = Array.IndexOf(order, arreyNumber);
            if(indexNumber > whereAmari)
            {
                //indexNumberがamariより大きいとき

            }
            

            arreyNumber++;
        }
    }


    //writtenを一つに統合
    writtens = string.Concat(written);

    //データを元の長さに整理
    //ファイルサイズが100以下の場合
    if (howLongDateInt < 100)
    {
        dates = Encoding.GetEncoding("UTF-8").GetBytes(string.Concat(new ArraySegment<string>(written, 0, howLongDateInt)));
    }
    else
    {
        dates = Encoding.GetEncoding("UTF-8").GetBytes(writtens);
    }
    
    //書きこ
    File.WriteAllBytes(whereSortedFile, dates);

}

static void createKey()
{
    string whereKeyFile = Question("Where will you want to create the keyFile?",false);
    //順序ファイルの生成
    int random;
    int count = 1;
    int[] order = new int[100];
    //百個数字が埋められるまで繰り返す。
    while (count < 100)
    {
        var randomer = new Random();
        random = randomer.Next(minValue: 0, maxValue: 100);

        //今までにない数かどうかを評価
        if (Array.IndexOf(order, random) < 0)
        {
            //生成した変数を"order"に代入
            order[count] = random;
            count++;
            Console.WriteLine(random);
        }
    }

    //keyFileに書き出し
    StreamWriter streamWriter = new StreamWriter(whereKeyFile);
    Console.WriteLine("Created StreamWriter");
    for(count= 0; count < order.Length; count++)
    {
        streamWriter.WriteLine(order[count]);
        Console.WriteLine(count + order[count]);
    }
    streamWriter.Close();
}

static void encrypt()
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
    if(bs.Length < 100)
    {
        Console.WriteLine("File size is less than 100");
        int dateLength=bs.Length;
        while(dateLength == 100)
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
        while(arrayNumber < 100)
        {
            f = f + quotient + 1;
            while(inNumber < f)
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
            while(inNumber < f)
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
        while(inNumber <= bs.Length - 1)
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

static void CreateDate()
{
    string whereCreateDateFile = Question("where do you want to create test date file?", false);
    int[] date = new int[100];
    for(int i = 0; i < date.Length; i++)
    {
        date[i] = i;
    }
    StreamWriter streamWriter = new StreamWriter(whereCreateDateFile);
    for(int i = 0; i < date.Length; i++)
    {
        streamWriter.WriteLine(date[i]);
    }
    streamWriter.Close();
}

static void Writedate(string[] date)
{
    int inNumber = 0;
    while(inNumber < date.Length)
    {
        Console.WriteLine(date[inNumber]);
        inNumber++;
    }
}