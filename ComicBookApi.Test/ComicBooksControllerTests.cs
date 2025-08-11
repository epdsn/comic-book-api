using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ComicBookApi.Controllers;
using ComicBookApi.Models;
using ComicBookApi.Repositories;
using ComicBookApi.Test;
using System.Threading.Tasks;
using System.Collections.Generic;
using ComicBookApi.TestUtilities;

namespace ComicBookApi.Test {
    public class ComicBooksControllerTests {
        private readonly Mock<IComicBookRepository> _comicBookRepositoryMock;
        private readonly ComicBooksController _controller;

        public ComicBooksControllerTests() {
            _comicBookRepositoryMock = new Mock<IComicBookRepository>();
            _controller = new ComicBooksController(_comicBookRepositoryMock.Object);
        }

        [Fact]
        public async Task GetComicBooks_ReturnsOkResult() {
            // Arrange
            var comicBooks = TestDataBuilder.GetSampleComicBooks();
            _comicBookRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(comicBooks);

            // Act
            var result = await _controller.GetComicBooks();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedComicBooks = Assert.IsAssignableFrom<IEnumerable<ComicBook>>(okResult.Value);
            Assert.Equal(comicBooks.Count, returnedComicBooks.Count());
            Assert.Equal(comicBooks, returnedComicBooks);
        }

        [Fact]
        public async Task GetComicBookById_ReturnsOkResult() {
            // Arrange
            var comicBook = TestDataBuilder.GetSampleComicBooks()[0];
            _comicBookRepositoryMock.Setup(repo => repo.GetByIdAsync(comicBook.Id)).ReturnsAsync(comicBook);

            // Act
            var result = await _controller.GetComicBookById(comicBook.Id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedComicBook = Assert.IsType<ComicBook>(okResult.Value);
            Assert.Equal(comicBook, returnedComicBook);
        }

        [Fact]
        public async Task GetComicBookById_ReturnsNotFoundResult() {
            // Arrange
            _comicBookRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((ComicBook)null);

            // Act
            var result = await _controller.GetComicBookById(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task CreateComicBook_ReturnsCreatedAtActionResult() {
            // Arrange
            var comicBook = TestDataBuilder.GetSampleComicBooks()[0];
            _comicBookRepositoryMock.Setup(repo => repo.CreateAsync(comicBook)).ReturnsAsync(comicBook);

            // Act
            var result = await _controller.CreateComicBook(comicBook);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(comicBook, createdAtActionResult.Value);
        }



        [Fact]
        public async Task UpdateComicBook_ReturnsOkResult() {
            // Arrange
            var comicBook = TestDataBuilder.GetSampleComicBooks()[0];
            _comicBookRepositoryMock.Setup(repo => repo.UpdateAsync(comicBook)).ReturnsAsync(comicBook);

            // Act
            var result = await _controller.UpdateComicBook(comicBook.Id, comicBook);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(comicBook, okResult.Value);
        }



        [Fact]
        public async Task DeleteComicBook_ReturnsOkResult() {
            // Arrange
            var comicBook = TestDataBuilder.GetSampleComicBooks()[0];
            _comicBookRepositoryMock.Setup(repo => repo.DeleteAsync(comicBook.Id)).ReturnsAsync(comicBook);

            // Act
            var result = await _controller.DeleteComicBook(comicBook.Id);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task DeleteComicBook_ReturnsNotFoundResult() {
            // Arrange
            _comicBookRepositoryMock.Setup(repo => repo.DeleteAsync(It.IsAny<int>())).ReturnsAsync((ComicBook)null);

            // Act
            var result = await _controller.DeleteComicBook(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        
    }
}