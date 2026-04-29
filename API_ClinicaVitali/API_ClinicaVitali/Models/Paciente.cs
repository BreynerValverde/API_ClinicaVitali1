namespace API_ClinicaVitali.Models
{
    public class Paciente
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Cedula { get; set; }
        public string? FechaNacimiento { get; set; }
        public string? Genero { get; set; }
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }
        public string? Correo { get; set; }
        public string? EstadoClinico { get; set; }
        public string? FechaRegistro { get; set; }
    }
}