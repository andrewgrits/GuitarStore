using ProductStore.Domain.Abstract;
using ProductStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web.Security;

namespace ProductStore.Domain.Concrete
{
    public class EFAdminAccountRepository : IAdminAccountRepository
    {
        EFDbContext context = new EFDbContext();

        public IEnumerable<AdminAccount> AdminAccounts
        {
            get { return context.AdminAccounts; }
        }

        public bool SaveAdminAccount(string login, string password, string oldLogin, string oldPassword)
        {
            var adminAccounts = context.AdminAccounts;
            bool succesfulChanging = false;

            foreach (var acc in adminAccounts)
            {
                if (acc.Login == oldLogin)
                {
                    using (var oldPassBytes = new Rfc2898DeriveBytes(oldPassword, acc.Salt))
                    {
                        byte[] key = oldPassBytes.GetBytes(20);

                        if (!key.SequenceEqual(acc.Key))
                        {
                            return succesfulChanging;
                        }
                    }
                    acc.Login = login;
                    FormsAuthentication.SetAuthCookie(acc.Login, false);
                    using (var newPassBytes = new Rfc2898DeriveBytes(password, 20))
                    {
                        acc.Key = newPassBytes.GetBytes(20);
                        acc.Salt = newPassBytes.Salt;
                        succesfulChanging = true;
                    }
                }
            }
            context.SaveChanges();
            return succesfulChanging;
        }

        public bool ValidateUser(string login, string password)
        {
            AdminAccount ValidatedAccount = null;
            var accounts = context.AdminAccounts;
            foreach (var account in accounts)
            {
                if (account.Login == login)
                {
                    using (var deriveBytes = new Rfc2898DeriveBytes(password, account.Salt))
                    {
                        byte[] key = deriveBytes.GetBytes(20);  // derive a 20-byte key

                        if (!key.SequenceEqual(account.Key))
                        {
                            return false;
                        }
                        ValidatedAccount = account;
                    }
                }
            }
            if (ValidatedAccount != null)
            {
                FormsAuthentication.SetAuthCookie(ValidatedAccount.Login, false);
                return true;
            }
            return false;
        }
    }
}
