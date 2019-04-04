INSERT INTO dbo.TDepartments(Id, TenanId, IsDelete, ParentDepartmentId, DepartmentName, Remark)
VALUES(NEWID(), -- Id - uniqueidentifier
'8CAB1C61-A36B-4910-B8EC-7351EA11BA71', -- TenanId - uniqueidentifier
'00000000-0000-0000-0000-000000000000' , -- IsDelete - bit
'00000000-0000-0000-0000-000000000000'   , -- ParentDepartmentId - int
N'新城通' , -- DepartmentName - nvarchar(100)
N'' -- Remark - nvarchar(500)
    )

	SELECT NEWID()

SELECT * FROM 	TDepartments
SELECT * FROM  TDrivers
SELECT * FROM TVehicles
INSERT INTO dbo.TDrivers(Id, TenanId, IsDelete, DepartmentId, Name, Sex, Age, PhoneNum, MobileNum, Address, DrivingLicNum, IssueDate, ExpirationDate, PermittedType,  Remark, Status, Image)
VALUES(NEWID(), -- Id - uniqueidentifier
'8CAB1C61-A36B-4910-B8EC-7351EA11BA71', -- TenanId - uniqueidentifier
'00000000-0000-0000-0000-000000000000' , -- IsDelete - bit
'D92BA7EF-8930-452D-A003-E80B155929CD', -- DepartmentId - uniqueidentifier
N'wangjf' , -- Name - nvarchar(50)
0   , -- Sex - int
20   , -- Age - int
N'110' , -- PhoneNum - nvarchar(20)
N'119' , -- MobileNum - nvarchar(20)
N'普安新村' , -- Address - nvarchar(100)
N'423r31314' , -- DrivingLicNum - nvarchar(50)
SYSDATETIME(), -- IssueDate - datetime2(7)
SYSDATETIME(), -- ExpirationDate - datetime2(7)
N'unkown' , -- PermittedType - nvarchar(30)
N'unkown' , -- Remark - nvarchar(500)
0   , -- Status - int
N'' -- Image - nvarchar(50)
    )

INSERT INTO dbo.TVehicles(Id, TenanId, IsDelete, DriverId, DepartmentId, PlateNumber, VehicleColor, VehicleBrand, VechileType, ApprovedSeating, VIN, EngineNo, PurchasePrice, TerminalNo, InitMileage, TankCapacity, VehicleLicOwner, BookOriginValue, Displacament, VehicleProperties, FACardNum, OilType, CarIcon, LoadWeight, CurbWeight, Remark, CurrentState, Image)
VALUES(NEWID(), -- Id - uniqueidentifier
'8CAB1C61-A36B-4910-B8EC-7351EA11BA71', -- TenanId - uniqueidentifier
'00000000-0000-0000-0000-000000000000' , -- IsDelete - bit
'A45228F2-259C-4204-B1A3-BC31A7F64CD8', -- DriverId - uniqueidentifier
'D92BA7EF-8930-452D-A003-E80B155929CD', -- DepartmentId - uniqueidentifier
N'鄂AX39007' , -- PlateNumber - nvarchar(20)
N'红色' , -- VehicleColor - nvarchar(20)
N'奔驰' , -- VehicleBrand - nvarchar(50)
N'轿车' , -- VechileType - nvarchar(50)
0   , -- ApprovedSeating - int
N'00001' , -- VIN - nvarchar(20)
N'00001' , -- EngineNo - nvarchar(20)
10   , -- PurchasePrice - int
N'10004x6' , -- TerminalNo - nvarchar(30)
0   , -- InitMileage - int
0   , -- TankCapacity - int
N'' , -- VehicleLicOwner - nvarchar(30)
N'' , -- BookOriginValue - nvarchar(20)
N'' , -- Displacament - nvarchar(20)
0   , -- VehicleProperties - int
N'' , -- FACardNum - nvarchar(20)
0   , -- OilType - int
N'' , -- CarIcon - nvarchar(100)
N'' , -- LoadWeight - nvarchar(20)
N'' , -- CurbWeight - nvarchar(20)
N'' , -- Remark - nvarchar(500)
0   , -- CurrentState - int
N'' -- Image - nvarchar(100)
    )


    --{
    --  "id": "a45228f2-259c-4204-b1a3-bc31a7f64cd8",
    --  "departmentId": "d92ba7ef-8930-452d-a003-e80b155929cd",
    --  "departmentName": null,
    --  "name": "wangjf",
    --  "sex": 0,
    --  "age": 20,
    --  "phoneNum": "110",
    --  "mobileNum": "119",
    --  "address": "普安新村",
    --  "drivingLicNum": "423r31314",
    --  "issueDate": "2019-03-09T17:15:03.2681419",
    --  "expirationDate": "2019-03-09T17:15:03.2681419",
    --  "permittedType": "unkown",
    --  "drivingLicType": "unkown",
    --  "remark": "unkown",
    --  "status": 0,
    --  "image": ""
    --}