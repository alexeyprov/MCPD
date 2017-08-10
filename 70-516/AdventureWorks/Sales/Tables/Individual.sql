CREATE TABLE [Sales].[Individual] (
    [CustomerID]   INT                                                     NOT NULL,
    [ContactID]    INT                                                     NOT NULL,
    [Demographics] XML(CONTENT [Sales].[IndividualSurveySchemaCollection]) NULL,
    [ModifiedDate] DATETIME                                                CONSTRAINT [DF_Individual_ModifiedDate] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_Individual_CustomerID] PRIMARY KEY CLUSTERED ([CustomerID] ASC),
    CONSTRAINT [FK_Individual_Contact_ContactID] FOREIGN KEY ([ContactID]) REFERENCES [Person].[Contact] ([ContactID]),
    CONSTRAINT [FK_Individual_Customer_CustomerID] FOREIGN KEY ([CustomerID]) REFERENCES [Sales].[Customer] ([CustomerID])
);


GO
CREATE XML INDEX [XMLVALUE_Individual_Demographics]
    ON [Sales].[Individual]([Demographics])
    USING XML INDEX [PXML_Individual_Demographics] FOR VALUE
    WITH (PAD_INDEX = OFF);


GO
CREATE XML INDEX [XMLPROPERTY_Individual_Demographics]
    ON [Sales].[Individual]([Demographics])
    USING XML INDEX [PXML_Individual_Demographics] FOR PROPERTY
    WITH (PAD_INDEX = OFF);


GO
CREATE XML INDEX [XMLPATH_Individual_Demographics]
    ON [Sales].[Individual]([Demographics])
    USING XML INDEX [PXML_Individual_Demographics] FOR PATH
    WITH (PAD_INDEX = OFF);


GO
CREATE PRIMARY XML INDEX [PXML_Individual_Demographics]
    ON [Sales].[Individual]([Demographics])
    WITH (PAD_INDEX = OFF);


GO
CREATE TRIGGER [Sales].[iuIndividual] ON [Sales].[Individual] 
AFTER INSERT, UPDATE NOT FOR REPLICATION AS 
BEGIN
DECLARE @Count int;
SET @Count = @@ROWCOUNT;
IF @Count = 0 
RETURN;
SET NOCOUNT ON;
IF EXISTS (SELECT * FROM inserted INNER JOIN [Sales].[Store] 
ON inserted.[CustomerID] = [Sales].[Store].[CustomerID]) 
BEGIN
IF @@TRANCOUNT > 0
BEGIN
ROLLBACK TRANSACTION;
END
END;
IF UPDATE([CustomerID]) OR UPDATE([Demographics]) 
BEGIN
UPDATE [Sales].[Individual] 
SET [Sales].[Individual].[Demographics] = N'<IndividualSurvey xmlns="http://schemas.microsoft.com/sqlserver/2004/07/adventure-works/IndividualSurvey"> 
<TotalPurchaseYTD>0.00</TotalPurchaseYTD> 
</IndividualSurvey>' 
FROM inserted 
WHERE [Sales].[Individual].[CustomerID] = inserted.[CustomerID] 
AND inserted.[Demographics] IS NULL;

UPDATE [Sales].[Individual] 
SET [Demographics].modify(N'declare default element namespace "http://schemas.microsoft.com/sqlserver/2004/07/adventure-works/IndividualSurvey"; 
insert <TotalPurchaseYTD>0.00</TotalPurchaseYTD> 
as first 
into (/IndividualSurvey)[1]') 
FROM inserted 
WHERE [Sales].[Individual].[CustomerID] = inserted.[CustomerID] 
AND inserted.[Demographics] IS NOT NULL 
AND inserted.[Demographics].exist(N'declare default element namespace 
"http://schemas.microsoft.com/sqlserver/2004/07/adventure-works/IndividualSurvey"; 
/IndividualSurvey/TotalPurchaseYTD') <> 1;
END;
END;
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'AFTER INSERT, UPDATE trigger inserting Individual only if the Customer does not exist in the Store table and setting the ModifiedDate column in the Individual table to the current date.', @level0type = N'SCHEMA', @level0name = N'Sales', @level1type = N'TABLE', @level1name = N'Individual', @level2type = N'TRIGGER', @level2name = N'iuIndividual';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Secondary XML index for value.', @level0type = N'SCHEMA', @level0name = N'Sales', @level1type = N'TABLE', @level1name = N'Individual', @level2type = N'INDEX', @level2name = N'XMLVALUE_Individual_Demographics';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Secondary XML index for property.', @level0type = N'SCHEMA', @level0name = N'Sales', @level1type = N'TABLE', @level1name = N'Individual', @level2type = N'INDEX', @level2name = N'XMLPROPERTY_Individual_Demographics';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Secondary XML index for path.', @level0type = N'SCHEMA', @level0name = N'Sales', @level1type = N'TABLE', @level1name = N'Individual', @level2type = N'INDEX', @level2name = N'XMLPATH_Individual_Demographics';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Primary XML index.', @level0type = N'SCHEMA', @level0name = N'Sales', @level1type = N'TABLE', @level1name = N'Individual', @level2type = N'INDEX', @level2name = N'PXML_Individual_Demographics';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Default constraint value of GETDATE()', @level0type = N'SCHEMA', @level0name = N'Sales', @level1type = N'TABLE', @level1name = N'Individual', @level2type = N'CONSTRAINT', @level2name = N'DF_Individual_ModifiedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Foreign key constraint referencing Contact.ContactID.', @level0type = N'SCHEMA', @level0name = N'Sales', @level1type = N'TABLE', @level1name = N'Individual', @level2type = N'CONSTRAINT', @level2name = N'FK_Individual_Contact_ContactID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Foreign key constraint referencing Customer.CustomerID.', @level0type = N'SCHEMA', @level0name = N'Sales', @level1type = N'TABLE', @level1name = N'Individual', @level2type = N'CONSTRAINT', @level2name = N'FK_Individual_Customer_CustomerID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Primary key (clustered) constraint', @level0type = N'SCHEMA', @level0name = N'Sales', @level1type = N'TABLE', @level1name = N'Individual', @level2type = N'CONSTRAINT', @level2name = N'PK_Individual_CustomerID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Date and time the record was last updated.', @level0type = N'SCHEMA', @level0name = N'Sales', @level1type = N'TABLE', @level1name = N'Individual', @level2type = N'COLUMN', @level2name = N'ModifiedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Personal information such as hobbies, and income collected from online shoppers. Used for sales analysis.', @level0type = N'SCHEMA', @level0name = N'Sales', @level1type = N'TABLE', @level1name = N'Individual', @level2type = N'COLUMN', @level2name = N'Demographics';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identifies the customer in the Contact table. Foreign key to Contact.ContactID.', @level0type = N'SCHEMA', @level0name = N'Sales', @level1type = N'TABLE', @level1name = N'Individual', @level2type = N'COLUMN', @level2name = N'ContactID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unique customer identification number. Foreign key to Customer.CustomerID.', @level0type = N'SCHEMA', @level0name = N'Sales', @level1type = N'TABLE', @level1name = N'Individual', @level2type = N'COLUMN', @level2name = N'CustomerID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Demographic data about customers that purchase Adventure Works products online.', @level0type = N'SCHEMA', @level0name = N'Sales', @level1type = N'TABLE', @level1name = N'Individual';

