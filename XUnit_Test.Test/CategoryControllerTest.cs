using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Shouldly;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Unit_Test_API;
using Unit_Test_API.Controllers;
using Xunit;

public class CategoryControllerTests
{
    private readonly IRepository<Category> _repositoryMock;
    private readonly CategoriesController _controller;

    public CategoryControllerTests()
    {
        _repositoryMock = Substitute.For<IRepository<Category>>();
        _controller = new CategoriesController(_repositoryMock);
    }

    [Fact]
    public async Task GetAllAsync_Should_Return_Ok_With_Categories()
    {
        // Arrange
        var categories = new List<Category>
        {
            new Category { Id = 1, Name = "Tech" },
            new Category { Id = 2, Name = "Food" }
        };

        _repositoryMock.GetAllAsync(Arg.Any<CancellationToken>())
                       .Returns(categories);

        // Act
        var result = await _controller.GetAllAsync(CancellationToken.None);

        // Assert
        var okResult = result.Result as OkObjectResult;
        okResult.ShouldNotBeNull();
        okResult.Value.ShouldBe(categories);
    }

    [Fact]
    public async Task GetByIdAsync_Should_Return_NotFound_When_Category_Does_Not_Exist()
    {
        // Arrange
        _repositoryMock.GetByIdAsync(1, Arg.Any<CancellationToken>())
                       .Returns((Category?)null);

        // Act
        var result = await _controller.GetByIdAsync(1, CancellationToken.None);

        // Assert
        result.Result.ShouldBeOfType<NotFoundObjectResult>();
    }

    [Fact]
    public async Task CreateAsync_Should_Call_AddAsync_And_Return_Created()
    {
        // Arrange
        var category = new Category { Id = 1, Name = "New Category" };

        // Act
        var result = await _controller.CreateAsync(category, CancellationToken.None);

        // Assert
        await _repositoryMock.Received(1)
                             .AddAsync(category, Arg.Any<CancellationToken>());

        result.Result.ShouldBeOfType<CreatedAtActionResult>();
    }

    [Fact]
    public async Task DeleteAsync_Should_Return_NoContent_When_Category_Exists()
    {
        // Arrange
        var category = new Category { Id = 1, Name = "Tech" };

        _repositoryMock.GetByIdAsync(1, Arg.Any<CancellationToken>())
                       .Returns(category);

        // Act
        var result = await _controller.DeleteAsync(1, CancellationToken.None);

        // Assert
        await _repositoryMock.Received(1)
                             .DeleteAsync(1, Arg.Any<CancellationToken>());

        result.ShouldBeOfType<NoContentResult>();
    }
}