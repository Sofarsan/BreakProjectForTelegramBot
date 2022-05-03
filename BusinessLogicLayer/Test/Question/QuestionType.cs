using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    [Serializable]
    public enum QuestionType
    {
        QuestionInput=1,
        QuestionYesNo,
        QuestionMultiSelect,
        QuestionSingleSelect,
        QuestionSort
    }
}
