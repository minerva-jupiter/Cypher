using System.Text;
using System;
using System.IO;

Console.WriteLine("Hello world. I woke up.");
string answer;
answer = Question("What do you want to do?");
while(answer != "help")
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
    string written;
    int a = 0;
    int b = 0;

    //質問
    string whereEncrypted;
    whereEncrypted = Question("Where is encrypted file?");
    string whereKeyFile;
    whereKeyFile = Question("Where is KeyFile?");
    string whereSortedFile;
    whereSortedFile = Question("Where will you create sorted file?");

    //順序ファイルの読み込み
    StreamReader sr = new StreamReader(whereKeyFile, encoding: Encoding.GetEncoding("UTF-8"));

    //配列に順序を書き込み
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
        written = File.ReadLines(whereEncrypted).Skip(b).First();

        //sortedFileに書き込み
        writer.WriteLine(written);
        
        a++;
    }
    writer.Close();
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

    //ファイルを百分割

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


    a = 0;
    while (a < 100)
    {
        //百分割したファイルのそれぞれをdate配列に代入
        date[a] = System.Text.Encoding.ASCII.GetString(bs);
    }
    //配列の順に検索
    a = 0;
    StreamWriter writer = new StreamWriter(whereEncryptedFile);
    while (a < 100)
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
