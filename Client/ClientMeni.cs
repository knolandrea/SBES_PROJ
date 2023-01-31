using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    internal class ClientMeni
    {
        public void StartMeni(WCFClient proxy)
        {
            proxy.ProveriStanje();
        }
    }
}
