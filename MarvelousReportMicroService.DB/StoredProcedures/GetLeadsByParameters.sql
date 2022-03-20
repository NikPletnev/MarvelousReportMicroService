create procedure [dbo].[GetLeadsByParameters]
	@Id integer = null,
	@NameParam varchar(30) = null,
	@LastNameParam varchar(30) = null,
    @StartBirthdate date = null,
    @EndBirthdate date = null,
	@EmailParam varchar(30) = null,
	@PhoneParam varchar(30) = null,
	@Role integer = null,
	@IsBanned integer = null
as
	select L.[ExternalId] as Id, L.[Name], L.LastName, L.BirthDate, L.Email, L.Phone, L.[Role], L.IsBanned from [dbo].[Lead] as L
	where (@Id is null or L.ExternalId = @Id)

	and (@NameParam is null or L.[Name] like @NameParam)
	and (@LastNameParam is null or L.LastName like @LastNameParam)

	and (@StartBirthdate is null or L.BirthDate >= @StartBirthdate)
	and (@EndBirthdate is null or L.BirthDate <= @EndBirthdate)

	and (@EmailParam is null or L.Email like @EmailParam)

	and (@PhoneParam is null or L.Phone like @PhoneParam)
	and (@Role is null or  L.[Role] like @Role)
	and (@IsBanned is null or L.IsBanned = @IsBanned)