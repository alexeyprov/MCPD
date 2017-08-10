﻿CREATE PROCEDURE [HumanResources].[uspUpdateEmployeeHireInfo]
@EmployeeID [int], 
@Title [nvarchar](50), 
@HireDate [datetime], 
@RateChangeDate [datetime], 
@Rate [money], 
@PayFrequency [tinyint], 
@CurrentFlag [dbo].[Flag] 
WITH EXECUTE AS CALLER
AS
BEGIN
SET NOCOUNT ON;
BEGIN TRY
BEGIN TRANSACTION;
UPDATE [HumanResources].[Employee] 
SET [Title] = @Title 
,[HireDate] = @HireDate 
,[CurrentFlag] = @CurrentFlag 
WHERE [EmployeeID] = @EmployeeID;
INSERT INTO [HumanResources].[EmployeePayHistory] 
([EmployeeID]
,[RateChangeDate]
,[Rate]
,[PayFrequency]) 
VALUES (@EmployeeID, @RateChangeDate, @Rate, @PayFrequency);
COMMIT TRANSACTION;
END TRY
BEGIN CATCH
IF @@TRANCOUNT > 0
BEGIN
ROLLBACK TRANSACTION;
END
EXECUTE [dbo].[uspLogError];
END CATCH;
END;
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Input parameter for the stored procedure uspUpdateEmployeeHireInfo. Enter the current flag for the employee.', @level0type = N'SCHEMA', @level0name = N'HumanResources', @level1type = N'PROCEDURE', @level1name = N'uspUpdateEmployeeHireInfo', @level2type = N'PARAMETER', @level2name = N'@CurrentFlag';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Input parameter for the stored procedure uspUpdateEmployeeHireInfo. Enter the pay frequency for the employee.', @level0type = N'SCHEMA', @level0name = N'HumanResources', @level1type = N'PROCEDURE', @level1name = N'uspUpdateEmployeeHireInfo', @level2type = N'PARAMETER', @level2name = N'@PayFrequency';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Input parameter for the stored procedure uspUpdateEmployeeHireInfo. Enter the new rate for the employee.', @level0type = N'SCHEMA', @level0name = N'HumanResources', @level1type = N'PROCEDURE', @level1name = N'uspUpdateEmployeeHireInfo', @level2type = N'PARAMETER', @level2name = N'@Rate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Input parameter for the stored procedure uspUpdateEmployeeHireInfo. Enter the date the rate changed for the employee.', @level0type = N'SCHEMA', @level0name = N'HumanResources', @level1type = N'PROCEDURE', @level1name = N'uspUpdateEmployeeHireInfo', @level2type = N'PARAMETER', @level2name = N'@RateChangeDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Input parameter for the stored procedure uspUpdateEmployeeHireInfo. Enter a hire date for the employee.', @level0type = N'SCHEMA', @level0name = N'HumanResources', @level1type = N'PROCEDURE', @level1name = N'uspUpdateEmployeeHireInfo', @level2type = N'PARAMETER', @level2name = N'@HireDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Input parameter for the stored procedure uspUpdateEmployeeHireInfo. Enter a title for the employee.', @level0type = N'SCHEMA', @level0name = N'HumanResources', @level1type = N'PROCEDURE', @level1name = N'uspUpdateEmployeeHireInfo', @level2type = N'PARAMETER', @level2name = N'@Title';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Input parameter for the stored procedure uspUpdateEmployeeHireInfo. Enter a valid EmployeeID from the Employee table.', @level0type = N'SCHEMA', @level0name = N'HumanResources', @level1type = N'PROCEDURE', @level1name = N'uspUpdateEmployeeHireInfo', @level2type = N'PARAMETER', @level2name = N'@EmployeeID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Updates the Employee table and inserts a new row in the EmployeePayHistory table with the values specified in the input parameters.', @level0type = N'SCHEMA', @level0name = N'HumanResources', @level1type = N'PROCEDURE', @level1name = N'uspUpdateEmployeeHireInfo';

