
using HospitalManagementSystem.Data;
using HospitalManagementSystem.Data.Mappers;
using HospitalManagementSystem.Mapping;
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
                typeof(ReceptionistProfile)
            );


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

