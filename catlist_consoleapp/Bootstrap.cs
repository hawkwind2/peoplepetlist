using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace catlist_consoleapp
{
    class Bootstrap
    {
        //Using Simple Injector IOC

        public static Container container;

        public static void Start()
        {
            container = new Container();
            // Register your types, for instance:
            container.Register<IPersonPets, PersonPets>(Lifestyle.Singleton);
            
            // Optionally verify the container.
            container.Verify();
           

        }
    }
}
