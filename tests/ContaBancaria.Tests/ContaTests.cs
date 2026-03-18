using System;
using Xunit;
using ContaBancaria;

namespace ContaBancaria.Tests
{
    public class ContaTests
    {
        
        [Fact]
        public void CriarConta_Valida()
        {
            var conta = new Conta("Lucas", 100m);

            Assert.Equal("Lucas", conta.Titular);
            Assert.Equal(100m, conta.Saldo);
            Assert.True(conta.Ativa);
        }

        [Fact]
        public void CriarConta_TitularInvalido()
        {
            Assert.Throws<ArgumentException>(() => new Conta("", 100m));
        }

        [Fact]
        public void CriarConta_SaldoNegativo()
        {
            Assert.Throws<ArgumentException>(() => new Conta("Lucas", -1m));
        }

        
        [Fact]
        public void Depositar_ValorValido()
        {
            var conta = new Conta("Lucas", 100m);

            conta.Depositar(50m);

            Assert.Equal(150m, conta.Saldo);
        }

        [Fact]
        public void Depositar_ValorZero()
        {
            var conta = new Conta("Lucas", 100m);

            Assert.Throws<ArgumentException>(() => conta.Depositar(0));
        }

        [Fact]
        public void Depositar_ContaEncerrada()
        {
            var conta = new Conta("Lucas", 0m);
            conta.Encerrar();

            Assert.Throws<InvalidOperationException>(() => conta.Depositar(10m));
        }

        
        [Fact]
        public void Sacar_ValorValido()
        {
            var conta = new Conta("Lucas", 100m);

            conta.Sacar(50m);

            Assert.Equal(50m, conta.Saldo);
        }

        [Fact]
        public void Sacar_SaldoInsuficiente()
        {
            var conta = new Conta("Lucas", 100m);

            Assert.Throws<InvalidOperationException>(() => conta.Sacar(200m));
        }

        [Fact]
        public void Sacar_ValorInvalido()
        {
            var conta = new Conta("Lucas", 100m);

            Assert.Throws<ArgumentException>(() => conta.Sacar(0));
        }

        [Fact]
        public void Sacar_ContaEncerrada()
        {
            var conta = new Conta("Lucas", 0m);
            conta.Encerrar();

            Assert.Throws<InvalidOperationException>(() => conta.Sacar(10m));
        }

        
        [Fact]
        public void Transferir_Valido()
        {
            var origem = new Conta("Lucas", 200m);
            var destino = new Conta("Maria", 100m);

            origem.Transferir(destino, 50m);

            Assert.Equal(150m, origem.Saldo);
            Assert.Equal(150m, destino.Saldo);
        }

        [Fact]
        public void Transferir_SaldoInsuficiente()
        {
            var origem = new Conta("Lucas", 50m);
            var destino = new Conta("Maria", 100m);

            Assert.Throws<InvalidOperationException>(() => origem.Transferir(destino, 100m));
        }

        [Fact]
        public void Transferir_DestinoNulo()
        {
            var conta = new Conta("Lucas", 100m);

            Assert.Throws<ArgumentNullException>(() => conta.Transferir(null, 10m));
        }

        [Fact]
        public void Transferir_ContaEncerrada()
        {
            var origem = new Conta("Lucas", 100m);
            var destino = new Conta("Maria", 100m);

            origem.Encerrar();

            Assert.Throws<InvalidOperationException>(() => origem.Transferir(destino, 10m));
        }

        
        [Fact]
        public void Encerrar_ComSaldo()
        {
            var conta = new Conta("Lucas", 100m);

            Assert.Throws<InvalidOperationException>(() => conta.Encerrar());
        }

        [Fact]
        public void Encerrar_SemSaldo()
        {
            var conta = new Conta("Lucas", 0m);

            conta.Encerrar();

            Assert.False(conta.Ativa);
        }

        [Fact]
        public void Encerrar_JaEncerrada()
        {
            var conta = new Conta("Lucas", 0m);
            conta.Encerrar();

            Assert.Throws<InvalidOperationException>(() => conta.Encerrar());
        }
    }
}