CREATE TABLE [Sales].[ContactCreditCard] (
    [ContactID]    INT      NOT NULL,
    [CreditCardID] INT      NOT NULL,
    [ModifiedDate] DATETIME CONSTRAINT [DF_ContactCreditCard_ModifiedDate] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_ContactCreditCard_ContactID_CreditCardID] PRIMARY KEY CLUSTERED ([ContactID] ASC, [CreditCardID] ASC),
    CONSTRAINT [FK_ContactCreditCard_Contact_ContactID] FOREIGN KEY ([ContactID]) REFERENCES [Person].[Contact] ([ContactID]),
    CONSTRAINT [FK_ContactCreditCard_CreditCard_CreditCardID] FOREIGN KEY ([CreditCardID]) REFERENCES [Sales].[CreditCard] ([CreditCardID])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Default constraint value of GETDATE()', @level0type = N'SCHEMA', @level0name = N'Sales', @level1type = N'TABLE', @level1name = N'ContactCreditCard', @level2type = N'CONSTRAINT', @level2name = N'DF_ContactCreditCard_ModifiedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Foreign key constraint referencing CreditCard.CreditCardID.', @level0type = N'SCHEMA', @level0name = N'Sales', @level1type = N'TABLE', @level1name = N'ContactCreditCard', @level2type = N'CONSTRAINT', @level2name = N'FK_ContactCreditCard_CreditCard_CreditCardID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Foreign key constraint referencing Contact.ContactID.', @level0type = N'SCHEMA', @level0name = N'Sales', @level1type = N'TABLE', @level1name = N'ContactCreditCard', @level2type = N'CONSTRAINT', @level2name = N'FK_ContactCreditCard_Contact_ContactID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Primary key (clustered) constraint', @level0type = N'SCHEMA', @level0name = N'Sales', @level1type = N'TABLE', @level1name = N'ContactCreditCard', @level2type = N'CONSTRAINT', @level2name = N'PK_ContactCreditCard_ContactID_CreditCardID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Date and time the record was last updated.', @level0type = N'SCHEMA', @level0name = N'Sales', @level1type = N'TABLE', @level1name = N'ContactCreditCard', @level2type = N'COLUMN', @level2name = N'ModifiedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Credit card identification number. Foreign key to CreditCard.CreditCardID.', @level0type = N'SCHEMA', @level0name = N'Sales', @level1type = N'TABLE', @level1name = N'ContactCreditCard', @level2type = N'COLUMN', @level2name = N'CreditCardID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Customer identification number. Foreign key to Contact.ContactID.', @level0type = N'SCHEMA', @level0name = N'Sales', @level1type = N'TABLE', @level1name = N'ContactCreditCard', @level2type = N'COLUMN', @level2name = N'ContactID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cross-reference table mapping customers in the Contact table to their credit card information in the CreditCard table. ', @level0type = N'SCHEMA', @level0name = N'Sales', @level1type = N'TABLE', @level1name = N'ContactCreditCard';

