create procedure [dbo].[GetLeadByParameters]
	@Id integer = null,
	@Name varchar = null,
    @LastName varchar = null,
    @Birthdate date = "0001-01-01",
	@Email varchar = null,
	@Phone varchar = null,
	@Role integer = null,
	@IsBanned integer = null
as
	select L.Id, L.[Name], L.LastName, L.BirthDate, L.Email, L.Phone, L.[Role], L.IsBanned from [dbo].Leads as L
	where (@Id is null or @Id is not null and L.Id = @Id)
	and (@Name is null or @Name is not null and L.[Name] = @Name)
	and (@LastName is null or @LastName is not null and L.LastName = @LastName)
	and (@Birthdate is null or @Birthdate is not null and L.Birthdate = @Birthdate)
	and (@Email is null or @Email is not null and L.Email = @Email)
	and (@Phone is null or @Phone is not null and L.Phone = @Phone)
	and (@Role is null or @Role is not null and L.[Role] = @Role)
	and (@IsBanned is null or @IsBanned is not null and L.IsBanned = @IsBanned)