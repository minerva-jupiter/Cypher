using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cypher
{
    internal class Key
    {
        public void createKey()
        {
            double[] ints = new double[100];
            for(int i = 0; i < 100; i++)
            {
                ints[i] = i;
            }
            double[] order = new double[100];
            order = GeneratKey();

            Console.WriteLine("Now genarating key...");
            //相関係数が0.5より小さくなるまで鍵ファイルを生成し続ける。
            while (ComputeCoeff(order.ToArray(), ints.ToArray()) < 0.3)
            {
                order = GeneratKey();
            }

            int count = 1;
            Question question = new Question();
            string whereKeyFile = question.Questions("Where will you want to create the keyFile?", false);


            //keyFileに書き出し
            StreamWriter streamWriter = new StreamWriter(whereKeyFile);
            for (count = 0; count < order.Length; count++)
            {
                streamWriter.WriteLine(order[count]);
            }
            streamWriter.Close();
        }


        public double[] GeneratKey()
        {
            //順序ファイルの生成
            int random;
            int count = 1;
            double[] order = new double[100];
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
                }
            }
            return order;
        }
        public double ComputeCoeff(double[] values1, double[] values2)
        {
            if (values1.Length != values2.Length)
                throw new ArgumentException("values must be the same length");

            var avg1 = values1.Average();
            var avg2 = values2.Average();

            var sum1 = values1.Zip(values2, (x1, y1) => (x1 - avg1) * (y1 - avg2)).Sum();

            var sumSqr1 = values1.Sum(x => Math.Pow((x - avg1), 2.0));
            var sumSqr2 = values2.Sum(y => Math.Pow((y - avg2), 2.0));

            var result = sum1 / Math.Sqrt(sumSqr1 * sumSqr2);

            return result;
        }
    }
}
