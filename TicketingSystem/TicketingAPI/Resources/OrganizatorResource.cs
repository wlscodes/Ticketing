﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TicketingAPI.Resources
{
    public class OrganizatorResource
    {
        public int CreatorId { get; set; }

        [Required]
        [StringLength(128, MinimumLength = 5)]
        public string Name { get; set; }
    }
}
