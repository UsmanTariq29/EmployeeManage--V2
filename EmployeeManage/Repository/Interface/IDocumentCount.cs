using EmployeeManage.ViewModels.Responses;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeManage.Repository.Interface
{
    public interface IDocumentCount
    {
        Task<IEnumerable<TotalDocumentsInfo>> DocumentsInfoList(CancellationToken token = default);
    }
}
