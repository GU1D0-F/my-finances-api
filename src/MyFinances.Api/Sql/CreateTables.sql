-- TABELAS USUARIOS
BEGIN TRANSACTION;
GO

CREATE TABLE [AspNetRoles] (
    [Id] nvarchar(450) NOT NULL,
    [Name] nvarchar(256) NULL,
    [NormalizedName] nvarchar(256) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [AspNetRoleClaims] (
    [Id] int NOT NULL IDENTITY,
    [RoleId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Users] (
    [Id] nvarchar(450) NOT NULL,
    [FullName] nvarchar(max) NULL,
    [BirthDate] datetime2 NOT NULL,
    [RegisterDate] datetime2 NOT NULL,
    [UserName] nvarchar(256) NULL,
    [NormalizedUserName] nvarchar(256) NULL,
    [Email] nvarchar(256) NULL,
    [NormalizedEmail] nvarchar(256) NULL,
    [EmailConfirmed] bit NOT NULL,
    [PasswordHash] nvarchar(max) NULL,
    [SecurityStamp] nvarchar(max) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    [PhoneNumber] nvarchar(max) NULL,
    [PhoneNumberConfirmed] bit NOT NULL,
    [TwoFactorEnabled] bit NOT NULL,
    [LockoutEnd] datetimeoffset NULL,
    [LockoutEnabled] bit NOT NULL,
    [AccessFailedCount] int NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([Id]),
);
GO

CREATE TABLE [AspNetUserClaims] (
    [Id] int NOT NULL IDENTITY,
    [UserId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetUserClaims_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
);
GO


CREATE TABLE [AspNetUserLogins] (
    [LoginProvider] nvarchar(450) NOT NULL,
    [ProviderKey] nvarchar(450) NOT NULL,
    [ProviderDisplayName] nvarchar(max) NULL,
    [UserId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
    CONSTRAINT [FK_AspNetUserLogins_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
);
GO


CREATE TABLE [AspNetUserRoles] (
    [UserId] nvarchar(450) NOT NULL,
    [RoleId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
    CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AspNetUserRoles_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserTokens] (
    [UserId] nvarchar(450) NOT NULL,
    [LoginProvider] nvarchar(450) NOT NULL,
    [Name] nvarchar(450) NOT NULL,
    [Value] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
    CONSTRAINT [FK_AspNetUserTokens_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);
GO

CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL;
GO

CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);
GO

CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);
GO

CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);
GO

CREATE INDEX [EmailIndex] ON [Users] ([NormalizedEmail]);
GO

CREATE UNIQUE INDEX [UserNameIndex] ON [Users] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL;
GO

COMMIT;
GO

-- Tabela para a classe Spending
CREATE TABLE Spendings (
    Id BIGINT IDENTITY(1,1) PRIMARY KEY,
    Data DATETIME NOT NULL,
    Categoria NVARCHAR(150),
    Descricao NVARCHAR(200),
    Valor DECIMAL(18, 2) NOT NULL,
    TipoTransacao INT NOT NULL,
    Observacao NVARCHAR(500),
    UserId nvarchar(450) NOT NULL,
    CONSTRAINT FK_Spendings_Users FOREIGN KEY (UserId) REFERENCES Users(Id)
);

-- Tabela para a classe Metric
CREATE TABLE Metrics (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    DataDe DATETIME,
    DataAte DATETIME,
    Saldo DECIMAL(18, 2),
    Entrada DECIMAL(18, 2),
    Saida DECIMAL(18, 2),
    Sobra DECIMAL(18, 2),
    UserId nvarchar(450) NOT NULL,
    CONSTRAINT FK_Metrics_Users FOREIGN KEY (UserId) REFERENCES Users(Id)
);

-- Tabela para a classe ReportImport
CREATE TABLE ReportImport (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Documento NVARCHAR(MAX),
    Type INT,
    Status INT,
    ExcelFile VARBINARY(MAX),
    FullPath NVARCHAR(MAX),
    StartTime DATETIME,
    EndTime DATETIME,
    UserId nvarchar(450) NOT NULL,
    CONSTRAINT FK_ReportImport_Users FOREIGN KEY (UserId) REFERENCES Users(Id)
);

CREATE TABLE ReportImportLog (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Log VARCHAR(4000),
    ReportImportId INT,
    FOREIGN KEY (ReportImportId) REFERENCES ReportImport(Id),
    CONSTRAINT FK_ReportImportLog_ReportImport FOREIGN KEY (ReportImportId) REFERENCES ReportImport(Id)
);

-- Tabela para a classe ReportExport
CREATE TABLE ReportExport
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    ExcelFile VARBINARY(MAX),
    Documento NVARCHAR(MAX),
    FullPath NVARCHAR(MAX),
    Status INT,
    StartTime DATETIME,
    EndTime DATETIME,
    Type INT,
    Filters NVARCHAR(4000),
    ExtensionType INT DEFAULT 1,
    UserId nvarchar(450) NOT NULL,
    CONSTRAINT FK_ReportExport_Users FOREIGN KEY (UserId) REFERENCES Users(Id)
);

CREATE TABLE ReportExportLog (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Log VARCHAR(4000),
    ReportExportId INT,
    FOREIGN KEY (ReportExportId) REFERENCES ReportExport(Id),
    CONSTRAINT FK_ReportExportLog_ReportExport FOREIGN KEY (ReportExportId) REFERENCES ReportExport(Id)
);



-- Tabelas de Analise dados financeiros
CREATE TABLE EntradaSaidaTotalPorMes (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Periodo NVARCHAR(50),
    TipoTransacao INT NOT NULL,
    Valor DECIMAL(18, 2),
    UserId nvarchar(450) NOT NULL,
    CONSTRAINT FK_EntradaSaidaTotalPorMes_Users FOREIGN KEY (UserId) REFERENCES Users(Id)
);


CREATE TABLE EntradaSaidaPorMesCategoria (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Periodo NVARCHAR(50),
    Categoria NVARCHAR(150),
    TipoTransacao INT NOT NULL,
    Valor DECIMAL(18, 2),
    UserId nvarchar(450) NOT NULL,
    CONSTRAINT FK_EntradaSaidaPorCategoria_Users FOREIGN KEY (UserId) REFERENCES Users(Id)
);
