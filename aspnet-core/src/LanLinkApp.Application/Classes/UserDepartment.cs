using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using LanLinkApp.Authorization.Users;
using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.Domain.Entities;
using Abp.UI;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using LanLinkApp.Users.Dto;
using Abp.Collections.Extensions;

namespace LanLinkApp.Classes
{
   

    //------------------------------------------------------------------------
    // INTERFACE
    //------------------------------------------------------------------------
                 

    // [DTO] Estrutura padrão de comunicação entre o Angular o ASP.Net   
    [AutoMapTo(typeof(UserDepartment))]
    public class UserDepartmentDTO : EntityDto
    {
        [Required]
        public int RefDepartmentID { get; set; }

        [Required]
        public int RefUserID { get; set; }

        public virtual DepartmentDTO RefDepartment { get; set; }                   

        public virtual UserDto RefUser { get; set; }    

    }


    public interface IUserDepartmentAppService : IAsyncCrudAppService<UserDepartmentDTO>
    {
        void CreateOne(long userid, int depid);

    }


    // [IService] Estrutura Padrão, oriunda do Pacote de Serviços, que relaciona o Repositório com o Angular .        
    public class UserDepartmentAppService : AsyncCrudAppService<UserDepartment, UserDepartmentDTO >    , IUserDepartmentAppService
    {
        private readonly IRepository<Department, int> _DepartmentRepository;
        private readonly IRepository<User, long> _UserRepository;
        private readonly IRepository<UserDepartment, int> _UserDepartmentRepository;

        public UserDepartmentAppService(IRepository<UserDepartment, int> repository)
        : base(repository)
        {
             //CreatePermissionName = "UserDepartmentManagement";
            _UserDepartmentRepository = repository;      
          
        }

        public void CreateOne(long userid, int depid)
        {         
            var UserDepartment = new UserDepartment { RefDepartmentID = depid, RefUserID = userid };
            _UserDepartmentRepository.Insert(UserDepartment);
        }
    }
    


}
