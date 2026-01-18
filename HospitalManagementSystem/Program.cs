
using HospitalManagementSystem.Data;
using HospitalManagementSystem.Mappers;
using HospitalManagementSystem.Repositories;
using HospitalManagementSystem.Services;
using HospitalManagementSystem.Services.ContactMessage;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            //builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<HospitalDbContext>(options =>
            options.UseSqlServer(
                builder.Configuration.GetConnectionString("hospitalConn")
                )
            );

            builder.Services.AddAutoMapper(
                typeof(UserProfile),
                typeof(NurseProfile),
                typeof(NurseDoctorProfile),
                typeof(RoomProfile),
                typeof(AppointmentProfile),
                typeof(DepartmentProfile),
                typeof(DoctorProfile),
                typeof(MedicalRecordProfile),
                typeof(ReceptionistProfile),
                typeof(PatientProfile),
                typeof(ContactMessageProfile),
                typeof(NursePatientProfile),
                typeof(BillingProfile)

            );

            builder.Services.AddScoped<IPationtRepository, PatientRepository>();
            builder.Services.AddScoped<IPatientService, PatientService>();

            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IUserService, UserService>();

            builder.Services.AddScoped<IContactMessageRepository, ContactMessageRepository>();
            builder.Services.AddScoped<IContactMessageService, ContactMessageService>();

            builder.Services.AddScoped<INursePatientRepository, NursePatientRepository>();
            builder.Services.AddScoped<INursePatientService, NursePatientService>();

            builder.Services.AddScoped<IBillingRepository, BillingRepository>();
            builder.Services.AddScoped<IBillingService, BillingService>();


            var app = builder.Build();
            

            // Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            //{
            //    app.UseSwagger();
            //    app.UseSwaggerUI();
            //}

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();


            app.Run();
        }
    }
}

