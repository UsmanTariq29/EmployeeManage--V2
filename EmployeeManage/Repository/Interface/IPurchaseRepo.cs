using EmployeeManage.Models;
using EmployeeManage.ViewModels.Request;
using EmployeeManage.ViewModels.Responses;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Data;
using System.Security.Policy;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeManage.Repository.Interface
{
    public interface IPurchaseRepo
    {
        Task SavePurchase(purchaseRequest model, DataTable purchasedItems, string userGUID, string branchGUID, string companyGUID,
           CancellationToken token = default);
        Task UpdatePurchase(purchaseRequest model, DataTable purchasedItems, string userGUID, string branchGUID, string companyGUID,
           CancellationToken token = default);
        Task SuperVision(int purchaseMasterID, string userGUID, string branchGUID, string companyGUID,
        CancellationToken token = default);
        Task Clearence(int purchaseMasterID, string PurchaseNumber, decimal cash, decimal Account, decimal Bank, decimal Check, string userGUID, string branchGUID, string companyGUID,
           CancellationToken token = default);
        Task<VoucherDisplayNumberResponseVM> VoucherDisplayNo(int purchaseMasterID, string branchGUID, string companyGUID,
           CancellationToken token = default);
        Task RemovePurchaseDetailAsync(int masterId, CancellationToken token = default);
        Task<IEnumerable<AllPurchaseResponseVM>> GetAllPurchasePayment(CancellationToken token = default);
        Task<IEnumerable<AllPurchaseResponseVM>> GetAllPurchase(CancellationToken token = default);
        //Task<TblPurchaseMaster> GetPurchase(int id, CancellationToken token = default);
        Task<IEnumerable<AllPurchaseResponseVM>> GetDetailsPurchase(int id, CancellationToken token = default);
        Task<IEnumerable<AllPurchaseResponseVM>> GetPurchaseById(int purchaseMasterId, CancellationToken token = default);
        Task<TblPurchaseDetail> GetPurchaseDetails(int id, CancellationToken token = default);
    }
}
