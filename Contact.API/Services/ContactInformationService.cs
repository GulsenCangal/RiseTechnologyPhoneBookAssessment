using Contact.API.Data;
using Contact.API.Models.Context;
using Contact.API.Models.Entity;
using Contact.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;

namespace Contact.API.Services
{
    public class ContactInformationService: IContactInformationService
    {
        ContactDbContext contactContext;
        public ContactInformationService(ContactDbContext context)
        {
            contactContext = context;
        }
        public async Task<ReturnData> AddContactInformation(Guid personId, ContactInformationData contactInformationData)
        {
            var checkPerson = await contactContext.personTable.Where(p => p.uuId == personId).FirstOrDefaultAsync();

            if(checkPerson==null)
            {
                return new ReturnData
                {
                    response = false,
                    message = "İletişim bilgisi eklenecek kişi bulunmamaktadır.",
                    data = null
                };
            }

            var contactInfoData = new ContactInformation()
            {
                personUuId = personId,
                informationType = contactInformationData.informationType,
                informationContent = contactInformationData.informationContent,
            };

            await contactContext.AddAsync(contactInfoData);
            await contactContext.SaveChangesAsync();

            return new ReturnData()
            {
                response = true,
                message = "İletişim bilgileri "+ checkPerson.name+" "+ checkPerson.surname+" isimli kişi için eklendi.",
                data = contactInfoData
            };
        }

        public async Task<ReturnData> DeleteContactInformation (Guid contactInformationId)
        {
            var deleteContactInfo = await contactContext.contactInformationTable.Where(p => p.uuId == contactInformationId).FirstOrDefaultAsync();

            if (deleteContactInfo == null)
            {
                return new ReturnData
                {
                    response = false,
                    message = "Silinecek böyle bir iletişim bilgisi bulunmamaktadır.",
                    data = null
                };
            }
            else
            {
                contactContext.contactInformationTable.Remove(deleteContactInfo);
                await contactContext.SaveChangesAsync();

                return new ReturnData
                {
                    response = true,
                    message = "İletişim bilgisi silindi.",
                    data = deleteContactInfo
                };
            }
        }

        public async Task<ReturnData> GetAllContactInformations()
        {
            int contactInfoCount = contactContext.personTable.Count();

            if (contactInfoCount == 0)
            {
                return new ReturnData
                {
                    response = false,
                    message = "Listelenecek iletişim bilgisi bulunmamaktadır.",
                    data = null
                };
            }

            var contactInfos = await contactContext.contactInformationTable.Select(p => new ContactInformationData
            {
                uuId = p.uuId,
                informationContent = p.informationContent,
                informationType= p.informationType

            }).ToListAsync();

            if (contactInfos == null)
            {
                return new ReturnData
                {
                    response = false,
                    message = "İletişim bilgileri listelenirken hata oluştu.",
                    data = null
                };
            }
            else
            {
                return new ReturnData
                {
                    response = true,
                    message = "İletişim bilgileri listelenmektedir.",
                    data = contactInfos
                };
            }
        }
    }
}
