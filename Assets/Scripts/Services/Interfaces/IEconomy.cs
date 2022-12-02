namespace Services.Interfaces
{
    public interface IEconomy
    {
        void IncreaseCoins(int coins);
        void ReduceCoins(int coins);
        int Coins { get; }
    }
}