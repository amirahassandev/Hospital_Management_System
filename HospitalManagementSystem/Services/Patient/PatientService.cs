using AutoMapper;
using HospitalManagementSystem.Data;
using HospitalManagementSystem.Data.Models;
using HospitalManagementSystem.Dto.Patient;
using HospitalManagementSystem.Dto.User;
using HospitalManagementSystem.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace HospitalManagementSystem.Services
{
    public class PatientService : IPatientService
    {
        private readonly IPationtRepository _repo;
        private readonly IUserService _repoUser;
        private readonly HospitalDbContext _db;

        private readonly IMapper _mapper;

        public PatientService(IPationtRepository _repo, IMapper _mapper, IUserService _repoUser, HospitalDbContext _db) 
        {
            this._repo = _repo;
            this._mapper = _mapper;
            this._repoUser = _repoUser;
            this._db = _db;
        }

        // ################  GET   ################ //
        async Task<IEnumerable<ReadPatient>> IPatientService.GetAll()
        {
            var patients = await _repo.GetAll();
            return _mapper.Map<IEnumerable<ReadPatient>>(patients);
        }

        async Task<ReadPatient?> IPatientService.GetPatient(int id)
        {
            var patient = await _repo.GetPatient(id);
            if (patient == null) return null;
            return _mapper.Map<ReadPatient>(patient);
        }

        async Task<IEnumerable<ReadPatient?>> IPatientService.GetAllDeactivatedPatientsAsync()
        {
            var patients = await _repo.GetAllDeactive();
            if (patients == null) return null;
            return _mapper.Map<IEnumerable<ReadPatient>>(patients);
        }

        async Task<string?> IPatientService.GetBloodTypeAsync(int id)
        {
            return await _repo.GetBloodType(id);
        }

        async Task<int> IPatientService.CountAsync()
        {
            return await _repo.Count();
        }


        // ################  ADD   ################ /

        async Task<ReadPatient?> IPatientService.AddPatientAsync(AddPatientDto patientDto)
        {
            if (patientDto == null) return null;
            var user = await _repoUser.CreateUserAsync(patientDto.createUserDto);  // UserReadDto with id

            var patient = _mapper.Map<Patient>(patientDto);

            patient.UserId = user.UserId;    
            patient.User = null;              

            bool isAdded = await _repo.AddPatient(patient);

            if (!isAdded) return null;

            return _mapper.Map<ReadPatient>(patient);
        }


        // ################  UPDATE   ################ /
        async Task<ReadPatient?> IPatientService.UpdatePatientAsync(int id, UpdatePatientDto patientDto)
        {
            if (patientDto == null) return null;
            var patient = await _db.Patients.FirstOrDefaultAsync(p => p.PatientId == id);
            var userId = patient.UserId;
            var user =  await _repoUser.UpdateUserAsync(userId, patientDto.UpdateUserDto); // UserReadDto

            return _mapper.Map<ReadPatient>(patient);
        }

        async Task<ReadPatient?> IPatientService.ReactivatePatientAsync(int id)
        {
            var isReactivated =  await _repo.ReactivatePatient(id);
            if (!isReactivated) return null;
            return await ((IPatientService)this).GetPatient(id);
        }

        async Task<ReadPatient?> IPatientService.DeactivatePatientAsync(int id)
        {
            var isDeactivated = await _repo.DeactivatePatient(id);
            if (!isDeactivated) return null;
            return await ((IPatientService)this).GetPatient(id);
        }


        // ################  DELETE   ################ /
        async Task<ReadPatient?> IPatientService.DeletePatientAsync(int id)
        {
            var patient = await _repo.GetPatient(id);
            await _repo.DeletePatient(id);
            return _mapper.Map<ReadPatient>(patient);
        }

        // ################  IS ??   ################ /

        async Task<bool> IPatientService.IsActiveAsync(int id)
        {
            return await _repo.IsActiveAsync(id);
        }

        async Task<bool> IPatientService.IsAdultAsync(int id)
        {
            var patient = await _repo.GetPatient(id);
            if(patient == null) throw new Exception();

            var user = await _repoUser.GetUserByIdAsync(patient.UserId);
            if (user == null) throw new Exception();

            if (user.Age > 18) return true;

            return false;
        }


        // ################  SEARCH   ################ //
        private static int CalculateAge(DateOnly dateOfBirth)
        {
            var today = DateOnly.FromDateTime(DateTime.Now);
            int age = today.Year - dateOfBirth.Year;
            if (today < dateOfBirth.AddYears(age))
                age--;
            return age;
        }

        async Task<IEnumerable<ReadPatient?>> IPatientService.SearchByAgeAsync(int minAge, int maxAge)
        {
            var patients = await _repo.GetAll();

            var filtered = patients
                .Where(p =>
                {
                    int age = CalculateAge(p.User.DateOfBirth);
                    return age >= minAge && age <= maxAge;
                });

            return _mapper.Map<IEnumerable<ReadPatient>>(filtered);
        }

        async Task<IEnumerable<ReadPatient?>> IPatientService.SearchByBloodTypeAsync(string bloodType)
        {
            var patients = await _repo.SearchByBloodType(bloodType);
            return _mapper.Map<IEnumerable<ReadPatient>>(patients);
        }

        async Task<IEnumerable<ReadPatient?>> IPatientService.SearchByGenderAsync(string gender)
        {
            var patients = await _repo.SearchByGender(gender);
            return _mapper.Map<IEnumerable<ReadPatient>>(patients);
        }

        async Task<IEnumerable<ReadPatient?>> IPatientService.SearchByNameAsync(string name)
        {
            var patients = await _repo.SearchByName(name);
            return _mapper.Map<IEnumerable<ReadPatient>>(patients);
        }

        
    }
}
