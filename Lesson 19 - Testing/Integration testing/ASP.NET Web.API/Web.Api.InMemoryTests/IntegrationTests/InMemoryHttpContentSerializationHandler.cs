using System;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Web.Api.InMemoryTests.IntegrationTests
{
    /// <summary>
    /// http://blogs.msdn.com/b/kiranchalla/archive/2012/05/06/in-memory-client-amp-host-and-integration-testing-of-your-web-api-service.aspx
    /// </summary>
    internal sealed class InMemoryHttpContentSerializationHandler : DelegatingHandler
    {
        public InMemoryHttpContentSerializationHandler(HttpMessageHandler innerHandler) : base(innerHandler)
        {
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request");
            }

            // Replace the original content with a StreamContent before the request
            // passes through upper layers in the stack
            request.Content = ConvertToStreamContent(request.Content);

            return base.SendAsync(request, cancellationToken).ContinueWith(responseTask =>
            {
                var response = responseTask.Result;

                // Replace the original content with a StreamContent before the response
                // passes through lower layers in the stack
                response.Content = ConvertToStreamContent(response.Content);

                return response;
            }, cancellationToken);
        }

        private static StreamContent ConvertToStreamContent(HttpContent originalContent)
        {
            if (originalContent == null)
            {
                return null;
            }

            var streamContent = originalContent as StreamContent;
            if (streamContent != null)
            {
                return streamContent;
            }

            var ms = new MemoryStream();

            // **** NOTE: ideally you should NOT be doing calling Wait() as its going to block this thread ****
            // if the original content is an ObjectContent, then this particular CopyToAsync() call would cause the MediaTypeFormatters to 
            // take part in Serialization of the ObjectContent and the result of this serialization is stored in the provided target memory stream.
            originalContent.CopyToAsync(ms).Wait();

            // Reset the stream position back to 0 as in the previous CopyToAsync() call,
            // a formatter for example, could have made the position to be at the end after serialization
            ms.Position = 0;

            streamContent = new StreamContent(ms);

            // copy headers from the original content
            foreach (var header in originalContent.Headers)
            {
                streamContent.Headers.TryAddWithoutValidation(header.Key, header.Value);
            }

            return streamContent;
        }
    }
}