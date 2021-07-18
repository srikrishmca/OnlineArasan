using System;
using System.Collections.Generic;

namespace OnlineArasan.Models
{
    public class Categories
    {
        public int SelectedCategoryId { get; set; }
        public List<Category> CategoriesList { get; set; }
    }
}