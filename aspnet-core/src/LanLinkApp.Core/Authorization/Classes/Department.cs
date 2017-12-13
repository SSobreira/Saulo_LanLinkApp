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
using Abp.EntityFrameworkCore;
using Abp.EntityFrameworkCore.Repositories;
using Abp.AutoMapper;

namespace LanLinkApp.Classes
{

    //------------------------------------------------------------------------
    // BANCO DE DADOS
    //------------------------------------------------------------------------
    public class Department : Entity, IHasCreationTime

    {
        public Department()
        {
            CreationTime = Clock.Now;    
        }

        public const int MaxDescriptionLength = 100;      

        [StringLength(MaxDescriptionLength)]
        public virtual string Name { get; set; }

        public ICollection<UserDepartment> UsersDepartment { get; set; }

        public ICollection<Transaction> Transactions { get; set; }

        public DateTime CreationTime { get   ; set   ; }
        

        // Só funcionaria no EntityFramework, mas estamos no EntityFramework Core
        //internal sealed class Configuration : DbMigrationsConfiguration<SimpleTaskSystem.EntityFramework.SimpleTaskSystemDbContext>
        //{
        //    public Configuration()
        //    {
        //        AutomaticMigrationsEnabled = false;
        //    }

        //    protected override void Seed(SimpleTaskSystem.EntityFramework.SimpleTaskSystemDbContext context)
        //    {
        //        context.People.AddOrUpdate(
        //            p => p.Name,
        //            new Department { Name = "Roupas" },
        //            new Department { Name = "Camas" },
        //            new Department { Name = "Perfumaria" } 
        //            );
        //    }
        //}
    }

    //------------------------------------------------------------------------
    // REPOSITÓRIOS
    //------------------------------------------------------------------------
                              
    // [Repository] Cria o repositório com todas as funções padrões, além de adicionar uma nova função "GetAllWithTransaction"
    // Não será necessário criar o DepartmentRepository visto que todos os métodos padrões são o suficiente
    // Além disso não há o método ainda pela BoilerPlate no EntityFrameWork Core
    //public interface IDeparmentRepository : IRepository<Department, long>
    //{
    //    List<Department> GetAllWithTransaction(int? UserOnIt, double? value);
    //}
                                                                                           

    ////------------------------------------------------------------------------
    //// INTERFACE
    ////------------------------------------------------------------------------

    //// [IApplicationService] Pacote de Serviços que servem de interface entre o Angular o ASP.Net
    //public interface IDepartmentAppService : IApplicationService     
    //{
    //    void CreateDepartment(DepartmentDTO input);
    //    void DeleteDepartment(DepartmentDTO input);
    //}

    //// [DTO] Estrutura padrão de comunicação entre o Angular o ASP.Net   
    //[AutoMapTo(typeof(Department))]
    //public class DepartmentDTO : EntityDto, IHasCreationTime
    //{
    //    [Required]
    //    [MaxLength(100)]
    //    public string Name { get; set; }
    //    public DateTime CreationTime { get ; set ; }
    //}

    //// [IService] Estrutura criada Manuamente, atualmente desativada, oriunda do Pacote de Serviços, que relaciona o Repositório com o Angular .
    ////public class DepartmentAppService2 : ApplicationService, IDepartmentAppService
    ////{
    ////    public DepartmentAppService2()
    ////    {
    ////        LocalizationSourceName = "DepartmentManager";
    ////    }

    ////    private readonly IRepository<Department> _DepartmentRepository;

    ////    public DepartmentAppService2(IRepository<Department> DepartmentRepository)
    ////    {
    ////        _DepartmentRepository = DepartmentRepository;
    ////    }

    ////    public void CreateDepartment(DepartmentDTO input)
    ////    {

    ////        var Department = _DepartmentRepository.FirstOrDefault(p => p.Name == input.Name);
    ////        if (Department != null)
    ////        {
    ////            throw new UserFriendlyException("There is already a Department with that name");
    ////        }

    ////        // Write some logs(Logger is defined in ApplicationService class)
    ////        Logger.Info("Creating a new department with description: " + input.Name);

    ////        Department = new Department { Name = input.Name };
    ////        _DepartmentRepository.Insert(Department);
    ////    }

    ////    public void DeleteDepartment(DepartmentDTO input)
    ////    {

    ////        // Write some logs(Logger is defined in ApplicationService class)
    ////        Logger.Info("Removing an old department with description: " + input.Name);

    ////        var Department = _DepartmentRepository.FirstOrDefault(p => p.Name == input.Name);
    ////        _DepartmentRepository.Delete(Department);

    ////        //Seria interessante remover os departamentos dos usuários existentes, mas ainda não se 
    ////        //compreendeu 100%  o funcionamento das bibliotecas padrões do ASP.NET BOILERPLATE
    ////    }
    ////}

    //// [IService] Estrutura Padrão, oriunda do Pacote de Serviços, que relaciona o Repositório com o Angular .

    //public class DepartmentAppService : AsyncCrudAppService<Department, DepartmentDTO>       
    //{        
    //    public DepartmentAppService(IRepository<Department> repository)
    //    : base(repository)
    //    {
    //        CreatePermissionName = "DeparmentManagement";
    //    }                                      
    //}

    //// [Organizer] Função para organizar
    //public class GetAllDepartmentInput : PagedAndSortedResultRequestDto
    //{
    //    public string Name { get; set; }
    //}
 

}
