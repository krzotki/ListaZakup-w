using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ListaZakupów
{
    class Utils
    {
        static public string toJSON(Object objectInstance)
        {
            return JsonConvert.SerializeObject(objectInstance);
        }

    }
}
