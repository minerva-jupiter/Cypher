using System.Text;
using System;
using System.IO;
using Cypher;

string answers;
Console.WriteLine("Hello world. I woke up.");
do{
    answers = Control();
}while (answers != "end") ;

static string Control()
{
    Question question = new Question();
    string answer;
    answer = question.Questions("What do you want to do?",false);
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
            Encrypt encrypt = new Encrypt();
            encrypt.encrypt();
        }
        else
        {
            if(answer == "sort")
            {
                SortProgram sortProgram = new SortProgram();
                sortProgram.sort();
            }
            else
            {
                if (answer == "test")
                {
                    Test test = new Test();
                    test.test();
                }
            }
        }
    }
    return answer;
}

static void createKey()
{
    Question question = new Question();
    string whereKeyFile = question.Questions("Where will you want to create the keyFile?",false);
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

