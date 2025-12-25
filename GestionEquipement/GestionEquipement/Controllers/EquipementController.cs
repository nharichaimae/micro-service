using GestionEquipement.Data;
using GestionEquipement.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestionEquipement.DTO;


namespace GestionEquipement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipementController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EquipementController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddEquipement([FromBody] EquipementDto dto)
        {
            bool pieceExiste = await _context.piece.AnyAsync(p => p.id == dto.id);
            if (!pieceExiste)
                return BadRequest("La pièce spécifiée n'existe pas.");

            var equipement = EquipementMapper.ToEntity(dto);

            _context.equipement.Add(equipement);
            await _context.SaveChangesAsync();

            return Ok(equipement);
        }
    }
}
