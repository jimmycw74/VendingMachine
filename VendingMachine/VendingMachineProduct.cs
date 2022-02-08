using System.Globalization;

namespace VendingMachine
{
    internal class VendingMachineProduct : IVendingMachineProduct
    {
        internal int ProductId { get; set; }
        internal string? ProductName { get; set; }
        internal float ProductPrice { get; set; }
        internal int ProductStock { get; set; } = 0;

        private bool ProductSelected { get; set; } = false;

        public static VendingMachineProduct ConvertFromCSVLine(string line)
        {
            string[] values = line.Split(',');
            VendingMachineProduct product = new VendingMachineProduct
            {
                ProductId = int.Parse(values[0]),
                ProductName = values[1],
                ProductPrice = float.Parse(values[2],
                      System.Globalization.NumberStyles.Any,
                      CultureInfo.InvariantCulture),
                ProductStock = int.Parse(values[3])
            };
            return product;
        }
        internal void setSelected(bool v)
        {
            this.ProductSelected = v;
        }
    }
}