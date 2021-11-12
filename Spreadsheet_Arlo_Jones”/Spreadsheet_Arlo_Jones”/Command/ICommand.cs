using Cpts321;

namespace Spreadsheet_Arlo_Jones_
{
    public interface ICommand
    {
        Cell Cell { get; set; }
        void Execute(object val);
    }
}
