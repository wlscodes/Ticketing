using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketingAPI.Resources;

namespace TicketingAPI.Services.Interfaces
{
    public interface IPlaceService
    {
        public Task<bool> AddPlace(CreatePlaceResource resource);
    }
}
