namespace DesafioProjetoHospedagem.Models
{
    public class Suite
    {
        public Suite() { }

        public Suite(string tipoSuite, int capacidade, decimal valorDiaria)
        {
            TipoSuite = tipoSuite;
            Capacidade = capacidade;
            ValorDiaria = valorDiaria;
        }

        public string TipoSuite { get; set; }
        public int Capacidade { get; set; }
        public decimal ValorDiaria { get; set; }

        public static Suite CriarSuitePorTipo(string tipoSuite)
        {
            switch (tipoSuite.ToLower())
            {
                case "comum":
                    return new Suite("Comum", 2, 100);
                case "premium":
                    return new Suite("Premium", 4, 200);
                case "presidencial":
                    return new Suite("Presidencial", 6, 300);
                default:
                    throw new Exception("Tipo de suíte inválido.");
            }
        }
    }
}