create  database ClubeSportDB
go 

use ClubeSportDB

create table Nationalitys(
nationalityID int primary key ,
name nvarchar  (20),
)

create table Peoples(
personID int identity(1,1) primary key,
fristName nvarchar(20) not null,
secondName nvarchar(20) not null,
thirdName nvarchar(20) not null,
familyName nvarchar(20) not null,
brithday Datetime not null,
gender bit not null,
nationalityID int references Nationalitys(nationalityID),
address nvarchar(200) not null,
phone nvarchar(15) not null,
)

create table Employees(
employeeID int identity(1,1) primary key,
personID int not null references Peoples(personID),
userName nvarchar(100) not null ,
password nvarchar(max) not null,
createdDate Datetime default getDate(),
isActive bit not null default 0,
addBy int references Employees(employeeID))


create table Sports(
sportID int identity(1,1) primary key,
name nvarchar(50) not null,
createdDate datetime default GETDATE(),
isActive bit default 1,
addBy  int references Employees(employeeID)
)




create table Members(
memberID int identity(1,1) primary key,
personID int references Peoples(personID),
isActive bit default(1),
addBy  int references Employees(employeeID)
)


create table Coaches(
coacheID int identity(1,1) primary key,
personID int references Peoples(personID),
startTraingDate Datetime default getDate(),
endTraingDate Datetime null,
personalImage nvarchar(max)not null,
isActive bit default 1,
addBy  int references Employees(employeeID)
)

create table CoachesTraingings(
CoachsTraingingID int identity(1,1)primary key,
sportID int not null references Sports(sportID),
coacheID int not null references Coaches(coacheID),
isAvaliable bit default 1,
dayilyStartAt nvarchar(10) not null,
dayilyEndAt nvarchar(10) not null,
trainingDay int null,
fee money not null,
addBy  int references Employees(employeeID)
) 

create table MemberSubscriptions(
memberSubscriptionID int identity(1,1) primary key,
CoachsTraingingID int not null references CoachesTraingings,
memberID int not null references Members,
fee money not null,
startDate datetime  default getDate(),
endDate datetime null,
addBy  int references Employees(employeeID))



GO

create view  Coaches_view as 
SELECT c.coacheID, p.personID, CAST(DAY(c.startTraingDate) AS nvarchar) + '-' + CAST(MONTH(c.startTraingDate) AS nvarchar) + '-' + CAST(YEAR(c.startTraingDate) AS nvarchar) + ' ' + CAST(FORMAT(c.startTraingDate, 'hh:mm tt') AS nvarchar) 
                  AS startTraingDate, p.fristName + ' ' + p.secondName + ' ' + p.thirdName + ' ' + p.familyName AS fullName, CASE WHEN p.gender = 1 THEN 'Male' ELSE 'Female' END AS gender, CAST(DAY(p.brithday) AS nvarchar) 
                  + '-' + CAST(MONTH(p.brithday) AS nvarchar) + '-' + CAST(YEAR(p.brithday) AS nvarchar) + ' ' + CAST(FORMAT(p.brithday, 'hh:mm tt') AS nvarchar) AS brithday, n.name AS nationality, p.phone, c.isActive, CAST(DAY(c.startTraingDate) 
                  AS nvarchar) + '-' + CAST(MONTH(c.endTraingDate) AS nvarchar) + '-' + CAST(YEAR(c.endTraingDate) AS nvarchar) + ' ' + CAST(FORMAT(c.endTraingDate, 'hh:mm tt') AS nvarchar) AS endTraingDate,
				  c.addBy
FROM     Peoples AS p INNER JOIN
                  Nationalitys AS n ON p.nationalityID = n.nationalityID INNER JOIN
                  dbo.Coaches AS c ON c.personID = p.personID




GO
create view CoachesTraingView as
SELECT ct.CoachsTraingingID, p.fristName + ' ' + p.secondName + ' ' + p.thirdName + ' ' + p.familyName AS fullName, ct.isAvaliable, s.name AS sportName, ct.dayilyStartAt, ct.dayilyEndAt, CAST(ct.fee AS nvarchar) AS cost
FROM     CoachesTraingings  ct INNER JOIN
                  Coaches  c ON ct.coacheID = c.coacheID INNER JOIN
                  Peoples  p ON p.personID = c.personID INNER JOIN
                  Sports s ON ct.sportID = s.sportID
WHERE  (c.isActive = 1)



GO
create view Employee_view as 
SELECT e.employeeID, p.personID, e.userName, CAST(DAY(e.createdDate) AS nvarchar) + '-' + CAST(MONTH(e.createdDate) AS nvarchar) + '-' + CAST(YEAR(e.createdDate) AS nvarchar) + ' ' + CAST(FORMAT(e.createdDate, 'hh:mm tt') 
                  AS nvarchar) AS createdDate, p.fristName + ' ' + p.secondName + ' ' + p.thirdName + ' ' + p.familyName AS fullName, CASE WHEN p.gender = 1 THEN 'Male' ELSE 'Female' END AS gender, CAST(DAY(p.brithday) AS nvarchar) 
                  + '-' + CAST(MONTH(p.brithday) AS nvarchar) + '-' + CAST(YEAR(p.brithday) AS nvarchar) + ' ' + CAST(FORMAT(p.brithday, 'hh:mm tt') AS nvarchar) AS brithday, n.name AS nationality, p.phone, e.isActive
FROM     Peoples AS p INNER JOIN
                  Nationalitys AS n ON p.nationalityID = n.nationalityID INNER JOIN
                 Employees AS e ON e.personID = p.personID


go

create  view  MemberSubscritptionView as
SELECT ms.memberSubscriptionID AS subscriptionID, s.name AS sportName,
(SELECT 
fristName + ' ' + secondName + ' ' + thirdName + ' ' + familyName AS Expr1 FROM  dbo.Peoples
 WHERE (personID = c.personID)) AS coatchName,
(SELECT fristName + ' ' + secondName + ' ' + thirdName + ' ' + familyName AS Expr1 FROM      dbo.Peoples AS Peoples_1
 WHERE   (personID = m.personID)) AS memberName, 
 CAST(DAY(ms.startDate) AS nvarchar) + '-' + 
CAST(MONTH(ms.startDate) AS nvarchar) + '-' + 
CAST(YEAR(ms.startDate) AS nvarchar) + ' ' + 
CAST(FORMAT(ms.startDate, 'hh:mm tt') AS nvarchar)
  AS startDate, 
   CAST(DAY(ms.endDate) AS nvarchar) + '-' + 
CAST(MONTH(ms.endDate) AS nvarchar) + '-' + 
CAST(YEAR(ms.endDate) AS nvarchar) + ' ' + 
CAST(FORMAT(ms.endDate, 'hh:mm tt') AS nvarchar)
  AS endDate,
 c.coacheID, m.memberID
FROM     MemberSubscriptions AS ms INNER JOIN
                  CoachesTraingings ct ON ms.CoachsTraingingID = ct.CoachsTraingingID INNER JOIN
                  Coaches  c ON c.coacheID = ct.coacheID INNER JOIN
                  Sports  s ON s.sportID = ct.sportID INNER JOIN
                  Members  m ON m.memberID = ms.memberID INNER JOIN
                  Peoples  p ON m.personID = p.personID 





GO
create view member_view as 
select m.memberID ,p.personID,
(p.fristName+' '+p.secondName+' '+p.thirdName+' '+ p.familyName ) as fullName,
case when p.gender = 1 then 'Male' else 'Female' end as gender
,
CAST(DAY(p.brithday) AS nvarchar) + '-' + 
CAST(MONTH(p.brithday) AS nvarchar) + '-' + 
CAST(YEAR(p.brithday) AS nvarchar) + ' ' + 
CAST(FORMAT(p.brithday, 'hh:mm tt') AS nvarchar) AS brithday
,n.name as nationality ,p.phone , m.isActive as isActive
from Peoples p inner join Nationalitys n on p.nationalityID = n.nationalityID
inner join Members m 
on m.personID = p.personID



 