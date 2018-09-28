using System.Collections.Generic;
using ProductStore.Domain.Entities;

namespace ProductStore.WebUI.Models
{
    public class ProductListViewModel
    {
        public IEnumerable<Guitar> Guitars { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public string CurrentCategory { get; set; }
    }
}