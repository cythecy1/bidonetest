using Backend.Model;
using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/person")]
    public class PersonController : ControllerBase
    {
        private readonly ILogger<PersonController> _logger;
        private readonly IPersonData personRepo;

        public PersonController(ILogger<PersonController> logger, IPersonData personRepo)
        {
            _logger = logger;
            this.personRepo = personRepo;
        }

        [HttpPost]
        public ActionResult CreatePerson([FromBody] PersonModel personModel)
        {
            if(ModelState.IsValid)
            {
                Person person = new Person { FirstName = personModel.FirstName, LastName = personModel.LastName };
                var returnPerson = personRepo.Save(person);

                return Created(String.Empty, returnPerson.Result.Id);
            }

            return BadRequest();
        }
    }
}
