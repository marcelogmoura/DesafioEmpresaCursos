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
        public async Task<IActionResult> Create([FromBody] AlunoRequest dto)
        {
            try
            {
                var response = await _alunoService.Create(dto);
                return StatusCode(201, response);
            }
            catch (ArgumentException ex) 
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {              
                return StatusCode(500, "Erro interno do servidor: " + ex.Message);
            }
        }
    }
}
