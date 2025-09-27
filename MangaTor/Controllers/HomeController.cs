using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.Context;
using DAL.Entities;
using MangaTor.Models;
using DAL.Entities.RequestParameters;
using Services.Contacts;

public class HomeController : Controller
{

    private readonly IServiceManager _service;

    public HomeController(IServiceManager service)
    {
        _service = service;
    }

    public async Task<IActionResult> Index([FromQuery] ChapterRequestParameters chapteRequest)
    {
        var homeChapters = _service.ChapterService.HomeChapters(chapteRequest,8).ToList();
        Pagination pagination = new Pagination() 
        {   
            CurrentPage=chapteRequest.PageNumber,
            ItemsPerPage=chapteRequest.PageSize,
            TotalItems=_service.ChapterService.TotalChapter(false)
        };  

        return View(new ChapterListViewModel()
        {
            Chapters = homeChapters,
            Pagination = pagination
        });
    }

    public IActionResult Privacy()
    {
        return View();
    }
}