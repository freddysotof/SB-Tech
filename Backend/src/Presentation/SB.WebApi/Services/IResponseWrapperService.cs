using SB.WebApi.Wrappers;

namespace SB.WebApi.Services
{
    public interface IResponseWrapperService<T> where T : class
    {
        Task<SuccessResponseWrapper<T>> WrapPagedResponseAsync(string response);
        Task<SuccessResponseWrapper<T>> WrapResponseAsync(string response);
        //PagedResponse<DestinationModel> PagedResponse(List<SourceData> rawData, int? limit, int offset);
    }
}
