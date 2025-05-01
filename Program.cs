using Dsw2025Ej8.Domain;
using Dsw2025Ej8.Exceptions;
using Dsw2025Ej8.Presentation;
using System.Globalization;

namespace Dsw2025Ej8
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SistemaBancario.SimularTransacciones();

            Console.WriteLine("Presiona cualquier tecla para salir...");
            Console.ReadKey();
        }
    }
}
