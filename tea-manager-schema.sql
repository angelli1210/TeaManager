IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
CREATE TABLE [Brands] (
    [Id] uniqueidentifier NOT NULL,
    [BrandId] int NOT NULL,
    [BrandName] nvarchar(max) NOT NULL,
    [Country] nvarchar(max) NOT NULL,
    [FoundedYear] int NOT NULL,
    [Email] nvarchar(max) NOT NULL,
    [Password] nvarchar(max) NOT NULL,
    [Phone] nvarchar(max) NOT NULL,
    [Address] nvarchar(max) NOT NULL,
    [BusinessRegNumber] nvarchar(max) NOT NULL,
    [OwnerName] nvarchar(max) NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    CONSTRAINT [PK_Brands] PRIMARY KEY ([Id])
);

CREATE TABLE [Suppliers] (
    [Id] uniqueidentifier NOT NULL,
    [SupplierId] int NOT NULL,
    [SupplierName] nvarchar(max) NOT NULL,
    [Country] nvarchar(max) NOT NULL,
    [ContactEmail] nvarchar(max) NOT NULL,
    [Phone] nvarchar(max) NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    CONSTRAINT [PK_Suppliers] PRIMARY KEY ([Id])
);

CREATE TABLE [Products] (
    [Id] uniqueidentifier NOT NULL,
    [ProductId] int NOT NULL,
    [ProductName] nvarchar(max) NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    [Price] decimal(18,2) NOT NULL,
    [Stock] int NOT NULL,
    [HarvestYear] int NOT NULL,
    [Origin] nvarchar(max) NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [BrandId] uniqueidentifier NOT NULL,
    [SupplierId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_Products] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Products_Brands_BrandId] FOREIGN KEY ([BrandId]) REFERENCES [Brands] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Products_Suppliers_SupplierId] FOREIGN KEY ([SupplierId]) REFERENCES [Suppliers] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [SupplierOrders] (
    [Id] uniqueidentifier NOT NULL,
    [SupplierOrderId] int NOT NULL,
    [Quantity] int NOT NULL,
    [OrderDate] datetime2 NOT NULL,
    [Remark] nvarchar(max) NULL,
    [CreatedAt] datetime2 NOT NULL,
    [SupplierId] uniqueidentifier NOT NULL,
    [ProductId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_SupplierOrders] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_SupplierOrders_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_SupplierOrders_Suppliers_SupplierId] FOREIGN KEY ([SupplierId]) REFERENCES [Suppliers] ([Id]) ON DELETE NO ACTION
);

CREATE INDEX [IX_Products_BrandId] ON [Products] ([BrandId]);

CREATE INDEX [IX_Products_SupplierId] ON [Products] ([SupplierId]);

CREATE INDEX [IX_SupplierOrders_ProductId] ON [SupplierOrders] ([ProductId]);

CREATE INDEX [IX_SupplierOrders_SupplierId] ON [SupplierOrders] ([SupplierId]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260507170650_InitialMigration', N'10.0.5');

COMMIT;
GO

BEGIN TRANSACTION;
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Address', N'BrandId', N'BrandName', N'BusinessRegNumber', N'Country', N'CreatedAt', N'Email', N'FoundedYear', N'OwnerName', N'Password', N'Phone') AND [object_id] = OBJECT_ID(N'[Brands]'))
    SET IDENTITY_INSERT [Brands] ON;
INSERT INTO [Brands] ([Id], [Address], [BrandId], [BrandName], [BusinessRegNumber], [Country], [CreatedAt], [Email], [FoundedYear], [OwnerName], [Password], [Phone])
VALUES ('00000000-0000-0000-0000-000000000001', N'Lipston House, 42-43 Park Street, London, UK', 1, N'Lipston', N'BRN123445', N'United Kingdom', '2024-12-10T00:00:00.0000000', N'info@lipston.com', 1871, N'Jorge Russell', N'Lipton@123', N'+44 5-1234-56344'),
('00000000-0000-0000-0000-000000000002', N'TeaCoo Plaza, 343 MG Road, Mumbai, India', 2, N'TeaCoo', N'BRN883456', N'India', '2020-06-15T00:00:00.0000000', N'info@teacoo.com', 1950, N'Priya Sundar', N'Teacoo2345$', N'+91 12-2234-56342'),
('00000000-0000-0000-0000-000000000003', N'LaobanTea Firm, 57 Tea Street, Pu''er City, Yunnan Province, China', 3, N'LaobanTea', N'BRN323595', N'China', '2019-12-23T00:00:00.0000000', N'info@LaobanTea.com', 1855, N'Li Wei', N'LaoBanTea8473$', N'+86 18-7533-34256'),
('00000000-0000-0000-0000-000000000004', N'NortLeaf Plaza, 343 MG Road, Colombo, Sri Lanka', 4, N'NortLeaf', N'BRN833626', N'Sri Lanka', '2021-02-13T00:00:00.0000000', N'info@NortLeaf.com', 1950, N'Suan Perera', N'NortLeaf2334$', N'+94 9384-32848'),
('00000000-0000-0000-0000-000000000005', N'HealthTea Building, 123 Wellness Avenue, New York, USA', 5, N'HealthTea', N'BRN994567', N'United States', '2022-03-15T00:00:00.0000000', N'info@HealthTea.com', 2001, N'Sarah Johnson', N'HealthTea274@q', N'+1 555-123-4567'),
('00000000-0000-0000-0000-000000000006', N'HomeTea House, 456 Tea Lane, Dublin, Ireland', 6, N'HomeTea', N'BRN894567', N'Ireland', '2022-06-15T00:00:00.0000000', N'info@Hometea.com', 1900, N'Sam Smith', N'HomeTea3543$', N'+353 1-234-434256'),
('00000000-0000-0000-0000-000000000007', N'TeaFeast Plaza, 343 MG Road, Kuala Lumpur, Malaysia', 7, N'TeaFeast', N'BRN827456', N'Malaysia', '2021-07-08T00:00:00.0000000', N'info@Teafeast.com', 1950, N'Ahmad Sandar', N'Teafeast2345$', N'+60 12-345-6789'),
('00000000-0000-0000-0000-000000000008', N'TeaForest Plaza, 343 MG Road, Nairobi, Kenya', 8, N'TeaForest', N'BRN883204', N'Kenya', '2023-10-15T00:00:00.0000000', N'info@TeaForest.com', 1950, N'Samuel Harper', N'Teaforest23749@', N'+254 12-365-8890'),
('00000000-0000-0000-0000-000000000009', N'SoftTeabank Building, 123 Financial Avenue, Toronto, Canada', 9, N'SoftTeabank', N'BRN884446', N'Canada', '2024-08-25T00:00:00.0000000', N'info@softteabank.com', 1999, N'Emily Davis', N'SoftTeabank2345$', N'+1 345-763-8901'),
('00000000-0000-0000-0000-000000000010', N'TeaSeason Building, 456 Tea Street, Sydney, Australia', 10, N'TeaSeason', N'BRN885557', N'Australia', '2023-03-14T00:00:00.0000000', N'info@TeaSeason.com', 1999, N'Michael Johnson', N'TeaSeason2345$', N'+61 2-1234-5678');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Address', N'BrandId', N'BrandName', N'BusinessRegNumber', N'Country', N'CreatedAt', N'Email', N'FoundedYear', N'OwnerName', N'Password', N'Phone') AND [object_id] = OBJECT_ID(N'[Brands]'))
    SET IDENTITY_INSERT [Brands] OFF;

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ContactEmail', N'Country', N'CreatedAt', N'Phone', N'SupplierId', N'SupplierName') AND [object_id] = OBJECT_ID(N'[Suppliers]'))
    SET IDENTITY_INSERT [Suppliers] ON;
INSERT INTO [Suppliers] ([Id], [ContactEmail], [Country], [CreatedAt], [Phone], [SupplierId], [SupplierName])
VALUES ('00000000-0000-0000-0000-000000000011', N'supply@lipston.com', N'United Kingdom', '2024-12-10T00:00:00.0000000', N'+44 6-2346-83745', 1, N'Lipston Supplier'),
('00000000-0000-0000-0000-000000000012', N'supply@kingstone.com', N'India', '2020-06-15T00:00:00.0000000', N'+91 12-3334-55384', 2, N'Kingstone Supplier'),
('00000000-0000-0000-0000-000000000013', N'supply@LaobanTeaHub.com', N'China', '2019-12-23T00:00:00.0000000', N'+86 18-7532-34253', 3, N'LaobanTeaHub Supplier'),
('00000000-0000-0000-0000-000000000014', N'supply@alonetea.com', N'Sri Lanka', '2021-02-13T00:00:00.0000000', N'+94 90-8394-23456', 4, N'AloneTea Supplier'),
('00000000-0000-0000-0000-000000000015', N'supply@TrolTrol.com', N'Sri Lanka', '2022-03-15T00:00:00.0000000', N'+94 11-234-5678', 5, N'TrolTrol Supplier'),
('00000000-0000-0000-0000-000000000016', N'supply@HalTea.com', N'Kenya', '2022-06-15T00:00:00.0000000', N'+254 11-234-5678', 6, N'HalTea Supplier'),
('00000000-0000-0000-0000-000000000017', N'supply@Thia.com', N'Thailand', '2021-07-08T00:00:00.0000000', N'+66 2-123-4567', 7, N'Thia Supplier'),
('00000000-0000-0000-0000-000000000018', N'supply@Funck.com', N'Germany', '2023-10-15T00:00:00.0000000', N'+49 30-4568-2345', 8, N'Funck Supplier'),
('00000000-0000-0000-0000-000000000019', N'supply@OolongTeaHub.com', N'China', '2024-08-25T00:00:00.0000000', N'+86 10-1234-5678', 9, N'OolongTeaHub Supplier'),
('00000000-0000-0000-0000-000000000020', N'supply@EdibleTea.com', N'Japan', '2023-03-14T00:00:00.0000000', N'+81 3-1234-5678', 10, N'EdibleTea Supplier');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ContactEmail', N'Country', N'CreatedAt', N'Phone', N'SupplierId', N'SupplierName') AND [object_id] = OBJECT_ID(N'[Suppliers]'))
    SET IDENTITY_INSERT [Suppliers] OFF;

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'BrandId', N'CreatedAt', N'Description', N'HarvestYear', N'Origin', N'Price', N'ProductId', N'ProductName', N'Stock', N'SupplierId') AND [object_id] = OBJECT_ID(N'[Products]'))
    SET IDENTITY_INSERT [Products] ON;
INSERT INTO [Products] ([Id], [BrandId], [CreatedAt], [Description], [HarvestYear], [Origin], [Price], [ProductId], [ProductName], [Stock], [SupplierId])
VALUES ('00000000-0000-0000-0000-000000000021', '00000000-0000-0000-0000-000000000001', '2022-12-10T00:00:00.0000000', N'A refreshing green tea with a crisp flavor and various health benefits.', 2022, N'Cornwell,United Kingdom', 18.59, 1, N'Lipston Fresh Green Tea', 80, '00000000-0000-0000-0000-000000000011'),
('00000000-0000-0000-0000-000000000022', '00000000-0000-0000-0000-000000000002', '2020-06-15T00:00:00.0000000', N'A flavorful black tea from the Assam region of India.', 2020, N'Assam,India', 20.88, 2, N'TeaCoo Assam Black Tea', 80, '00000000-0000-0000-0000-000000000012'),
('00000000-0000-0000-0000-000000000023', '00000000-0000-0000-0000-000000000003', '2019-12-23T00:00:00.0000000', N'A rich and earthy tea from the Yunnan province of China, famous for its unique fermentation process.', 2019, N'Yunnan,China', 30.24, 3, N'LaobanTea Pu''erh Tea', 20, '00000000-0000-0000-0000-000000000013'),
('00000000-0000-0000-0000-000000000024', '00000000-0000-0000-0000-000000000004', '2021-02-13T00:00:00.0000000', N'A flavorful black tea from the Nuwera Eliya region of Sri Lanka.', 2021, N'Nuwera Eliya,Sri Lanka', 50.0, 4, N'NortLeaf Nuwera Premium Black Tea', 20, '00000000-0000-0000-0000-000000000014'),
('00000000-0000-0000-0000-000000000025', '00000000-0000-0000-0000-000000000005', '2022-03-15T00:00:00.0000000', N'An organic tea blend from the Mauna Kea region of Hawaii, beautifully balanced with floral and earthy notes.', 2022, N'Hawaii,United States', 25.45, 5, N'HealthTea Organic Mauna Kea Tea', 80, '00000000-0000-0000-0000-000000000015'),
('00000000-0000-0000-0000-000000000026', '00000000-0000-0000-0000-000000000006', '2022-06-15T00:00:00.0000000', N'Fresh morning tea with a clear and flavorful taste, perfect for starting your day.', 2022, N'Dublin,Ireland', 19.59, 6, N'HomeTea Irish Breakfast Tea', 150, '00000000-0000-0000-0000-000000000016'),
('00000000-0000-0000-0000-000000000027', '00000000-0000-0000-0000-000000000007', '2021-07-08T00:00:00.0000000', N'Premium earl grey tea with a refreshing citrus flavor and a hint of bergamot, perfect for tea lovers.', 2021, N'Chiang Mai,Thailand', 15.59, 7, N'TeaFeast Earl Grey Tea', 95, '00000000-0000-0000-0000-000000000017'),
('00000000-0000-0000-0000-000000000028', '00000000-0000-0000-0000-000000000008', '2023-10-15T00:00:00.0000000', N'Blend of fabulous teas from Ostfriesland, Germany.', 2023, N'Ostfriesland,Germany', 18.95, 8, N'TeaForest Ostfriesentee', 80, '00000000-0000-0000-0000-000000000018'),
('00000000-0000-0000-0000-000000000029', '00000000-0000-0000-0000-000000000009', '2024-08-25T00:00:00.0000000', N'High-quality oolong tea from Yunnan, China.', 2024, N'Yunnan,China', 28.66, 9, N'SoftTeabank Premium Yunnan Oolong Tea', 95, '00000000-0000-0000-0000-000000000019'),
('00000000-0000-0000-0000-000000000030', '00000000-0000-0000-0000-000000000010', '2023-03-14T00:00:00.0000000', N'Fantastic sencha green tea from Japan, with a fresh and grassy flavor.', 2024, N'Shizuoka,Japan', 33.59, 10, N'TeaSeason Sencha Green Tea', 80, '00000000-0000-0000-0000-000000000020');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'BrandId', N'CreatedAt', N'Description', N'HarvestYear', N'Origin', N'Price', N'ProductId', N'ProductName', N'Stock', N'SupplierId') AND [object_id] = OBJECT_ID(N'[Products]'))
    SET IDENTITY_INSERT [Products] OFF;

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260509120948_SeedData', N'10.0.5');

COMMIT;
GO

BEGIN TRANSACTION;
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'OrderDate', N'ProductId', N'Quantity', N'Remark', N'SupplierId', N'SupplierOrderId') AND [object_id] = OBJECT_ID(N'[SupplierOrders]'))
    SET IDENTITY_INSERT [SupplierOrders] ON;
INSERT INTO [SupplierOrders] ([Id], [CreatedAt], [OrderDate], [ProductId], [Quantity], [Remark], [SupplierId], [SupplierOrderId])
VALUES ('00000000-0000-0000-0000-000000000031', '2024-01-15T00:00:00.0000000', '2024-01-15T00:00:00.0000000', '00000000-0000-0000-0000-000000000021', 100, N'Initial bulk order from UK', '00000000-0000-0000-0000-000000000011', 1),
('00000000-0000-0000-0000-000000000032', '2024-02-10T00:00:00.0000000', '2024-02-10T00:00:00.0000000', '00000000-0000-0000-0000-000000000022', 80, N'Quarterly restock', '00000000-0000-0000-0000-000000000012', 2),
('00000000-0000-0000-0000-000000000033', '2024-03-05T00:00:00.0000000', '2024-03-05T00:00:00.0000000', '00000000-0000-0000-0000-000000000023', 60, NULL, '00000000-0000-0000-0000-000000000013', 3),
('00000000-0000-0000-0000-000000000034', '2024-04-18T00:00:00.0000000', '2024-04-18T00:00:00.0000000', '00000000-0000-0000-0000-000000000024', 40, N'Premium grade requested', '00000000-0000-0000-0000-000000000014', 4),
('00000000-0000-0000-0000-000000000035', '2024-05-22T00:00:00.0000000', '2024-05-22T00:00:00.0000000', '00000000-0000-0000-0000-000000000025', 120, N'Organic certification verified', '00000000-0000-0000-0000-000000000015', 5),
('00000000-0000-0000-0000-000000000036', '2024-06-30T00:00:00.0000000', '2024-06-30T00:00:00.0000000', '00000000-0000-0000-0000-000000000026', 200, N'Large order for European market', '00000000-0000-0000-0000-000000000016', 6),
('00000000-0000-0000-0000-000000000037', '2024-07-12T00:00:00.0000000', '2024-07-12T00:00:00.0000000', '00000000-0000-0000-0000-000000000027', 70, NULL, '00000000-0000-0000-0000-000000000017', 7),
('00000000-0000-0000-0000-000000000038', '2024-08-08T00:00:00.0000000', '2024-08-08T00:00:00.0000000', '00000000-0000-0000-0000-000000000028', 90, N'Express delivery requested', '00000000-0000-0000-0000-000000000018', 8),
('00000000-0000-0000-0000-000000000039', '2024-09-17T00:00:00.0000000', '2024-09-17T00:00:00.0000000', '00000000-0000-0000-0000-000000000029', 110, N'Premium oolong batch', '00000000-0000-0000-0000-000000000019', 9),
('00000000-0000-0000-0000-000000000040', '2024-10-25T00:00:00.0000000', '2024-10-25T00:00:00.0000000', '00000000-0000-0000-0000-000000000030', 150, N'First shipment to Australia', '00000000-0000-0000-0000-000000000020', 10);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'OrderDate', N'ProductId', N'Quantity', N'Remark', N'SupplierId', N'SupplierOrderId') AND [object_id] = OBJECT_ID(N'[SupplierOrders]'))
    SET IDENTITY_INSERT [SupplierOrders] OFF;

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260509122117_SeedSupplierOrders', N'10.0.5');

COMMIT;
GO

BEGIN TRANSACTION;
DECLARE @var nvarchar(max);
SELECT @var = QUOTENAME([d].[name])
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Brands]') AND [c].[name] = N'Password');
IF @var IS NOT NULL EXEC(N'ALTER TABLE [Brands] DROP CONSTRAINT ' + @var + ';');
ALTER TABLE [Brands] DROP COLUMN [Password];

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260515074017_RemoveBrandPassword', N'10.0.5');

COMMIT;
GO

