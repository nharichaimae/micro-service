using GestionEquipement.DTO;
using GestionEquipement.Models;

namespace GestionEquipement.Mappers
{
	public static class EquipementMapper
	{
		public static equipementModel ToEntity(EquipementDto dto)
		{
			return new equipementModel
			{
				nom = dto.Nom,
				description = dto.Description,
				etat = "OFF",
				id = dto.id   
			};
		}
	}
}
