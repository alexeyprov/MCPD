CREATE PROCEDURE [HumanResources].[uspUpdateEmployeeLogin]
@EmployeeID [int], 
@ManagerID [int],
@LoginID [nvarchar](256),
@Title [nvarchar](50),
@HireDate [datetime],
@CurrentFlag [dbo].[Flag]
WITH EXECUTE AS CALLER
AS
BEGIN
SET NOCOUNT ON;
BEGIN TRY
UPDATE [HumanResources].[Employee] 
SET [ManagerID] = @ManagerID 
,[LoginID] = @LoginID 
,[Title] = @Title 
,[HireDate] = @HireDate 
,[CurrentFlag] = @CurrentFlag 
WHERE [EmployeeID] = @EmployeeID;
END TRY
BEGIN CATCH
EXECUTE [dbo].[uspLogError];
END CATCH;
END;
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Input parameter for the stored procedure uspUpdateEmployeeHireInfo. Enter the current flag for the employee.', @level0type = N'SCHEMA', @level0name = N'HumanResources', @level1type = N'PROCEDURE', @level1name = N'uspUpdateEmployeeLogin', @level2type = N'PARAMETER', @level2name = N'@CurrentFlag';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Input parameter for the stored procedure uspUpdateEmployeeHireInfo. Enter a hire date for the employee.', @level0type = N'SCHEMA', @level0name = N'HumanResources', @level1type = N'PROCEDURE', @level1name = N'uspUpdateEmployeeLogin', @level2type = N'PARAMETER', @level2name = N'@HireDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Input parameter for the stored procedure uspUpdateEmployeeHireInfo. Enter a title for the employee.', @level0type = N'SCHEMA', @level0name = N'HumanResources', @level1type = N'PROCEDURE', @level1name = N'uspUpdateEmployeeLogin', @level2type = N'PARAMETER', @level2name = N'@Title';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Input parameter for the stored procedure uspUpdateEmployeeHireInfo. Enter a valid login for the employee.', @level0type = N'SCHEMA', @level0name = N'HumanResources', @level1type = N'PROCEDURE', @level1name = N'uspUpdateEmployeeLogin', @level2type = N'PARAMETER', @level2name = N'@LoginID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Input parameter for the stored procedure uspUpdateEmployeeHireInfo. Enter a valid ManagerID for the employee.', @level0type = N'SCHEMA', @level0name = N'HumanResources', @level1type = N'PROCEDURE', @level1name = N'uspUpdateEmployeeLogin', @level2type = N'PARAMETER', @level2name = N'@ManagerID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Input parameter for the stored procedure uspUpdateEmployeeLogin. Enter a valid EmployeeID from the Employee table.', @level0type = N'SCHEMA', @level0name = N'HumanResources', @level1type = N'PROCEDURE', @level1name = N'uspUpdateEmployeeLogin', @level2type = N'PARAMETER', @level2name = N'@EmployeeID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Updates the Employee table with the values specified in the input parameters for the given EmployeeID.', @level0type = N'SCHEMA', @level0name = N'HumanResources', @level1type = N'PROCEDURE', @level1name = N'uspUpdateEmployeeLogin';

