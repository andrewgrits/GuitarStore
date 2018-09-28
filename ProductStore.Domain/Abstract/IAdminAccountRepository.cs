using ProductStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductStore.Domain.Abstract
{
    public interface IAdminAccountRepository
    {
        IEnumerable<AdminAccount> AdminAccounts { get; }
        bool ValidateUser(string login, string password);
        bool SaveAdminAccount(string login, string password,
                              string oldLogin, string oldPassword);
    }
}
