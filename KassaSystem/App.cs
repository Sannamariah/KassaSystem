using KassaSystemet;
using System.Security.Cryptography;

public class App
{
    public void Run()
    {

        KassaMeny();


    }

    //måste fixa kvitto spar separerar för varje månad
   








    public void KassaMeny()
    {
        bool isRunning = true;

        while (isRunning)
        {

            Console.WriteLine("================");
            Console.WriteLine("===   Kassa  ===");
            Console.WriteLine("================");
            Console.WriteLine("1. Ny kund");
            Console.WriteLine("2. Admin");
            Console.WriteLine("0. Avsluta");



            int sel = CheckNumber(0, 1);

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
                default:
                    sel = Convert.ToInt32(Console.ReadLine());
                    if (sel < 0 && sel < 2)
                    {
                        Console.WriteLine("Felaktigt val! Vänligen mata in en siffra 0-2");
                        sel = Convert.ToInt32(Console.ReadLine());
                    }
                    break;

            }


        }
    }

    public void AdminMeny()
    {
        var list = new List<Product>();

        if (File.Exists("Products.txt"))
        {
            var ladda = File.ReadAllLines("Products.txt").ToList();

            foreach (var pro in ladda)
            {
                var newArray = pro.Split(",");
                list.Add(new Product(int.Parse(newArray[0]), newArray[1], int.Parse(newArray[2])));
            }

        }

        var receiptList = new List<Sale>();

        Console.Clear();
        while (true)
        {

            Console.WriteLine("==================");
            Console.WriteLine("=== ADMIN MENY ===");
            Console.WriteLine("==================");
            Console.WriteLine("1. Lägg till produkt");
            Console.WriteLine("2. Se produkter"); 
            Console.WriteLine("0. Avsluta");
            var sel = Console.ReadLine();
            if (sel == "1")
            {

                Console.WriteLine("****LÄGG TILL PRDOUKT****");

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
            if (sel == "2")
            {
                foreach (var item in list)
                {
                    Console.WriteLine($"ProduktId: {item.ProductId} Artikel: {item.Name} Pris: {item.BasePrice} kr.");
                }

                Console.ReadLine();
            }
            if (sel == "0")
                break;
        }
    }


    public void Kassa()
    {

        Console.Clear();
        Console.WriteLine("####KASSA####");
        var list = new List<Product>();
        


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


        Sale sale = new Sale();

        while (true)
        {

            string input = Console.ReadLine().ToLower();
            string[] inputs = input.Split(' ');
            if (input == "pay")
            {
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

                Console.WriteLine("Felaktig inmatning, ange ett tal");
                continue;
            }

            return tal;
        }
    }
}

