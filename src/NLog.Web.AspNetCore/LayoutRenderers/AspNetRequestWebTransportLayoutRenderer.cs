#if NET7_0_OR_GREATER
using Microsoft.AspNetCore.Http.Features;
using NLog.LayoutRenderers;
using NLog.Web.Internal;
using System.Text;

namespace NLog.Web.LayoutRenderers
{
    /// <summary>
    /// Indicates if this request is a WebTransport request.
    /// <remarks>
    /// <code>${aspnet-web-transport}</code>
    ///
    /// To use this preview API, you must enable preview features in your project by setting the EnablePreviewFeatures property to True in your project file.
    /// For more information, see https://aka.ms/dotnet-preview-features.
    ///
    /// WebTransport is a preview feature.
    /// </remarks>
    /// </summary>
    [LayoutRenderer("aspnet-web-transport")]
    public class AspNetRequestWebTransportLayoutRenderer : AspNetLayoutRendererBase
    {
        /// <inheritdoc/>
        protected override void DoAppend(StringBuilder builder, LogEventInfo logEvent)
        {
#pragma warning disable CA2252
            var webTransportFeature = HttpContextAccessor.HttpContext.TryGetFeature<IHttpWebTransportFeature>();
            builder.Append(webTransportFeature?.IsWebTransportRequest == true? '1' : '0');
#pragma warning restore CA2252
        }
    }
}
#endif
