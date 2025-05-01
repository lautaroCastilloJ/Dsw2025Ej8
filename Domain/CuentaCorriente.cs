namespace Dsw2025Ej8.Domain;

public class CuentaCorriente : CuentaBancaria
{
    public decimal LimiteDeDescubierto { get; set; } 
    public decimal Comision { get; set; }
    public override string TipoDeCuenta => "Cuenta Corriente";

    public CuentaCorriente(string numero, decimal saldo, string[] titulares)
        : base(numero, saldo, titulares) { } 

    public override void Depositar(decimal monto)
    {
        ValidarMonto(monto);
        ValidarEstado();
        Saldo += monto - (monto * Comision);
    }

    public override void Retirar(decimal monto)
    {
        ValidarMonto(monto);
        ValidarEstado();

        
        decimal saldoDespuesDelRetiro = Saldo - monto; 

        if (saldoDespuesDelRetiro < -(LimiteDeDescubierto)) // -(LimiteDeDescubierto) significa que el saldo despues del retiro puede ser negativo hasta el límite de descubierto.
        {
            Estado = Estado.Suspendida;
            throw new Exceptions.SaldoInsuficienteException();
        }
        
        Saldo = saldoDespuesDelRetiro;

        if (Saldo < 0)
            Estado = Estado.Suspendida; // "...la cuenta debe quedar suspendida si queda en descubierto(saldo negativo)..."

    }

}