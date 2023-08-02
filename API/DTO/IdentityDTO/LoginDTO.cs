namespace API.DTO.IdentityDTO
{
    /// <summary>
    /// Login DTO
    /// </summary>
    public class LoginDTO
    {
        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; } = null!;

        /// <summary>
        /// password
        /// </summary>
        public string Password { get; set; } = null!;

    }
}
