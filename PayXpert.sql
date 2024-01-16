if exists (select * from sys.databases where name='PayXpert')
begin
drop database PayXpert
end
go

create database PayXpert

use PayXpert

CREATE TABLE Employee (
    EmployeeID INTEGER IDENTITY(1, 1) PRIMARY KEY,
    FirstName VARCHAR(255) NOT NULL,
    LastName VARCHAR(255) NOT NULL,
    DateOfBirth DATE NOT NULL,
    Gender VARCHAR(10) NOT NULL,
    Email VARCHAR(255) NOT NULL,
    PhoneNumber VARCHAR(100) NOT NULL,
    Address VARCHAR(255) NOT NULL,
    Designation VARCHAR(255) NOT NULL,
    JoiningDate DATE NOT NULL,
    TerminationDate DATE
)

CREATE TABLE Payroll (
    PayrollID INTEGER IDENTITY(1, 1) PRIMARY KEY,
    EmployeeID INTEGER FOREIGN KEY REFERENCES Employee(EmployeeID) ON DELETE CASCADE,
    PayPeriodStartDate DATE NOT NULL,
    PayPeriodEndDate DATE NOT NULL,
    BasicSalary DECIMAL NOT NULL,
    OvertimePay DECIMAL NOT NULL,
    Deductions DECIMAL NOT NULL,
	NetSalary DECIMAL NOT NULL
)

CREATE TABLE Tax (
    TaxID INTEGER IDENTITY(1, 1) PRIMARY KEY,
    EmployeeID INTEGER FOREIGN KEY REFERENCES Employee(EmployeeID)  ON DELETE CASCADE,
    TaxYear INTEGER CHECK (TaxYear between 2019 and 2023 or TaxYear = NULL),
    TaxableIncome DECIMAL,
    TaxAmount DECIMAL
);

CREATE TABLE FinancialRecord (
    RecordID INTEGER IDENTITY(1, 1) PRIMARY KEY,
    EmployeeID INTEGER FOREIGN KEY REFERENCES Employee(EmployeeID)  ON DELETE CASCADE,
    RecordDate INTEGER NOT NULL,
    Description VARCHAR(255) NULL,
    Amount DECIMAL NOT NULL,
    RecordType VARCHAR(255) NOT NULL
);

INSERT INTO Employee (FirstName,LastName,DateOfBirth,Gender,Email,PhoneNumber,Address,Designation,JoiningDate,TerminationDate)
VALUES
  ('Anika','Galloway','1990-02-17','Female','risus.donec@google.couk','633-2844','Ap #290-2166 Turpis Street','Systems Engineer','2019-12-23','2022-10-15'),
  ('Zenaida','Hawkins','1999-10-29','Female','tincidunt.tempus@outlook.edu','772-2157','Ap #195-8307 Nulla St.','HR','2019-01-30',NULL),
  ('Karen','Barber','1999-08-03','Others','natoque.penatibus@protonmail.com','183-8935','482-5887 Donec Rd.','Systems Analyst','2020-10-21',NULL),
  ('Shoshana','Summers','1991-12-03','Female','dis.parturient.montes@aol.net','1-719-433-1335','P.O. Box 420, 3564 Lacinia Rd.','Technical Support Engineer','2021-05-04',NULL),
  ('Sonia','Cline','1991-09-27','Female','varius@protonmail.ca','482-1672','725-4522 A Ave','Java Developer','2023-04-26','2023-11-14'),
  ('Adrian','Armstrong','1999-05-21','Male','accumsan.neque@aol.couk','1-461-134-7586','997-306 Sollicitudin Ave','Project Manager','2020-02-28',NULL),
  ('Claudia','Trujillo','1990-06-21','Female','etiam.gravida@aol.com','1-832-674-1097','7235 Proin Road','UI/UX Designer','2022-05-31',NULL),
  ('Britanney','Daniel','2001-01-16','Male','egestas.blandit.nam@aol.org','1-868-521-4612','P.O. Box 198, 3786 Aliquam St.','Web Developer','2019-11-13',NULL),
  ('Regan','Sanchez','2000-01-26','Female','praesent.luctus@protonmail.ca','1-226-465-5118','546-3729 Fusce Street','Database Administrator','2020-11-19',NULL),
  ('Brooke','Norton','1997-01-12','Male','tellus.suspendisse@google.couk','1-375-587-6014','2030 Facilisis St.','Data Scientist','2023-10-22',NULL),
  ('Velma','Terry','2000-11-07','Female','integer@icloud.org','1-548-984-8434','354-7089 Interdum Ave','Project Manager','2022-06-24',NULL),
  ('Harding','Soto','1992-01-28','Others','non.lorem@protonmail.edu','1-478-385-2742','582-2214 Proin Av.','Data Scientist','2018-06-16',NULL),
  ('Tyler','Griffin','1992-06-21','Male','fringilla.donec@icloud.org','1-859-685-7156','Ap #463-8740 Magna. Rd.','Database Administrator','2019-06-01',NULL),
  ('Ashely','Dalton','1996-06-14','Female','commodo.ipsum@yahoo.org','539-4853','698-6893 Elementum St.','UI/UX Designer','2020-03-05',NULL),
  ('Kim','Cardenas','1998-11-03','Male','arcu.vestibulum@google.ca','1-404-329-6312','894-1181 Integer St.','DevOps Engineer','2021-11-03','2022-12-13'),
  ('Michael','Sellers','2000-10-09','Male','nunc.quisque@outlook.com','818-0812','8308 Arcu. Street','.Net Developer','2020-05-20',NULL),
  ('Richard','Ochoa','1995-10-26','Female','orci.ut@hotmail.org','1-632-623-9763','P.O. Box 511, 8501 Vel Street','Database Administrator','2020-10-01',NULL),
  ('Elaine','Romero','1991-09-14','Female','adipiscing.elit@outlook.org','1-411-151-1698','625-1882 Iaculis Rd.','UI/UX Designer','2020-09-23',NULL),
  ('Helen','William','2001-02-14','Male','convallis.convallis.dolor@google.net','405-2724','362-1494 Senectus Rd.','Java Developer','2018-04-01',NULL),
  ('Ivory','O''connor','1999-01-12','Male','vel@icloud.org','1-454-842-7656','3259 Curabitur St.','Database Administrator','2018-05-09','2022-03-25'),
  ('Bethany','Erickson','1993-12-16','Female','enim@icloud.edu','1-711-866-6425','Ap #805-956 Scelerisque Street','UI/UX Designer','2020-01-07',NULL),
  ('Danielle','Pitts','1991-06-11','Others','risus.quis.diam@hotmail.org','892-9652','P.O. Box 299, 3625 A, Avenue','ELK Engineer','2021-11-14',NULL),
  ('Yvonne','Mccarty','1990-09-17','Female','et@aol.edu','1-489-770-6024','P.O. Box 446, 2304 Sit Street','Software Developer','2019-06-13',NULL),
  ('Aladdin','Nunez','1993-01-26','Male','semper.nam@google.edu','1-515-610-5158','569-9020 Ut, St.','Data Scientist','2020-12-07',NULL),
  ('Medge','Montgomery','1997-06-10','Others','sagittis.felis.donec@aol.couk','470-8698','779-7774 Et Ave','Software Developer','2019-02-11','2020-01-22'),
  ('Wing','Sweet','1994-05-21','Female','sed.nunc@aol.edu','1-953-357-1224','948-9123 Donec Av.','Systems Analyst','2022-03-28','2023-03-28'),
  ('Thaddeus','Flores','2000-07-01','Male','purus.duis@outlook.net','263-6419','Ap #176-4381 Sagittis Rd.','Network Administrator','2020-10-28',NULL),
  ('Zephr','Cabrera','1992-11-23','Male','aenean@yahoo.com','265-7522','P.O. Box 990, 7888 Ante Avenue','Database Administrator','2022-07-25',NULL),
  ('Abigail','Shaw','1994-04-14','Male','odio.semper.cursus@aol.couk','147-6974','959-6537 Massa St.','Systems Engineer','2020-02-22',NULL),
  ('Natalie','Bowen','1999-08-23','Male','per.inceptos@aol.com','478-7312','P.O. Box 852, 3680 Metus. Avenue','DevOps Engineer','2020-05-15',NULL)

INSERT INTO Payroll (EmployeeID,PayPeriodStartDate,PayPeriodEndDate,BasicSalary,OvertimePay,Deductions,NetSalary)
VALUES
  (9,'2020-08-04','2022-06-25',90654,5759,476,95937),
  (10,'2021-04-09','2022-09-09',45297,6419,2826,48890),
  (1,'2021-04-07','2022-02-16',26132,7541,2515,31158),
  (2,'2020-12-23','2021-04-07',75847,6969,2537,80279),
  (7,'2021-01-26','2022-04-10',97352,3114,1252,99214),
  (8,'2020-05-31','2023-01-25',84208,140,2073,82275),
  (3,'2021-11-05','2023-12-04',93188,4632,2007,95813),
  (4,'2022-09-04','2023-11-19',63743,8708,2768,69683),
  (6,'2021-05-25','2022-09-11',60662,3363,111,63914),
  (5,'2020-01-17','2021-09-07',93376,8865,1204,101037),
  (11,'2021-10-05','2022-10-24',25926,6715,2481,30160),
  (19,'2020-03-27','2022-05-04',82085,9724,472,91337),
  (12,'2020-04-08','2021-09-09',65275,4579,1840,68014),
  (18,'2022-10-08','2023-12-21',90359,5040,1531,93868),
  (13,'2021-01-22','2022-05-02',98846,3055,1732,100169),
  (17,'2022-06-16','2023-09-08',95883,7553,232,103204),
  (14,'2020-08-24','2021-01-22',23935,4413,737,27611),
  (16,'2021-08-30','2022-12-29',45327,7431,955,51803),
  (20,'2021-05-11','2022-09-14',43292,9962,2548,50706),
  (15,'2022-01-15','2023-03-25',49540,8673,1516,56697),
  (25,'2022-09-20','2023-08-20',82622,3771,1958,84435),
  (21,'2019-02-01','2022-01-10',49623,9352,391,58584),
  (26,'2020-06-20','2022-08-26',73605,127,2521,71211),
  (22,'2021-10-07','2023-11-29',25769,7301,956,32114),
  (27,'2020-10-16','2022-03-04',68480,7767,1723,74524),
  (23,'2022-02-28','2023-02-09',58387,4392,1178,61601),
  (28,'2020-10-16','2022-04-15',66424,3882,410,69896),
  (24,'2020-07-18','2023-01-23',38991,7059,2773,43277),
  (30,'2018-05-27','2020-08-26',81953,5536,493,86996),
  (29,'2021-11-21','2023-09-03',72248,629,1162,71715)

INSERT INTO Tax (EmployeeID,TaxYear,TaxableIncome,TaxAmount)
VALUES
  (25,2019,1047700,102909),
  (1,2022,837864,92233),
  (26,2020,1144804,84252),
  (2,2022,979155,167344),
  (27,2022,1099165,25853),
  (3,2020,854264,129473),
  (28,2022,768667,111127),
  (4,2022,944623,170266),
  (29,2021,1040382,29785),
  (5,2023,1144403,27574),
  (30,2020,1266763,64600),
  (6,2019,1276381,99628),
  (11,2022,1153606,91958),
  (7,2022,1044763,94460),
  (12,2023,1230945,130994),
  (8,2020,1041748,141977),
  (13,2021,922641,97721),
  (9,2023,957272,45985),
  (14,2022,988832,129290),
  (10,2022,1258132,166943),
  (15,2019,1047700,102909),
  (21,2022,837864,92233),
  (16,2020,1144804,84252),
  (22,2022,979155,167344),
  (17,2022,1099165,25853),
  (23,2020,854264,129473),
  (18,2022,768667,111127),
  (24,2022,944623,170266),
  (19,2021,1040382,29785),
  (20,2023,1144403,27574)

INSERT INTO FinancialRecord (EmployeeID,RecordDate,Description,Amount,RecordType)
VALUES
  (5,2019,'Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Curabitur sed',5645,'Tax Payment'),
  (1,2022,'Lorem ipsum dolor sit amet, consectetuer adipiscing elit.',8962,'Tax Payment'),
  (6,2020,'Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Curabitur sed',3303,'Tax Payment'),
  (2,2022,'Lorem ipsum dolor sit',12731,'Tax Payment'),
  (7,2022,'Lorem ipsum dolor sit amet, consectetuer adipiscing elit.',12134,'Tax Payment'),
  (3,2020,'Lorem ipsum dolor sit amet, consectetuer adipiscing elit.',8635,'Expense'),
  (8,2022,'Lorem ipsum dolor',10239,'Tax Payment'),
  (4,2022,'Lorem ipsum dolor sit amet, consectetuer adipiscing',12264,'Tax Payment'),
  (9,2021,'Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Curabitur',8865,'Tax Payment'),
  (10,2023,'Lorem ipsum dolor',12955,'Expense'),
  (30,2020,'Lorem ipsum dolor sit amet,',9008,'Expense'),
  (21,2019,'Lorem ipsum dolor sit amet,',8835,'Income'),
  (29,2022,'Lorem ipsum dolor sit',2837,'Expense'),
  (22,2022,'Lorem ipsum dolor sit amet, consectetuer adipiscing',14567,'Expense'),
  (28,2023,'Lorem ipsum dolor',10227,'Expense'),
  (23,2020,'Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Curabitur',7898,'Expense'),
  (27,2021,'Lorem ipsum dolor sit amet, consectetuer adipiscing elit.',10133,'Income'),
  (24,2023,'Lorem ipsum dolor sit amet, consectetuer',13851,'Expense'),
  (26,2022,'Lorem ipsum dolor sit amet, consectetuer adipiscing',8009,'Expense'),
  (25,2022,'Lorem ipsum dolor sit amet,',11987,'Tax Payment'),
  (11,2019,'Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Curabitur sed',5645,'Tax Payment'),
  (20,2022,'Lorem ipsum dolor sit amet, consectetuer adipiscing elit.',8962,'Tax Payment'),
  (12,2020,'Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Curabitur sed',3303,'Tax Payment'),
  (19,2022,'Lorem ipsum dolor sit',12731,'Tax Payment'),
  (13,2022,'Lorem ipsum dolor sit amet, consectetuer adipiscing elit.',12134,'Tax Payment'),
  (18,2020,'Lorem ipsum dolor sit amet, consectetuer adipiscing elit.',8635,'Expense'),
  (14,2022,'Lorem ipsum dolor',10239,'Tax Payment'),
  (17,2022,'Lorem ipsum dolor sit amet, consectetuer adipiscing',12264,'Tax Payment'),
  (15,2021,'Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Curabitur',8865,'Tax Payment'),
  (16,2023,'Lorem ipsum dolor',12955,'Expense')

	
select * from FinancialRecord

select TaxAmount from Tax where TaxID = 1