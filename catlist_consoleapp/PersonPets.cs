using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace catlist_consoleapp
{
    public class PersonPets
    {
        HttpClient _client;
        public PersonPets(HttpClient client)
        {
            _client = client;
        }
        public async Task<Person[]> GetPersonListAsync(string path)
        {
            Person[] persons = null;
            HttpResponseMessage response = await _client.GetAsync(_client.BaseAddress.AbsoluteUri + path);
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

        public void ShowPets(Person[] persons)
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

        private static IEnumerable<Pet> SortByName(Pet[] pets)
        {
            return pets.OrderBy(p => p.Name);
        }

    }
}
