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
        Key key = new Key();
        key.createKey();
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


