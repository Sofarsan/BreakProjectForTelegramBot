using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    [Serializable]
    public class OptionAnswer
    {
        public string Text { get; set; } = string.Empty;
        public bool? IsValid { get; set; } = false;
        public OptionAnswer() { }
        public OptionAnswer(string Text, bool? IsValid = false)
        {
            this.IsValid = IsValid;
            this.Text = Text;
        }
    }
}