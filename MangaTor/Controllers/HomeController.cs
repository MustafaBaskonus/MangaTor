using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.Context;
using DAL.Entities;
using MangaTor.Models;
using DAL.Entities.RequestParameters;
using Services.Contacts;

public class HomeController : Controller
{

    private readonly IServiceManager _services;

    public HomeController(IServiceManager services)
    {
        _services = services;
    }

    public async Task<IActionResult> Index([FromQuery] ChapterRequestParameters chapteRequest)
    {
        var homeChapters = _services.ChapterService.HomeChapters(chapteRequest,8).ToList();
        Pagination pagination = new Pagination() 
        {   
            CurrentPage=chapteRequest.PageNumber,
            ItemsPerPage=chapteRequest.PageSize,
            TotalItems=_services.ChapterService.TotalChapter(false)
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