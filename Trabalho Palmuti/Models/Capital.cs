namespace Trabalho_Palmuti.Models
{
    public class Capital
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string EstadoSigla { get; set; }
        public int EstadoId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}