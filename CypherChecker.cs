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
            Question question = new Question();
            ResdFiles resdFiles = new ResdFiles();
            int[] key;
            key = resdFiles.ReadKeyFile(question.Questions("where is file key", true));

        }
    }
}
