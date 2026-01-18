using AutoMapper;
using HospitalManagementSystem.Data.Models;
using HospitalManagementSystem.Dto.NursePatient;
using HospitalManagementSystem.Dto.Patient;
using HospitalManagementSystem.Repositories;

namespace HospitalManagementSystem.Services
{
    public class NursePatientService: INursePatientService
    {
        private readonly INursePatientRepository _repository;
        private readonly IPationtRepository _repositoryPatient;

        private readonly IMapper _mapper;

        public NursePatientService(INursePatientRepository _repository, IMapper _mapper, IPationtRepository _repositoryPatient)
        {
            this._repository = _repository;
            this._mapper = _mapper;
            this._repositoryPatient = _repositoryPatient;
        }

        async Task<IEnumerable<ReadNursePatientDto>> INursePatientService.GetAllNursePatients()
        {
            var nursePatients = await _repository.GetAllNursePatients();
            return _mapper.Map<IEnumerable<ReadNursePatientDto>>(nursePatients);
        }

        async Task<bool> INursePatientService.IsNursePatientExist(int nurseId, int patientId)
        {
            bool isExisted = await _repository.IsNursePatientExist(nurseId, patientId);
            return isExisted;
        }

        async Task<ReadNursePatientDto?> INursePatientService.AssignNurseToPatient(AddNursePatientDto? nursePatientDto)
        {
            if (nursePatientDto == null) return null;
            var nursePatient = _mapper.Map<NursePatient>(nursePatientDto);
            if (nursePatient == null) return null;
            await _repository.AssignNurseToPatient(nursePatient);

            return _mapper.Map<ReadNursePatientDto>(nursePatient);
        }

        async Task<ReadNursePatientDto?> INursePatientService.UpdateNursePatient(UpdateNursePatientDto nursePatientDto)
        {
            bool isExisted = await _repository.IsNursePatientExist(nursePatientDto.NurseId, nursePatientDto.PatientId);
            if(!isExisted) return null;

            var nursePatient = await _repository.GetNursePatient(nursePatientDto.NurseId, nursePatientDto.PatientId);
            if (nursePatient == null) return null;
            if (nursePatientDto.Shift != null)
            {
                nursePatient.NursePatientShift = nursePatientDto.Shift;
            }
            if (nursePatientDto.Notes != null)
            {
                nursePatient.Notes = nursePatientDto.Notes;
            }

            bool isUpdated = await _repository.UpdateNursePatient(nursePatient);
            if(!isUpdated) return null;
            return _mapper.Map<ReadNursePatientDto>(nursePatient);
        }

        async Task<ReadNursePatientDto?> INursePatientService.RemoveNursePatient(int nurseId, int patientId)
        {
            var nursePatient = await _repository.GetNursePatient(nurseId, patientId);
            var readNursePatient = _mapper.Map<ReadNursePatientDto>(nursePatient);
            var isRemoved = await _repository.RemoveNursePatient(nursePatient);
            if(!isRemoved) return null;
            return readNursePatient;
        }
        async Task<IEnumerable<ReadNursePatientDto?>> INursePatientService.GetAssignmentsForPatient(int patientId)
        {
            var patient = await _repositoryPatient.GetPatient(patientId);
            if (patient == null) return null;
            var assignmentsPatients = await _repository.GetAssignmentsForPatient(patientId);
            return _mapper.Map<IEnumerable<ReadNursePatientDto>>(assignmentsPatients);
        }
    }
}
