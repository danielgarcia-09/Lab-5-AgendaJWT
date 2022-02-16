using AgendaManager.Bl.Dto;
using AgendaManager.Model.Models;
using AgendaManager.Services.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AgendaManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Authorize]
    public class BaseController<T, TDto, TContext> : ControllerBase
       where T : BaseEntity
       where TDto : BaseDto
       where TContext : DbContext
    {
        private readonly IBaseService<T, TDto, TContext> _service;
        public BaseController(IBaseService<T, TDto, TContext> service)
        {
            _service = service;
        }

        [HttpGet]
        public virtual async Task<IActionResult> Get()
        {
            return Ok(_service.Get());
        }

        [HttpGet("{id}")]
        public virtual async Task<IActionResult> GetById(int id)
        {
            var dto = await _service.GetById(id);

            if (dto is null)
            {
                return NotFound();
            }
            return Ok(dto);
        }

        [HttpPost]
        public virtual async Task<IActionResult> Create(TDto dto)
        {
            var newDto = await _service.Create(dto);

            if (newDto is null)
            {
                return BadRequest();
            }
            return Ok(newDto);
        }

        [HttpPut]
        public virtual async Task<IActionResult> Update(int id, TDto dto)
        {
            var updatedDto = await _service.Update(id, dto);

            if (updatedDto is null)
            {
                return BadRequest();
            }
            return Ok(updatedDto);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var isDeleted = await _service.Delete(id);

            if (isDeleted)
            {
                return NoContent();
            }
            return NotFound();
        }
    }
}
