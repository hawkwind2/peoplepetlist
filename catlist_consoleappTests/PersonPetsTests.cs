using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace catlist_consoleapp.Tests
{
    [TestClass()]
    public class PersonPetsTests
    {
        HttpClient client;
        static readonly string jsonBaseUri = "http://agl-developer-test.azurewebsites.net/";
        static readonly string jsonCatList = "people.json";
        Person[] persons;

        [TestMethod()]
        public void PersonPetsTest()
        {
            client= new HttpClient();
            client.BaseAddress = new Uri(jsonBaseUri);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            Assert.IsNotNull(client, "http connection");
        }

        [TestMethod()]
        public void GetPersonListAsyncTest()
        {
            PersonPetsTest();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress.AbsoluteUri + jsonCatList).Result;
            Assert.IsNotNull(response, "No response meeage from web service");
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.OK);

            Assert.IsTrue(response.IsSuccessStatusCode, "connection fail");

            if (response.IsSuccessStatusCode)
                persons = response.Content.ReadAsAsync<Person[]>().Result;

            Assert.IsNotNull(persons, "no persons populated");
        }

        [TestMethod()]
        public void ShowPetsTest()
        {
            GetPersonListAsyncTest();
            Assert.IsNotNull(persons, "no persons populated");
            if (persons != null)
            {
                foreach (var person in persons)
                {
                    if (person.Pets == null)
                        Assert.Inconclusive($"no pets for person {person.Name}");

                    foreach (var pet in person.Pets)
                    {
                        if(pet!=null)                        
                            Assert.IsNotNull(pet.Name, $"{person.Name} has ${pet.Type} named {pet.Name}");
                    }

                }
            }
            
        }
    }
}