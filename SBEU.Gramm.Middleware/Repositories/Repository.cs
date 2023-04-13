using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SBEU.Gramm.DataLayer.DataBase;
using SBEU.Gramm.Middleware.Repositories.Interfaces;
using Serilog;

namespace SBEU.Gramm.Middleware.Repositories
{
    public class Repository : IRepository
    {
        protected readonly ApiDbContext _context;

        protected readonly ILogger _logger;
        protected Repository( ApiDbContext context, ILogger logger)
        {
            _logger = logger.ForContext<IRepository>();
            _context = context;
        }
    }
}
