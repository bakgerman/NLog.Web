#if NET7_0_OR_GREATER

using Microsoft.AspNetCore.Http.Features;
using NLog.Web.LayoutRenderers;
using NSubstitute;
using Xunit;

namespace NLog.Web.Tests.LayoutRenderers
{
    public class AspNetRequestExtendedConnectProtocolLayoutRendererTests : LayoutRenderersTestBase<AspNetRequestExtendedConnectProtocolLayoutRenderer>
    {
        [Fact]
        public void SuccessTest()
        {
            // Arrange
            var (renderer, httpContext) = CreateWithHttpContext();

            var httpExtendedConnectFeature = Substitute.For<IHttpExtendedConnectFeature>();
            httpExtendedConnectFeature.Protocol.Returns("http");

            var collection = new FeatureCollection();
            collection.Set<IHttpExtendedConnectFeature>(httpExtendedConnectFeature);

            httpContext.Features.Returns(collection);

            // Act
            string result = renderer.Render(new LogEventInfo());

            // Assert
            Assert.Equal("http", result);
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