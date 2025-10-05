using DesafioEmpresaCursos.Domain.Dtos.Request;
using DesafioEmpresaCursos.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace DesafioEmpresaCursos.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunosController : ControllerBase
    {
        private readonly IAlunoService _alunoService;

        public AlunosController(IAlunoService alunoService)
        {
            _alunoService=alunoService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] AlunoRequest dto)
        {
            try
            {
                var response = _alunoService.Create(dto);
                return StatusCode(201, dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }
    }
}
