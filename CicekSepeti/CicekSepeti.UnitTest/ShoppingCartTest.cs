using CicekSepeti.DataAccess.IRepositories;
using CicekSepeti.DataAccess.UnitOfWork;
using CicekSepeti.Domain.Entities;
using CicekSepeti.Service.IServices;
using CicekSepeti.Service.Services;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace CicekSepeti.UnitTest
{
    public class ShoppingCartTest
    {

        [Fact]
        public async Task Should_IsSuccessFalse_UserIsNull()
        {
            // arrange
            var userId = Guid.NewGuid();
            var productId = Guid.NewGuid();
            var count = 5;

            ShoppingCart shoppingCart = new ShoppingCart { UserId = userId, ProductId = productId, Count = count };
            User user = new User { Id = userId, Name = "a", Password = "fd" };
            Product product = new Product { Id = productId, Count = 1, Name = "asd", Price = 12 };

            var userRepositoriesMock = new Mock<IUserRepository>(MockBehavior.Loose);
            userRepositoriesMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((User)null);

            var productRepositoriesMock = new Mock<IProductRepository>(MockBehavior.Loose);
            productRepositoriesMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(product);

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(m => m.UserRepository).Returns(userRepositoriesMock.Object);
            unitOfWorkMock.Setup(m => m.ProductRepository).Returns(productRepositoriesMock.Object);

            IShoppingCartService shoppingCartServce = new ShoppingCartService(unitOfWorkMock.Object);

            // act
            var actual = await shoppingCartServce.AddShoppingCart(shoppingCart);

            // assert
            Assert.False(actual.IsSuccess);
        }
        [Fact]
        public async Task Should_IsSuccessFalse_ProductIsNull()
        {
            // arrange
            var userId = Guid.NewGuid();
            var productId = Guid.NewGuid();
            var count = 5;

            ShoppingCart shoppingCart = new ShoppingCart { UserId = userId, ProductId = productId, Count = count };
            User user = new User { Id = userId, Name = "a", Password = "fd" };
            Product product = new Product { Id = productId, Count = 1, Name = "asd", Price = 12 };

            var userRepositoriesMock = new Mock<IUserRepository>(MockBehavior.Loose);
            userRepositoriesMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(user);

            var productRepositoriesMock = new Mock<IProductRepository>(MockBehavior.Loose);
            productRepositoriesMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Product)null);

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(m => m.UserRepository).Returns(userRepositoriesMock.Object);
            unitOfWorkMock.Setup(m => m.ProductRepository).Returns(productRepositoriesMock.Object);

            IShoppingCartService shoppingCartServce = new ShoppingCartService(unitOfWorkMock.Object);

            // act
            var actual = await shoppingCartServce.AddShoppingCart(shoppingCart);

            // assert
            Assert.False(actual.IsSuccess);
        }
        [Fact]
        public async Task Should_IsSuccessFalse_CountIsBigger()
        {
            // arrange
            var userId = Guid.NewGuid();
            var productId = Guid.NewGuid();
            var count = 5;

            ShoppingCart shoppingCart = new ShoppingCart { UserId = userId, ProductId = productId, Count = count };
            User user = new User { Id = userId, Name = "a", Password = "fd" };
            Product product = new Product { Id = productId, Count = 1, Name = "asd", Price = 12 };

            var userRepositoriesMock = new Mock<IUserRepository>(MockBehavior.Loose);
            userRepositoriesMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(user);

            var productRepositoriesMock = new Mock<IProductRepository>(MockBehavior.Loose);
            productRepositoriesMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(product);

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(m => m.UserRepository).Returns(userRepositoriesMock.Object);
            unitOfWorkMock.Setup(m => m.ProductRepository).Returns(productRepositoriesMock.Object);

            IShoppingCartService shoppingCartServce = new ShoppingCartService(unitOfWorkMock.Object);

            // act
            var actual = await shoppingCartServce.AddShoppingCart(shoppingCart);

            // assert
            Assert.False(actual.IsSuccess);
        }

        [Fact]
        public async Task Should_IsSuccessTrue_AddNewShoppingCart()
        {
            // arrange
            var userId = Guid.NewGuid();
            var productId = Guid.NewGuid();
            var count = 5;

            ShoppingCart shoppingCart = new ShoppingCart { UserId = userId, ProductId = productId, Count = count };
            User user = new User { Id = userId, Name = "a", Password = "fd" };
            Product product = new Product { Id = productId, Count = 10, Name = "asd", Price = 12 };

            var userRepositoriesMock = new Mock<IUserRepository>(MockBehavior.Loose);
            userRepositoriesMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(user);

            var productRepositoriesMock = new Mock<IProductRepository>(MockBehavior.Loose);
            productRepositoriesMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(product);

            var shoppingCartRepositoriesMock = new Mock<IShoppingCartRepository>(MockBehavior.Loose);
            shoppingCartRepositoriesMock.Setup(x => x.GetShoppingCartsByUserIdAndProductId(It.IsAny<Guid>(), It.IsAny<Guid>())).ReturnsAsync((ShoppingCart)null);

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(m => m.UserRepository).Returns(userRepositoriesMock.Object);
            unitOfWorkMock.Setup(m => m.ProductRepository).Returns(productRepositoriesMock.Object);
            unitOfWorkMock.Setup(m => m.ShoppingCartRepository).Returns(shoppingCartRepositoriesMock.Object);

            IShoppingCartService shoppingCartServce = new ShoppingCartService(unitOfWorkMock.Object);

            // act
            var actual = await shoppingCartServce.AddShoppingCart(shoppingCart);

            // assert
            Assert.True(actual.IsSuccess);
        }
        [Fact]
        public async Task Should_IsSuccessTrue_UpdateShoppingCart()
        {
            // arrange
            var userId = Guid.NewGuid();
            var productId = Guid.NewGuid();

            ShoppingCart shoppingCart = new ShoppingCart { Id = Guid.NewGuid(), UserId = userId, ProductId = productId, Count = 1 };
            User user = new User { Id = userId, Name = "a", Password = "fd" };
            Product product = new Product { Id = productId, Count = 10, Name = "asd", Price = 12 };

            var userRepositoriesMock = new Mock<IUserRepository>(MockBehavior.Loose);
            userRepositoriesMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(user);

            var productRepositoriesMock = new Mock<IProductRepository>(MockBehavior.Loose);
            productRepositoriesMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(product);

            var shoppingCartRepositoriesMock = new Mock<IShoppingCartRepository>(MockBehavior.Loose);
            shoppingCartRepositoriesMock.Setup(x => x.GetShoppingCartsByUserIdAndProductId(It.IsAny<Guid>(), It.IsAny<Guid>())).ReturnsAsync(shoppingCart);

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(m => m.UserRepository).Returns(userRepositoriesMock.Object);
            unitOfWorkMock.Setup(m => m.ProductRepository).Returns(productRepositoriesMock.Object);
            unitOfWorkMock.Setup(m => m.ShoppingCartRepository).Returns(shoppingCartRepositoriesMock.Object);

            IShoppingCartService shoppingCartServce = new ShoppingCartService(unitOfWorkMock.Object);

            // act
            var actual = await shoppingCartServce.AddShoppingCart(shoppingCart);

            // assert
            Assert.True(actual.IsSuccess);
        }

    }
}
