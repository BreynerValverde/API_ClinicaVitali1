using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using API_ClinicaVitali.Models;
using System.Text.Json;

namespace API_ClinicaVitali.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize]
    public class HistorialClinicoController : ControllerBase
    {
        string ruta = "Data/historial.json";

        private List<HistorialClinico> Leer()
        {
            if (!System.IO.File.Exists(ruta))
                return new List<HistorialClinico>();

            string json = System.IO.File.ReadAllText(ruta);
            return JsonSerializer.Deserialize<List<HistorialClinico>>(json) ?? new List<HistorialClinico>();
        }

        private void Guardar(List<HistorialClinico> lista)
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
        public IActionResult Post(HistorialClinico nuevo)
        {
            var lista = Leer();
            lista.Add(nuevo);
            Guardar(lista);
            return Ok(nuevo);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, HistorialClinico datos)
        {
            var lista = Leer();
            var h = lista.FirstOrDefault(x => x.Id == id);
            if (h == null) return NotFound();

            if (datos.IdPaciente != 0) h.IdPaciente = datos.IdPaciente;
            if (datos.IdMedico != 0) h.IdMedico = datos.IdMedico;
            if (datos.Diagnostico != null) h.Diagnostico = datos.Diagnostico;
            if (datos.Tratamiento != null) h.Tratamiento = datos.Tratamiento;
            if (datos.FechaConsulta != null) h.FechaConsulta = datos.FechaConsulta;

            Guardar(lista);
            return Ok(h);
        }
    }
}