namespace Dsw2025Ej8.Domain;

public class CuentaCorriente : CuentaBancaria
{
    public decimal LimiteDeDescubierto { get; set; } // Cuando el saldo de tu cuenta llega a cero, pero seguís gastando se activa el descubierto, el cual tiene un límite máximo permitido por el Banco. Tu cuenta tiene un límite de descubierto de $1000. Por ejemplo: Si tu saldo es $0, puedes seguir utilizando hasta $1000, pero quedando en negativo.
    public decimal Comision { get; set; }

    public CuentaCorriente(string numero, decimal saldo, string[] titulares)
        : base(numero, saldo, titulares) { } // permite reutilizar lógica de inicialización sin duplicarla en cada clase derivada.

    public override void Depositar(decimal monto)
    {
        ValidarMonto(monto);
        ValidarEstado();
        Saldo += monto - (monto * Comision);
    }

    public override void Retirar(decimal monto)
    {
        ValidarMonto(monto); // Verifica que el monto sea mayor a 0
        ValidarEstado(); // Verifica que la cuenta esté activa

        
        decimal saldoDespuesDelRetiro = Saldo - monto; // Verificamos si la operación excedería el límite de descubierto

        if (saldoDespuesDelRetiro < -LimiteDeDescubierto) // -LimiteDeDescubierto significa que el saldo puede ser negativo hasta el límite de descubierto.
        {
            Estado = Estado.Suspendida; // La cuenta se suspende por exceder el descubierto permitido
            throw new Exceptions.SaldoInsuficienteException();
        }
        
        Saldo = saldoDespuesDelRetiro; // Actualizamos el saldo

        if (Saldo < 0)
            Estado = Estado.Suspendida; // "...la cuenta debe quedar suspendida si queda en descubierto(saldo negativo)..."

    }

    public override string Tipo => "Cuenta Corriente";
}


/*
 
      
 
 */