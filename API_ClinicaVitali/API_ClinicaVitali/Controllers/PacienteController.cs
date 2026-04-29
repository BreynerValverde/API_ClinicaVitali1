using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using API_ClinicaVitali.Models;
using System.Text.Json;

namespace API_ClinicaVitali.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize]
    public class PacienteController : ControllerBase
    {
        string ruta = "Data/pacientes.json";

        private List<Paciente> Leer()
        {
            if (!System.IO.File.Exists(ruta))
                return new List<Paciente>();

            string json = System.IO.File.ReadAllText(ruta);
            return JsonSerializer.Deserialize<List<Paciente>>(json) ?? new List<Paciente>();
        }

        private void Guardar(List<Paciente> lista)
        {
            string json = JsonSerializer.Serialize(lista, new JsonSerializerOptions { WriteIndented = true });
            System.IO.File.WriteAllText(ruta, json);
        }

        [HttpGet]
        public IActionResult Get() => Ok(Leer());

        [HttpGet("{id}")]
        public IActionResult GetId(int id)
        {
            var dato = Leer().FirstOrDefault(x => x.Id == id);
            return dato == null ? NotFound() : Ok(dato);
        }

        [HttpGet("cedula/{cedula}")]
        public IActionResult Cedula(string cedula)
        {
            var dato = Leer().FirstOrDefault(x => x.Cedula == cedula);
            return dato == null ? NotFound() : Ok(dato);
        }

        [HttpPost]
        public IActionResult Post(Paciente nuevo)
        {
            var lista = Leer();
            lista.Add(nuevo);
            Guardar(lista);
            return Ok(nuevo);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Paciente datos)
        {
            var lista = Leer();
            var p = lista.FirstOrDefault(x => x.Id == id);
            if (p == null) return NotFound();

            if (datos.Nombre != null) p.Nombre = datos.Nombre;
            if (datos.Cedula != null) p.Cedula = datos.Cedula;
            if (datos.FechaNacimiento != null) p.FechaNacimiento = datos.FechaNacimiento;
            if (datos.Genero != null) p.Genero = datos.Genero;
            if (datos.Direccion != null) p.Direccion = datos.Direccion;
            if (datos.Telefono != null) p.Telefono = datos.Telefono;
            if (datos.Correo != null) p.Correo = datos.Correo;
            if (datos.EstadoClinico != null) p.EstadoClinico = datos.EstadoClinico;
            if (datos.FechaRegistro != null) p.FechaRegistro = datos.FechaRegistro;

            Guardar(lista);
            return Ok(p);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var lista = Leer();
            var p = lista.FirstOrDefault(x => x.Id == id);
            if (p == null) return NotFound();

            lista.Remove(p);
            Guardar(lista);
            return Ok();
        }
    }
}