using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc;

using System.Security.Claims;
using SBEU.Gramm.DataLayer.DataBase.Entities.Base;
using SBEU.Gramm.Middleware;
using Serilog;
using static SBEU.Gramm.Api.Service.CustomConsole;
using static SBEU.Gramm.Api.Service.ConsoleLog;

namespace SBEU.Gramm.Api.Service
{
    public class ControllerExt : Controller
    {
        /* A property that returns the user id of the current user. */
        public string UserId
        {
            get
            {
                var claimIdentity = this.User.Identity as ClaimsIdentity;
                var userId = claimIdentity.Claims.First(x => x.Type == "Id").Value;
                return userId;
            }
        }

        /// <summary>
        /// > This function returns a BadRequest response with a custom status code
        /// </summary>
        /// <param name="error">The error message to be returned to the client.</param>
        /// <param name="code">The HTTP status code to return.</param>
        /// <returns>
        /// A BadRequest object with a custom status code.
        /// </returns>
        protected IActionResult BadRequest(string error, int code)
        {
            var bad = BadRequest(error);
            bad.StatusCode = code;
            return bad;
        }

        protected async Task<IActionResult> Try(Func<Task<IActionResult>> action, [CallerMemberName] string name = "")
        {
            
            await WriteLog(CL($@"Called "),
                CL($@"{name}",ConsoleColor.DarkYellow),
                CL(" from "),
                CL($@"{GetType().Name}",ConsoleColor.Green),
                CL(", "),
                CL($@"{HttpContext.Connection.RemoteIpAddress}", ConsoleColor.DarkMagenta, true));
            var sw = new Stopwatch();
            sw.Start();
            try
            {
                return await action.Invoke();
            }
            catch (Exception e)
            {
                await WriteLog(CL($@"{e.Message}",ConsoleColor.DarkRed,true));

                Log.Logger.Error(e.Message);
                if (e?.InnerException?.Message != null)
                {
                    Log.Logger.Error("\n\n" +e.InnerException.Message+"\n\n");
                }
                return BadRequest(e.Message,e.Status());
            }
            finally
            {
                await WriteLog(CL("End of request. Request time = "),
                    CL($@"{sw.ElapsedMilliseconds}ms", ConsoleColor.Cyan, true));
            }
        }
    }

    public static class ControllerMethodExt {
        
    }
}
