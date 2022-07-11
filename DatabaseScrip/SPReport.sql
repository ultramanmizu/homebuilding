-- =============================================
-- Author:		warin kesorn
-- Create date: 10/07/2022
-- Description:	contract description report
-- =============================================
CREATE PROCEDURE [dbo].[usp_get_contract_description]
	@ContractId nvarchar(250) 
AS
BEGIN

	SELECT [Id]
      ,[ConstructionContractId]
      ,[LineNo]
      ,[Detail]
      ,FORMAT([Quantity], 'N', 'en-us') AS [Quantity] 
      ,[Unit]
      ,FORMAT([UnitPrice], 'N', 'en-us') AS [UnitPrice] 
      ,FORMAT([Total], 'N', 'en-us') AS [Total] 
      ,[Description]
      ,[Sequence]
      ,[IsEnabled]
      ,[IsDeleted]
      ,[CreatedById]
      ,[CreatedDate]
      ,[UpdatedById]
      ,[UpdatedDate]
      ,[DeletedById]
      ,[DeletedDate]
	FROM ContractDescription
	WHERE [ConstructionContractId] = @ContractId
	ORDER BY [LineNo]

END
GO

-- =============================================
-- Author:		warin kesorn
-- Create date: 10/07/2022
-- Description:	summary report
-- =============================================
CREATE PROCEDURE [dbo].[usp_get_contract_receipt]
	@OwnerName nvarchar(250) null,
	@StartDate datetime null,
	@EndDate datetime null 
AS
BEGIN
	IF (@StartDate is null) 	BEGIN	SET @StartDate = cast('1753-1-1' as datetime) END
	IF (@EndDate is null) 	BEGIN	SET @EndDate = GETDATE() END
	
	SELECT c.[Id]
      ,c.[ContractNumber]
      ,FORMAT(c.[ContractDate],'dd/MM/yyyy') AS ContractDate
      ,c.[MadeAt]
      ,c.[OwnerName]
      ,c.[OwnerAddress]
      ,c.[OwnerTel]
      ,c.[OwnerNumber]
      ,c.[ContractorName]
      ,c.[ContractorAddress]
      ,c.[ContractorTel]
      ,c.[ContractorLicenseNumber]
      ,c.[DescriptionOfWork]
      ,c.[WorkSite]
      ,FORMAT(c.ContractValue, 'N', 'en-us') AS ContractValue
	  ,r.[Id] as ReceiptId
      ,r.[Round]
      ,r.[ReceiptNumber]
      ,r.[ReceiptDate]
      ,FORMAT(r.[Total], 'N', 'en-us') AS Total
      ,c.[Description]
      ,c.[Sequence]
      ,c.[IsEnabled]
      ,c.[IsDeleted]
      ,c.[CreatedById]
      ,c.[CreatedDate]
      ,c.[UpdatedById]
      ,c.[UpdatedDate]
      ,c.[DeletedById]
      ,c.[DeletedDate]
	FROM [dbo].[ConstructionContract] c
		left join [dbo].[Receipt] r on c.Id = r.ConstructionContractId
	WHERE c.OwnerName LIKE '%'+ISNULL(@OwnerName,'')+'%' AND c.ContractDate between @StartDate AND @EndDate
	ORDER BY [ContractDate] , [Round]

END
GO

-- =============================================
-- Author:		warin kesorn
-- Create date: 10/07/2022
-- Description:	contract withdraw report
-- =============================================
CREATE PROCEDURE [dbo].[usp_get_contract_withdraw]
	@ContractId nvarchar(250) 
AS
BEGIN
	SELECT [Id]
      ,[ConstructionContractId]
      ,[LineNo]
      ,[Detail]
      ,FORMAT([Percent], 'N', 'en-us') AS [Percent] 
      ,FORMAT([Total], 'N', 'en-us') AS [Total] 
      ,[Description]
      ,[Sequence]
      ,[IsEnabled]
      ,[IsDeleted]
      ,[CreatedById]
      ,[CreatedDate]
      ,[UpdatedById]
      ,[UpdatedDate]
      ,[DeletedById]
      ,[DeletedDate]
	FROM Withdraw
	WHERE [ConstructionContractId] = @ContractId
	ORDER BY [LineNo]

END
GO

-- =============================================
-- Author:		warin kesorn
-- Create date: 10/07/2022
-- Description:	receipt history
-- =============================================
CREATE PROCEDURE [dbo].[usp_get_receipt_history]
	@ReceiptId nvarchar(250) 
AS
BEGIN
	
	DECLARE @round INT , @contractId NVARCHAR(250)
	SELECT @round = [Round] , @contractId = [ConstructionContractId] FROM Receipt WHERE Id = @ReceiptId 

	SELECT [Id]
      ,[ConstructionContractId]
      ,[Round]
      ,[ReceiptNumber]
      ,FORMAT([ReceiptDate],'dd/MM/yyyy') AS [ReceiptDate]
      ,FORMAT([Total], 'N', 'en-us') AS [Total]
      ,[Description]
      ,[Sequence]
      ,[IsEnabled]
      ,[IsDeleted]
      ,[CreatedById]
      ,[CreatedDate]
      ,[UpdatedById]
      ,[UpdatedDate]
      ,[DeletedById]
      ,[DeletedDate]
  FROM [dbo].[Receipt]
  WHERE [ConstructionContractId] = @contractId and [Round] < @round
  ORDER BY [Round] ASC

END
GO

