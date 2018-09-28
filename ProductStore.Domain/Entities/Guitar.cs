using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ProductStore.Domain.Entities
{
    public class Guitar
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Display(Name = "Название")]
        [Required(ErrorMessage = "Введите название товара")]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Описание")]
        [Required(ErrorMessage = "Введите описание товара")]
        public string Description { get; set; }

        [Display(Name = "Цена")]
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Введите положительное значение для цены")]
        public decimal Price { get; set; }

        [Display(Name = "Категория")]
        [Required(ErrorMessage = "Введите категорию товара"), StringLength(3000)]
        public string Category { get; set; }

        public byte[] ImageData { get; set; }
        public string ImageMimeType { get; set; }
    }
}
