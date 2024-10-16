namespace API.DTO
{
    public class UserDTO
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Mail { get; set; }
        public bool IsAdmin { get; set; }
        public string Token { get; set; }
    }
}