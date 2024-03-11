namespace Questao5.Application.Commands.Responses
{
    public class RespostaGeral
    {
        public string MensagemValidacao { get; set; }

        public string TipoValidacao { get; set; }

        public object ResultadoAcao { get; set;  }

        public void SetDadosInconsistentes(string mensagem, string tipo)
        {
            MensagemValidacao = mensagem;
            TipoValidacao = tipo;
        }
    }
}
