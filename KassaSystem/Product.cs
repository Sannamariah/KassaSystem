﻿using KassaSystemet;
using Microsoft.VisualBasic;
using System.Collections.Generic;


namespace KassaSystemet
{ 
    public class Product
    {
        
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal BasePrice { get; set; }

        private string priceType;

     

        public void SetPriceType(string value)
        {
            priceType = value;
        }

        public Product(int id, string name, decimal basePrice)
        {
            ProductId = id;
            Name = name;
            BasePrice = basePrice;
        }
        public override string ToString()
        {
            return $"{ProductId} {Name} {BasePrice} {priceType}";
        }

    }
    public class SaleItem
    {
        public Product Product;
        public decimal Ammount;

        public SaleItem(Product product, decimal ammount)
        {
            Product = product;
            Ammount = ammount;
        }

        public decimal GetTotalPrice()
        {
               return Ammount * Product.BasePrice;    
        }

        public override string ToString()
        {
            return $"{Product.Name} {Ammount} * {Product.BasePrice} = {GetTotalPrice()}";
        }
    }

    public class Sale
    {
        public List<String> AddedProducts = new();
        public int ReceiptNumber { get; set; }

        public DateTime date;
        public List<SaleItem> items;

        public Sale()
        {
            date = DateTime.Now;
            items = new List<SaleItem>();
            NextReceiptNumber();
        }

        public void NextReceiptNumber()
        {
            ReceiptNumber = 1;
            if (File.Exists("receiptNumber.txt")){
                ReceiptNumber = Convert.ToInt32(File.ReadAllText("receiptNumber.txt"));
            }
            File.WriteAllText("receiptNumer.txt", (ReceiptNumber + 1).ToString());
        
        }

        public decimal GetTotalPrice()
        {
            decimal total = 0;
            foreach (SaleItem item in items)
            {
                total += item.GetTotalPrice();
            }
            return total;
        }

        public void AddItem(Product product, decimal Ammount)
        {
            SaleItem item = new SaleItem(product, Ammount);
            items.Add(item);
        }

        public void PrintReceipt()
        {
            List<Product> list = new List<Product>().ToList();

            var strings = new List<string>();
            Console.WriteLine($"KVITTO {date}");
            Console.WriteLine($"{ReceiptNumber}"); 


            foreach (SaleItem item in items)
            {
                Console.WriteLine(item);
                strings.Add(item.ToString());
            }
            Console.WriteLine($"Total: {GetTotalPrice()}");
            Console.WriteLine("**************************************");
 
 
        }
        public override string ToString()
        {
            string s = "";
            s = $"KVITTO {date}" + Environment.NewLine;
            s += $"RECNR {ReceiptNumber}" + Environment.NewLine;
            foreach (SaleItem item in items)
            {
                s += item.ToString() + Environment.NewLine;
            }
            s += $"Total: {GetTotalPrice()}" + Environment.NewLine;
            s += "**************************************" + Environment.NewLine;
            return s;
        }



    }
}








