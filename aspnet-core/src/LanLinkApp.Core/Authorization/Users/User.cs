using System;
using System.Collections.Generic;      
using Abp.Authorization.Users;
using Abp.Extensions;
using LanLinkApp.Classes;

namespace LanLinkApp.Authorization.Users
{
    public class User : AbpUser<User>
    {
        public const string DefaultPassword = "lanlink";

        public ICollection<Transaction> Transactions { get; set; }   

        public ICollection<UserDepartment> UserDepartments { get; set; }

        //public string[] DepartmentNames()
        //{                                              
        //    var myList = new string[NumberOfDepartments()];
        //    var count = 0;
        //    foreach (UserDepartment d in UserDepartments)
        //    {
        //        myList[count] = d.RefDepartment.Name;
        //        count++;
        //    }
        //    return myList;
        //}

        //public int NumberOfDepartments()
        //{                                                 
        //    var count = 0;
        //    foreach (UserDepartment d in UserDepartments)
        //    {
        //        count++;
        //    }
        //    return count;
        //}


        public static string CreateRandomPassword()
        {
            return Guid.NewGuid().ToString("N").Truncate(16);
        }

        public static User CreateTenantAdminUser(int tenantId, string emailAddress)
        {
            var user = new User
            {
                TenantId = tenantId,
                UserName = AdminUserName,
                Name = AdminUserName,
                Surname = AdminUserName,
                EmailAddress = emailAddress
            };

            user.SetNormalizedNames();

            return user;
        }
    }
}
