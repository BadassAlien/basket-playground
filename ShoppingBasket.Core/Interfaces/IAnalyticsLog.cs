namespace ShoppingBasket.Core.Interfaces
{
    public interface IAnalyticsLog
    {
        void Log(IBasket basket, double total);
    }
}
