namespace Helicoil_Smart.Domain.Entities
{
    public class Registro
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Cedula { get; set; }
        public DateTime FechaHora { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }
    }
}
