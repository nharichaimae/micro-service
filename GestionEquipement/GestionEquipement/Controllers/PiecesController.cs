using GestionEquipement.Data;
using GestionEquipement.DTO;
using GestionEquipement.Mappers;
using GestionEquipement.Models;
using GestionEquipement.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestionEquipement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PiecesController : ControllerBase
    {
        private readonly PieceService _pieceService;
        private readonly AppDbContext _context;

        public PiecesController(PieceService pieceService, AppDbContext context)
        {
            _pieceService = pieceService;
            _context = context;
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] AddPieceDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.name))
                return BadRequest(new { success = false, message = "Le nom est obligatoire" });

            var userId = int.Parse(HttpContext.Items["UserId"].ToString());
            if (userId == null)
                return Unauthorized(new { success = false, message = "Utilisateur non authentifié" });

            var piece = PieceMapper.ToPiece(dto, userId);

            var idPiece = await _pieceService.AddPieceAsync(piece);

            return CreatedAtAction(
                nameof(GetPiecesWithEquipements),
                new { userId = userId },
                new { success = true, message = "Pièce ajoutée avec succès", id = idPiece }
            );
        }

        [HttpGet("with-equipements/{userId}")]
        public async Task<IActionResult> GetPiecesWithEquipements(int userId)
        {
            var result = await (
                from p in _context.piece
                join e in _context.equipement on p.id equals e.id into eqs
                where p.user_id == userId
                select new
                {
                    p.id,
                    p.name,
                    Equipements = eqs.Select(e => new
                    {
                        e.nom,
                        e.description,
                        e.etat
                    }).ToList()
                }
            ).ToListAsync();

            return Ok(result);
        }
    }
}
