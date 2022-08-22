using Contact.API.Data;
using Contact.API.Models.Context;
using Contact.API.Models.Entity;
using Contact.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Intrinsics.X86;

namespace Contact.API.Services
{
    public class PersonService :IPersonService
    {
        ContactDbContext contactContext;
        public PersonService(ContactDbContext context)
        {
            contactContext = context;
        }

        public async Task<ReturnData> AddPerson(PersonData personData)
        {
            var person = new Person()
            {
                name = personData.name,
                surname = personData.surname,
                company = personData.company
            };

            await contactContext.personTable.AddAsync(person);
            await contactContext.SaveChangesAsync();

            if (person.uuId != Guid.Empty)
            {
                return new ReturnData
                {
                    response = true,
                    message = "Kişi eklendi.",
                    data = person
                };
            }
            else
            {
                return new ReturnData
                {
                    response = false,
                    message = "Kişi eklenemedi.",
                    data = null
                };
            }           
        }

        public async Task<ReturnData> DeletePerson(Guid personUuId)
        {
            var deletePerson = await contactContext.personTable.Where(p => p.uuId == personUuId).FirstOrDefaultAsync();

           if(deletePerson == null)
            {
                return new ReturnData
                {
                    response = false,
                    message = "Silinecek böyle bir kayıt bulunmamaktadır.",
                    data = null
                };
            }
           else
            {
                contactContext.personTable.Remove(deletePerson);
                await contactContext.SaveChangesAsync();

                return new ReturnData
                {
                    response = true,
                    message = "Kişi silindi.",
                    data = deletePerson
                };
            }            
        }

        public async Task<ReturnData> GetAllPersons()
        {
            int personCount = contactContext.personTable.Count();

            if (personCount == 0)
            {
                return new ReturnData
                {
                    response = false,
                    message = "Listelenecek kayıt bulunmamaktadır.",
                    data = null
                };
            }

            var allPersons= await contactContext.personTable.Select(p => new PersonData
            {
                uuId = p.uuId,
                name = p.name,
                surname = p.surname,
                company = p.company,
            }).ToListAsync();

           if(allPersons==null)
            {
                return new ReturnData
                {
                    response = false,
                    message = "Kişiler listelenirken hata oluştu.",
                    data = null
                };
            }
            else
            {
                return new ReturnData
                {
                    response = true,
                    message = "Kişiler listelenmektedir.",
                    data = allPersons
                };
            }
        }

        public async Task<ReturnData> GetPerson(Guid personId)
        {
            int personCount = contactContext.personTable.Count(a => a.uuId == personId);
            if (personCount == 0)
            {
                return new ReturnData
                {
                    response = false,
                    message = "Kişi bulunmamaktadır.",
                    data = null
                };
            }

            var getPerson= await contactContext.personTable.Where(p => p.uuId == personId).Select(p => new PersonData
            {
                uuId = p.uuId,
                name = p.name,
                surname = p.surname,
                company = p.company
            }).FirstOrDefaultAsync();

            
            if (getPerson == null)
            {
                return new ReturnData
                {
                    response = false,
                    message = "Kişi listelenirken hata oluştu.",
                    data = null
                };
            }
            else
            {
                return new ReturnData
                {
                    response = true,
                    message = "Kişi listelenmektedir.",
                    data = getPerson
                };
            }
        }

        public async Task<ReturnData> GetPersonDetail(Guid personId)
        {
            var result = await contactContext.personTable.Where(p => p.uuId == personId).Select(p => new PersonDetailData
            {
                person = new PersonData()
                {
                    uuId = p.uuId,
                    name = p.name,
                    surname = p.surname,
                    company = p.company
                },
                contactInformation = p.contactInformation.Select(c => new ContactInformationData
                {
                    uuId = c.uuId,
                    informationType = c.informationType,
                    informationContent = c.informationContent
                }).ToList()
            }).FirstOrDefaultAsync();

            if (result == null)
            {
                return new ReturnData
                {
                    response = true,
                    message = "Kişi listelenememektedir.",
                    data = null
                };
            }
            else
            {
                return new ReturnData
                {
                    response = true,
                    message = "Kişi listelenmektedir.",
                    data = result
                };
            }
        }
    }
}
