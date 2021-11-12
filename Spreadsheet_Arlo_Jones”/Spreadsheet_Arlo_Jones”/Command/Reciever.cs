namespace Spreadsheet_Arlo_Jones_
{
    public class Reciever
    {
        public object Val { get; set; }
        public ICommand Command { get; set; }
        public Reciever(object val, ICommand command)
        {

        }
    }
}
