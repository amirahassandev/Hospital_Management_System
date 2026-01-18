using AutoMapper;
using HospitalManagementSystem.Data.Models;
using HospitalManagementSystem.Dto.Billing;
using HospitalManagementSystem.Repositories;

namespace HospitalManagementSystem.Services
{
    public class BillingService : IBillingService
    {
        private readonly IBillingRepository _repository;
        private readonly IMapper _mapper;
        public BillingService(IBillingRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        async Task<bool> IBillingService.IsExist(int patientId, int paymentStatusId)
        {
            bool isExisted = await _repository.IsExist(patientId, paymentStatusId);
            return isExisted;
        }

        async Task<ReadBillingDto?> IBillingService.CreateBillingAsync(AddBillingDto dto)
        {
            var billing = _mapper.Map<Billing>(dto);
            billing = await _repository.AddAsync(billing);
            return _mapper.Map<ReadBillingDto>(billing);
        }

        async Task<bool> IBillingService.DeleteBillingAsync(int billingId)
        {
            var bill = await ((IBillingService)this).UpdateBillingAsync(billingId, new UpdateBillingDto() {PaymentStatusId = 1 });
            return bill != null;
        }

        async Task<IEnumerable<ReadBillingDto>> IBillingService.GetAllBillingsAsync()
        {
            var bills = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<ReadBillingDto>>(bills);
        }

        async Task<ReadBillingDto?> IBillingService.GetBillingAsync(int billingId)
        {
            var bill = await _repository.GetByIdAsync(billingId);
            if (bill == null) return null;
            return _mapper.Map<ReadBillingDto>(bill);
        }
        public async Task<ReadBillingDto?> UpdateBillingAsync(int billingId, UpdateBillingDto dto)
        {
            var billing = await _repository.GetByIdAsync(billingId);
            if (billing == null) return null;

            if (dto.TotalAmount != 0)
                billing.TotalAmount = dto.TotalAmount;

            if (dto.PaymentStatusId != 0 && dto.PaymentStatusId != billing.PaymentStatusId)
                billing.PaymentStatusId = dto.PaymentStatusId;
            var readBill = _mapper.Map<ReadBillingDto>(billing);
            await _repository.UpdateAsync(billing);
            return readBill;
        }
    }
}
