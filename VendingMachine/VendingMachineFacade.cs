using System.Globalization;
using System.Reflection;

namespace VendingMachine
{
    public class VendingMachineFacade
    {

        private VendingMachineWallet _wallet;
        private VendingMachineCoins _coins;
        private List<VendingMachineProduct> products;

        public VendingMachineFacade()
        {
            _wallet = new VendingMachineWallet();
            _coins = new VendingMachineCoins();
            string basePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string filePath = Path.Combine(basePath, "products.csv");
            products = File.ReadAllLines(filePath)
                   .Skip(1)
                   .Select(v => VendingMachineProduct.ConvertFromCSVLine(v))
                   .ToList();
        }

        public float[] validCoinsList()
        {
            return _coins.ValidCoinsList();
        }

        public string processCommand(string c)
        {
            string result = "";
            if (c.StartsWith("ENTER"))
            {
                try
                {
                    //select product
                    string[] split = c.Split(' ');
                    float coin = float.Parse(split[1],
                      System.Globalization.NumberStyles.Any,
                      CultureInfo.InvariantCulture);
                    if (_coins.ValidateCoin(coin))
                    {
                        _wallet.AddCoin(coin);
                        result = "Amount entered: " + _wallet.GetBalance().ToString("0.00");
                    }
                    else result = "** invalid coin **";
                }
                catch (Exception)
                {
                    Console.WriteLine("** error **");
                }
            }
            else if (c.StartsWith("SELECT"))
            {
                try
                {
                    //select product
                    string[] split = c.Split(' ');
                    int ProductId = int.Parse(split[1]);
                    VendingMachineProduct? product = null;
                    {
                        ProductId = int.Parse(split[1]);
                    };
                    foreach (VendingMachineProduct t in products)
                    {
                        if (t.ProductId == ProductId) product = t;
                    }
                    //check if has enough money
                    if (product == null)
                    {
                        result = "**product invalid**";
                    }
                    else
                    {
                        if (product.ProductStock > 0)
                        {
                            if (_wallet.GetBalance() >= product.ProductPrice)
                            {
                                result = "**product dispensed**\n";
                                product.ProductStock -= 1;
                                _wallet.removeAmount(product.ProductPrice);
                                result += _wallet.dispenseChange() + '\n';
                                result += "THANK YOU";

                            }
                            else
                            {
                                result = "Please deposit more " + (product.ProductPrice - _wallet.GetBalance()).ToString("0.00");
                            }
                        }
                        else
                        {
                            result = "**product sold out**\n";
                            if (_wallet.GetBalance() >= 0)
                            {
                                result += "Amount entered: " + _wallet.GetBalance().ToString("0.00");
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("** error **");
                }
            }
            else if (c.Equals("SHOW"))
            {
                foreach (VendingMachineProduct p in products)
                {
                    Console.WriteLine(p.ProductId + " " + p.ProductName + " " + p.ProductPrice + " - " + (p.ProductStock < 1 ? "SOLD OUT" : p.ProductStock + " Items Left"));
                }
            }
            else if (c.Equals("RETURN COINS"))
            {
                result = "Returned money: " + _wallet.GetBalance();
                _wallet.removeAmount(_wallet.GetBalance());
            }
            else if (c.Equals("QUIT"))
            {
                result = "CMD:QUIT";
            }
            else
            {
                result = "CMD:command invalid";
            }

            return result;
        }

        public int GetTotalProducts()
        {
            return products.Count;
        }

        public void addCoin(float coin)
        {
            if (_coins.ValidateCoin(coin))
                _wallet.AddCoin(coin);
            else
                Console.WriteLine("COIN REJECTED");
        }

        public float getBalance() { return _wallet.GetBalance(); }

        public bool validateCoin(float coin) { return _coins.ValidateCoin(coin); }

        public void selectProduct(int ProductId)
        {
            foreach (VendingMachineProduct product in products)
                if (product.ProductId == ProductId)
                    product.setSelected(true); ;

        }

    }
}
