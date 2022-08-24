using Contact.API.Data;
using Contact.API.Models.Entity;
using Contact.API.Services;
using Contact.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Contact.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService personService;
        private readonly IContactInformationService contactInformationService;

        public PersonController(IPersonService personService, IContactInformationService contactInformationService)
        {
            this.personService = personService;
            this.contactInformationService = contactInformationService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ReturnData>> AddPerson([FromBody] PersonData personData)
        {
            if (personData == null)
                return BadRequest();

            var result = await personService.AddPerson(personData);

            if (result.response==true)
                return Created("", result);

            return BadRequest(result.message);
        }

        [HttpDelete("{personId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ReturnData>> DeletePerson([FromRoute] Guid personId)
        {
            var result = await personService.DeletePerson(personId);

            if (result.response == false)
            {
                return NotFound(result.message);
            }
            else
            {
                return Ok(result);
            }            
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ReturnData>> GetAllPersons()
        {
            var allPersons = await personService.GetAllPersons();

            if (allPersons.response == false)
            {
                return NotFound(allPersons.message);
            }
            else
            {
                return Ok(allPersons);
            }
        }

        [HttpGet("{personId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ReturnData>> GetPerson(Guid personId)
        {
            var person = await personService.GetPerson(personId);

            if (person.response == false)
            {
                return NotFound(person.message);
            }
            else
            {
                return Ok(person);
            }
        }

        [HttpPost("{personId}/ContactInformations")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ReturnData>> AddContactInformation(Guid personId, [FromBody] ContactInformationData contactInformationData)
        {
            if (contactInformationData == null)
            {
                return BadRequest();
            }

            var result = await contactInformationService.AddContactInformation(personId, contactInformationData);

            if (result.response == true)
            {
                return Created("", result);
            }
            else
            {
                return NotFound(result.message);
            }
           
        }

        [HttpDelete("ContactInformations/{contactInformationId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ReturnData>> DeleteContactInformation([FromRoute] Guid contactInformationId)
        {

            var result = await contactInformationService.DeleteContactInformation(contactInformationId);

            if (result.response == false)
            {
                return NotFound(result.message);
            }
            else
            {
                return Ok(result);
            }            
        }

        [HttpGet("{personId}/Detail")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ReturnData>> GetPersonDetail(Guid personId)
        {
            var result = await personService.GetPersonDetail(personId);

            if (result.response == false)
            {
                return NotFound(result.message);
            }
            else
            {
                return Ok(result);
            }
        }

        [HttpGet("ContactInformations")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ReturnData>> GetAllContactInformations()
        {
            var result = await contactInformationService.GetAllContactInformations();

            if (result.response == false)
            {
                return NotFound(result.message);
            }
            else
            {
                return Ok(result);
            }
        }
    }
}
