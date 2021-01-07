using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketingClient.Data
{
    public class Storage : IStorage
    {
        private Dictionary<string, string> _dictionary;

        public Storage()
        {
            _dictionary = new Dictionary<string, string>();
        }

        public void AddItem(string key, string value)
        {
            _dictionary.Add(key, value);
        }

        public string GetItem(string key)
        {
            string value = String.Empty;

            _ =_dictionary.TryGetValue(key, out value);

            return value;
        }
    }
}
