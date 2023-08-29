using EmployeeManage.Models;
using EmployeeManage.ViewModels.Responses;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeManage.Repository.Interface
{
  public interface IBarcodeRepo
    {

        Task holdOrderMaster(DataTable orderItems, decimal TotalAmount, string userId, string branchGUID,string CompanyId,string Holdkey, int CustomerId , CancellationToken token = default);
        Task SaveOrderMaster(DataTable orderItems, decimal TotalAmount, decimal cashAmount, decimal balance, string userId, string branchGUID,string CompanyId,int CustomerId, CancellationToken token = default);
        Task<int> SaveCustomerDetails(string name, string PhoneNo,  string NTN, string Cnic ,string userGUID,string CompanyGUID,string BranchGUID, CancellationToken token = default);
        Task<CustomerResponse> GetCustomerData(int id,CancellationToken token = default);
        Task<CustomerResponse> GetwalkingcustomerData(string id,string userGUID, CancellationToken token = default); 
        Task<IList<UnholdOrderResponse>> getHoldedOrder(string id, CancellationToken token = default);
        Task<IEnumerable<BarcodeResponse>> GetAllGeneratedbarcodes(BarcodeResponse model,CancellationToken token = default);
        Task GenerateBarcode(int id, CancellationToken token = default);
        Task<TblItemBarcode> GetBarcode(int id,CancellationToken token = default);
        Task<BarcodeResponse> SearchBarcode(string GeneratedBarcode,CancellationToken token = default);
        Task <IEnumerable<BarcodeResponse>> GetAllItemsBarcode(CancellationToken token = default);

    }
}
