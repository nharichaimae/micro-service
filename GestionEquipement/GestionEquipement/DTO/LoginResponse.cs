namespace GestionEquipement.DTO
{
    public class LoginResponse
    {
        public bool Authenticated { get; set; }
        public string Token { get; set; }
        public UserDto User { get; set; }
    }
}
