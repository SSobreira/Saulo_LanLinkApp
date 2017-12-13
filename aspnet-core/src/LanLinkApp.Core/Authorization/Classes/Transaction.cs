using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using LanLinkApp.Authorization.Users;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace LanLinkApp.Classes
{
    public class Transaction : Entity, IHasCreationTime
    {

        public const int MaxDescriptionLength = 500;

        [StringLength(MaxDescriptionLength)]
        public virtual string Description { get; set; }

        public virtual double Value { get; set; }

        [ForeignKey("UserCreatorID")]
        public virtual User UserCreator { get; set; }
        public long UserCreatorID { get; set; }

        [ForeignKey("DepartmentCreatorID")]
        public virtual Department DepartmentCreator { get; set; }
        public int DepartmentCreatorID { get; set; }

        public DateTime CreationTime { get ; set; }
    }
}
