namespace VendingMachine
{
    internal class VendingMachineWallet : IVendingMachineWallet
    {
        private float _amount = .0f;
        
        //add money/coin to balance
        public void AddCoin(float coin)
        {
            _amount += coin;
        }

        //return current balance
        public float GetBalance()
        {
            return _amount;
        }

        //return change after user successfully buy something
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

        //remove the price of the product from the balance
        public void removeAmount(float productPrice)
        {
            _amount -= productPrice;
        }
    }
}
