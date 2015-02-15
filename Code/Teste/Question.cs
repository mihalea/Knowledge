using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teste
{
    public class Question
    {
        public string Intrebare;
        public List<String> ans = new List<String>();
        public int correctAnswer;

        public Question()
        {
            Intrebare = String.Empty;
            correctAnswer = -1;
        }

        public void setQuestion(string q)
        {
            Intrebare = q;
        }

        public void addAnswer(string a)
        {
            ans.Add(a);
        }

        public void setCorrect(string c)
        {
            correctAnswer = Convert.ToInt32(c);
        }
    }
}
