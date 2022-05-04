using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class Reply
    {
        public string Name { get; set; }
        public string Question { get; set; }
        public List<string> UserAnswer { get; set; }

        public Reply(User user, Question question, Question answer)
        {
            Name = user.FirstName+user.LastName;
            Question = question._questionText;
            UserAnswer = answer.Answer;
        }

    }
}
