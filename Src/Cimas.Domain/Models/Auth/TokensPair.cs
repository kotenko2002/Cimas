﻿namespace Cimas.Domain.Models.Auth
{
    public class TokensPair
    {
        public Token AccessToken { get; set; }
        public Token RefreshToken { get; set; }
    }
}
