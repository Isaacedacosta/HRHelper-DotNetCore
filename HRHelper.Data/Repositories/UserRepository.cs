using HRHelper.Data.Context;
using HRHelper.Domain.Entities;
using HRHelper.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRHelper.Data.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ExtendDbContext context) : base(context) { }

        public IEnumerable<User> GetAll()
        {
            return Query(item => !item.IsDeleted);
        }
    }
}
