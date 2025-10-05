using DesafioEmpresaCursos.Domain.Dtos.Request;
using DesafioEmpresaCursos.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace DesafioEmpresaCursos.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TurmasController : ControllerBase
    {
        private readonly ITurmaService _turmaService;

        public TurmasController(ITurmaService turmaService)
        {
            _turmaService = turmaService;
        }
                
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TurmaRequest dto)
        {
            try
            {
                var response = await _turmaService.Create(dto);
                return StatusCode(201, response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
                
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var response = await _turmaService.GetAll();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro interno do servidor: " + ex.Message);
            }
        }
                
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var response = await _turmaService.GetById(id);
                return Ok(response);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro interno do servidor: " + ex.Message);
            }
        }
                
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] TurmaRequest dto)
        {
            try
            {
                var response = await _turmaService.Update(id, dto);
                return Ok(response);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
                
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var numeroTurmaRemovida = await _turmaService.Delete(id);

                return Ok(new
                {
                    message = $"Turma '{numeroTurmaRemovida}' excluída com sucesso.",
                    numeroTurma = numeroTurmaRemovida
                });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
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