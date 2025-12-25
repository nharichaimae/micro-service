using GestionEquipement.DTO;
using GestionEquipement.Models;

namespace GestionEquipement.Mappers
{
    public class PieceMapper
    {
        public static Piece ToPiece(AddPieceDto dto, int userId)
        {
            return new Piece
            {
                name = dto.name,
                user_id = userId
            };
        }
    }
}
