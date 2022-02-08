namespace VendingMachine
{
    internal class VendingMachineWallet : IVendingMachineWallet
    {
        private float _amount = .0f;
        public void AddCoin(float coin)
        {
            _amount += coin;
        }

        public float GetBalance()
        {
            return _amount;
        }
        public string dispenseChange()
        {
            string result = "";
            if (_amount > 0)
            {
                result = "**change of " + _amount.ToString("0.00") + " **";
                _amount = 0;
            }
            return result;
        }

        public void removeAmount(float productPrice)
        {
            _amount -= productPrice;
        }
    }
}
