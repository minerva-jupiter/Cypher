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
    answer = Question("What do you want to do?");
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
        }
    }
    return answer;
}


static string Question(string text)
{
    Console.WriteLine(text);
    string answer = Console.ReadLine();
    while(answer == null)
    {
        Console.WriteLine("Plese type anything");
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
    int a = 0;
    int b = 0;
    byte[] dates = new byte[100];
    int o1;

    //質問
    string whereEncrypted;
    whereEncrypted = Question("Where is encrypted file?");
    string whereKeyFile;
    whereKeyFile = Question("Where is KeyFile?");
    string whereSortedFile;
    whereSortedFile = Question("Where will you create sorted file?");
    string howLongDate;
    howLongDate = Question("How Long the date file?");
    int howLongDateInt = int.Parse(howLongDate);

    //順序ファイルの読み込み
    StreamReader sr = new StreamReader(whereKeyFile, encoding: Encoding.GetEncoding("UTF-8"));

    //配列に順序を読み込み
    while (a < 100)
    {
        order[a] = int.Parse(sr.ReadLine());
        a++;
    }

    //配列の順に検索
    a = 0;
    StreamWriter writer = new StreamWriter(whereSortedFile, true);
    while (a < 100)
    {
        //参照すべき行を検索
        b = Array.IndexOf(order, a);

        //参照して"written"に代入
        written[a] = File.ReadLines(whereEncrypted).Skip(b).First();

        //stringをbyteに変換して代入
        a++;
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
    //順序ファイルの生成
    int random;
    int count = 0;
    int[] order = new int[100];
    //百個数字が埋められるまで繰り返す。
    while (count >= 100)
    {
        //Int32と同じサイズのバイト配列にランダムな値を設定する
        //byte[] bs = new byte[sizeof(int)];
        byte[] bs = new byte[4];
        System.Security.Cryptography.RNGCryptoServiceProvider rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
        rng.GetBytes(bs);
        //.NET Framework 4.0以降
        rng.Dispose();

        //Int32に変換する
        random = System.BitConverter.ToInt32(bs, 0);

        //下二桁の抽出
        random = random % 100;

        //書き出し
        //Console.WriteLine(random);

        //今までにない数かどうかを評価
        if (Array.IndexOf(order, random) >= 0)
        {
            //生成した変数を"order"に代入
            order[count] = random;
            count++;
        }
    }

    //keyFileに書き出し
    string whereKeyFile = Question("Where will you want to create the keyFile?");
    StreamWriter streamWriter = new StreamWriter(whereKeyFile, true);
    for(count= 0; count < order.Length; count++)
    {
        streamWriter.WriteLine(order[count]);
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
    int a = 0;
    int b = 0;
    int c = 0;
    int d = 0;
    int chapter = 0;
    string e;
    int f = 0;
    byte[] o1 = new byte[1];

    //質問
    string whereDateFile;
    whereDateFile = Question("Where is the file you want to encrypt?");
    string whereKeyFile;
    whereKeyFile = Question("Where is keyFile?");
    string whereEncryptedFile;
    whereEncryptedFile = Question("Where will you create encrypted file?");

    //順序ファイルの読み込み
    StreamReader sr = new StreamReader(whereKeyFile);
    Console.WriteLine("読み込み終わり");

    //配列に順序を書き込み
    while (a < 100)
    {
        order[a] = int.Parse(sr.ReadLine());
        a++;
    }
    //データ配列をファイルに書き込む

    //ファイルをバイトごとに分割

    //ファイルを開く
    System.IO.FileStream fileStream = new System.IO.FileStream(
        whereDateFile,
        System.IO.FileMode.Open,
        System.IO.FileAccess.Read);

    //ファイルを読み込むバイト型配列を作成する
    byte[] bs = new byte[fileStream.Length];

    //ファイルの内容をすべて読み込む
    fileStream.Read(bs, 0, bs.Length);

    //閉じる
    fileStream.Close();
    

    //100dyteを満たさない場合は残りのファイルにゼロを代入
    if(bs.Length > 100)
    {
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
        chapter = bs.Length / 100;
        d = chapter;
        c = 0;
        //ファイルを百分割してdata配列に代入
        while (a < 100)
        {
            o1[0] = bs[c];
            e = BitConverter.ToString(o1);
            c++;
            while (d + chapter >= c)
            {
                o1[0] = bs[c];
                e += BitConverter.ToString(o1);
                c++;
            }
            d = d + chapter;

            date[a] = e;
            a++;
        }
    }
    else
    {
        //長さを99で割って、均等に入れる。
        c = 0;
        a = 0;
        b = bs.Length / 99;
        d = bs.Length % 99;
        f = b;
        while (a < 99)
        {
            o1[0] = bs[c];
            e = BitConverter.ToString(o1);
            c++;
            while (f > c + b)
            {
                o1[0] = bs[c];
                e += BitConverter.ToString(o1);
                c++;
            }
            f = f + b;

            a++;
        }
        //100個めに余りを入れる。
        o1[0] = bs[c];
        e = BitConverter.ToString(o1);
        while (d >= c + b)
        {
            o1[0] = bs[c];
            e += BitConverter.ToString(o1);
        }
        date[100] = e;
    }
    //データの全体のサイズを表示
    Console.WriteLine(bs.Length);


    //encryptedファイルの生成を開始
    //配列の順に検索
    a = 0;
    StreamWriter writer = new StreamWriter(whereEncryptedFile);
    while (a < date.Length)
    {
        //参照すべき行を検索
        b = order[a];

        //参照して"written"に代入
        written = File.ReadLines(date[a]).Skip(a).First();

        //"encrypted"に書き込み
        
        writer.WriteLine(written);

        a++;
        
    }
    writer.Close();
}
