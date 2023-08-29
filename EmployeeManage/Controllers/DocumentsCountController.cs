using EmployeeManage.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EmployeeManage.Controllers
{
    public class DocumentsCountController : Controller
    {
        private readonly IDocumentCount _documentCount;
        public DocumentsCountController(IDocumentCount documentCount)
        {
            _documentCount = documentCount;
        }

        public async Task<IActionResult> DocumentCountAction()
        {
            var documentcount = await _documentCount.DocumentsInfoList();
            return View(documentcount);
        }
    }
}