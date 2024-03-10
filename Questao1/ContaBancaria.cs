using System.Globalization;

namespace Questao1
{
    class ContaBancaria {

        public long NumeroConta { get; }

        public string NomeTitular { get; set; }

        public double Saldo { get; set; }

        public ContaBancaria(long numeroConta, string nomeTitular)
        {
            this.NumeroConta = numeroConta;
            this.NomeTitular = nomeTitular;
        }

        public ContaBancaria(long numeroConta, string nomeTitular, double saldo)
        {
            this.NumeroConta = numeroConta;
            this.NomeTitular = nomeTitular;
            this.Saldo = saldo;
        }

        public void Deposito(double valor)
        {
            this.Saldo += valor;
        }

        public void Saque(double valor)
        {
            this.Saldo -= valor + 3.50;
        }

        public override string ToString()
        {
            return "Conta " + this.NumeroConta + ", Titular: " + this.NomeTitular + ", Saldo: $ " + this.Saldo.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture);
        }
    }
}
