use Hospital

INSERT INTO [Role] (RoleType) VALUES
('Admin'),
('Receptionist'),
('Doctor'),
('Nurse'),
('Patient');

INSERT INTO Department (DepartmentName, DepartmentDescription) VALUES
('Cardiology', 'Heart-related treatments and care'),
('Neurology', 'Brain and nervous system care'),
('Pediatrics', 'Child healthcare'),
('Orthopedics', 'Bone and muscle treatments'),
('Emergency', 'Immediate care and urgent cases');

INSERT INTO Specialization (SpecializationName) VALUES
('Cardiologist'),
('Neurologist'),
('Pediatrician'),
('Orthopedic Surgeon'),
('General Practitioner');

INSERT INTO PaymentStatus (StatusName) VALUES
('Paid'),
('Pending'),
('Partial'),
('Cancelled');

INSERT INTO AppointmentStatus ([Status]) VALUES
('Pending'),
('Confirmed'),
('Completed'),
('Cancelled'),
('No-Show');

INSERT INTO RoomStatus ([Status]) VALUES
('Available'),
('Occupied'),
('Maintenance');


INSERT INTO [User] (PasswordHash, Email, FirstName, LastName, DateOfBirth, Gender, Phone, RoleId)
VALUES
('hash1', 'admin@example.com', 'Alice', 'Smith', '1980-01-15', 0, '01000000001', 1),  -- Admin
('hash2', 'reception1@example.com', 'Bob', 'Johnson', '1990-05-20', 1, '01000000002', 2),  -- Receptionist
('hash3', 'doctor1@example.com', 'Carol', 'Williams', '1985-03-10', 0, '01000000003', 3),  -- Doctor
('hash4', 'nurse1@example.com', 'David', 'Brown', '1992-07-12', 1, '01000000004', 4),      -- Nurse
('hash5', 'patient1@example.com', 'Eve', 'Davis', '2000-11-25', 0, '01000000005', 5);     -- Patient

INSERT INTO Receptionist (ReceptionistShift, UserId)
VALUES
('2026-01-05', 2);

INSERT INTO Nurse (UserId, DepartmentId)
VALUES
(4, 1);  -- Nurse David in Cardiology

INSERT INTO Doctor (UserId, YearsOfExperience, Certificate, SpecializationId, LicenseNumber, IsActive, ConsultationFee)
VALUES
(3, 10, 'MD Cardiology', 1, 'LIC-2024-001', 1, 500.00);  -- Doctor Carol

INSERT INTO Patient (BloodType, UserId)
VALUES
('A+', 5);  -- Patient Eve

INSERT INTO MedicalRecords (Diagnosis, Treatment, Notes, PatientId, DoctorId)
VALUES
('Hypertension', 'Medication', 'Patient needs follow-up in 1 month', 1, 1),
('Flu', 'Rest and fluids', 'Monitor temperature', 1, 1);

INSERT INTO Room (RoomNumber, DepartmentId, RoomStatusId) VALUES
('101', 1, 1),  -- Cardiology, Available
('102', 1, 2),  -- Cardiology, Occupied
('201', 2, 1),  -- Neurology, Available
('202', 2, 3);  -- Neurology, Maintenance

INSERT INTO Appointments (AppointmentDate, AppointmentStatusId, MedicalId, DoctorId, PatientId) VALUES
('2026-01-05 09:00', 1, 1, 1, 1),
('2026-01-06 10:00', 2, 2, 1, 1);

INSERT INTO EmergencyContact (Contact, PatientId) VALUES
('01012345678', 1),
('01098765432', 1);

INSERT INTO Billing (TotalAmount, BillDate, PaymentStatusId, PatientId) VALUES
(500.00, '2026-01-05', 1, 1),  -- Paid
(200.00, '2026-01-06', 2, 1);  -- Pending

INSERT INTO NurseDoctor (NurseDoctorShift, Notes, NurseId, DoctorId) VALUES
('2026-01-05 08:00', 'Assisting in cardiology', 1, 1);

INSERT INTO NursePatient (NursePatientShift, Notes, NurseId, PatientId) VALUES
('2026-01-05 09:00', 'Regular checkup', 1, 1);

INSERT INTO Messages (MessageDescription, PatientId) VALUES
('I have a fever and headache', 1),
('Need to reschedule my appointment', 1);

INSERT INTO Admissions (AdmissionDate, DischargeDate, RoomId, PatientId) VALUES
('2026-01-05', '2026-01-10', 2, 1),
('2026-01-06', '2026-01-08', 3, 1);

INSERT INTO Prescriptions (MedicationName, Dosage, StartDate, EndDate, MedicalId) VALUES
('Paracetamol', '500mg', '2026-01-05', '2026-01-10', 1),
('Ibuprofen', '200mg', '2026-01-06', '2026-01-08', 2);

