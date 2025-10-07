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

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] AlunoUpdateRequest dto)
        {
            try
            {
                var alunoAtualizado = await _alunoService.Update(id, dto);
                return Ok(alunoAtualizado);

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


        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _alunoService.Delete(id);
                return NoContent();
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

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var alunos = await _alunoService.GetAll();
                return Ok(alunos);
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
                var aluno = await _alunoService.GetById(id);
                return Ok(aluno);
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
    }
}