#if NET7_0_OR_GREATER
using Microsoft.AspNetCore.Http.Features;
using NLog.LayoutRenderers;
using NLog.Web.Internal;
using System.Text;

namespace NLog.Web.LayoutRenderers
{
    /// <summary>
    /// Indicates if the current request is a Extended CONNECT request that can be transitioned to an opaque connection via AcceptAsync.
    /// 
    /// 1 if true
    /// 0 if false
    /// <remarks>
    /// <code>${aspnet-request-extended-connect}</code>
    /// </remarks>
    /// </summary>
    [LayoutRenderer("aspnet-request-extended-connect")]
    public class AspNetRequestExtendedConnectLayoutRenderer : AspNetLayoutRendererBase
    {
        /// <inheritdoc/>
        protected override void DoAppend(StringBuilder builder, LogEventInfo logEvent)
        {
            var extendedConnectFeature = HttpContextAccessor.HttpContext.TryGetFeature<IHttpExtendedConnectFeature>();
            builder.Append(extendedConnectFeature?.IsExtendedConnect == true ? '1' : '0');
        }
    }
}
#endif