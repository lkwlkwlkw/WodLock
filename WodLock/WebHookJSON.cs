using System;

public class WebHookJSON
{
    public string? Event { get; set; }
    public User? _User { get; set; }
    public string? Memberships { get; set; }

    public class User
    {        
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }

        public string? Type { get; set;}
        public string? Gender { get; set; }
    }

    public class DaneKlienta
    {
        public int? ErrorCode { get; set; }
        public User? User { get; set; }
        public string? Message { get; set; }
    }

    




}
