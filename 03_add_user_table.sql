CREATE TABLE [User](
	[Id] [uniqueidentifier] NOT NULL PRIMARY KEY,
	[FirstName] [nvarchar](100) NOT NULL,
	[LastName] [nvarchar](100) NOT NULL,
	[CityId] [uniqueidentifier] NOT NULL
)

ALTER TABLE [User] ADD  CONSTRAINT [FK_User_City] FOREIGN KEY([CityId])
REFERENCES [City] ([Id])