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

    switch (answer)
    {
        case "createKey":
            Key key = new Key();
            key.createKey();
            break;

        case "encrypt":
            Encrypt encrypt = new Encrypt();
            encrypt.encrypt();
            break;
        case "sort":
            SortProgram sortProgram = new SortProgram();
            sortProgram.sort();
            break;
        case "test":
            Test test = new Test();
            test.test();
            break;
        case "check":
            CypherChecker checker = new CypherChecker();
            checker.Inspection();
            break;
        default:
            Console.WriteLine("Anything is false. Plese change the word or spell.");
            break;
    }
    return answer;
}


