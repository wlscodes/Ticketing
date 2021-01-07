using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketingClient.Data
{
    public interface IStorage
    {
        public string GetItem(string key);

        public void AddItem(string key, string value);
    }
}
