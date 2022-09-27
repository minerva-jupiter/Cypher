using System;
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
            int[] date = new int[1000];
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
