﻿
namespace databaseAPI.Models
{
    public class User
    {

        public int ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        public bool CanLogin { get; set; }
        public bool MustChangePassword { get; set; }

    }
}
