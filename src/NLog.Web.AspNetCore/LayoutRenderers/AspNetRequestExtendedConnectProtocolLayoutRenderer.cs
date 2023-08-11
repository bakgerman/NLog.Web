#if NET7_0_OR_GREATER
using Microsoft.AspNetCore.Http.Features;
using NLog.LayoutRenderers;
using NLog.Web.Internal;
using System.Text;

namespace NLog.Web.LayoutRenderers
{
    /// <summary>
    /// The :protocol in the current request if the request a Extended CONNECT request that can be transitioned to an opaque connection via AcceptAsync.
    /// <remarks>
    /// <code>${aspnet-request-extended-connect-protocol}</code>
    /// </remarks>
    /// </summary>
    [LayoutRenderer("aspnet-request-extended-connect-protocol")]
    public class AspNetRequestExtendedConnectProtocolLayoutRenderer : AspNetLayoutRendererBase
    {
        /// <inheritdoc/>
        protected override void DoAppend(StringBuilder builder, LogEventInfo logEvent)
        {
            var extendedConnectFeature = HttpContextAccessor.HttpContext.TryGetFeature<IHttpExtendedConnectFeature>();
            builder.Append(extendedConnectFeature?.Protocol);
        }
    }
}
#endif