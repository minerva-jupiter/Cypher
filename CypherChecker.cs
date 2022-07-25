using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cypher;

namespace Cypher
{
    internal class CypherChecker
    {
        public void Inspection()
        {
            int countChecker=0;
            //鍵の読み込み
            Question question = new Question();
            ResdFiles resdFiles = new ResdFiles();
            int[] key;
            string whereKeyFile = question.Questions("Where key file", true);
            key = resdFiles.ReadKeyFile(whereKeyFile);
            double[] key2Double;
            int[] key2 = new int[key.Length];
            Key key1 = new Key();

            //鍵を作って比較
            Console.WriteLine("Please wait a minuits");
            Console.WriteLine("Starting the inspection...");
            var sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            key2Double = key1.GeneratKey();
            for(int i = 0; i < key2Double.Length; i++)
            {
                key2[i] = Convert.ToInt32(key2Double[i]);
            }
            countChecker++;
            //繰り返し
            while (key!=key2)
            {
                key2Double = key1.GeneratKey();
                for (int i = 0; i < key2Double.Length; i++)
                {
                    key2[i] = Convert.ToInt32(key2Double[i]);
                }
                countChecker++;
            }
            sw.Stop();
            Console.WriteLine("Number of verifications is " + countChecker);
            Console.WriteLine("Time taken is " + sw.ElapsedMilliseconds);
        }
    }
}
