namespace VendingMachine
{
    internal interface IVendingMachineWallet
    {
        public void AddCoin(float coin);
        public float GetBalance();
        public void removeAmount(float productPrice);
        public string dispenseChange();
    }
}
