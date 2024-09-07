select * from Employees

select * from Roles


INSERT INTO Employees (FirstName, LastName, Email, Phone, HireDate, Position, DepartmentID, RoleID)
VALUES ('Issa', 'Doe', 'Issa@example.com', '555-1234', '2022-05-01', 'Manager', 1, 1),
       ('Amro', 'Smith', 'Amro@example.com', '555-5678', '2023-07-15', 'Senior Developer', 3, 2),
       ('Samer', 'Johnson', 'Samer@example.com', '555-8765', '2024-01-10', 'Junior Developer', 3, 3),
       ('Shaker', 'Brown', 'Shaker@example.com', '555-4321', '2022-11-20', 'Artist', 2, 4);


       commit


       CREATE TABLE RolesChangeRequest (
    RolesChangeRequestId INT PRIMARY KEY IDENTITY(1,1),
    EmployeeId INT NOT NULL,
    OldRoleId INT NOT NULL,
    NewRoleId INT NOT NULL,
    RequestDate DATE NOT NULL,
    Status VARCHAR(50) NOT NULL,
    Reason VARCHAR(255),
    
    -- Foreign Key Constraints
    FOREIGN KEY (EmployeeId) REFERENCES Employees(EmployeeId),
    FOREIGN KEY (OldRoleId) REFERENCES Roles(RoleId),
    FOREIGN KEY (NewRoleId) REFERENCES Roles(RoleId)
);


commit

select * from RolesChangeRequest

INSERT INTO RolesChangeRequest (EmployeeId, OldRoleId, NewRoleId, RequestDate, Status, Reason)
VALUES 
-- John Doe requesting a change from Manager to Senior Manager
(1, 1, 2, '2024-09-01', 'Pending', 'Seeking more responsibilities'),

-- Jane Smith requesting a change from HR to HR Manager
(2, 3, 4, '2024-08-20', 'Approved', 'Promoted to HR Manager'),

-- Michael Brown requesting a change from Developer to Senior Developer
(3, 4, 1, '2024-07-15', 'Rejected', 'Not enough experience yet');



commit
