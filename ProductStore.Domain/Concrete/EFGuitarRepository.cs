using ProductStore.Domain.Abstract;
using ProductStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductStore.Domain.Concrete
{
    public class EFGuitarRepository : IGuitarRepository
    {
        EFDbContext context = new EFDbContext();

        public IEnumerable<Guitar> Guitars
        {
            get { return context.Guitars; }
        }

        public void SaveProduct(Guitar guitar, bool changedImage)
        {
            if (guitar.Id == 0)
            {
                context.Guitars.Add(guitar);
            }
            else
            {
                Guitar dbEntry = context.Guitars.Find(guitar.Id);
                if(dbEntry!=null)
                {
                    dbEntry.Name = guitar.Name;
                    dbEntry.Description = guitar.Description;
                    dbEntry.Price = guitar.Price;
                    dbEntry.Category = guitar.Category;
                    if(changedImage)
                    {
                        dbEntry.ImageData = guitar.ImageData;
                        dbEntry.ImageMimeType = guitar.ImageMimeType;
                    }
                }
            }

            context.SaveChanges();
        }

        public Guitar DeleteProduct(int id)
        {
            Guitar dbEntry = context.Guitars.Find(id);
            if (dbEntry != null)
            {
                context.Guitars.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
