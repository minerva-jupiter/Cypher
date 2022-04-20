using System.Text;
using System;
using System.IO;

static void main(string[] args)
{

}
void sort()
{
    //復号プログラム

    //変数宣言
    int[] order = new int[100];
    string written;
    int a = 0;
    int b = 0;

    //順序ファイルの読み込み
    StreamReader sr = new StreamReader(path: "D:\a.txt", encoding: Encoding.GetEncoding("UTF-8"));
    Console.WriteLine("読み込み終わり");

    //配列に順序を書き込み
    while (a < 100)
    {
        order[a] = int.Parse(sr.ReadLine());
        a++;
    }

    //配列の順に検索
    a = 0;
    StreamWriter writer = new StreamWriter("after.txt", true);
    while (a < 100)
    {
        //参照すべき行を検索
        b = Array.IndexOf(order, a);

        //参照して"written"に代入
        written = File.ReadLines(@"D:\date.txt").Skip(b).First();

        //"after"に書き込み
        Encoding sjisEnc = Encoding.GetEncoding("UTF-8");
        writer.WriteLine(written);

    }
    writer.Close();
}

void testCreate()
{
    int a = 0;
    int[] order;
    while (a < 100)
    {
        StreamWriter streamWriter = new StreamWriter(@"D:\a.txt", false, Encoding.GetEncoding("UTF-16"));
        streamWriter.WriteLine(a);
        Console.WriteLine(a + "を書き込んでいます。");
        a++;
    }
}

void createOrder()
{
    //順序ファイルの生成
    int random;
    int a = 0;
    int[] order = new int[100];
    //百個数字が埋められるまで繰り返す。
    while (a >= 100)
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
        //現在時刻を取得
        //this.dateTime = DateTime.Now;
        //下二桁を抽出
        random = random % 100;
        //書き出し
        Console.WriteLine(random);

        //生成した変数を"order"に代入
        order[a] = random;

        //if (Array.IndexOf(order, random) >= 0)
        //{
        //生成した変数を"order"に代入
        //order[a] = random;
        //a++;

    }
}

void encrypt()
{
    //変数宣言
    int[] order = new int[100];
    string written;
    int a = 0;
    int b = 0;

    //順序ファイルの読み込み
    StreamReader sr = new StreamReader(path: "D:\a.txt", encoding: Encoding.GetEncoding("UTF-8"));
    Console.WriteLine("読み込み終わり");

    //配列に順序を書き込み
    while (a < 100)
    {
        order[a] = int.Parse(sr.ReadLine());
        a++;
    }
    //配列の順に検索
    a = 0;
    while (a < 100)
    {
        //参照すべき行を検索
        b = Array.IndexOf(order, a);

        //参照して"written"に代入
        written = File.ReadLines(@"D:\after.txt").Skip(b).First();

        //"encrypted"に書き込み
        StreamWriter writer = new StreamWriter("encrypted.txt", true);
        writer.WriteLine(written);

    }
    writer.Close();
}