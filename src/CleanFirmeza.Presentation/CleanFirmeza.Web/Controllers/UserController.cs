using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CleanFirmeza.Web.Models;
using Microsoft.AspNetCore.Authorization;
namespace CleanFirmeza.Web.Controllers;

public class UserController : Controller
{
    private readonly ILogger<UserController> _logger;

    public UserController(ILogger<UserController> logger)
    {
        _logger = logger;
    }
    
    [Authorize]
    public IActionResult Index()
    {
        return View();
    }
}