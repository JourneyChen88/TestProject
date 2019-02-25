using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApplication1.Pages
{
   
    public class UserModel : PageModel
    {
        public string Message { get; set; }
        public void OnGet()
        {
            Message = "用户列表.";
        }
    }
}