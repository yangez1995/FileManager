using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class FileManagerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public JsonResult GetFileMessage(string rootPath)
        {
            DirectoryInfo root = new DirectoryInfo(rootPath);

            var folders = from dir in root.EnumerateDirectories()
                        select new FileMessage
                        {
                            Name = dir.Name,
                            DateModified = dir.LastWriteTime.ToString()
                        };

            var files = from file in root.EnumerateFiles()
                        select new FileMessage
                        {
                            Name = file.Name,
                            DateModified = file.LastWriteTime.ToString(),
                            Type = file.Extension,
                            Size = file.Length.ToString()
                        };

            return Json(new { folders, files });
        }
    }

    public class FileMessage
    {
        public string Name { get; set; }
        public string DateModified { get; set; }
        public string Type { get; set; }
        public string Size { get; set; }
    }
}