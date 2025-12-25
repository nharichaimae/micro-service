using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionEquipement.Models
{
    public class equipementModel
    {
        [Key] 

        public int id_equipement { get; set; }
        public string nom { get; set; } = string.Empty;

        public string description { get; set; } = string.Empty;
        public string etat { get; set; } = "OFF";
        [ForeignKey("Piece")]
        public  int id { get; set; }
        public Piece Piece { get; set; }
    }
}
