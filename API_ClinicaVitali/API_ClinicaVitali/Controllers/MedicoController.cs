using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using API_ClinicaVitali.Models;
using System.Text.Json;

namespace API_ClinicaVitali.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize]
    public class MedicoController : ControllerBase
    {
        string ruta = "Data/medicos.json";

        private List<Medico> Leer()
        {
            if (!System.IO.File.Exists(ruta))
                return new List<Medico>();

            string json = System.IO.File.ReadAllText(ruta);
            return JsonSerializer.Deserialize<List<Medico>>(json) ?? new List<Medico>();
        }

        private void Guardar(List<Medico> lista)
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
        public IActionResult Post(Medico nuevo)
        {
            var lista = Leer();
            lista.Add(nuevo);
            Guardar(lista);
            return Ok(nuevo);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Medico datos)
        {
            var lista = Leer();
            var m = lista.FirstOrDefault(x => x.Id == id);
            if (m == null) return NotFound();

            if (datos.Nombre != null) m.Nombre = datos.Nombre;
            if (datos.CedulaProfesional != null) m.CedulaProfesional = datos.CedulaProfesional;
            if (datos.Especialidad != null) m.Especialidad = datos.Especialidad;
            if (datos.Telefono != null) m.Telefono = datos.Telefono;
            if (datos.Correo != null) m.Correo = datos.Correo;
            if (datos.HorarioConsulta != null) m.HorarioConsulta = datos.HorarioConsulta;
            if (datos.Estado != null) m.Estado = datos.Estado;

            Guardar(lista);
            return Ok(m);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var lista = Leer();
            var m = lista.FirstOrDefault(x => x.Id == id);
            if (m == null) return NotFound();

            lista.Remove(m);
            Guardar(lista);
            return Ok();
        }
    }
}