using NewsLike.Domain.Common;

namespace NewsLike.Domain.Entities
{
    public class Like : BaseEntity
    {
        public Guid UserId { get; set; }
        public Guid NewsId { get; set; }
    }
}
