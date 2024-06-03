using KnilaBE.Models;
using KnilaBE.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace KnilaBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ContactsController : ControllerBase
    {
        private readonly IContactRepository _contactRepository;
        public ContactsController(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetContacts()
        {
            var results = await _contactRepository.GetContacts();
            Log.Information("Get contacts endpoint executed");
            return Ok(results);
        }

        [HttpPost]
        public async Task<IActionResult> CreateContact(Contact contact)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                var contactRes = await _contactRepository.CreateContact(contact);
                return Ok(new { message = "Created contact successfully!!", id = contactRes.Id });
            }
        }
        [HttpPut]
        public async Task<IActionResult> EditContact(Contact contactToUpdate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                var contactRes=await _contactRepository.UpdateContact(contactToUpdate);
                return Ok(contactRes);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteContact(int id)
        {
            var delRes=await _contactRepository.DeleteContact(id);
            if (delRes)
            {
                return NoContent();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
