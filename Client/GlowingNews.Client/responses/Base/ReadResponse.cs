using OneOf.Types;
using OneOf;
using GlowingNews.Client.DTOs.Error;

namespace GlowingNews.Client.responses.Base
{
    public class ReadResponse<TResponse> : OneOfBase<Success<TResponse>, NotFound, List<ValidationFailedDto>>
        where TResponse : class
    {
        protected ReadResponse(OneOf<Success<TResponse>, NotFound, List<ValidationFailedDto>> input)
            : base(input)
        {
        }

        public static implicit operator ReadResponse<TResponse>(Success<TResponse> _) => new(_);
        public static implicit operator ReadResponse<TResponse>(NotFound _) => new(_);
        public static implicit operator ReadResponse<TResponse>(List<ValidationFailedDto> _) => new(_);
    }
}