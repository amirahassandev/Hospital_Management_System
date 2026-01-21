using AutoMapper;
using HospitalManagementSystem.Data.Models;
using HospitalManagementSystem.Dto.Admission;
using HospitalManagementSystem.Repositories;

namespace HospitalManagementSystem.Services.Admissions
{
    public class AdmissionService : IAdmissionService
    {
        private readonly IAdmissionRepository _repository;
        private readonly IMapper _mapper;
        public AdmissionService(IAdmissionRepository _repository, IMapper _mapper)
        {
            this._repository = _repository;
            this._mapper = _mapper;
        }

        async Task<ReadAdmissionDto?> IAdmissionService.CreateAdmissionAsync(AddAdmissionDto admissionDto)
        {
            var admission = _mapper.Map<Admission>(admissionDto);
            admission = await _repository.AddAsync(admission);
            return _mapper.Map<ReadAdmissionDto>(admission);
        }

        //async Task<bool> IAdmissionService.DeleteAdmissionAsync(int admissionId)
        //{
            
        //}

        async Task<ReadAdmissionDto?> IAdmissionService.GetAdmissionByIdAsync(int admissionId)
        {
            var admision = await _repository.GetByIdAsync(admissionId);
            if(admision == null) return null;
            return _mapper.Map<ReadAdmissionDto>(admision);
        }

        async Task<IEnumerable<ReadAdmissionDto?>> IAdmissionService.GetAllAdmissionsAsync()
        {
            var admissions = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<ReadAdmissionDto>>(admissions);
        }


        async Task<bool> IAdmissionService.IsRoomAvailableAsync(string roomNumber)
        {
            return await _repository.IsRoomAvailableAsync(roomNumber);
        }

        async Task<bool> IAdmissionService.IsRoomExistedAsync(string roomNumber)
        {
            return await _repository.IsRoomExistedAsync(roomNumber);
        }

        async Task<ReadAdmissionDto?> IAdmissionService.UpdateAdmissionAsync(int admissionId , UpdateAdmissionDto dto)
        {
            var existing = await _repository.GetByIdAsync(admissionId);
            if (existing == null) return null;

            // Update only provided fields
            if (dto.AdmissionDate.HasValue)
                existing.AdmissionDate = dto.AdmissionDate;

            if (dto.DischargeDate.HasValue)
                existing.DischargeDate = dto.DischargeDate;

            if (dto.PatientId != 0)
                existing.PatientId = dto.PatientId;

            if (!string.IsNullOrWhiteSpace(dto.RoomNumber))
            {
                var room = await _repository.GetByRoomIdAsync(int.Parse(dto.RoomNumber));
                if (room == null)
                    throw new Exception("Room does not exist.");
                existing.RoomId = room.FirstOrDefault()?.RoomId ?? existing.RoomId;

                if (dto.RoomStatusId != 0)
                    existing.Room.RoomStatusId = dto.RoomStatusId;
            }
            else if (dto.RoomStatusId != 0)
            {
                existing.Room.RoomStatusId = dto.RoomStatusId;
            }

            var isUpdated = await _repository.UpdateAsync(existing);
            if (!isUpdated) return null;

            return _mapper.Map<ReadAdmissionDto>(existing);
        }

    }
}
