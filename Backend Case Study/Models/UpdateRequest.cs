namespace Backend_Case_Study.Models
{
    public class UpdateRequest
    {
        public Portfolio Source;
        public Portfolio Target;
        public string ShareSymbol;
        public int Volume;
        public ProcessType processType;
    }

    public enum ProcessType
    {
        Buy,
        Sell
    }
}
