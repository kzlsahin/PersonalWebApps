UPDATE dbo.TrackItems
Set IsActive=1
where IsActive=0;
Select * from dbo.TrackItems;