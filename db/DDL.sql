IF OBJECT_ID('dbo.Produto','U')   IS NOT NULL DROP TABLE dbo.Produto;
IF OBJECT_ID('dbo.Categoria','U') IS NOT NULL DROP TABLE dbo.Categoria;

CREATE TABLE dbo.Categoria (
    Id   INT IDENTITY(1,1) PRIMARY KEY,
    Nome NVARCHAR(120) NOT NULL UNIQUE
);

CREATE TABLE dbo.Produto (
    Id          INT IDENTITY(1,1) PRIMARY KEY,
    Nome        NVARCHAR(150) NOT NULL,
    Descricao   NVARCHAR(500) NULL,
    Valor       DECIMAL(10,2) NOT NULL CONSTRAINT CK_Produto_Valor_NaoNegativo CHECK (Valor >= 0),
    CategoriaId INT NOT NULL
        CONSTRAINT FK_Produto_Categoria REFERENCES dbo.Categoria(Id)
);

CREATE INDEX IX_Produto_CategoriaId ON dbo.Produto(CategoriaId);

INSERT INTO dbo.Categoria (Nome) VALUES ('Eletrônicos'), ('Acessórios');
INSERT INTO dbo.Produto (Nome, Descricao, Valor, CategoriaId) VALUES
('Smartphone Samsung', 'Galaxy S23 Ultra 256GB', 4999.99, 1);

