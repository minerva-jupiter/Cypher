using System.Text;
using System;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;
using Microsoft.Win32.SafeHandles;

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
        while(answer == null | File.Exists(answer))
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
    int b = 0;
    byte[] dates = new byte[100];

    //質問
    string whereEncrypted;
    whereEncrypted = Question("Where is encrypted file?",true);
    string whereKeyFile;
    whereKeyFile = Question("Where is KeyFile?",true);
    string whereSortedFile;
    whereSortedFile = Question("Where will you create sorted file?",false);
    string howLongDate;
    howLongDate = Question("How Long the date file?",false);
    int howLongDateInt = int.Parse(howLongDate);

    //順序ファイルの読み込み
    StreamReader sr = new StreamReader(whereKeyFile, encoding: Encoding.GetEncoding("UTF-8"));

    //配列に順序を読み込み
    while (inNumber < 100)
    {
        order[inNumber] = int.Parse(sr.ReadLine());
        inNumber++;
    }

    //配列の順に検索
    inNumber = 0;
    StreamWriter writer = new StreamWriter(whereSortedFile, true);
    while (inNumber < 100)
    {
        //参照すべき行を検索
        b = Array.IndexOf(order, inNumber);

        //参照して"written"に代入
        written[inNumber] = File.ReadLines(whereEncrypted).Skip(b).First();

        //stringをbyteに変換して代入
        inNumber++;
    }
    writer.Close();

    //writtenを一つに統合
    writtens = string.Concat(written);
    
    //ファイルサイズが100以下の場合
    if(howLongDateInt < 100)
    {
        dates = Encoding.GetEncoding("UTF-8").GetBytes(string.Concat(new ArraySegment<string>(written, 0, howLongDateInt)));
    }
    else
    {
        dates = Encoding.GetEncoding("UTF-8").GetBytes(writtens);
    }




    //復元ファイルの生成
    File.WriteAllBytes(whereKeyFile, dates);
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
    whereDateFile = Question("Where is the file you want to encrypt?",true);

    string whereKeyFile;
    whereKeyFile = Question("Where is keyFile?",true);

    string whereEncryptedFile;
    whereEncryptedFile = Question("Where will you create encrypted file?", false);


    //順序ファイルの読み込み
    StreamReader sr = new StreamReader(whereKeyFile);
    Console.WriteLine("KeyFile was readed");

    //エンコード先ファイルの存在確認と生成
    if (File.Exists(whereEncryptedFile)==false)
    {
        File.Create(whereEncryptedFile);
    }

    //配列に順序を書き込み
    while (inNumber < 100)
    {
        order[inNumber] = int.Parse(sr.ReadLine());
        inNumber++;
    }
    Console.WriteLine("Order was written to int.");

    //ファイルを開く
    System.IO.FileStream fileStream = new System.IO.FileStream(
        whereDateFile,
        System.IO.FileMode.Open,
        System.IO.FileAccess.Read);
    Console.WriteLine("Date File was readed.");

    //ファイルを読み込むバイト型配列を作成する
    byte[] bs = new byte[fileStream.Length];

    //ファイルの内容をすべて読み込む
    fileStream.Read(bs, 0, bs.Length);
    Console.WriteLine("Date was readed to byte.");

    //閉じる
    fileStream.Close();
    
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
    StreamWriter outputFile = new(Path.Combine(whereEncryptedFile));
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
