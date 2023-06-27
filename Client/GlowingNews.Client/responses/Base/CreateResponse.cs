using GlowingNews.Client.DTOs.Error;
using OneOf;
using OneOf.Types;

namespace GlowingNews.Client.responses.Base
{
    //TODO:Create response return these Success<JwtToken> , Success<IdResult> , Success<MessageResult>
    public class CreateResponse<TResponse> : OneOfBase<Success<TResponse>, Success<Guid>, List<ValidationFailedDto>, Error<string>>
    {
        protected CreateResponse(OneOf<Success<TResponse>, Success<Guid>, List<ValidationFailedDto>, Error<string>> input)
            : base(input)
        {
        }

        public static implicit operator CreateResponse<TResponse>(Success<TResponse> _) => new(_);
        public static implicit operator CreateResponse<TResponse>(Error<string> _) => new(_);
        public static implicit operator CreateResponse<TResponse>(List<ValidationFailedDto> _) => new(_);
    }
}