using Dsw2025Ej8.Domain;
using Dsw2025Ej8.Domain.Exceptions;
using System.Globalization;

namespace Dsw2025Ej8
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("=== Sistema de Cuentas Bancarias ===\n");

                // Crear 4 cuentas (2 de cada tipo)
                var cajasDeAhorro = new CajaDeAhorro[]
                {
                    new CajaDeAhorro("CA-001", 1000m, new[] { "Juan Pérez" }) { TasaDeInteres = 0.05m },
                    new CajaDeAhorro("CA-002", 500m, new[] { "María García", "Pedro López" }) { TasaDeInteres = 0.04m }
                };

                var cuentasCorrientes = new CuentaCorriente[]
                {
                    new CuentaCorriente("CC-001", 2000m, new[] { "Ana Rodríguez" }) { Comision = 0.01m, LimiteDeDescubierto = 1000m },
                    new CuentaCorriente("CC-002", 1500m, new[] { "Carlos Sánchez", "Laura Martínez" }) { Comision = 0.02m, LimiteDeDescubierto = 2000m }
                };

                Console.WriteLine("Realizando operaciones...\n");

                // Probar depósitos
                cajasDeAhorro[0].Depositar(500m);
                cuentasCorrientes[0].Depositar(1000m);

                // Probar retiros
                cajasDeAhorro[1].Retirar(200m);
                cuentasCorrientes[1].Retirar(500m);

                // Aplicar interés a cajas de ahorro
                cajasDeAhorro[0].AplicarInteres();
                cajasDeAhorro[1].AplicarInteres();

                // Validaciones con excepciones controladas
                try
                {
                    Console.WriteLine("Intentando depositar monto negativo...");
                    cajasDeAhorro[0].Depositar(-100m);
                }
                catch (MontoNoValidoException ex)
                {
                    Console.WriteLine($"[Error controlado] {ex.Message}");
                }

                try
                {
                    Console.WriteLine("Intentando retirar más del saldo disponible...");
                    cajasDeAhorro[1].Retirar(10000m);
                }
                catch (SaldoInsuficienteException ex)
                {
                    Console.WriteLine($"[Error controlado] {ex.Message}");
                }

                try
                {
                    Console.WriteLine("Intentando operar con cuenta suspendida...");
                    cajasDeAhorro[1].Depositar(100m);
                }
                catch (CuentaNoActivaException ex)
                {
                    Console.WriteLine($"[Error controlado] {ex.Message}");
                }

                // Crear una lista combinada de todas las cuentas
                var todasLasCuentas = new CuentaBancaria[]
                {
                    cajasDeAhorro[0],
                    cajasDeAhorro[1],
                    cuentasCorrientes[0],
                    cuentasCorrientes[1]
                };

                // Mostrar resumen final
                Console.WriteLine("\n=== Resumen de cuentas ===");
                foreach (var cuenta in todasLasCuentas)
                {
                    var resumen = new //Clase anónima
                    {
                        Numero = cuenta.Numero,
                        Tipo = cuenta.Tipo,
                        Titulares = string.Join(", ", cuenta.Titulares),
                        Saldo = cuenta.Saldo.ToString("C", CultureInfo.CreateSpecificCulture("es-AR")),
                        Estado = cuenta.Estado
                    };

                    Console.WriteLine($"Cuenta: {resumen.Numero}");
                    Console.WriteLine($"Tipo: {resumen.Tipo}");
                    Console.WriteLine($"Titulares: {resumen.Titulares}");
                    Console.WriteLine($"Saldo: {resumen.Saldo}");
                    Console.WriteLine($"Estado: {resumen.Estado}");
                    Console.WriteLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Error inesperado] {ex.Message}");
            }

            Console.WriteLine("Presiona cualquier tecla para salir...");
            Console.ReadKey();
        }
    }
}
