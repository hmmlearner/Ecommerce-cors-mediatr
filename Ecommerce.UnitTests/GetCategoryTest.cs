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
using System.Net.Sockets;

namespace Ecommerce.UnitTests
{

    [TestFixture]
    public class GetCategoryRequestHandlerTests
    {
        private Mock<IApplicationDbcontext> _contextMock;
        private Mock<IMapper> _mapperMock;
        private GetCategoryRequestHandler _handler;

        [SetUp]
        public void Setup()
        {
            _contextMock = new Mock<IApplicationDbcontext>();
            _mapperMock = new Mock<IMapper>();

            //// Configure AutoMapper
            //var config = new MapperConfiguration(cfg =>
            //{
            //    cfg.CreateMap<Category, CategoryDto>();
            //});
            ////_mapperMock = config.CreateMapper();



          //  _handler = new GetCategoryRequestHandler(_contextMock.Object, _mapperMock.Object);


        }


  

        [TearDown]
        public void Teardown()
        {
            // Clean up resources if necessary
        }

        [Test]
        public async Task Handle_ValidCommand_ReturnsCategoryDto()
        {
   
            var cancellationToken = CancellationToken.None;

            // Create a list of categories
            var categoryId = Guid.NewGuid(); ;
            var category = new Category(categoryId, "Test Category", 100);
            var categories = new List<Category> { category }.AsQueryable();

            var categoryDto = new CategoryDto { Id = categoryId, Name = "Test Category", DisplayOrder = 100 };


            // Create a mock DbSet and set up the IQueryable implementation

            // Mock DbSet<Category>
            var mockSet = new Mock<DbSet<Category>>();
            mockSet.As<IQueryable<Category>>().Setup(m => m.Provider).Returns(new TestAsyncQueryProvider<Category>(categories.AsQueryable().Provider));
            mockSet.As<IQueryable<Category>>().Setup(m => m.Expression).Returns(categories.AsQueryable().Expression);
            mockSet.As<IQueryable<Category>>().Setup(m => m.ElementType).Returns(categories.AsQueryable().ElementType);
            mockSet.As<IQueryable<Category>>().Setup(m => m.GetEnumerator()).Returns(categories.AsQueryable().GetEnumerator());

            mockSet.As<IAsyncEnumerable<Category>>().Setup(m => m.GetAsyncEnumerator(It.IsAny<CancellationToken>())).Returns(new TestAsyncEnumerator<Category>(categories.GetEnumerator()));





            // Set up the Categories property to return the mock DbSet
            _contextMock.Setup(c => c.Categories).Returns(mockSet.Object);



            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Category, CategoryDto>();
               // cfg.CreateMap<CategoryDto, Category>();
            });


            var mapper = configuration.CreateMapper();

            var request = new GetCategory(categoryId);
            _handler = new GetCategoryRequestHandler(_contextMock.Object, mapper);

            /* var mapper = new Mapper(configuration);

             // Mock the IMapper
             var _mapperMock = new Mock<IMapper>();
             _mapperMock.Setup(x => x.ConfigurationProvider).Returns(configuration);
             _mapperMock.Setup(x => x.Map<CategoryDto>(It.IsAny<Category>())).Returns((Category source) => mapper.Map<CategoryDto>(source));
             _mapperMock.Setup(x => x.Map<Category>(It.IsAny<CategoryDto>())).Returns((CategoryDto source) => mapper.Map<Category>(source));*/








            // _contextMock.Setup(x => x.Find(It.IsAny<Expression<Func<User, bool>>>()).Returns(predicate => mock.Where(predicate));

            // Set up the mapper to map Category to CategoryDto
            //_mapperMock.Setup(m => m.Map<CategoryDto>(It.IsAny<Category>())).Returns(categoryDto);

            //var mapper = new Mock<IMapper>();
            //_mapperMock.Setup(x => x.ConfigurationProvider)
            //    .Returns(
            //        () => new MapperConfiguration(
            //            cfg => { cfg.CreateMap<CategoryDto, Category>(); }));




            //_handler = new GetCategoryRequestHandler(_contextMock.Object, _mapperMock.Object);


            // Act
            var result = await _handler.Handle(request, cancellationToken);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(categoryId, result.Id);
            Assert.AreEqual("Test Category", result.Name);
            Assert.AreEqual(100, result.DisplayOrder);
            /*
            var categoryId = Guid.NewGuid();
            var category = new Category(categoryId, "Test Category", 100 );
            var categoryDto = new CategoryDto { Id = categoryId, Name = "Test Category", DisplayOrder = 100 };

            

            _contextMock.Setup(c => c.Categories.FirstOrDefault(x => x.Id == categoryId)).Returns(category);

           /// _mapperMock.Setup(m => m.ConfigurationProvider)
            //           .Returns(new MapperConfiguration(cfg => cfg.CreateMap<Category, CategoryDto>()));

            //_mapperMock.Setup(m => m.Map<CategoryDto>(It.IsAny<Category>())).Returns(categoryDto);

            var request = new GetCategory(categoryId);
                
            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(categoryDto.Id, result.Id);
            Assert.AreEqual(categoryDto.Name, result.Name);
            Assert.AreEqual(categoryDto.DisplayOrder, result.DisplayOrder);*/
        }

        [Test]
        public void Handle_InvalidCommand_ThrowsException()
        {


            var categoryId = Guid.NewGuid();
            var category = new Category(categoryId, "Test Category", 100);
            var categoryDto = new CategoryDto { Id = categoryId, Name = "Test Category", DisplayOrder = 100 };


           // _contextMock.Setup(c => c.Categories(x => x.Id == categoryId)).Returns(category);

            /// _mapperMock.Setup(m => m.ConfigurationProvider)
            //           .Returns(new MapperConfiguration(cfg => cfg.CreateMap<Category, CategoryDto>()));

            //_mapperMock.Setup(m => m.Map<CategoryDto>(It.IsAny<Category>())).Returns(categoryDto);

            var request = new GetCategory(categoryId);

            // Act
           // var result = await _handler.Handle(request, CancellationToken.None);

            // Act & Assert
            Assert.ThrowsAsync<KeyNotFoundException>(async () => await _handler.Handle(request, CancellationToken.None));
        }
    }


    //public static class MockDbSetExtensions
    //{
    //    public static Mock<DbSet<T>> BuildMockDbSet<T>(this IQueryable<T> data) where T : class
    //    {
    //        var mockSet = new Mock<DbSet<T>>();
    //        mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(data.Provider);
    //        mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(data.Expression);
    //        mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(data.ElementType);
    //        mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
    //        mockSet.As<IAsyncEnumerable<T>>()
    //               .Setup(m => m.GetAsyncEnumerator(It.IsAny<CancellationToken>()))
    //               .Returns(new TestAsyncEnumerator<T>(data.GetEnumerator()));
    //        mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(new TestAsyncQueryProvider<T>(data.Provider));

    //        return mockSet;
    //    }
    //}

}