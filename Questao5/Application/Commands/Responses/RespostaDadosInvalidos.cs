namespace Questao5.Application.Commands.Responses
{
    public class RespostaDadosInvalidos
    {
        public string Mensagem { get; set; }

        public string Tipo { get; set; }

        public RespostaDadosInvalidos(string mensagem, string tipo)
        {
            Mensagem = mensagem;
            Tipo = tipo;
        }
    }
}
