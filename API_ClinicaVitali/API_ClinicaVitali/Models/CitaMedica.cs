namespace API_ClinicaVitali.Models
{
    public class CitaMedica
    {
        public int Id { get; set; }
        public int IdPaciente { get; set; }
        public int IdMedico { get; set; }
        public string? Fecha { get; set; }
        public string? Hora { get; set; }
        public string? Especialidad { get; set; }
        public string? Estado { get; set; }
    }
}