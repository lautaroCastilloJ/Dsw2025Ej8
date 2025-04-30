namespace Dsw2025Ej8.Domain;

public class CajaDeAhorro : CuentaBancaria
{
    public decimal TasaDeInteres { get; set; }

    public CajaDeAhorro(string numero, decimal saldo, string[] titulares) // No tiene lógica adicional porque no necesita inicializar nada más en ese momento (por ejemplo, TasaDeInteres se asigna después, como se pide en el enunciado).
        : base(numero, saldo, titulares) { } //“Antes de que empiece el constructor de la subclase, ejecutá el constructor de la superclase con estos valores y luego el de la subclase”

    public override void Depositar(decimal monto)
    {
        ValidarMonto(monto);
        ValidarEstado();
        Saldo += monto;
    }

    public override void Retirar(decimal monto)
    {
        ValidarMonto(monto);
        ValidarEstado();

        if (Saldo < monto)
        {
            Estado = Estado.Suspendida;
            throw new Exceptions.SaldoInsuficienteException();
        }

        Saldo -= monto;
    }

    public override void AplicarInteres()
    {
        ValidarEstado(); // Verifica que la cuenta esté activa antes de aplicar el interés
        Saldo += Saldo * TasaDeInteres;
    }

    public override string Tipo => "Caja de Ahorro";
}

/*
  if (Estado == Estado.Activa)
            Saldo += Saldo * TasaDeInteres;
 
 */