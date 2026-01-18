using HospitalManagementSystem.Data.Models;
using HospitalManagementSystem.Dto.Billing;
using HospitalManagementSystem.Helpers;

namespace HospitalManagementSystem.Mappers
{
    public class BillingProfile : AutoMapper.Profile
    {

        public BillingProfile() 
        {
            CreateMap<AddBillingDto, Billing>();
            CreateMap<Billing, ReadBillingDto>()
                .ForMember(rb => rb.PaymentStatus, o => o.MapFrom(b => b.PaymentStatus.StatusName))
                .ForMember(rb => rb.patientName, o => o.MapFrom(b => HelperMethods.GetFullName(b.Patient.User.FirstName, b.Patient.User.LastName)));
            CreateMap<UpdateBillingDto, Billing>()
                .ForMember(b => b.PatientId, o => o.Ignore());
        }
    }
}
