using System.ComponentModel.DataAnnotations;

namespace NewsLike.Domain.Common
{
    public abstract class BaseEntity
    {
        public BaseEntity()
        {
            if (CreatedDate == null) CreatedDate = DateTimeOffset.UtcNow;
        }

        [Key]
        public Guid Id { get; set; }

        public DateTimeOffset? CreatedDate { get; set; }
    }
}
