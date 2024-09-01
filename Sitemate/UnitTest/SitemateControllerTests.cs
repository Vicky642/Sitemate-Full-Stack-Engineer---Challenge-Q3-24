using Microsoft.AspNetCore.Mvc;
using Moq;
using Sitemate.Controllers;
using Sitemate.Models;
using Sitemate.Repository;
using System.Collections.Generic;
using Xunit;

namespace Sitemate.UnitTest
{
    public class SitemateControllerTests
    {
        private readonly SitemateController _controller;
        private readonly Mock<SitemateRepository<Issue>> _mockRepository;

        public SitemateControllerTests()
        {
            _mockRepository = new Mock<SitemateRepository<Issue>>();
            _controller = new SitemateController(_mockRepository.Object);
        }

        [Fact]
        public void GetIssues_ReturnsOkResult_WithIssues()
        {
            // Arrange
            var issues = new List<Issue> { new Issue { Id = 1 }, new Issue { Id = 2 } };
            _mockRepository.Setup(repo => repo.GetAll()).Returns(issues);

            // Act
            var result = _controller.GetIssues();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedIssues = Assert.IsType<List<Issue>>(okResult.Value);
            Assert.Equal(2, returnedIssues.Count);
        }

        [Fact]
        public void GetIssue_ReturnsOkResult_WithIssue()
        {
            // Arrange
            var issue = new Issue { Id = 1 };
            _mockRepository.Setup(repo => repo.Get(1)).Returns(issue);

            // Act
            var result = _controller.GetIssue(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedIssue = Assert.IsType<Issue>(okResult.Value);
            Assert.Equal(1, returnedIssue.Id);
        }

        [Fact]
        public void GetIssue_ReturnsNotFoundResult_WhenIssueDoesNotExist()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.Get(It.IsAny<int>())).Returns((Issue)null);

            // Act
            var result = _controller.GetIssue(1);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void PostIssue_ReturnsCreatedAtActionResult_WithIssue()
        {
            // Arrange
            var issue = new Issue { Id = 1 };
            _mockRepository.Setup(repo => repo.Add(issue)).Verifiable();

            // Act
            var result = _controller.PostIssue(issue);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.Equal(nameof(_controller.GetIssue), createdResult.ActionName);
            Assert.Equal(issue.Id, createdResult.RouteValues["id"]);
        }

        [Fact]
        public void PutIssue_ReturnsNoContent_WhenIssueIsUpdated()
        {
            // Arrange
            var issue = new Issue { Id = 1 };
            _mockRepository.Setup(repo => repo.Update(issue)).Verifiable();

            // Act
            var result = _controller.PutIssue(1, issue);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void PutIssue_ReturnsBadRequest_WhenIdDoesNotMatch()
        {
            // Act
            var result = _controller.PutIssue(1, new Issue { Id = 2 });

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void DeleteIssue_ReturnsNoContent_WhenIssueIsDeleted()
        {
            // Arrange
            var issue = new Issue { Id = 1 };
            _mockRepository.Setup(repo => repo.Get(1)).Returns(issue);
            _mockRepository.Setup(repo => repo.Delete(1)).Verifiable();

            // Act
            var result = _controller.DeleteIssue(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void DeleteIssue_ReturnsNotFound_WhenIssueDoesNotExist()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.Get(It.IsAny<int>())).Returns((Issue)null);

            // Act
            var result = _controller.DeleteIssue(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

    }
}
