using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SBEU.Gramm.DataLayer.DataBase.Entities.Interfaces;

namespace SBEU.Gramm.DataLayer.DataBase.Entities.Base
{
    public abstract class BaseEntity : IBaseEntity
    {
        [Key]
        public string Id { get; set; }
    }
}
