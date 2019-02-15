using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECAuthorization.ECApp
{
    public class EmailRequirement: IAuthorizationRequirement
    {
        public bool VerifiedEmail { get; set; }

        public EmailRequirement(bool verified)
        {
            VerifiedEmail = verified;
        }
    }
}
