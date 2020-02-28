using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Json;

namespace Data
{
    public class FileBasedPersonData : IPersonData
    {
        private readonly DataContractJsonSerializer personSerializer;
        public FileBasedPersonData()
        {
            personSerializer = new DataContractJsonSerializer(typeof(Person));
        }

        public DataContractJsonSerializer DataContractJsonSerializer { get; }

        public async Task<Person> Save(Person newPerson)
        {
            await Task.Factory.StartNew(() =>
            {
                string newId = Guid.NewGuid().ToString();
                using (FileStream stream1 = new FileStream($"./PersonList/{newId}.json", FileMode.CreateNew))
                {
                    newPerson.Id = newId;
                    personSerializer.WriteObject(stream1, newPerson);
                }
            });

            return newPerson;
        }
    }
}
