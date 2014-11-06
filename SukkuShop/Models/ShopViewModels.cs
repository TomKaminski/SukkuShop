using System;
using System.Collections.Generic;

namespace SukkuShop.Models
{
    public enum SortMethod
    {
        CenaMalejąco,
        CenaRosnąco,
        Promocje,
        Nowości,
        Popularność
    }

    public class PagingInfo
    {
        public int TotalItems { get; set; }
        public int ItemsPerPage { get; set; }
        public int CurrentPage { get; set; }

        public int TotalPages
        {
            get { return (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage); }
        }
    }

    public class ProductsListViewModel
    {
        public IEnumerable<ProductViewModel> Products { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public string CurrentCategory { get; set; }
        public SortMethod CurrentSortMethod { get; set; }
        public string CurrentSubCategory { get; set; }  
    }

    public class ProductModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Promotion { get; set; }
        public decimal Price { get; set; }
        public string ImageName { get; set; }
        public int QuantityInStock { get; set; }
        public decimal PriceAfterDiscount { get; set; }
        public bool Bestseller { get; set; }
        public bool Novelty { get; set; }
        public string Category { get; set; }
        public DateTime DateAdded { get; set; }
        public int OrdersCount { get; set; }
    }

    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Promotion { get; set; }
        public decimal Price { get; set; }
        public string ImageName { get; set; }
        public int QuantityInStock { get; set; }
        public decimal PriceAfterDiscount { get; set; }
        public bool Bestseller { get; set; }
        public bool Novelty { get; set; }
    }

}