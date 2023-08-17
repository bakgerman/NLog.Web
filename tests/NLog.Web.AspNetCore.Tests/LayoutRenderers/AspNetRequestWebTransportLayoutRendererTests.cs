#if NET7_0_OR_GREATER

using Microsoft.AspNetCore.Http.Features;
using NLog.Web.LayoutRenderers;
using NSubstitute;
using Xunit;

namespace NLog.Web.Tests.LayoutRenderers
{
    public class AspNetRequestWebTransportLayoutRendererTests : LayoutRenderersTestBase<AspNetRequestWebTransportLayoutRenderer>
    {
        [Fact]
        public void TrueTest()
        {
            // Arrange
            var (renderer, httpContext) = CreateWithHttpContext();
#pragma warning disable CA2252
            var webTransportFeature = Substitute.For<IHttpWebTransportFeature>();
            webTransportFeature.IsWebTransportRequest.Returns(true);

            var collection = new FeatureCollection();
            collection.Set<IHttpWebTransportFeature>(webTransportFeature);
#pragma warning restore CA2252

            httpContext.Features.Returns(collection);

            // Act
            string result = renderer.Render(new LogEventInfo());

            // Assert
            Assert.Equal("1", result);
        }

        [Fact]
        public void FalseTest()
        {
            // Arrange
            var (renderer, httpContext) = CreateWithHttpContext();

#pragma warning disable CA2252
            var webTransportFeature = Substitute.For<IHttpWebTransportFeature>();
            webTransportFeature.IsWebTransportRequest.Returns(false);

            var collection = new FeatureCollection();
            collection.Set<IHttpWebTransportFeature>(webTransportFeature);
#pragma warning restore CA2252

            httpContext.Features.Returns(collection);

            // Act
            string result = renderer.Render(new LogEventInfo());

            // Assert
            Assert.Equal("0", result);
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
            Assert.Equal("0", result);
        }

        protected override void NullRendersEmptyString()
        {
            // Arrange
            var (renderer, _) = CreateWithHttpContext();

            // Act
            string result = renderer.Render(LogEventInfo.CreateNullEvent());

            // Assert
            Assert.Equal("0", result);
        }
    }
}
#endif
