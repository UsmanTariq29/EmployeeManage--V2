using EmployeeManage.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeManage.Model
{
    public class UserRepo : IUser
    {
        private EmployeesDBContext _db;

        public UserRepo(EmployeesDBContext db)
        {
            _db = db;
        }
        public async Task<TblUser> user(string userName, string Password, CancellationToken token = default)
        {
            var result = _db.TblUsers
                .AsNoTracking()
                .Where(u => u.UserName.Equals(userName))
                .Where(u => u.Password == Password)
                .FirstOrDefaultAsync(token);

            return await result;
        }
    }
}