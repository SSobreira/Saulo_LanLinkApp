using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using LanLinkApp.Authorization.Users;
using Abp.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace LanLinkApp.Classes
{
    public class UserDepartment : Entity
    {
        //------------------------------------------------------------------------
        // BANCO DE DADOS
        //------------------------------------------------------------------------             

        [ForeignKey("RefDepartmentID")]
        public virtual Department RefDepartment  { get; set; }
        public int RefDepartmentID { get; set; }

        [ForeignKey("RefUserID")]
        public virtual User RefUser { get; set; }
        public long RefUserID { get; set; }

    }
}
