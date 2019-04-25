IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190416014703_initial')
BEGIN
    CREATE TABLE [PurchaseOrder] (
        [Id] int NOT NULL IDENTITY,
        [TBMITServices] nvarchar(max) NULL,
        [BusinessFunction] nvarchar(max) NULL,
        [SupportingServices] nvarchar(max) NULL,
        [EgovBRM] nvarchar(max) NULL,
        [Application] nvarchar(max) NULL,
        [EntryDate] datetime2 NOT NULL,
        [UnitPrice] decimal(18,2) NOT NULL,
        [Units] int NOT NULL,
        CONSTRAINT [PK_PurchaseOrder] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190416014703_initial')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190416014703_initial', N'2.2.2-servicing-10034');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190417001742_add_po_fields')
BEGIN
    EXEC sp_rename N'[PurchaseOrder].[TBMITServices]', N'TBMName', N'COLUMN';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190417001742_add_po_fields')
BEGIN
    EXEC sp_rename N'[PurchaseOrder].[SupportingServices]', N'TBMITService', N'COLUMN';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190417001742_add_po_fields')
BEGIN
    ALTER TABLE [PurchaseOrder] ADD [ApprovalDate] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190417001742_add_po_fields')
BEGIN
    ALTER TABLE [PurchaseOrder] ADD [Description] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190417001742_add_po_fields')
BEGIN
    ALTER TABLE [PurchaseOrder] ADD [Entity] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190417001742_add_po_fields')
BEGIN
    ALTER TABLE [PurchaseOrder] ADD [ExpirationDate] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190417001742_add_po_fields')
BEGIN
    ALTER TABLE [PurchaseOrder] ADD [Finance] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190417001742_add_po_fields')
BEGIN
    ALTER TABLE [PurchaseOrder] ADD [ITFunction] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190417001742_add_po_fields')
BEGIN
    ALTER TABLE [PurchaseOrder] ADD [ITTower] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190417001742_add_po_fields')
BEGIN
    ALTER TABLE [PurchaseOrder] ADD [SeatsPerLicense] int NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190417001742_add_po_fields')
BEGIN
    ALTER TABLE [PurchaseOrder] ADD [SeatsUsed] int NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190417001742_add_po_fields')
BEGIN
    ALTER TABLE [PurchaseOrder] ADD [TBMCategory] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190417001742_add_po_fields')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190417001742_add_po_fields', N'2.2.2-servicing-10034');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190417015130_made_fields_required')
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[PurchaseOrder]') AND [c].[name] = N'ITTower');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [PurchaseOrder] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [PurchaseOrder] ALTER COLUMN [ITTower] nvarchar(max) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190417015130_made_fields_required')
BEGIN
    DECLARE @var1 sysname;
    SELECT @var1 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[PurchaseOrder]') AND [c].[name] = N'ITFunction');
    IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [PurchaseOrder] DROP CONSTRAINT [' + @var1 + '];');
    ALTER TABLE [PurchaseOrder] ALTER COLUMN [ITFunction] nvarchar(max) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190417015130_made_fields_required')
BEGIN
    DECLARE @var2 sysname;
    SELECT @var2 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[PurchaseOrder]') AND [c].[name] = N'Entity');
    IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [PurchaseOrder] DROP CONSTRAINT [' + @var2 + '];');
    ALTER TABLE [PurchaseOrder] ALTER COLUMN [Entity] nvarchar(max) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190417015130_made_fields_required')
BEGIN
    DECLARE @var3 sysname;
    SELECT @var3 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[PurchaseOrder]') AND [c].[name] = N'BusinessFunction');
    IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [PurchaseOrder] DROP CONSTRAINT [' + @var3 + '];');
    ALTER TABLE [PurchaseOrder] ALTER COLUMN [BusinessFunction] nvarchar(max) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190417015130_made_fields_required')
BEGIN
    DECLARE @var4 sysname;
    SELECT @var4 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[PurchaseOrder]') AND [c].[name] = N'Application');
    IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [PurchaseOrder] DROP CONSTRAINT [' + @var4 + '];');
    ALTER TABLE [PurchaseOrder] ALTER COLUMN [Application] nvarchar(max) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190417015130_made_fields_required')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190417015130_made_fields_required', N'2.2.2-servicing-10034');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190417024844_nullable_dates')
BEGIN
    DECLARE @var5 sysname;
    SELECT @var5 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[PurchaseOrder]') AND [c].[name] = N'ExpirationDate');
    IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [PurchaseOrder] DROP CONSTRAINT [' + @var5 + '];');
    ALTER TABLE [PurchaseOrder] ALTER COLUMN [ExpirationDate] datetime2 NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190417024844_nullable_dates')
BEGIN
    DECLARE @var6 sysname;
    SELECT @var6 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[PurchaseOrder]') AND [c].[name] = N'ApprovalDate');
    IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [PurchaseOrder] DROP CONSTRAINT [' + @var6 + '];');
    ALTER TABLE [PurchaseOrder] ALTER COLUMN [ApprovalDate] datetime2 NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190417024844_nullable_dates')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190417024844_nullable_dates', N'2.2.2-servicing-10034');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190417165142_entity_enum')
BEGIN
    DECLARE @var7 sysname;
    SELECT @var7 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[PurchaseOrder]') AND [c].[name] = N'Entity');
    IF @var7 IS NOT NULL EXEC(N'ALTER TABLE [PurchaseOrder] DROP CONSTRAINT [' + @var7 + '];');
    ALTER TABLE [PurchaseOrder] ALTER COLUMN [Entity] int NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190417165142_entity_enum')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190417165142_entity_enum', N'2.2.2-servicing-10034');
END;

GO

