PRAGMA foreign_keys = ON;

CREATE TABLE IF NOT EXISTS clientes (
    cpf TEXT NOT NULL,
    nome TEXT NOT NULL,
    datanascimento TEXT NOT NULL,
    email TEXT NOT NULL,

    CONSTRAINT PK_clientes PRIMARY KEY (cpf)
);

CREATE TABLE IF NOT EXISTS dividas (
    id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
    valor DECIMAL(10, 2) NOT NULL,
    paga INTEGER NOT NULL,
    datacriacao TEXT NOT NULL,
    datapagamento TEXT NULL,
    clientecpf TEXT NOT NULL,

    CONSTRAINT FK_dividas_clientes
        FOREIGN KEY (clientecpf)
        REFERENCES clientes (cpf)
        ON DELETE CASCADE
);

CREATE INDEX IF NOT EXISTS IX_dividas_clientecpf
    ON dividas (clientecpf);
