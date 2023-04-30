using GlowingNews.Client.DTOs.Error;
using OneOf;
using OneOf.Types;

namespace GlowingNews.Client.responses.Base
{
    public class CreateResponse : OneOfBase<Success<string>, Success<Guid>, List<ValidationFailedDto>, Error<string>>
    {
        protected CreateResponse(OneOf<Success<string>, Success<Guid>, List<ValidationFailedDto>, Error<string>> input)
            : base(input)
        {
        }

        public static implicit operator CreateResponse(Success<string> _) => new(_);
        public static implicit operator CreateResponse(Error<string> _) => new(_);
        public static implicit operator CreateResponse(Success<Guid> _) => new(_);
        public static implicit operator CreateResponse(List<ValidationFailedDto> _) => new(_);
    }
}