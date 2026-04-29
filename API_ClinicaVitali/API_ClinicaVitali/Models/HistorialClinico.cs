namespace API_ClinicaVitali.Models
{
    public class HistorialClinico
    {
        public int Id { get; set; }
        public int IdPaciente { get; set; }
        public int IdMedico { get; set; }
        public string? Diagnostico { get; set; }
        public string? Tratamiento { get; set; }
        public string? FechaConsulta { get; set; }
    }
}