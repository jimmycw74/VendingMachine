namespace VendingMachine
{
    internal class VendingMachineCoins : IVendingMachineCoins
    {
        //all euro coins that are accepted
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

        //due any "coin" check if is a valid coin
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
