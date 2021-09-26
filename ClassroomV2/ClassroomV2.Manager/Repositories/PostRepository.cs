using ClassroomV2.DataAccessLayer.Repositories;
using ClassroomV2.Manager.Context;
using ClassroomV2.Manager.Entities;
using Microsoft.EntityFrameworkCore;


namespace ClassroomV2.Manager.Repositories
{
    public class PostRepository : Repository<Post, int>, IPostRepository
    {
        public PostRepository(IManagerContext context)
            : base((DbContext)context)
        {
        }
    }
}
