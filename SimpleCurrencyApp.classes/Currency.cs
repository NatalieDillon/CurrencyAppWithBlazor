namespace SimpleCurrencyApp.classes
{
    public class Currency
    {
        public string Code { get; }
        public string Name { get; }
        public decimal Rate { get; }

        public string Details { get { return $"{Code}: {Name}"; } }

        public Currency(string code, string name, decimal rate)
        {
            Code = code;
            Name = name;
            Rate = rate;
        }


    }
}