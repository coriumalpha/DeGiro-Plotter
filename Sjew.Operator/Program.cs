using SJew.Business;
using SJew.Entities.Models;
using System;
using System.Collections.Generic;

namespace Sjew.Operator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Degiro Plotter");
            Console.WriteLine();

            Controller controller = new Controller();
            
            controller.ShowTotalRevenue();
            controller.ShowTotalRevenue(false);

            Console.WriteLine();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
