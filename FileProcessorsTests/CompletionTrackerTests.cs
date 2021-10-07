using Microsoft.VisualStudio.TestTools.UnitTesting;
using FileAnalyzer;

namespace FileProcessorsTests
{
    [TestClass]
    public class CompletionTrackerTests
    {
        [TestMethod]
        public void CompletionTrackerShouldReturnZeroPercentageAtStart()
        {
            var tracker = new CompletionTracker(100);
            Assert.AreEqual(0, tracker.CurrentPercentage);
        }

        [TestMethod]
        public void CompletionTrackerShouldReturnCorrectPercentageAfterUpdate1()
        {
            var tracker = new CompletionTracker(200);
            tracker.UpdateCompletion(25);
            Assert.AreEqual(12, tracker.CurrentPercentage);
        }

        [TestMethod]
        public void CompletionTrackerShouldReturnCorrectPercentageAfterUpdate2()
        {
            var tracker = new CompletionTracker(200);
            tracker.UpdateCompletion(50);
            Assert.AreEqual(25, tracker.CurrentPercentage);
        }

        [TestMethod]
        public void CompletionTrackerShouldReturnOneHundredPercentageWhenUpdateReachesTarget()
        {
            var tracker = new CompletionTracker(75);
            tracker.UpdateCompletion(50);
            tracker.UpdateCompletion(25);
            Assert.AreEqual(100, tracker.CurrentPercentage);
        }

        [TestMethod]
        public void CompletionTrackerShouldReturnOneHundredPercentageWhenUpdateExceedsTarget()
        {
            var tracker = new CompletionTracker(75);
            tracker.UpdateCompletion(50);
            tracker.UpdateCompletion(50);
            Assert.AreEqual(100, tracker.CurrentPercentage);
        }
    }
}
