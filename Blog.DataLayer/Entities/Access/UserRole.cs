using Blog.DataLayer.Entities.Account;
using Blog.DataLayer.Entities.Common;

namespace Blog.DataLayer.Entities.Access
{
    public class UserRole : BaseEntity
    {
        #region properties

        public long UserId { get; set; }

        public long RoleId { get; set; }

        #endregion

        #region Relations

        public User User { get; set; }

        public Role Role { get; set; }

        #endregion
    }
}
