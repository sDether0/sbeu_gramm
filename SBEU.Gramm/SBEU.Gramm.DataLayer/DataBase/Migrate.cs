using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Hosting;

namespace SBEU.Gramm.DataLayer.DataBase
{
    internal class Migrate : IDesignTimeDbContextFactory<ApiDbContext>
    {
        public ApiDbContext CreateDbContext(string[] args)
        {
            var opt = new DbContextOptionsBuilder<ApiDbContext>();
            opt.UseLazyLoadingProxies().UseNpgsql("User ID=postgres;Password=1namQfeg1;Host=127.0.0.1;Port=5432;Database=SBEU.Gramm;");
            return new ApiDbContext(opt.Options);
        }
    }
}
