namespace VendingMachine
{
    internal interface IVendingMachineCoins
    {
        float[] ValidCoinsList();
        bool ValidateCoin(float i);
    }
}