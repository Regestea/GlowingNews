using GlowingNews.Client.DTOs.Error;
using OneOf;
using OneOf.Types;

namespace GlowingNews.Client.responses.Base
{
    public class UpdateResponse : OneOfBase<Success<Guid>, ValidationFailedDto>
    {
        protected UpdateResponse(OneOf<Success<Guid>, ValidationFailedDto> input)
            : base(input)
        {
        }

        public static implicit operator UpdateResponse(Success<Guid> _) => new(_);

        public static implicit operator UpdateResponse(ValidationFailedDto _) => new(_);
    }
}