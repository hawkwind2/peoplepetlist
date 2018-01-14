using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace catlist_consoleapp
{
    class Program
    {
        static HttpClient client = new HttpClient();
        static readonly string jsonBaseUri = "http://agl-developer-test.azurewebsites.net/";
        static readonly string jsonCatList = "people.json";

        static void Main(string[] args)
        {
            //Runs block until it completes.  
            //Normally an app doesn't block the main thread, but this app doesn't allow any interaction.
            
            RunAsync().GetAwaiter().GetResult();
        }        

        static async Task RunAsync()
        {
            client.BaseAddress = new Uri(jsonBaseUri);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                PersonPets personPets = new PersonPets(client);
                var personList = await personPets.GetPersonListAsync(jsonCatList);
                if (personList != null)
                {
                    personPets.ShowPets(personList);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }

    }
}
