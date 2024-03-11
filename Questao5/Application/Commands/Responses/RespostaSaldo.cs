namespace Questao5.Application.Commands.Responses
{
    public class RespostaSaldo
    {
        public int NumeroContaCorrente { get; set; }

        public string NomeTitular { get; set; }

        public string DataHoraConsulta { get; set; }

        public double SaldoAtual { get; set; }

        public RespostaSaldo(int numeroConta, string nomeTitular)
        {
            NumeroContaCorrente = numeroConta;
            NomeTitular = nomeTitular;
            DataHoraConsulta = DateTime.Now.ToString();
            SaldoAtual = 0.00;
        }
    }
}
