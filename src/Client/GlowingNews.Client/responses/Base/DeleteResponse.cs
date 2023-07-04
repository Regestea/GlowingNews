using GlowingNews.Client.DTOs.Error;
using OneOf.Types;
using OneOf;

namespace GlowingNews.Client.responses.Base
{
    public class DeleteResponse : OneOfBase<Success>
    {
        protected DeleteResponse(OneOf<Success> input)
            : base(input)
        {
        }

        public static implicit operator DeleteResponse(Success _) => new(_);
    }
}