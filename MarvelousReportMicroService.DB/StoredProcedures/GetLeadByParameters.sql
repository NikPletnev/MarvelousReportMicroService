﻿create procedure [dbo].[GetLeadsByParameters]
	@Id integer = null,

	@Name varchar(30) = null,
	@NameParam varchar(30) = null,

    @LastName varchar(30) = null,
	@LastNameParam varchar(30) = null,

    @StartBirthdate date = null,
    @EndBirthdate date = null,

	@Email varchar(30) = null,
	@EmailParam varchar(30) = null,

	@Phone varchar(20) = null,
	@PhoneParam varchar(30) = null,

	@Role integer = null,
	@IsBanned integer = null
as
	select L.Id, L.[Name], L.LastName, L.BirthDate, L.Email, L.Phone, L.[Role], L.IsBanned from [dbo].[Lead] as L
	where (@Id is null or @Id is not null and L.Id = @Id)

	and (@Name is null or @Name is not null and L.[Name] like @NameParam)
	and (@LastName is null or @LastName is not null and L.LastName like @LastNameParam)

	and (@StartBirthdate is null or @StartBirthdate is not null and L.BirthDate >= @StartBirthdate)
	and (@EndBirthdate is null or @EndBirthdate is not null and L.BirthDate <= @EndBirthdate)

	and (@Email is null or @Email is not null and L.Email like @EmailParam)

	and (@Phone is null or @Phone is not null and L.Phone like @PhoneParam)
	and (@Role is null or @Role is not null and L.[Role] like @Role)
	and (@IsBanned is null or @IsBanned is not null and L.IsBanned = @IsBanned)