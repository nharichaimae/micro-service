using System.ComponentModel.DataAnnotations;

namespace GestionEquipement.Models
{
    public class Piece
    {
        [Key]
        public int id { get; set; }        
        public string name { get; set; } = string.Empty;  
        public int user_id { get; set; }
        public ICollection<equipementModel> equipements { get; set; }
    }
}
