// using FluentAssertions;
// using TransactionVisualizer.Models.DataStructureModels.Graph;
// using TransactionVisualizer.Models.Transaction;
// using TransactionVisualizer.Utility.Builders.GraphBuilders.EdgeBuilders;
// using TransactionVisualizer.Utility.Converters;
// using Xunit;
//
// namespace TransactionVisualizer.Utility.Builders.GraphBuilders.EdgeBuilders.Tests
// {
//     public class TransactionToEdgeTests
//     {
//         [Fact]
//         public void Build_ShouldReturnEdgeWithCorrectProperties()
//         {
//             // Arrange
//             var transaction = new Transaction
//             {
//                 Id = 1,
//                 SourceAccount = 123,
//                 DestinationAccount = 456,
//                 TransactionType = TransactionType.Paya,
//                 Amount = 100.0m,
//                 Date = "2023-07-12"
//             };
//             var builder = new TransactionToEdge();
//
//             // Act
//             var edge = builder.Build(transaction);
//
//             // Assert
//             edge.Should().NotBeNull();
//             edge.Source.Should().Be(transaction.SourceAccount);
//             edge.Destination.Should().Be(transaction.DestinationAccount);
//             edge.Content.Should().Be(transaction);
//             edge.Weight.Should().Be(transaction.Amount);
//         }
//     }
// }