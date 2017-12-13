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

namespace LanLinkApp.Classes
{


    //------------------------------------------------------------------------
    // INTERFACE
    //------------------------------------------------------------------------
                                                                                      

    // [DTO] Estrutura padrão de comunicação entre o Angular o ASP.Net   
    [AutoMapTo(typeof(Transaction))]
    public class TransactionDTO : EntityDto, IHasCreationTime
    {
        [Required]                           
        [StringLength(500)]
        public  string Description { get; set; }       

        [Required]
        public double Value { get; set; }

        //public virtual User UserCreator { get; set; }
        [Required]
        public int UserCreatorId { get; set; }           
        public virtual UserDto UserCreator { get; set; }

        //public virtual Department DepartmentCreator { get; set; }
        [Required]
        public int DepartmentCreatorId { get; set; }         
        public virtual DepartmentDTO DepartmentCreator { get; set; }

        public DateTime CreationTime { get; set; }
    }



    public interface ITransactiontAppService : IAsyncCrudAppService<TransactionDTO>
    {
        void CreateOne(long userid, string description, double value, int departid);

    }




    // [IService] Estrutura Padrão, oriunda do Pacote de Serviços, que relaciona o Repositório com o Angular .

    public class TransactionAppService : AsyncCrudAppService<Transaction, TransactionDTO> , ITransactiontAppService
    {

        private readonly IRepository<Transaction, int> _TransactionRepository;

        public TransactionAppService(IRepository<Transaction> repository)
        : base(repository)
        {
            //CreatePermissionName = "TransactionManagement";   
            _TransactionRepository = repository;
        }

        public void CreateOne(long userid, string description, double value, int departid)
        {
            var Transaction = new Transaction { UserCreatorID = userid, Description = description, Value = value, DepartmentCreatorID = departid, CreationTime = DateTime.Now };
            _TransactionRepository.Insert(Transaction);
        }
    }
      

   
}
