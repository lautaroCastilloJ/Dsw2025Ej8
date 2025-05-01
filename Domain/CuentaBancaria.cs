using System;

namespace Dsw2025Ej8.Domain;

public abstract class CuentaBancaria
{
    public string Numero { get; }
    public decimal Saldo { get; protected set; }
    public Estado Estado { get; protected set; }
    public string[] Titulares { get; }
    public abstract string TipoDeCuenta { get; }



    protected CuentaBancaria(string numero, decimal saldo, string[] titulares) 
    {
        Numero = numero;
        Saldo = saldo;
        Estado = Estado.Activa;
        Titulares = titulares;
    }

    protected void ValidarMonto(decimal monto)
    {
        if (monto <= 0)
            throw new Exceptions.MontoNoValidoException();
    }

    protected void ValidarEstado()
    {
        if (Estado != Estado.Activa)
            throw new Exceptions.CuentaNoActivaException(Estado);
    }

    public abstract void Depositar(decimal monto);
    public abstract void Retirar(decimal monto); 
    public virtual void AplicarInteres() { } 
    


}



/*
 NOTAS:

// Como CuentaBancaria es una clase abstracta no puede ser instanciada directamente 
por eso el constructor de objetos o instancias de la clase es protected, así las clases derivadas 
pueden usar su constructor con base(...). protected asegura que solo las clases hijas puedan usar ese constructor.
 
// el método Retirar no es virtual porque todas las cuentas deben implementar su propia lógica de retiro 
“Esto DEBE ser implementado en una clase derivada”.

// es virtual porque no todas las cuentas aplican interés y tampoco lo hacen de la misma manera, por lo tanto es opcional 
“Esto puede ser sobrescrito en una clase derivada”.
 
 */