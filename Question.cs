using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cypher
{
    internal class Question
    {
        public string Questions(string text, bool exsis)
        {
            Console.WriteLine(text);
            string answer = Console.ReadLine();
            if (exsis)
            {
                while (answer != null && File.Exists(answer) == false)
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
    }
}
