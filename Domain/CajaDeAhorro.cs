namespace Dsw2025Ej8.Domain;

public class CajaDeAhorro : CuentaBancaria
{
    public decimal TasaDeInteres { get; set; }
    public override string TipoDeCuenta => "Caja de Ahorro";

    public CajaDeAhorro(string numero, decimal saldo, string[] titulares) // No tiene lógica adicional porque no necesita inicializar nada más al momento de instanciar, por ejemplo, TasaDeInteres se asigna después, como se pide en el enunciado.
        : base(numero, saldo, titulares) { } 

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
        ValidarEstado(); //La cuenta debe estar Activa.
        Saldo += (Saldo * TasaDeInteres);
    }

}