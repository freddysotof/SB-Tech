using Microsoft.AspNetCore.WebUtilities;
using SB.Domain.Common.Models;

namespace SB.WebApi.Services
{
    public interface IPageUriService
    {
        Task<Uri> GetPageUriAsync(Pagination filter, string route);
    }
    public class PageUriService : IPageUriService
    {
        private readonly string _baseUri;
        public PageUriService(string baseUri)
        {
            _baseUri = baseUri;
        }
        public Task<Uri> GetPageUriAsync(Pagination filter, string route)
        {
            var _enpointUri = new Uri(string.Concat(_baseUri, route));
            var modifiedUri = QueryHelpers.AddQueryString(_enpointUri.ToString(), "limit", filter.Limit?.ToString());
            modifiedUri = QueryHelpers.AddQueryString(modifiedUri, "offset", filter.Offset.ToString());
            return Task.FromResult(new Uri(modifiedUri));
        }
    }
}
