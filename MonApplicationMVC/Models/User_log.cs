using System.ComponentModel.DataAnnotations;

namespace MonApplicationMVC.Models
{
    public class User_log
    {
        [Key]
        public int idUser_log { get; set; }
        public string user { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;
    }
}
