#if NET7_0_OR_GREATER

using Microsoft.AspNetCore.Http.Features;
using NLog.Web.LayoutRenderers;
using NSubstitute;
using Xunit;

namespace NLog.Web.Tests.LayoutRenderers
{
    public class AspNetWebTransportSessionIdLayoutRendererTests : LayoutRenderersTestBase<AspNetWebTransportSessionIdLayoutRenderer>
    {
        [Fact]
        public void SuccessTest()
        {
            // Arrange
            var (renderer, httpContext) = CreateWithHttpContext();

#pragma warning disable CA2252
            var webTransportSessionFeature = Substitute.For<IWebTransportSession>();
            webTransportSessionFeature.SessionId.Returns(54321);

            var collection = new FeatureCollection();
            collection.Set<IWebTransportSession>(webTransportSessionFeature);
#pragma warning restore CA2252

            httpContext.Features.Returns(collection);

            // Act
            string result = renderer.Render(new LogEventInfo());

            // Assert
            Assert.Equal("54321", result);
        }

        [Fact]
        public void NullTest()
        {
            // Arrange
            var (renderer, httpContext) = CreateWithHttpContext();

            var collection = new FeatureCollection();
            httpContext.Features.Returns(collection);

            // Act
            string result = renderer.Render(new LogEventInfo());

            // Assert
            Assert.Equal("", result);
        }

        protected override void NullRendersEmptyString()
        {
            // Arrange
            var (renderer, _) = CreateWithHttpContext();

            // Act
            string result = renderer.Render(LogEventInfo.CreateNullEvent());

            // Assert
            Assert.Equal("", result);
        }
    }
}
#endif