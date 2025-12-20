using GamblingSimulator.Core.Models;
using GamblingSimulator.Core.Storage;

namespace GamblingSimulator.Tests
{
    [TestFixture]
    internal class JsonSlotRepositoryTests
    {
        private string _testFilePath;       

        [SetUp]
        public void Setup()
        {
            _testFilePath = Path.Combine(Path.GetTempPath(), $"test_gambling_{Guid.NewGuid()}.json");
        }

        [TearDown]
        public void TearDown()
        {
            if (File.Exists(_testFilePath))
            {
                File.Delete(_testFilePath);
            }
        }

        [Test]
        public void Constructor_WhenFileIsNotValid_ShouldLoadDefaultData()
        {
            var repository = new JsonSlotRepository(_testFilePath);

            File.WriteAllText(_testFilePath, "Invalid JSON Content");

            Assert.That(repository.Balance, Is.EqualTo(1000));
            Assert.That(repository.History, Is.Empty);
        }

        [Test]
        public void RecordSlotResult_ShouldUpdateBalance_WhenWinning()
        {
            var repository = new JsonSlotRepository(_testFilePath);

            var winResult = new SlotResult(
                GameName: "DummySlot",
                PlayedAt: DateTime.Now,
                Symbols: ["7", "7", "7"],
                Bet: 100,
                Payout: 500
            );

            repository.RecordSlotResult(winResult);

            // 1000-100+500 = 1400
            Assert.That(repository.Balance, Is.EqualTo(1400));
            Assert.That(repository.History.Count, Is.EqualTo(1));
        }

        [Test]
        public void RecordSlotResult_ShouldUpdateBalance_WhenLosing()
        {
            var repository = new JsonSlotRepository(_testFilePath);
            var loseResult = new SlotResult(
                GameName: "DummySlot",
                PlayedAt: DateTime.Now,
                Symbols: ["7", "8", "9"],
                Bet: 200,
                Payout: 0
            );
            repository.RecordSlotResult(loseResult);
            // 1000-200+0 = 800
            Assert.That(repository.Balance, Is.EqualTo(800));
            Assert.That(repository.History.Count, Is.EqualTo(1));
        }
    }
}
