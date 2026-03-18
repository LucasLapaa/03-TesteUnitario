using System;

namespace ContaBancaria
{
    public class Conta
    {
        public string Titular { get; private set; }
        public decimal Saldo { get; private set; }
        public bool Ativa { get; private set; }

        public Conta(string titular, decimal saldoInicial)
        {
            if (string.IsNullOrWhiteSpace(titular))
                throw new ArgumentException("Titular inválido");

            if (saldoInicial < 0)
                throw new ArgumentException("Saldo inicial não pode ser negativo");

            Titular = titular;
            Saldo = saldoInicial;
            Ativa = true;
        }

        public void Depositar(decimal valor)
        {
            if (!Ativa)
                throw new InvalidOperationException("Conta encerrada");

            if (valor <= 0)
                throw new ArgumentException("Valor deve ser maior que zero");

            Saldo += valor;
        }

        public void Sacar(decimal valor)
        {
            if (!Ativa)
                throw new InvalidOperationException("Conta encerrada");

            if (valor <= 0)
                throw new ArgumentException("Valor deve ser maior que zero");

            if (Saldo < valor)
                throw new InvalidOperationException("Saldo insuficiente");

            Saldo -= valor;
        }

        public void Transferir(Conta destino, decimal valor)
        {
            if (destino == null)
                throw new ArgumentNullException(nameof(destino));

            if (!Ativa || !destino.Ativa)
                throw new InvalidOperationException("Conta inválida");

            if (valor <= 0)
                throw new ArgumentException("Valor deve ser maior que zero");

            if (Saldo < valor)
                throw new InvalidOperationException("Saldo insuficiente");

            Sacar(valor);
            destino.Depositar(valor);
        }

        public void Encerrar()
        {
            if (!Ativa)
                throw new InvalidOperationException("Conta já encerrada");

            if (Saldo != 0)
                throw new InvalidOperationException("Saldo deve ser zero");

            Ativa = false;
        }
    }
}