using static System.Runtime.InteropServices.JavaScript.JSType;

internal class App : AppBase
{
    public void Run()
    {

        KassaMeny();

    }

    //måste fixa kvitto spar separerar för varje månad
    // ska finnas artiklar datum pris och total pris
    //PAY = klar med köpet

   



   


    public void KassaMeny()
    {
        bool isRunning = true;

        while (isRunning)
        {

            Console.WriteLine("================");
            Console.WriteLine("===   Kassa  ===");
            Console.WriteLine("================");
            Console.WriteLine("1. Ny kund");
            Console.WriteLine("0. Avsluta");
            Console.WriteLine("2. Admin");


            int sel = CheckNumber(0, 1);

            switch (sel)
            {

                case 1:
                    Kassa();
                    break;

                    case 2:
                 //   Admin(); skapa en admin vy.
                    break;

                case 0:
                    isRunning = false;
                    break;
                default:
                    sel = Convert.ToInt32(Console.ReadLine());
                    if (sel < 0 && sel < 2)
                    {
                        Console.WriteLine("Felaktigt val! Mata in igen");
                        sel = Convert.ToInt32(Console.ReadLine());
                    }
                    break;

            }


        }
    }

    public void Kassa()
    {


        Console.Clear();
        Console.WriteLine("KASSA");
        string input = Console.ReadLine();
        string[] inputs = input.Split(' ');

        foreach (string inp in inputs)
        {
            Kvitto();
        }

        //meningen är att den ska skriva ut det man lagt in och räkna ut hur mycket det blir


        Console.Read();
    }



    private void OrderTotal()
    {
        //här inne ska själva funktionen för att räkna ihop alla värderna finnas

        // produkt * antal = 
    }



    private void Kvitto()
    {
        var date = DateTime.Now;

        Console.WriteLine("Kvitto");
        Console.WriteLine($"Datum: {date.ToLocalTime()}");
        Console.WriteLine("Ordern"); // här skrivs alla produkter ut !
        Console.WriteLine($"Total: {OrderTotal} kr");
        // Här ska produkterna valda skrivas in  och antal med pris
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

    public bool CheckString()
    {
        while (true)
        {
            string pay = Console.ReadLine().ToLower();

            if (pay == "pay")
            {
                Console.Clear();
                Kvitto();
                return true;

            }
              else
                return false;
            
        }
    }


}