using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace catlist_consoleapp
{
    class Program
    {
        static IPersonPets _personPets;

        static void Main(string[] args)
        {
            Bootstrap.Start();

            _personPets = Bootstrap.container.GetInstance<IPersonPets>();
            //Runs block until it completes.  
            //Normally an app doesn't block the main thread, but this app doesn't allow any interaction.

            _personPets.RunAsync().GetAwaiter().GetResult();
        }        

        

    }
}
