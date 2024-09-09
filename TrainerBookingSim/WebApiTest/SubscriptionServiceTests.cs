using BusinessLogic.Services;
using DataAccess.Models;
using DataAccess.Repositories.Interfaces;
using FluentAssertions;
using Moq;

namespace WebApiTest;

public class SubscriptionServiceTests
    {
        private readonly Mock<ISubscriptionRepository> _mockRepository;
        private readonly SubscriptionService _service;

        public SubscriptionServiceTests()
        {
            _mockRepository = new Mock<ISubscriptionRepository>();
            _service = new SubscriptionService(_mockRepository.Object);
        }

        [Fact]
        public async Task AddOrContinueSubscriptionAsync_ShouldUpdateExistingSubscription_WhenSubscriptionIsValid()
        {
            // Arrange
            string userId = "1";
            var additionalVisits = 5;
            var daysValid = 10;
            var existingSubscription = new Subscription
            {
                UserId = userId,
                Visits = 3,
                CreatedDate = DateTime.Now.AddDays(-5),
                ExpiryDate = DateTime.Now.AddDays(5),
                IsExpired = false
            };

            _mockRepository.Setup(repo => repo.GetByUserIdAsync(userId))
                .ReturnsAsync(new List<Subscription> { existingSubscription });

            // Act
            await _service.AddOrContinueSubscriptionAsync(userId, additionalVisits, daysValid);

            // Assert
            existingSubscription.Visits.Should().Be(8);
            existingSubscription.ExpiryDate.Should().BeCloseTo(DateTime.Now.AddDays(daysValid), precision: TimeSpan.FromSeconds(1));
            _mockRepository.Verify(repo => repo.UpdateAsync(It.Is<Subscription>(sub =>
                sub.UserId == userId &&
                sub.Visits == 8 &&
                sub.ExpiryDate.Date == DateTime.Now.AddDays(daysValid).Date)), Times.Once);
            _mockRepository.Verify(repo => repo.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task AddOrContinueSubscriptionAsync_ShouldAddNewSubscription_WhenNoExistingSubscriptionFound()
        {
            // Arrange
            string userId = "1";
            var visits = 5;
            var daysValid = 10;

            _mockRepository.Setup(repo => repo.GetByUserIdAsync(userId))
                .ReturnsAsync(new List<Subscription>());

            // Act
            await _service.AddOrContinueSubscriptionAsync(userId, visits, daysValid);

            // Assert
            _mockRepository.Verify(repo => repo.AddAsync(It.Is<Subscription>(sub =>
                sub.UserId == userId &&
                sub.Visits == visits &&
                sub.CreatedDate.Date == DateTime.Now.Date &&
                sub.ExpiryDate.Date == DateTime.Now.AddDays(daysValid).Date &&
                !sub.IsExpired)), Times.Once);
            _mockRepository.Verify(repo => repo.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task AddOrContinueSubscriptionAsync_ShouldNotUpdateOrAdd_WhenExistingSubscriptionIsExpired()
        {
            // Arrange
            string userId = "1";
            var additionalVisits = 5;
            var daysValid = 10;
            var expiredSubscription = new Subscription
            {
                UserId = userId,
                Visits = 3,
                CreatedDate = DateTime.Now.AddDays(-15),
                ExpiryDate = DateTime.Now.AddDays(-1),
                IsExpired = true
            };

            _mockRepository.Setup(repo => repo.GetByUserIdAsync(userId))
                .ReturnsAsync(new List<Subscription> { expiredSubscription });

            // Act
            await _service.AddOrContinueSubscriptionAsync(userId, additionalVisits, daysValid);

            // Assert
            _mockRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Subscription>()), Times.Never);
            _mockRepository.Verify(repo => repo.AddAsync(It.IsAny<Subscription>()), Times.Once);
            _mockRepository.Verify(repo => repo.SaveChangesAsync(), Times.Once);
        }
    }