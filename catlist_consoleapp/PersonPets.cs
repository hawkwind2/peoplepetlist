using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace catlist_consoleapp
{
    public class PersonPets: IPersonPets
    {
        static HttpClient client = new HttpClient();
        static readonly string jsonBaseUri = Properties.Settings.Default.serviceBaseUri;
        static readonly string jsonCatList = Properties.Settings.Default.srcName;

        public async Task RunAsync()
        {
            client.BaseAddress = new Uri(jsonBaseUri);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                var personList = await GetPersonListAsync(jsonCatList);
                if (personList != null)
                {
                    ShowPets(personList);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }

        private async Task<Person[]> GetPersonListAsync(string path)
        {
            Person[] persons = null;
            HttpResponseMessage response = await client.GetAsync(client.BaseAddress.AbsoluteUri + path);
            if (response.IsSuccessStatusCode)
            {
                persons = await response.Content.ReadAsAsync<Person[]>();
            }
            else
            {
                Console.WriteLine(response.ReasonPhrase, response.StatusCode);
            }
            return persons;
        }

        private void ShowPets(Person[] persons)
        {
            foreach (var person in persons)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{person.Gender}");
                Console.ResetColor();
                if (person.Pets != null)
                {
                    foreach (var pet in SortByName(person.Pets))
                    {
                        Console.WriteLine($"\t {pet.Name}");
                    }
                }
                else
                    Console.WriteLine($"\t-NONE-");
            }
        }

        private IEnumerable<Pet> SortByName(Pet[] pets)
        {
            return pets.OrderBy(p => p.Name);
        }

    }
}
