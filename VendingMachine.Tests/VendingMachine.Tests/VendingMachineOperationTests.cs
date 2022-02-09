using Xunit;


namespace VendingMachine.Tests
{
    public class VendingMachineOperationTests
    {

        private readonly VendingMachineFacade _operation;
        private readonly float[] invalidCoins = { 0.35f, 0.54f };

        public VendingMachineOperationTests()
        {
            _operation = new VendingMachineFacade();
        }


        //Accept Coins
        [Fact]
        public void shouldRejectInvalidCoins()
        {
            // Act
            int totalReject = 0;
            foreach (float i in invalidCoins)
            {
                if (!_operation.validateCoin(i)) totalReject++;
            }
            // Assert
            Assert.Equal(totalReject, invalidCoins.Length);
        }

        //Accept Coins
        [Fact]
        public void shouldAddCoinsToAmount()
        {
            // Act
            float totalAdded = 0f;
            foreach (float c in _operation.validCoinsList())
            {
                _operation.addCoin(c);
                totalAdded += c;
            }
            // Assert
            Assert.Equal(totalAdded, _operation.getBalance());
        }

        //Select Product
        [Fact]
        public void shouldRejectInvalidProducts()
        {
            // Assert
            Assert.Equal("**product invalid**", _operation.processCommand("SELECT 999"));
        }

        //Select Product
        [Fact]
        public void shouldRejectPurchaseWithNotEnoughMoney()
        {
            // Act
            // Assert
            Assert.StartsWith("PRICE", _operation.processCommand("SELECT 1"));
        }

        //Select Product
        [Fact]
        public void shouldDispenseProduct()
        {
            // Act
            _operation.processCommand("ENTER 1");
            // Assert
            Assert.EndsWith("THANK YOU", _operation.processCommand("SELECT 1"));
        }

        //Make Change
        [Fact]
        public void shouldReturnChange()
        {
            // Act
            _operation.processCommand("ENTER 1");
            // Assert
            Assert.Equal("Returned money:1", _operation.processCommand("RETURN COINS"));
        }

        //Return Coins
        [Fact]
        public void shouldReturnTotalAmount()
        {
            // Act
            _operation.processCommand("ENTER 1");
            _operation.processCommand("ENTER 1");
            _operation.processCommand("ENTER 1");
            // Assert
            Assert.Equal("Returned money:3", _operation.processCommand("RETURN COINS"));
        }

        //Sold Out
        [Fact]
        public void shouldRejectSoldOutProducts()
        {
            // Act
            // Assert
            Assert.StartsWith("**product sold out**", _operation.processCommand("SELECT 14"));
        }

    }
}
