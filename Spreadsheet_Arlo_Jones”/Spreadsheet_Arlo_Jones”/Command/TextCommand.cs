using Cpts321;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spreadsheet_Arlo_Jones_
{
    public class TextCommand : ICommand
    {
        public Cell Cell { get; set; }

        public TextCommand(Cell cell)
        {
            Cell = cell;
        }

        public void Execute(object val)
        {
            Cell.Text = (string)val;
        }
    }
}
