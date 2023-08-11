#if NET7_0_OR_GREATER
using Microsoft.AspNetCore.Http.Features;
using NLog.LayoutRenderers;
using NLog.Web.Internal;
using System.Text;

namespace NLog.Web.LayoutRenderers
{
    /// <summary>
    /// The id of the WebTransport session
    /// <remarks>
    /// <code>${aspnet-web-transport-session-id}</code>
    ///
    /// To use this preview API, you must enable preview features in your project by setting the EnablePreviewFeatures property to True in your project file.
    /// For more information, see https://aka.ms/dotnet-preview-features.
    ///
    /// WebTransport is a preview feature.
    /// </remarks>
    /// </summary>
    [LayoutRenderer("aspnet-web-transport-session-id")]
    public class AspNetWebTransportSessionIdLayoutRenderer : AspNetLayoutRendererBase
    {
        /// <inheritdoc/>
        protected override void DoAppend(StringBuilder builder, LogEventInfo logEvent)
        {
#pragma warning disable CA2252
            var webTransportFeature = HttpContextAccessor.HttpContext.TryGetFeature<IWebTransportSession>();
            builder.Append(webTransportFeature?.SessionId);
#pragma warning restore CA2252
        }
    }
}
#endif
