using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NumberPairSearcher.Tests
{
    [TestClass]
    public class NumberPairSearcherTests
    {
        [TestMethod]
        public void Search_NullSource_ShouldThrow()
        {
            // Arrange

            // Act
            Action ctor = () => new NumberPairSearcher(null);

            // Assert
            ctor.ShouldThrow<ArgumentNullException>();
        }

        [TestMethod]
        public void Search_EmptySource_ShouldReturnEmptyResult()
        {
            // Arrange
            var target = new NumberPairSearcher(new int[0]);

            // Act
            var actual = target.Search(100);

            // Assert
            actual.Should().BeEmpty();
        }

        [TestMethod]
        public void Search_SourceHasNoPairs_ShouldReturnEmptyResult()
        {
            // Arrange
            var target = new NumberPairSearcher(new[] { 1, 5, 7 });

            // Act
            var actual = target.Search(100);

            // Assert
            actual.Should().BeEmpty();
        }

        [TestMethod]
        public void Search_SourceHasOnePair_ShouldReturnResult()
        {
            // Arrange
            var target = new NumberPairSearcher(new[] { 1, 5, 7 });

            // Act
            var actual = target.Search(6);

            // Assert
            actual.ShouldAllBeEquivalentTo(new[] { Tuple.Create(1, 5) });
        }

        [TestMethod]
        public void Search_SourceHasTwoPairs_ShouldReturnResult()
        {
            // Arrange
            var target = new NumberPairSearcher(new[] { 1, 5, 7, 2, 4 });

            // Act
            var actual = target.Search(6);

            // Assert
            actual.ShouldAllBeEquivalentTo(new[] { Tuple.Create(1, 5), Tuple.Create(2, 4) });
        }

        [TestMethod]
        public void Search_SourceHasDuplicates_ShouldReturnResult()
        {
            // Arrange
            var target = new NumberPairSearcher(new[] { 3, 3, 7 });

            // Act
            var actual = target.Search(6);

            // Assert
            actual.ShouldAllBeEquivalentTo(new[] { Tuple.Create(3, 3) });
        }

        [TestMethod]
        public void Search_SourceHasTargetButNoZero_ShouldNotReturnResult()
        {
            // Arrange
            var target = new NumberPairSearcher(new[] { 6, 7 });

            // Act
            var actual = target.Search(6);

            // Assert
            actual.Should().BeEmpty();
        }

        [TestMethod]
        public void Search_SourceHasTargetAndZero_ShouldReturnResult()
        {
            // Arrange
            var target = new NumberPairSearcher(new[] { 6, 7, 0 });

            // Act
            var actual = target.Search(6);

            // Assert
            actual.ShouldAllBeEquivalentTo(new[] { Tuple.Create(0, 6) });
        }

        [TestMethod]
        public void Search_SourceHasPairWithNegativeNumber_ShouldReturnResult()
        {
            // Arrange
            var target = new NumberPairSearcher(new[] { 7, -1 });

            // Act
            var actual = target.Search(6);

            // Assert
            actual.ShouldAllBeEquivalentTo(new[] { Tuple.Create(-1, 7) });
        }

        [TestMethod]
        public void Search_SourceHasSeveralPairs_ShouldReturnResult()
        {
            // Arrange
            var target = new NumberPairSearcher(new[] { 1, 5, 1, 5, -1, 7, 2, 4 });

            // Act
            var actual = target.Search(6);

            // Assert
            actual.ShouldAllBeEquivalentTo(new[] { Tuple.Create(1, 5), Tuple.Create(1, 5), Tuple.Create(-1, 7), Tuple.Create(2, 4) });
        }
    }
}
