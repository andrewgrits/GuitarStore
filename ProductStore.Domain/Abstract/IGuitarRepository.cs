using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductStore.Domain.Entities;

namespace ProductStore.Domain.Abstract
{
    public interface IGuitarRepository
    {
        IEnumerable<Guitar> Guitars { get; }
        void SaveProduct(Guitar guitars, bool changedImage);
        Guitar DeleteProduct(int id);
    }
}
