using EmployeeManage.Models;

using System.Threading;
using System.Threading.Tasks;

namespace EmployeeManage.Model
{
    public interface IUser
    {
        Task<TblUser> user(string userName, string Password, CancellationToken token = default);
    }
}