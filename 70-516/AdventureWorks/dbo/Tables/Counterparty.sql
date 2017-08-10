CREATE TABLE [dbo].[Counterparty] (
    [CounterpartyId] INT           IDENTITY (1, 1) NOT NULL,
    [AccountNumber]  NVARCHAR (15) NOT NULL,
    CONSTRAINT [PK_Counterparty] PRIMARY KEY CLUSTERED ([CounterpartyId] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [AK_Counterparty_AccountNumber]
    ON [dbo].[Counterparty]([AccountNumber] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unique nonclustered index.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Counterparty', @level2type = N'INDEX', @level2name = N'AK_Counterparty_AccountNumber';

