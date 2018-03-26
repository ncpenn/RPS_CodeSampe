CREATE TABLE [dbo].[Policy]
(
	[PolicyId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [PolicyNumber] VARCHAR(50) NOT NULL, 
    [EffectiveDate] DATETIME2 NOT NULL, 
    [ExpirationDate] DATETIME2 NOT NULL, 
    [PrimaryInsuredId] UNIQUEIDENTIFIER NOT NULL, 
    [RiskInsuredId] UNIQUEIDENTIFIER NOT NULL,
	CONSTRAINT constraint_PrimaryInsuredIdID UNIQUE(PrimaryInsuredId),
	CONSTRAINT constraint_RiskInsuredIdID UNIQUE(RiskInsuredId)
)
