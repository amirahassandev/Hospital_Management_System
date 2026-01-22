using AutoMapper;
using HospitalManagementSystem.Data.Models;
using HospitalManagementSystem.Dto.Prescription;
using HospitalManagementSystem.Repositories;

namespace HospitalManagementSystem.Services
{
    public class PrescriptionService : IPrescriptionService
    {
        private readonly IMapper _mapper;
        private readonly IPrescriptionRepository _repository;
        public PrescriptionService(IMapper _mapper, IPrescriptionRepository _repository) 
        {
            this._mapper = _mapper;
            this._repository = _repository;
        }
        async Task<ReadPrescriptionDto?> IPrescriptionService.CreatePrescriptionAsync(AddPrescriptionDto prescriptionDto)
        {
            var prescription = _mapper.Map<Prescription>(prescriptionDto);

            var createdPrescription = await _repository.AddAsync(prescription);
            if (createdPrescription == null) return null;

            return _mapper.Map<ReadPrescriptionDto>(createdPrescription);
        }

        //Task<bool> IPrescriptionService.DeletePrescriptionAsync(int prescriptionId)
        //{
        //    throw new NotImplementedException();
        //}

        async Task<IEnumerable<ReadPrescriptionDto>> IPrescriptionService.GetActivePrescriptionsAsync()
        {
            var prescription = await _repository.GetActiveAsync();
            return _mapper.Map<IEnumerable<ReadPrescriptionDto>>(prescription);
        }

        async Task<IEnumerable<ReadPrescriptionDto>> IPrescriptionService.GetAllPrescriptionsAsync()
        {
            var prescription = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<ReadPrescriptionDto>>(prescription);
        }

        async Task<IEnumerable<ReadPrescriptionDto>> IPrescriptionService.GetByMedicalIdAsync(int medicalId)
        {
            var prescription = await _repository.GetByMedicalIdAsync(medicalId);
            return _mapper.Map<IEnumerable<ReadPrescriptionDto>>(prescription);
        }

        async Task<ReadPrescriptionDto?> IPrescriptionService.GetPrescriptionByIdAsync(int prescriptionId)
        {
            var prescription = await _repository.GetByIdAsync(prescriptionId);
            return _mapper.Map<ReadPrescriptionDto>(prescription);
        }

        async Task<ReadPrescriptionDto?> IPrescriptionService.UpdatePrescriptionAsync(int prescriptionId ,UpdatePrescriptionDto dto)
        {
            var prescription = await _repository.GetByIdAsync(prescriptionId);
            if (prescription == null) return null;

            // Update only sent values
            if (dto.MedicationName != null)
                prescription.MedicationName = dto.MedicationName;

            if (dto.Dosage != null)
                prescription.Dosage = dto.Dosage;

            if (dto.StartDate.HasValue)
                prescription.StartDate = dto.StartDate;

            if (dto.EndDate.HasValue)
                prescription.EndDate = dto.EndDate;

            if (dto.MedicalId.HasValue)
                prescription.MedicalId = dto.MedicalId.Value;

            await _repository.UpdateAsync(prescription);

            return _mapper.Map<ReadPrescriptionDto>(prescription);
        }

    }
}
