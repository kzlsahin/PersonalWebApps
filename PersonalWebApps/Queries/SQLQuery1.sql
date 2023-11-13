UPDATE dbo.AspNetUsers
Set UserName='senturk.device1@gmail.com', Email='senturk.device1@gmail.com'
Where EmailConfirmed=0;
Select * From dbo.AspNetUsers;