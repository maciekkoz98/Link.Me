using System;

namespace LinkMe.Core
{
    public class User
    {
        public int ID { get; set; }
        public string Login { get; set; }
        public string HashedPassword { get; set; }
        public UserType UserType { get; set; }
    }
}
