 using KnilaBE.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace KnilaBE.Repository
{
    public class ContactRepository : IContactRepository
    {
        private readonly ApplicationDBContext _dbContext;

        public ContactRepository(ApplicationDBContext dBContext)
        {
            _dbContext = dBContext;
        }

        async Task<Contact> IContactRepository.CreateContact(Contact contact)
        {
            var result=await _dbContext.Contacts.AddAsync(contact);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

        async Task<bool> IContactRepository.DeleteContact(int id)
        {
            var result = await _dbContext.Contacts.FirstOrDefaultAsync(c=>c.Id == id);
            if (result != null)
            {
                _dbContext.Contacts.Remove(result);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        async Task<IEnumerable<Contact>> IContactRepository.GetContacts()
        {
            var results=await _dbContext.Contacts.ToListAsync();
            return results;
        }

        async Task<Contact> IContactRepository.UpdateContact(Contact contact)
        {
            var result = await _dbContext.Contacts.FirstOrDefaultAsync(c=>c.Id==contact.Id);
            if (result != null)
            {
                result.FirstName=contact.FirstName;
                result.LastName=contact.LastName;
                result.Email=contact.Email;
                result.PhoneNumber=contact.PhoneNumber;
                result.City=contact.City;   
                result.State=contact.State; 
                result.Address=contact.Address;
                result.Country=contact.Country; 
                result.PostalCode=contact.PostalCode;
                await _dbContext.SaveChangesAsync();
                return result;
            }
            return null;
        }
    }
}
