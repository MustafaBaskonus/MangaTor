using DAL.Entities.RequestParameters;
using MangaTor.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Contacts;

namespace MangaTor.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CommentsController : Controller
    {
        private readonly IServiceManager _services;

        public CommentsController(IServiceManager services)
        {
            _services = services;
        }

        public async Task<IActionResult> Index(CommentRequestParameters commentRequest)
        {
            var comments = await _services.CommentService.GetAllCommentsDto(commentRequest);
            Pagination pagination = new Pagination()
            {
                CurrentPage = commentRequest.PageNumber,
                ItemsPerPage = commentRequest.PageSize,
                TotalItems = _services.CommentService.TotolComment(commentRequest),
            };
            return View(new CommentListViewModel()
            {
                Comments = comments,
                Pagination = pagination
            });
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null )
            {
                return RedirectToAction("Index");
            }
            await _services.CommentService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
