using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.Models;
using FlooringMastery.Models.Interfaces;
using System.IO;

namespace FlooringMastery.Data
{
    public class ProductRepository : IProductRepository
    {
        private string path = @"C:\Users\John\Desktop\SoftwareGuild\C#\Badge 2\Milestone 2\Flooring Mastery\products.txt";

        public List<Product> LoadProductType()
        {
            List<Product> Products = new List<Product>();

            if (File.Exists(path))
            {
                var reader = File.ReadAllLines(path);
                for (int i = 1; i < reader.Length; i++)
                {
                    var columns = reader[i].Split(',');
                    var product = new Product();
                    product.ProductType = (columns[0]);
                    product.CostPerSquareFoot = decimal.Parse(columns[1]);
                    product.LaborCostPerSquareFoot = decimal.Parse(columns[2]);
                    Products.Add(product);
                }
            }
            else
            {
                Console.Write("Product file not found.");
                Console.ReadKey();
            }
            return Products;
        }
    }
}
