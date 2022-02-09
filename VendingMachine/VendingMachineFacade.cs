using System.Globalization;
using System.Reflection;

namespace VendingMachine
{
    public class VendingMachineFacade
    {

        private VendingMachineWallet _wallet;
        private VendingMachineCoins _coins;
        private List<VendingMachineProduct> products;

        //constructor - prepare wallet, coins, read csv and set default culture
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
            Resources.Resources.Culture = new CultureInfo("en-US");
        }

        //return the help display 
        public string getHelpDisplay()
        {
            return "HELP - Nonono nonono nono\nnono nonono nono no\nnonono nonono nonono";
        }

        //expose the list of valid coins
        public float[] validCoinsList()
        {
            return _coins.ValidCoinsList();
        }

        //heart of the system - process every command passed to this facade
        public string processCommand(string c)
        {
            string result = "";

            //---- ENTER COINS
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
                        result = Resources.Resources.amount_entered + _wallet.GetBalance().ToString("0.00");
                    }
                    else result = Resources.Resources.invalid_coin;
                }
                catch (Exception)
                {
                    Console.WriteLine(Resources.Resources.error);
                }
            }

            //---- CHANGE LANGUAGE
            else if (c.StartsWith("LANGUAGE"))
            {
                try
                {
                    string[] split = c.Split(" ");
                    if (split[1].ToUpper().Equals("PT") || split[1].ToUpper().Equals("EN"))
                    {
                        Resources.Resources.Culture = new CultureInfo(split[1]);
                        result = Resources.Resources.language_changed_to + split[1];
                    }
                    else
                        result = Resources.Resources.language + split[1] + Resources.Resources.not_supported;

                }
                catch {
                    result = Resources.Resources.error_language;
                }

            }

            //---- SELECT PRODUCT
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
                        result = Resources.Resources.error_product_invalid;
                    }
                    else
                    {
                        if (product.ProductStock > 0)
                        {
                            if (_wallet.GetBalance() >= product.ProductPrice)
                            {
                                result = Resources.Resources.product_dispensed;
                                product.ProductStock -= 1;
                                _wallet.removeAmount(product.ProductPrice);
                                result += _wallet.dispenseChange() + '\n';
                                result += Resources.Resources.thank_you;

                            }
                            else
                            {
                                result = Resources.Resources.price + product.ProductPrice + "\n" + Resources.Resources.please_deposit_more + (product.ProductPrice - _wallet.GetBalance()).ToString("0.00");
                            }
                        }
                        else
                        {
                            result = "**product sold out**\n";
                            if (_wallet.GetBalance() >= 0)
                            {
                                result += Resources.Resources.amount_entered + " " + _wallet.GetBalance().ToString("0.00");
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine(Resources.Resources.error);
                }
            }

            //---- SHOW LIST OF PRODUCTS
            else if (c.Equals("SHOW"))
            {
                foreach (VendingMachineProduct p in products)
                {
                    Console.WriteLine(p.ProductId + " " + p.ProductName + " " + p.ProductPrice + " - " + (p.ProductStock < 1 ? Resources.Resources.sold_out : p.ProductStock + Resources.Resources.item_left));
                }
            }

            //---- CANCEL ALL AND RETURN MONEY TO USER
            else if (c.Equals("RETURN COINS"))
            {
                result = Resources.Resources.returned_money + _wallet.GetBalance();
                _wallet.removeAmount(_wallet.GetBalance());
            }

            //---- QUIT THE VENDING MACHINE USER INTERFACE
            else if (c.Equals("QUIT"))
            {
                result = "CMD:QUIT";
            }

            //---- ANY OTHER NOT VALID COMMAND
            else
            {
                result = "CMD:command invalid";
            }

            return result;
        }

        //expose the wallet method to add coin/money
        public void addCoin(float coin)
        {
            if (_coins.ValidateCoin(coin))
                _wallet.AddCoin(coin);
            else
                Console.WriteLine(Resources.Resources.coin_rejected);
        }

        //expose the method from wallet to return total of money
        public float getBalance() { return _wallet.GetBalance(); }

        //expose the method from coins to validate coin
        public bool validateCoin(float coin) { return _coins.ValidateCoin(coin); }

        //expose the initial display to the user interface
        public string getInitialDisplay()
        {
            return Resources.Resources.insert_coin;
        }

    }
}
