using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace WPF
{
    static class Endpoints
    {
        private const string BASE = "https://localhost:7267/api/";
        public const string FERRIES_ALL = $"{BASE}Ferry";
    }
}
