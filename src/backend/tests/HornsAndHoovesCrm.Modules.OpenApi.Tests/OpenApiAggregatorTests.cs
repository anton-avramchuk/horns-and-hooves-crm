// using HornsAndHoovesCrm.Modules.OpenApi.Services;
// using Moq;
//
// namespace HornsAndHoovesCrm.Modules.OpenApi.Tests;
//
// public class OpenApiAggregatorTests
// {
//     [Fact]
//     public async Task GetCombinedOpenApiDocumentAsync_ReturnsCombinedDocument()
//     {
//         // Arrange
//         var mockHttpFactory = new Mock<IHttpClientFactory>();
//         var mockAddressProvider = new Mock<IClusterAddressProvider>();
//
//         var addresses = new List<string>
//         {
//             "http://localhost:5001/",
//             "http://localhost:5002/"
//         };
//         mockAddressProvider.Setup(x => x.GetClusterAddresses()).Returns(addresses);
//
//         var openApiJson1 = @"
//         {
//             ""openapi"": ""3.0.1"",
//             ""info"": { ""title"": ""Service1"", ""version"": ""v1"" },
//             ""paths"": {
//                 ""/api/service1"": {
//                     ""get"": {
//                         ""responses"": { ""200"": { ""description"": ""OK"" } }
//                     }
//                 }
//             }
//         }";
//
//         var openApiJson2 = @"
//         {
//             ""openapi"": ""3.0.1"",
//             ""info"": { ""title"": ""Service2"", ""version"": ""v1"" },
//             ""paths"": {
//                 ""/api/service2"": {
//                     ""get"": {
//                         ""responses"": { ""200"": { ""description"": ""OK"" } }
//                     }
//                 }
//             }
//         }";
//
//         var handler = new FakeHttpMessageHandler(new Dictionary<string, string>
//         {
//             { "http://localhost:5001/openapi/v1.json", openApiJson1 },
//             { "http://localhost:5002/openapi/v1.json", openApiJson2 }
//         });
//
//         var httpClient = new HttpClient(handler);
//         mockHttpFactory.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(httpClient);
//
//         var aggregator = new OpenApiAggregator(mockHttpFactory.Object, mockAddressProvider.Object);
//
//         // Act
//         var combinedDoc = await aggregator.GetCombinedOpenApiDocumentAsync();
//
//         // Assert
//         Assert.NotNull(combinedDoc);
//         Assert.Equal(2, combinedDoc.Paths.Count);
//         Assert.Contains("/api/service1", combinedDoc.Paths.Keys);
//         Assert.Contains("/api/service2", combinedDoc.Paths.Keys);
//     }
// }