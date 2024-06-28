using NUnit.Framework;
using Moq;
using MediatR;
using Application.Common.Catalogue.Commands;
using Application.Common.Catalogue.Queries;
using Infrastructure.Persistence;
using Domain.Entities;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Timers;

namespace Ecommerce.UnitTests
{

    [TestFixture]
    public class CreateCategoryCommandHandlerTests
    {
        private Mock<IApplicationDbcontext> _contextMock;
        private Mock<IMapper> _mapperMock;
        private CreateCategoryCommandHandler _handler;

        [SetUp]
        public void Setup()
        {
            _contextMock = new Mock<IApplicationDbcontext>();
            _mapperMock = new Mock<IMapper>();
            _handler = new CreateCategoryCommandHandler(_contextMock.Object, _mapperMock.Object);

            // Setup mock for Categories DbSet
            var categoriesDbSetMock = new Mock<DbSet<Category>>();
            _contextMock.Setup(c => c.Categories).Returns(categoriesDbSetMock.Object);

            // Setup mock for Add method to simulate ID generation
            _contextMock.Setup(c => c.Categories.Add(It.IsAny<Category>()))
                        .Callback<Category>(category => category.Id = Guid.NewGuid());

            // Setup mock for SaveChangesAsync to simply return 1
            _contextMock.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()))
                        .ReturnsAsync(1);
        }

        [TearDown]
        public void Teardown()
        {
            // Clean up resources if necessary
        }

        [Test]
        public async Task Handle_ValidCommand_ReturnsCategoryDto()
        {
            // Arrange
            var createCategoryDto = new CreateCategoryDto { Name = "Test Category", DisplayOrder = 100 };
            var createCategoryCommand = new CreateCategoryCommand(createCategoryDto);
            var category = new Category("Test Category", 100);
            var categoryDto = new CategoryDto { Id = Guid.NewGuid(), Name = "Test Category", DisplayOrder = 100 };

            _mapperMock.Setup(m => m.Map<Category>(It.IsAny<CreateCategoryDto>())).Returns(category);
            _mapperMock.Setup(m => m.Map<CategoryDto>(It.IsAny<Category>())).Returns(categoryDto);

            // Act
            var result = await _handler.Handle(createCategoryCommand, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(categoryDto.Id, result.Id);
            Assert.AreEqual(categoryDto.Name, result.Name);
            Assert.AreEqual(categoryDto.DisplayOrder, result.DisplayOrder);

            _contextMock.Verify(c => c.Categories.Add(It.IsAny<Category>()), Times.Once);
            _contextMock.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public void Handle_InvalidCommand_ThrowsException()
        {
            // Arrange
            var createCategoryCommand = new CreateCategoryCommand(null);

            // Act & Assert
            Assert.ThrowsAsync<ArgumentNullException>(async () => await _handler.Handle(createCategoryCommand, CancellationToken.None));
        }
    }

}