﻿using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Infrastructure.Auth;

namespace WebAPI.Infrastructure
{
    public static class ApplicationTokens
    {
        private static ConcurrentDictionary<string, SecurityKey> _tokens = new ConcurrentDictionary<string, SecurityKey>
        {
            ["AdminPanel"] = JwtSecurityKey.Create("FucZKmfPWlvY91oFOPjJ"),
            ["BasePanel"] = JwtSecurityKey.Create("386B17FD36C9176795BF91ABEEC43")
        };
        public static ConcurrentDictionary<string, SecurityKey> Tokens => _tokens;
    }
}
