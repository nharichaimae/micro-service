using GestionEquipement.Data;
using GestionEquipement.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionEquipement.Services
{
    public class PieceService
    {
        private readonly AppDbContext _context;

        public PieceService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> AddPieceAsync(Piece piece)
        {
            _context.piece.Add(piece);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine(ex.InnerException?.Message);
                throw;
            }
            return piece.id;
        }
    }
}
