using KassaSystemet;
using System.Security.Cryptography;

public class App
{
    public void Run()
    {

        KassaMeny();
    }

 
   




    public void KassaMeny()
    {
        
        bool isRunning = true;
        Console.Clear();

        while (isRunning)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("================");
            Console.WriteLine("===   KASSA  ===");
            Console.WriteLine("================");
            Console.ResetColor();
            Console.WriteLine("");
            Console.WriteLine("1. Ny kund");
            Console.WriteLine("2. Admin");  
            Console.WriteLine("0. Avsluta");



            int sel = CheckNumber(0, 2);

            switch (sel)
            {

                case 1:
                    Kassa();
                    break;

                case 2:
                    AdminMeny();
                    break;

                case 0:
                    isRunning = false;
                    break;

            }


        }
    }

    public void AdminMeny()
    {

        var list = new List<Product>();

        
        var receiptList = new List<Sale>();


        Console.Clear();
        while (true)
        {
            
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("==================");
            Console.WriteLine("=== ADMIN MENY ===");
            Console.WriteLine("==================");
            Console.ResetColor();
            Console.WriteLine("");
            Console.WriteLine("1. Lägg till produkt");
            Console.WriteLine("2. Se produkter"); 
            Console.WriteLine("3. Kassa meny");
            var sel = CheckNumber(0,3);
           
            if (sel == 1)
            {
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine("****LÄGG TILL PRDOUKT****");
                Console.ResetColor();
                Console.WriteLine("");

                Console.Write("Ange produkt Id:");
                int Id = Convert.ToInt32(Console.ReadLine());
                Console.Write("Ange produkt namn:");
                string Name = Console.ReadLine();
                Console.Write("Ange pris:");
                decimal Price = Convert.ToDecimal(Console.ReadLine());

                var product2 = new Product(Id, Name, Price);
                list.Add(product2);
                SaveToFile(list);


            }
            if (sel == 2)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine("-----Produkter----");
                Console.ResetColor();
                Console.WriteLine("");
                var ladda = File.ReadAllLines("Products.txt").ToList();

                foreach (var pro in ladda)
                {
                    var newArray = pro.Split(",");
                    if (newArray.Length == 3)
                    {
                        list.Add(new Product(int.Parse(newArray[0]), newArray[1], int.Parse(newArray[2])));
                    }
                }
                foreach (var p in list)
                {
                    Console.WriteLine(p.ToString());
                }
                Console.WriteLine("");

            }
            if (sel == 3)
                KassaMeny();
          
                
        }
    }


    public void Kassa()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine("####  KASSA  ####");
        Console.WriteLine("");
        Console.WriteLine("-----Produkter----");
        Console.ResetColor();
        var list = new List<Product>();
        var receiptList = new List<Sale>();

        var ladda = File.ReadAllLines("Products.txt").ToList();

        foreach (var pro in ladda)
        {
            var newArray = pro.Split(",");
            if (newArray.Length == 3)
            {
                list.Add(new Product(int.Parse(newArray[0]), newArray[1], int.Parse(newArray[2])));
            }
        }
        foreach (var p in list)
        {
            Console.WriteLine(p.ToString());

        }
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine("---------------------");
        Console.WriteLine("");
        Console.ResetColor();



        Sale sale = new Sale();

        while (true)
        {
            string input = Console.ReadLine().ToLower();
            string[] inputs = input.Split(' ');

            
            if (input == "pay")
            {
                Console.Clear();
                Console.WriteLine("");
                sale.PrintReceipt();
                File.AppendAllText("receipt_" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt", sale.ToString());


                break;
            }
             var pId = Convert.ToInt32(inputs[0]);
             var antal = Convert.ToInt32(inputs[1]);
             var prod = list.FirstOrDefault(p => p.ProductId == pId);

            if (prod != null)
            {

                sale.AddItem(prod, antal);
            }



            sale.PrintReceipt();




        }


    }


    public void SaveToFile(List<Product> list)
    {

        var strings = new List<string>();
   
        foreach (var product in list)
        {
            string productString = product.ProductId + "," + product.Name + "," + product.BasePrice;
            strings.Add(productString);
        }

        File.AppendAllLines("Products.txt", strings);
    }






    public int CheckNumber(int min, int max)
    {
        int tal = 0;

        while (true)
        {

            if (int.TryParse(Console.ReadLine(), out tal) == false)
            {

                Console.WriteLine("Felaktig inmatning, försök igen");
                continue;
            }
            if (tal < min || tal > max)
            {
                Console.WriteLine("Felaktig inmatning, ange ett tal som finns på menyn");
                continue;

            }

            return tal;
        }
    }
}

