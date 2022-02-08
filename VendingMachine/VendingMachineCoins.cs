namespace VendingMachine
{
    internal class VendingMachineCoins : IVendingMachineCoins
    {
        internal static readonly float[] validCoins =  {
            0.05f ,
            0.10f ,
            0.20f ,
            0.50f ,
            1f ,
            2f
        };
        public float[] ValidCoinsList()
        {
            return validCoins;
        }

        public bool ValidateCoin(float i)
        {
            Boolean notValid = true;
            foreach (float c in validCoins)
                if (i == c) notValid = false;
            if (notValid) return false;
            else return true;
        }
    }
}
