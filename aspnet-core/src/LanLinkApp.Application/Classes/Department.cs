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

namespace LanLinkApp.Classes
{
   

    //------------------------------------------------------------------------
    // INTERFACE
    //------------------------------------------------------------------------
          

   

    // [IService] Estrutura criada Manuamente, atualmente desativada, oriunda do Pacote de Serviços, que relaciona o Repositório com o Angular .
    //public class DepartmentAppService2 : ApplicationService, IDepartmentAppService
    //{
    //    public DepartmentAppService2()
    //    {
    //        LocalizationSourceName = "DepartmentManager";
    //    }

    //    private readonly IRepository<Department> _DepartmentRepository;

    //    public DepartmentAppService2(IRepository<Department> DepartmentRepository)
    //    {
    //        _DepartmentRepository = DepartmentRepository;
    //    }

    //    public void CreateDepartment(DepartmentDTO input)
    //    {

    //        var Department = _DepartmentRepository.FirstOrDefault(p => p.Name == input.Name);
    //        if (Department != null)
    //        {
    //            throw new UserFriendlyException("There is already a Department with that name");
    //        }

    //        // Write some logs(Logger is defined in ApplicationService class)
    //        Logger.Info("Creating a new department with description: " + input.Name);

    //        Department = new Department { Name = input.Name };
    //        _DepartmentRepository.Insert(Department);
    //    }

    //    public void DeleteDepartment(DepartmentDTO input)
    //    {

    //        // Write some logs(Logger is defined in ApplicationService class)
    //        Logger.Info("Removing an old department with description: " + input.Name);

    //        var Department = _DepartmentRepository.FirstOrDefault(p => p.Name == input.Name);
    //        _DepartmentRepository.Delete(Department);

    //        //Seria interessante remover os departamentos dos usuários existentes, mas ainda não se 
    //        //compreendeu 100%  o funcionamento das bibliotecas padrões do ASP.NET BOILERPLATE
    //    }
    //}

    // [IService] Estrutura Padrão, oriunda do Pacote de Serviços, que relaciona o Repositório com o Angular .

    // [DTO] Estrutura padrão de comunicação entre o Angular o ASP.Net   


    [AutoMapTo(typeof(Department))]
    public class DepartmentDTO : EntityDto, IHasCreationTime
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public DateTime CreationTime { get ; set ; }
    }

    // [IApplicationService] Pacote de Serviços que servem de interface entre o Angular o ASP.Net
    public interface IDepartmentAppService : IApplicationService
    {
        void CreateOne(string name);               
    }

    public class DepartmentAppService : AsyncCrudAppService<Department, DepartmentDTO>  , IDepartmentAppService
    {

        private readonly IRepository<Department, int> _DepartmentRepository;

        public DepartmentAppService(IRepository<Department> repository)
        : base(repository)
        {
            //CreatePermissionName = "DepartmentManagement";  
            _DepartmentRepository = repository;
        }

        public void CreateOne(string name)
        {
            var Department = new Department { Name = name, CreationTime = DateTime.Now };
            _DepartmentRepository.Insert(Department);
        }
    }

    // [Organizer] Função para organizar
    public class GetAllDepartmentInput : PagedAndSortedResultRequestDto
    {
        public string Name { get; set; }
    }

   
}
