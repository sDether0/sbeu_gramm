using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SBEU.Gramm.Api.Service;
using SBEU.Gramm.DataLayer.DataBase;

using System.IO;
using SBEU.Gramm.DataLayer.DataBase.Entities;
using SBEU.Gramm.Middleware;
using SBEU.Gramm.Middleware.Repositories;
using SBEU.Gramm.Models;
using SBEU.Gramm.Models.Responses;
using Swashbuckle.AspNetCore.Annotations;

using static SBEU.Gramm.Api.Service.CustomConsole;
using static SBEU.Gramm.Api.Service.ConsoleLog;

namespace SBEU.Gramm.Api.Controllers
{
    [Route("[controller]")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UploadController : ControllerExt
    {
        private readonly ApiDbContext _context;
        private readonly IMapper _mapper;
        
        public UploadController(ApiDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [SwaggerOperation("Upload content")]
        [SwaggerResponse(200, "Return id/name", typeof(string))]
        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file, [FromQuery]NContentObject.ContentType type)
        {
            return await Try(async () =>
            {
               
                string nname;
                if (type == NContentObject.ContentType.Image)
                {
                    if (!Directory.Exists("Contents/Temp")) Directory.CreateDirectory("Contents/Temp");
                    await using (var stream = System.IO.File.Create("Contents/Temp/" + file.FileName))
                    {
                        await file.CopyToAsync(stream);
                    }

                    nname = await ImageConvertion.ProcessImage(file.FileName);
                }
                else
                {
                    nname = Guid.NewGuid().ToString() + Guid.NewGuid();
                    await using (var stream = System.IO.File.Create("Contents/" + nname))
                    {
                        await file.CopyToAsync(stream);
                    }
                }

                var link = Constants.Url + nname;

                var content = new NContentObject
                {
                    Id = nname,
                    Link = link,
                    Type = type,
                    PhysicalPath = "Contents/" + nname
                };
                await _context.AddAsync(content);
                await _context.SaveChangesAsync();
                return Ok( nname);
            });
        }

        [SwaggerOperation("Download file")]
        [SwaggerResponse(200, "File",null,"image/jpeg", "video/mp4", "audio/mpeg")]
        [HttpGet("/load/{name}")]
        public async Task<FileResult> Load(string name)
        {
           
            
                await WriteLog(CL($@" : Called "), CL(nameof(Load), ConsoleColor.DarkYellow), CL(" from "),
                    CL(GetType().Name, ConsoleColor.Green), CL(" with id = "),
                    CL($"{name}", ConsoleColor.DarkCyan, true));
                var xname = name.Replace("-low", "");
                xname = xname.Replace("-middle", "");
                var cont = await _context.ContentObjects.FindAsync(xname);
                string mime = "";
                string ext = "";
                switch (cont.Type)
                {
                    case NContentObject.ContentType.Image:
                        mime = "image/jpeg";
                        ext = ".jpeg";
                        break;
                    case NContentObject.ContentType.Video:
                        mime = "video/mp4";
                        ext = ".mp4";
                        break;
                    case NContentObject.ContentType.Sound:
                        mime = "audio/mpeg";
                        ext = ".mpeg";
                        break;
                }

                return File(await System.IO.File.ReadAllBytesAsync("Contents/" + name), mime, name + ext);
        }

        
    }
}
 