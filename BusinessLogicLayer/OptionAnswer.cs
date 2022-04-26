using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class OptionAnswer
    {
        public string Text { get; set; } = string.Empty;
        public bool? IsValid { get; set; } = false;

        public OptionAnswer(string Text, bool? IsValid)
        {
            this.IsValid = IsValid;
            this.Text = Text;
        }
    }
}