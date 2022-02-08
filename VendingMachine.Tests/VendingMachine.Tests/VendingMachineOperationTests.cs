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
        [Fact]
        public void shouldSelectProduct()
        {
            // Act
            int ProductId = 0;
            // Assert
            Assert.Equal("a", "b");
        }
        [Fact]
        public void shouldRejectInvalidProducts()
        {
            // Act
            // Assert
            Assert.Equal("a", "b");
        }
        [Fact]
        public void shouldRejectPurchaseWithNotEnoughMoney()
        {
            // Act
            // Assert
            Assert.Equal("a", "b");
        }
        [Fact]
        public void shouldDispenseProduct()
        {
            // Act
            // Assert
            Assert.Equal("a", "b");
        }
        [Fact]
        public void shouldReturnChange()
        {
            // Act
            // Assert
            Assert.Equal("a", "b");
        }
        [Fact]
        public void shouldReturnTotalAmount()
        {
            // Act
            // Assert
            Assert.Equal("a", "b");
        }
        [Fact]
        public void shouldRejectSoldOutProducts()
        {
            // Act
            // Assert
            Assert.Equal("a", "b");
        }

    }
}
