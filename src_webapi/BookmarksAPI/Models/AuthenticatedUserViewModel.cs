﻿namespace BookmarksAPI.Models
{
    public class AuthenticatedUserViewModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public TokenViewModel TokenInfo { get; set; }
    }
}
