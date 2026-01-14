create database Hospital

use Hospital

Create Table [Role](
	RoleId int Identity(1,1) primary key,
	RoleType nvarchar(20) not null
)

Create table Department(
	DepartmentId int Identity(1,1) primary key,
	DepartmentName nvarchar(50) not null,
	DepartmentDescription nvarchar(200)
)

Create table Specialization(
	SpecializationId int Identity(1,1) primary key,
	SpecializationName varchar(50) not null,
)

Create table PaymentStatus(
	PaymentStatusId int Identity(1,1) primary key,
	StatusName varchar(20) not null Unique
)

Create table AppointmentStatus(
	AppointmentStatusId int identity(1,1) primary key,
	[Status] nvarchar(20) Not null, -- Pending, Confirmed, Completed, Cancelled, No-Show
)

Create table RoomStatus(
	RoomStatusId int identity(1,1) primary key,
	[Status] nvarchar(20) Not null, -- Available, Occupied, Maintenance
)

Create table [User](
	UserId int Identity(1,1) primary key,
	PasswordHash nvarchar(255) not null,
	Email nvarchar(50) unique not null,
	CreatedAt DateTime default Getdate(),
	FirstName nvarchar(50) not null,
	LastName nvarchar(50),
	DateOfBirth Date not null,
	Gender Bit NOT NULL,  -- 0 (Female), 1(Male)
	Phone nvarchar(20) NOT NULL,
	RoleId int not  null,
	IsActive BIT NOT NULL DEFAULT 1,
	Constraint FK_USER_ROLE Foreign key (RoleId) References [Role](RoleId)
)


Create table Receptionist(
	ReceptionistId int Identity(1,1) primary key,
	ReceptionistShift Date not null,
	UserId int not null Unique, 
	IsActive BIT NOT NULL DEFAULT 1
	Constraint FK_RECEPTIONIST_USER Foreign key (UserId) References [User](UserId)
)

Create table Nurse(
	NurseId int Identity(1,1) primary key,
	UserId int not null Unique,
	DepartmentId int not null,
	IsActive BIT NOT NULL DEFAULT 1,
	Constraint FK_NURSE_USER Foreign key (UserId) References [User](UserId),
	Constraint FK_NURSE_DEPARTMENT Foreign key (DepartmentId) References Department(DepartmentId)
)

Create table Doctor(
	DoctorId int Identity(1,1) primary key,
	YearsOfExperience int default(0),
	UserId int not null Unique,
	[Certificate] varchar(50),
	SpecializationId int not null,
	LicenseNumber NVARCHAR(255) NOT NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    ConsultationFee DECIMAL(10,2) NOT NULL,
	Constraint FK_DOCTOR_USER Foreign key (UserId) References [User](UserId),
	Constraint FK_SPECIALIZATION_USER Foreign key (SpecializationId) References Specialization(SpecializationId),
)

Create table Patient(
	PatientId int Identity(1,1) primary key,
	BloodType nvarchar(20) not null,
	UserId int not null Unique, 
	IsActive BIT NOT NULL DEFAULT 1,
	DeactivationReason nvarchar(255),
	Constraint FK_PATIENT_USER Foreign key (UserId) References [User](UserId)
)

Create Table MedicalRecords(
	MedicalId int Identity(1,1) primary key,	
	Diagnosis nvarchar(50) not null,
	Treatment nvarchar(50) not null,
	Notes nvarchar(50) not null,
	RecordDate Date default getDate(),
	PatientId int not null,
	DoctorId int not null,
	Constraint FK_MEDICAL_PATIENT Foreign key (PatientId) References Patient(PatientId),
	Constraint FK_MEDICAL_DOCTOR Foreign key (DoctorId) References Doctor(DoctorId),
)

Create table Appointments(
	AppointmentsId int Identity(1,1) primary key,
	AppointmentDate DateTime default getdate() not null,
	AppointmentStatusId int not null,
	MedicalId int not null,
	DoctorId int not null,
	PatientId int not null,

	Constraint FK_APPOINTMENTS_APPOINTMENT Foreign key (AppointmentStatusId) References AppointmentStatus(AppointmentStatusId),
	Constraint FK_APPOINTMENTS_MEDICAL Foreign key (MedicalId) references MedicalRecords(MedicalId),
	Constraint FK_APPOINTMENTS_Doctor Foreign key (DoctorId) References Doctor(DoctorId),
	Constraint FK_APPOINTMENTS_PATIENT Foreign key (PatientId) References Patient(PatientId),
)

Create table EmergencyContact(
	EmergencyContactId int Identity(1,1) primary key,
	Contact nvarchar(20) not null,
	PatientId int not null, 
	Constraint FK_EMERGENCY_PATIENT Foreign key (PatientId) References Patient(PatientId)
)

Create table Billing(
	BillingId int Identity(1,1) primary key,
	TotalAmount decimal(10,2) not null,
	BillDate Date default getDate(),
	PaymentStatusId int not null,
	PatientId int not null, 
	Constraint FK_BILLING_STATUS Foreign key (PaymentStatusId) References PaymentStatus(PaymentStatusId),
	Constraint FK_BILLING_PATIENT Foreign key (PatientId) References Patient(PatientId)
)


Create table NurseDoctor(
	NurseDoctorId int Identity(1,1) primary key,
	NurseDoctorShift DateTime default GetDate(),
	Notes nvarchar(255),
	NurseId int not null,
	DoctorId int not null,
	Constraint FK_NURSEDOCTOR_NURSE Foreign key (NurseId) References Nurse(NurseId),
	Constraint FK_NURSEDOCTOR_Doctor Foreign key (DoctorId) References Doctor(DoctorId)
)

Create table NursePatient(
	NursePatientId int Identity(1,1) primary key,
	NursePatientShift DateTime default GetDate(),
	Notes nvarchar(255),
	NurseId int not null,
	PatientId int not null,
	Constraint FK_NURSEPATIENT_NURSE Foreign key (NurseId) References Nurse(NurseId),
	Constraint FK_NURSEPATIENT_PATIENT Foreign key (PatientId) References Patient(PatientId)
)

Create Table [Messages](
	MessageId int Identity(1,1) primary key,
	MessageDescription Nvarchar(200) not null,
	PatientId int not null,
	CreatedAt DateTime default Getdate(),
	Constraint FK_CONTACTMESSAGE_PATIENT Foreign key (PatientId) references Patient(PatientId),
)

Create Table Room(
	RoomId int Identity(1,1) primary key,
	RoomNumber Nvarchar(20) not null,
	DepartmentId int not null,
	RoomStatusId int not null,
	Constraint FK_ROOM_DEPARTMENT Foreign key (DepartmentId) references Department(DepartmentId),
	Constraint FK_AROOM_STATUS Foreign key (RoomStatusId) References RoomStatus(RoomStatusId),
)

Create Table Admissions(
	AdmissionsId int Identity(1,1) primary key,	
	AdmissionDate Date default Getdate(),
	DischargeDate Date,
	RoomId int not null,
	PatientId int not null,
	Constraint FK_ADMISSIONS_ROOM Foreign key (RoomId) references Room(RoomId),
	Constraint FK_ADMISSIONS_PATIENT Foreign key (PatientId) references Patient(PatientId),
)

Create Table Prescriptions(
	PrescriptionsId int Identity(1,1) primary key,	
	MedicationName nvarchar(50) not null,
	Dosage nvarchar(50) not null,
	StartDate Date,
	EndDate Date,
	MedicalId int not null,
	Constraint FK_PRESCRIPTIONS_MEDICAL Foreign key (MedicalId) references MedicalRecords(MedicalId),
)