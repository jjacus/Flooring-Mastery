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
    public class TaxRepository : ITaxRepository
    {
    private string path = @"C:\Users\John\Desktop\SoftwareGuild\C#\Badge 2\Milestone 2\Flooring Mastery\taxes.txt";
    public List<Tax> LoadTaxRate()
        {

            List<Tax> Taxes = new List<Tax>();
            if (File.Exists(path))
            {
                var reader = File.ReadAllLines(path);
                for (int i = 1; i < reader.Length; i++)
                {
                    var columns = reader[i].Split(',');
                    var tax = new Tax();
                    tax.StateAbbreviation = (columns[0]);
                    tax.StateName = columns[1];
                    tax.TaxRate = decimal.Parse(columns[2]);
                    Taxes.Add(tax);
                }
            }
            else
            {
                Console.Write("Tax file not found.");
                Console.ReadKey();
            }
            return Taxes;
        }
    }
}
