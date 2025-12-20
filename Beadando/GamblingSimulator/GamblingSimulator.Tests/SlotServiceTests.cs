using Moq;
using GamblingSimulator.Core.Services;
using GamblingSimulator.Core.Contracts;
using GamblingSimulator.Core.Exceptions;

namespace GamblingSimulator.Tests.Services
{
    [TestFixture]
    public class SlotServiceTests
    {
        private SlotService _slotService;
        private Mock<ISlot> _slotMock;
        private Mock<ISlotRepository> _repositoryMock;

        [SetUp]
        public void Setup()
        {
            _slotMock = new Mock<ISlot>();
            _repositoryMock = new Mock<ISlotRepository>();

            _slotService = new SlotService();
        }

        [Test]
        public void Spin_WhenBalanceIsInsufficient_ShouldThrowException()
        {
            int currentBalance = 50;
            int betAmount = 100;

            _repositoryMock.Setup(r => r.Balance).Returns(currentBalance);

            Assert.Throws<InsufficientBalanceException>(() =>
                _slotService.Spin(_slotMock.Object, betAmount, _repositoryMock.Object)
            );
        }
    }
}