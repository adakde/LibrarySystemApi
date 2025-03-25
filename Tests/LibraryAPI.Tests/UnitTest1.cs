using LibaryApi.Controllers;
using LibaryApi.Data;
using LibaryApi.Entities;
using LibaryApi.Entities.Enum;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using Xunit;

namespace LibraryAPI.Tests;

public class LibaryControllerTests
{
    private readonly DbContextOptions<AppDbContext> _options;
    [Fact]
    public async void GetBooks_ReturnsEmptyList_WhenNoBooks()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "GetBooks_ReturnsEmptyList_WhenNoBooks")
            .Options;
        await using (var context = new AppDbContext(options))
        {
            var LibaryController = new LibraryController(context);
            // Act
            var result = await LibaryController.GetBooks();
            // Assert
            Assert.Empty(result.Value);
            Assert.NotNull(result.Value);
            Assert.NotNull(result);
        }
    }
    [Fact]
    public async Task ReserveBook_UpdatesStatus_ToReserved()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "Test_Reserve")
            .Options;

        // Seed data
        await using (var arrangeContext = new AppDbContext(options))
        {
            await arrangeContext.Books.AddAsync(new Book
            {
                Id = 1,
                Status = Status.Available
            });
            await arrangeContext.SaveChangesAsync();
        }

        // Act
        await using (var actContext = new AppDbContext(options))
        {
            var controller = new LibraryController(actContext);
            await controller.ReserveBook(1);
        }

        // Assert
        await using (var assertContext = new AppDbContext(options))
        {
            var book = await assertContext.Books.FindAsync(1);
            Assert.Equal(Status.Reserved, book.Status);
        }
    }
    [Fact]
    public async Task ReserveBook_ShouldReturnNotFound_WhenBookNotExists()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "Test_Not_Found")
            .Options;

        // Act & Assert
        await using (var context = new AppDbContext(options))
        {
            var controller = new LibraryController(context);
            var result = await controller.ReserveBook(999); 

            Assert.IsType<NotFoundResult>(result);
        }
    }
}
