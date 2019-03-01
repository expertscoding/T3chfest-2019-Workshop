using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECAuthorization.ECApi
{
    public class DayRequirement : IAuthorizationRequirement
    {
        public int Day { get; set; }

        public DayRequirement(int day)
        {
            Day = day;
        }
    }
}
