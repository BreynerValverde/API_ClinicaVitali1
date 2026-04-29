using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using API_ClinicaVitali.Models;
using System.Text.Json;

namespace API_ClinicaVitali.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize]
    public class CitaMedicaController : ControllerBase
    {
        string ruta = "Data/citas.json";

        private List<CitaMedica> Leer()
        {
            if (!System.IO.File.Exists(ruta))
                return new List<CitaMedica>();

            string json = System.IO.File.ReadAllText(ruta);
            return JsonSerializer.Deserialize<List<CitaMedica>>(json) ?? new List<CitaMedica>();
        }

        private void Guardar(List<CitaMedica> lista)
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

        [HttpPost]
        public IActionResult Post(CitaMedica nueva)
        {
            var lista = Leer();
            lista.Add(nueva);
            Guardar(lista);
            return Ok(nueva);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, CitaMedica datos)
        {
            var lista = Leer();
            var c = lista.FirstOrDefault(x => x.Id == id);
            if (c == null) return NotFound();

            if (datos.IdPaciente != 0) c.IdPaciente = datos.IdPaciente;
            if (datos.IdMedico != 0) c.IdMedico = datos.IdMedico;
            if (datos.Fecha != null) c.Fecha = datos.Fecha;
            if (datos.Hora != null) c.Hora = datos.Hora;
            if (datos.Especialidad != null) c.Especialidad = datos.Especialidad;
            if (datos.Estado != null) c.Estado = datos.Estado;

            Guardar(lista);
            return Ok(c);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var lista = Leer();
            var c = lista.FirstOrDefault(x => x.Id == id);
            if (c == null) return NotFound();

            lista.Remove(c);
            Guardar(lista);
            return Ok();
        }
    }
}