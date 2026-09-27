using hairDresser.Application.Interfaces;
using MediatR;

namespace hairDresser.Application.Caching
{
    public class CachingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly ICacheService _cacheService;

        public CachingBehavior(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (request is not ICacheableQuery cacheableQuery)
            {
                return await next();
            }

            var cached = await _cacheService.GetAsync<TResponse>(cacheableQuery.CacheKey, cancellationToken);
            if (cached is not null)
            {
                return cached;
            }

            var response = await next();

            if (response is not null)
            {
                await _cacheService.SetAsync(cacheableQuery.CacheKey, response, cacheableQuery.Expiration, cancellationToken);
            }

            return response;
        }
    }
}