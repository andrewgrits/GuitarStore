using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ProductStore.Domain.Entities
{
    public class AdminAccount
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Display(Name = "Логин")]
        [Required(ErrorMessage = "Введите пароль")]
        public string Login { get; set; }

        public byte[] Salt { get; set; }
        public byte[] Key { get; set; }
    }
}
