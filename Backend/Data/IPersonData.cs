using System;
using System.Threading.Tasks;

namespace Data
{
    public interface IPersonData
    {
        Task<Person> Save(Person newPerson);
    }
}
