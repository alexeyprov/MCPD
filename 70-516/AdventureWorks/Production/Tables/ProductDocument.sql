CREATE TABLE [Production].[ProductDocument] (
    [ProductID]    INT      NOT NULL,
    [DocumentID]   INT      NOT NULL,
    [ModifiedDate] DATETIME CONSTRAINT [DF_ProductDocument_ModifiedDate] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_ProductDocument_ProductID_DocumentID] PRIMARY KEY CLUSTERED ([ProductID] ASC, [DocumentID] ASC),
    CONSTRAINT [FK_ProductDocument_Document_DocumentID] FOREIGN KEY ([DocumentID]) REFERENCES [Production].[Document] ([DocumentID]),
    CONSTRAINT [FK_ProductDocument_Product_ProductID] FOREIGN KEY ([ProductID]) REFERENCES [Production].[Product] ([ProductID])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Default constraint value of GETDATE()', @level0type = N'SCHEMA', @level0name = N'Production', @level1type = N'TABLE', @level1name = N'ProductDocument', @level2type = N'CONSTRAINT', @level2name = N'DF_ProductDocument_ModifiedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Foreign key constraint referencing Document.DocumentID.', @level0type = N'SCHEMA', @level0name = N'Production', @level1type = N'TABLE', @level1name = N'ProductDocument', @level2type = N'CONSTRAINT', @level2name = N'FK_ProductDocument_Document_DocumentID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Foreign key constraint referencing Product.ProductID.', @level0type = N'SCHEMA', @level0name = N'Production', @level1type = N'TABLE', @level1name = N'ProductDocument', @level2type = N'CONSTRAINT', @level2name = N'FK_ProductDocument_Product_ProductID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Primary key (clustered) constraint', @level0type = N'SCHEMA', @level0name = N'Production', @level1type = N'TABLE', @level1name = N'ProductDocument', @level2type = N'CONSTRAINT', @level2name = N'PK_ProductDocument_ProductID_DocumentID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Date and time the record was last updated.', @level0type = N'SCHEMA', @level0name = N'Production', @level1type = N'TABLE', @level1name = N'ProductDocument', @level2type = N'COLUMN', @level2name = N'ModifiedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Document identification number. Foreign key to Document.DocumentID.', @level0type = N'SCHEMA', @level0name = N'Production', @level1type = N'TABLE', @level1name = N'ProductDocument', @level2type = N'COLUMN', @level2name = N'DocumentID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Product identification number. Foreign key to Product.ProductID.', @level0type = N'SCHEMA', @level0name = N'Production', @level1type = N'TABLE', @level1name = N'ProductDocument', @level2type = N'COLUMN', @level2name = N'ProductID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cross-reference table mapping products to related product documents.', @level0type = N'SCHEMA', @level0name = N'Production', @level1type = N'TABLE', @level1name = N'ProductDocument';

