using System.Globalization;
using Dsw2025Ej8.Domain;
using Dsw2025Ej8.Exceptions;

namespace Dsw2025Ej8.Presentation
{
    public class SistemaBancario
    {
        public static void SimularTransacciones()
        {
            try
            {
                Console.WriteLine("=== Sistema de Cuentas Bancarias ===\n");

                var cajasDeAhorro = new CajaDeAhorro[]
                {
                    new CajaDeAhorro("CA-001", 1000m, new[] { "Lautaro Castillo" }) { TasaDeInteres = 0.05m },
                    new CajaDeAhorro("CA-002", 500m, new[] { "Bernabé Figueroa", "Julieta Sleiman" }) { TasaDeInteres = 0.04m }
                };

                var cuentasCorrientes = new CuentaCorriente[]
                {
                    new CuentaCorriente("CC-001", 2000m, new[] { "Luisina Svaldi" }) { Comision = 0.01m, LimiteDeDescubierto = 1000m },
                    new CuentaCorriente("CC-002", 1500m, new[] { "Carlos Palacios", "Edinson Cavani" }) { Comision = 0.02m, LimiteDeDescubierto = 2000m }
                };

                Console.WriteLine("Realizando operaciones...\n");

                // Operaciones sobre Cajas de Ahorro
                cajasDeAhorro[0].Depositar(500m);
                cajasDeAhorro[1].Retirar(200m);
                cajasDeAhorro[0].AplicarInteres();
                cajasDeAhorro[1].AplicarInteres();
                // Operaciones sobre Cuentas Corrientes
                cuentasCorrientes[0].Depositar(1000m);
                cuentasCorrientes[1].Retirar(500m);

                // Operaciones que deberían lanzar excepciones
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

     
                var todasLasCuentas = new CuentaBancaria[] //Array de tipo CuentaBancaria, que puede contener tanto Cajas de Ahorro como Cuentas Corrientes
                {
                    cajasDeAhorro[0],
                    cajasDeAhorro[1],
                    cuentasCorrientes[0],
                    cuentasCorrientes[1]
                };

    
                Console.WriteLine("\n=== Resumen de cuentas ===");
                Console.WriteLine("---------------------------------------------------------------------");

                foreach (var cuenta in todasLasCuentas)
                {
                    var resumen = new //Clase anónima
                    {
                        Numero = cuenta.Numero,
                        Tipo = cuenta.TipoDeCuenta,
                        Titulares = string.Join(", ", cuenta.Titulares),
                        Saldo = cuenta.Saldo.ToString("C", CultureInfo.CreateSpecificCulture("es-AR")),
                        Estado = cuenta.Estado
                    };

                    Console.WriteLine($"Cuenta: {resumen.Numero}");
                    Console.WriteLine($"Tipo: {resumen.Tipo}");
                    Console.WriteLine($"Titulares: {resumen.Titulares}");
                    Console.WriteLine($"Saldo: {resumen.Saldo}");
                    Console.WriteLine($"Estado: {resumen.Estado}");
                    Console.WriteLine("---------------------------------------------------------------------");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Error inesperado] {ex.Message}");
            }

        }
    }
}
