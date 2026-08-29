
CREATE TABLE [alunos] (
    [id] BIGINT IDENTITY(1,1) NOT NULL,
    [Nome] VARCHAR(100) NOT NULL,
    [Curso] VARCHAR(100) NOT NULL,
    [Genero] VARCHAR(9) NOT NULL,
    [Endereço] VARCHAR(100) NOT NULL,
    [Telefone] VARCHAR(12) NOT NULL,
    [Email] VARCHAR(80) NOT NULL,
    [Nascimento] DATE NOT NULL,
    [Emprestimos] INT NOT NULL,
    [Habilitado] BIT NOT NULL,
    CONSTRAINT [PK_alunos] PRIMARY KEY ([id])
);


CREATE TABLE [funcionarios] (
    [id] BIGINT IDENTITY(1,1) NOT NULL,
    [Nome] VARCHAR(100) NOT NULL,
    [Função] VARCHAR(100) NOT NULL,
    [Genero] VARCHAR(9) NOT NULL,
    [Telefone] VARCHAR(12) NOT NULL,
    [Endereço] VARCHAR(100) NOT NULL,
    [Email] VARCHAR(80) NOT NULL,
    [Nascimento] DATE NOT NULL,
    [Emprestimos] INT NOT NULL,
    [Habilitado] BIT NOT NULL,
    CONSTRAINT [PK_funcionarios] PRIMARY KEY ([id])
);


CREATE TABLE [livros] (
    [id] BIGINT IDENTITY(1,1) NOT NULL,
    [Autor] VARCHAR(100) NOT NULL,
    [Estoque] BIGINT NOT NULL,
    [Titulo] VARCHAR(100) NOT NULL,
    [Emprestados] BIGINT NOT NULL,
    [Disponiveis] BIGINT NOT NULL,
    [Habilitado] BIT NOT NULL,
    CONSTRAINT [PK_livros] PRIMARY KEY ([id])
);


CREATE TABLE [Users] (
    [id] BIGINT IDENTITY(1,1) NOT NULL,
    [Username] VARCHAR(100) NOT NULL,
    [Fullname] VARCHAR(100) NOT NULL,
    [passwordhash] VARCHAR(300) NOT NULL,
    [refreshtoken] VARCHAR(300) NULL,
    [refreshtokenexpirytime] DATETIME2 NULL,
    [Enable] BIT NOT NULL,
    [Key] VARCHAR(500) NOT NULL,
    [email] VARCHAR(150) NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([id])
);


CREATE TABLE [EmprestimosAlunos] (
    [id] BIGINT IDENTITY(1,1) NOT NULL,
    [IdAluno] BIGINT NOT NULL,
    [IdLivro] BIGINT NOT NULL,
    [Inicio] DATE NOT NULL,
    [Fim] DATE NOT NULL,
    [Devolvido] BIT NOT NULL,
    [ValorMulta] INT NOT NULL,
    [Multado] BIT NOT NULL,
    CONSTRAINT [PK_EmprestimosAlunos] PRIMARY KEY ([id]),
    CONSTRAINT [FK_EmprestimosAlunos_alunos_IdAluno] FOREIGN KEY ([IdAluno]) REFERENCES [alunos] ([id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_EmprestimosAlunos_livros_IdLivro] FOREIGN KEY ([IdLivro]) REFERENCES [livros] ([id]) ON DELETE NO ACTION
);


CREATE TABLE [EmprestimosFuncionarios] (
    [id] BIGINT IDENTITY(1,1) NOT NULL,
    [IdFuncionario] BIGINT NOT NULL,
    [IdLivro] BIGINT NOT NULL,
    [Inicio] DATE NOT NULL,
    [Fim] DATE NOT NULL,
    [Devolvido] BIT NOT NULL,
    [ValorMulta] INT NOT NULL,
    [Multado] BIT NOT NULL,
    CONSTRAINT [PK_EmprestimosFuncionarios] PRIMARY KEY ([id]),
    CONSTRAINT [FK_EmprestimosFuncionarios_funcionarios_IdFuncionario] FOREIGN KEY ([IdFuncionario]) REFERENCES [funcionarios] ([id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_EmprestimosFuncionarios_livros_IdLivro] FOREIGN KEY ([IdLivro]) REFERENCES [livros] ([id]) ON DELETE NO ACTION
);


CREATE INDEX [IX_EmprestimosAlunos_IdAluno] ON [EmprestimosAlunos] ([IdAluno]);
CREATE INDEX [IX_EmprestimosAlunos_IdLivro] ON [EmprestimosAlunos] ([IdLivro]);

CREATE INDEX [IX_EmprestimosFuncionarios_IdFuncionario] ON [EmprestimosFuncionarios] ([IdFuncionario]);
CREATE INDEX [IX_EmprestimosFuncionarios_IdLivro] ON [EmprestimosFuncionarios] ([IdLivro]);

CREATE UNIQUE INDEX [IX_Users_Username] ON [Users] ([Username]);