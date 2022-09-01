using System;
using System.Collections.Generic;
using System.Text;
using TercihSihirbazi.Entities.Interfaces;

namespace TercihSihirbazi.Entities.Token
{
    public class JwtRefreshToken : IToken
    {
        public string RefreshToken { get; set; }
    }
}
