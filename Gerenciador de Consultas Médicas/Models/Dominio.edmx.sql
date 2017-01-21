
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 12/12/2016 21:11:28
-- Generated from EDMX file: C:\Users\marci\Documents\Visual Studio 2015\Projects\Gerenciador de Consultas Médicas\Gerenciador de Consultas Médicas\Models\Dominio.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Consultas online];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_pacientesconsultas]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[consultasSet] DROP CONSTRAINT [FK_pacientesconsultas];
GO
IF OBJECT_ID(N'[dbo].[FK_conveniospacientes]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[pacientesSet] DROP CONSTRAINT [FK_conveniospacientes];
GO
IF OBJECT_ID(N'[dbo].[FK_medicosconsultas]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[consultasSet] DROP CONSTRAINT [FK_medicosconsultas];
GO
IF OBJECT_ID(N'[dbo].[FK_medicosavaliacoes]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[avaliacoes] DROP CONSTRAINT [FK_medicosavaliacoes];
GO
IF OBJECT_ID(N'[dbo].[FK_especialidadesmedicos]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[medicosSet] DROP CONSTRAINT [FK_especialidadesmedicos];
GO
IF OBJECT_ID(N'[dbo].[FK_clinicascontasBancarias]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[contasBancariasSet] DROP CONSTRAINT [FK_clinicascontasBancarias];
GO
IF OBJECT_ID(N'[dbo].[FK_clinicasconsultas]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[consultasSet] DROP CONSTRAINT [FK_clinicasconsultas];
GO
IF OBJECT_ID(N'[dbo].[FK_contasBancariaspagamentos]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[pagamentosSet] DROP CONSTRAINT [FK_contasBancariaspagamentos];
GO
IF OBJECT_ID(N'[dbo].[FK_clinicasmedicos]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[medicosSet] DROP CONSTRAINT [FK_clinicasmedicos];
GO
IF OBJECT_ID(N'[dbo].[FK_medicosagenda]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[agendaSet] DROP CONSTRAINT [FK_medicosagenda];
GO
IF OBJECT_ID(N'[dbo].[FK_pacientesavaliacoes]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[avaliacoes] DROP CONSTRAINT [FK_pacientesavaliacoes];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[pacientesSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[pacientesSet];
GO
IF OBJECT_ID(N'[dbo].[medicosSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[medicosSet];
GO
IF OBJECT_ID(N'[dbo].[clinicasSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[clinicasSet];
GO
IF OBJECT_ID(N'[dbo].[pagamentosSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[pagamentosSet];
GO
IF OBJECT_ID(N'[dbo].[conveniosSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[conveniosSet];
GO
IF OBJECT_ID(N'[dbo].[consultasSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[consultasSet];
GO
IF OBJECT_ID(N'[dbo].[especialidadesSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[especialidadesSet];
GO
IF OBJECT_ID(N'[dbo].[avaliacoes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[avaliacoes];
GO
IF OBJECT_ID(N'[dbo].[contasBancariasSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[contasBancariasSet];
GO
IF OBJECT_ID(N'[dbo].[sysdiagrams]', 'U') IS NOT NULL
    DROP TABLE [dbo].[sysdiagrams];
GO
IF OBJECT_ID(N'[dbo].[agendaSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[agendaSet];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'pacientesSet'
CREATE TABLE [dbo].[pacientesSet] (
    [idPaciente] int IDENTITY(1,1) NOT NULL,
    [nome] nvarchar(50)  NOT NULL,
    [cpf] nvarchar(max)  NOT NULL,
    [endereco] nvarchar(50)  NOT NULL,
    [bairro] nvarchar(max)  NOT NULL,
    [cidade] nvarchar(40)  NOT NULL,
    [estado] nvarchar(30)  NOT NULL,
    [telefone] nvarchar(max)  NOT NULL,
    [celular] nvarchar(max)  NULL,
    [email] nvarchar(60)  NOT NULL,
    [senha] nvarchar(18)  NOT NULL,
    [convenios_idConvenio] int  NOT NULL
);
GO

-- Creating table 'medicosSet'
CREATE TABLE [dbo].[medicosSet] (
    [idMedico] int IDENTITY(1,1) NOT NULL,
    [nome] nvarchar(50)  NOT NULL,
    [email] nvarchar(60)  NOT NULL,
    [senha] nvarchar(18)  NOT NULL,
    [preco] nvarchar(max)  NULL,
    [situacao] smallint  NOT NULL,
    [especialidades_idEspecialidade] int  NOT NULL,
    [clinicas_idClinica] int  NOT NULL,
    [adm] smallint  NOT NULL
);
GO

-- Creating table 'clinicasSet'
CREATE TABLE [dbo].[clinicasSet] (
    [idClinica] int IDENTITY(1,1) NOT NULL,
    [nome] nvarchar(50)  NOT NULL,
    [cnpj] nvarchar(max)  NOT NULL,
    [endereco] nvarchar(50)  NOT NULL,
    [complemento] nvarchar(40)  NULL,
    [bairro] nvarchar(30)  NOT NULL,
    [cep] nvarchar(max)  NOT NULL,
    [cidade] nvarchar(50)  NOT NULL,
    [estadoClinica] varchar(25)  NULL,
    [email] nvarchar(60)  NOT NULL,
    [telefone] nvarchar(max)  NOT NULL,
    [fax] nvarchar(max)  NULL,
    [hrAtendimento] varchar(30)  NULL,
    [duracaoConsultas] int  NOT NULL,
    [especialidades] nvarchar(max)  NOT NULL,
    [convenios] varchar(50)  NULL
);
GO

-- Creating table 'pagamentosSet'
CREATE TABLE [dbo].[pagamentosSet] (
    [idPagamento] int IDENTITY(1,1) NOT NULL,
    [valor] float  NOT NULL,
    [data] datetime  NOT NULL,
    [descontos] float  NULL,
    [consultas_idConsulta] int  NOT NULL,
    [contasBancarias_idContasBancarias] int  NOT NULL
);
GO

-- Creating table 'conveniosSet'
CREATE TABLE [dbo].[conveniosSet] (
    [idConvenio] int IDENTITY(1,1) NOT NULL,
    [descricao] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'consultasSet'
CREATE TABLE [dbo].[consultasSet] (
    [idConsulta] int IDENTITY(1,1) NOT NULL,
    [data] nvarchar(max)  NOT NULL,
    [horario] nvarchar(max)  NOT NULL,
    [check_in] int  NULL,
    [check_out] int  NULL,
    [pacientes_idPaciente] int  NOT NULL,
    [medicos_idMedico] int  NOT NULL,
    [clinicas_idClinica] int  NOT NULL
);
GO

-- Creating table 'especialidadesSet'
CREATE TABLE [dbo].[especialidadesSet] (
    [idEspecialidade] int IDENTITY(1,1) NOT NULL,
    [ds_especialidade] varchar(50)  NULL
);
GO

-- Creating table 'avaliacoes'
CREATE TABLE [dbo].[avaliacoes] (
    [idAvaliacao] int IDENTITY(1,1) NOT NULL,
    [notas] int  NOT NULL,
    [comentarios] varchar(100)  NULL,
    [media] float  NOT NULL,
    [data] datetime  NOT NULL,
    [medicos_idMedico] int  NOT NULL,
    [pacientes_idPaciente] int  NOT NULL
);
GO

-- Creating table 'contasBancariasSet'
CREATE TABLE [dbo].[contasBancariasSet] (
    [idContasBancarias] int IDENTITY(1,1) NOT NULL,
    [agencia] nvarchar(max)  NOT NULL,
    [digitoAgencia] nvarchar(max)  NOT NULL,
    [cedente] nvarchar(max)  NOT NULL,
    [digitoCedente] nvarchar(max)  NOT NULL,
    [carteira] nvarchar(max)  NOT NULL,
    [contrato] nvarchar(max)  NOT NULL,
    [convenio] nvarchar(max)  NOT NULL,
    [clinicas_idClinica] int  NOT NULL
);
GO

-- Creating table 'sysdiagrams'
CREATE TABLE [dbo].[sysdiagrams] (
    [name] nvarchar(128)  NOT NULL,
    [principal_id] int  NOT NULL,
    [diagram_id] int IDENTITY(1,1) NOT NULL,
    [version] int  NULL,
    [definition] varbinary(max)  NULL
);
GO

-- Creating table 'agendaSet'
CREATE TABLE [dbo].[agendaSet] (
    [idAgenda] int IDENTITY(1,1) NOT NULL,
    [data] nvarchar(max)  NOT NULL,
    [horarioAtendimento] nvarchar(max)  NOT NULL,
    [horarioAgendado] nvarchar(max)  NULL,
    [medicos_idMedico] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [idPaciente] in table 'pacientesSet'
ALTER TABLE [dbo].[pacientesSet]
ADD CONSTRAINT [PK_pacientesSet]
    PRIMARY KEY CLUSTERED ([idPaciente] ASC);
GO

-- Creating primary key on [idMedico] in table 'medicosSet'
ALTER TABLE [dbo].[medicosSet]
ADD CONSTRAINT [PK_medicosSet]
    PRIMARY KEY CLUSTERED ([idMedico] ASC);
GO

-- Creating primary key on [idClinica] in table 'clinicasSet'
ALTER TABLE [dbo].[clinicasSet]
ADD CONSTRAINT [PK_clinicasSet]
    PRIMARY KEY CLUSTERED ([idClinica] ASC);
GO

-- Creating primary key on [idPagamento] in table 'pagamentosSet'
ALTER TABLE [dbo].[pagamentosSet]
ADD CONSTRAINT [PK_pagamentosSet]
    PRIMARY KEY CLUSTERED ([idPagamento] ASC);
GO

-- Creating primary key on [idConvenio] in table 'conveniosSet'
ALTER TABLE [dbo].[conveniosSet]
ADD CONSTRAINT [PK_conveniosSet]
    PRIMARY KEY CLUSTERED ([idConvenio] ASC);
GO

-- Creating primary key on [idConsulta] in table 'consultasSet'
ALTER TABLE [dbo].[consultasSet]
ADD CONSTRAINT [PK_consultasSet]
    PRIMARY KEY CLUSTERED ([idConsulta] ASC);
GO

-- Creating primary key on [idEspecialidade] in table 'especialidadesSet'
ALTER TABLE [dbo].[especialidadesSet]
ADD CONSTRAINT [PK_especialidadesSet]
    PRIMARY KEY CLUSTERED ([idEspecialidade] ASC);
GO

-- Creating primary key on [idAvaliacao] in table 'avaliacoes'
ALTER TABLE [dbo].[avaliacoes]
ADD CONSTRAINT [PK_avaliacoes]
    PRIMARY KEY CLUSTERED ([idAvaliacao] ASC);
GO

-- Creating primary key on [idContasBancarias] in table 'contasBancariasSet'
ALTER TABLE [dbo].[contasBancariasSet]
ADD CONSTRAINT [PK_contasBancariasSet]
    PRIMARY KEY CLUSTERED ([idContasBancarias] ASC);
GO

-- Creating primary key on [diagram_id] in table 'sysdiagrams'
ALTER TABLE [dbo].[sysdiagrams]
ADD CONSTRAINT [PK_sysdiagrams]
    PRIMARY KEY CLUSTERED ([diagram_id] ASC);
GO

-- Creating primary key on [idAgenda] in table 'agendaSet'
ALTER TABLE [dbo].[agendaSet]
ADD CONSTRAINT [PK_agendaSet]
    PRIMARY KEY CLUSTERED ([idAgenda] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [pacientes_idPaciente] in table 'consultasSet'
ALTER TABLE [dbo].[consultasSet]
ADD CONSTRAINT [FK_pacientesconsultas]
    FOREIGN KEY ([pacientes_idPaciente])
    REFERENCES [dbo].[pacientesSet]
        ([idPaciente])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_pacientesconsultas'
CREATE INDEX [IX_FK_pacientesconsultas]
ON [dbo].[consultasSet]
    ([pacientes_idPaciente]);
GO

-- Creating foreign key on [convenios_idConvenio] in table 'pacientesSet'
ALTER TABLE [dbo].[pacientesSet]
ADD CONSTRAINT [FK_conveniospacientes]
    FOREIGN KEY ([convenios_idConvenio])
    REFERENCES [dbo].[conveniosSet]
        ([idConvenio])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_conveniospacientes'
CREATE INDEX [IX_FK_conveniospacientes]
ON [dbo].[pacientesSet]
    ([convenios_idConvenio]);
GO

-- Creating foreign key on [medicos_idMedico] in table 'consultasSet'
ALTER TABLE [dbo].[consultasSet]
ADD CONSTRAINT [FK_medicosconsultas]
    FOREIGN KEY ([medicos_idMedico])
    REFERENCES [dbo].[medicosSet]
        ([idMedico])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_medicosconsultas'
CREATE INDEX [IX_FK_medicosconsultas]
ON [dbo].[consultasSet]
    ([medicos_idMedico]);
GO

-- Creating foreign key on [medicos_idMedico] in table 'avaliacoes'
ALTER TABLE [dbo].[avaliacoes]
ADD CONSTRAINT [FK_medicosavaliacoes]
    FOREIGN KEY ([medicos_idMedico])
    REFERENCES [dbo].[medicosSet]
        ([idMedico])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_medicosavaliacoes'
CREATE INDEX [IX_FK_medicosavaliacoes]
ON [dbo].[avaliacoes]
    ([medicos_idMedico]);
GO

-- Creating foreign key on [especialidades_idEspecialidade] in table 'medicosSet'
ALTER TABLE [dbo].[medicosSet]
ADD CONSTRAINT [FK_especialidadesmedicos]
    FOREIGN KEY ([especialidades_idEspecialidade])
    REFERENCES [dbo].[especialidadesSet]
        ([idEspecialidade])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_especialidadesmedicos'
CREATE INDEX [IX_FK_especialidadesmedicos]
ON [dbo].[medicosSet]
    ([especialidades_idEspecialidade]);
GO

-- Creating foreign key on [clinicas_idClinica] in table 'contasBancariasSet'
ALTER TABLE [dbo].[contasBancariasSet]
ADD CONSTRAINT [FK_clinicascontasBancarias]
    FOREIGN KEY ([clinicas_idClinica])
    REFERENCES [dbo].[clinicasSet]
        ([idClinica])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_clinicascontasBancarias'
CREATE INDEX [IX_FK_clinicascontasBancarias]
ON [dbo].[contasBancariasSet]
    ([clinicas_idClinica]);
GO

-- Creating foreign key on [clinicas_idClinica] in table 'consultasSet'
ALTER TABLE [dbo].[consultasSet]
ADD CONSTRAINT [FK_clinicasconsultas]
    FOREIGN KEY ([clinicas_idClinica])
    REFERENCES [dbo].[clinicasSet]
        ([idClinica])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_clinicasconsultas'
CREATE INDEX [IX_FK_clinicasconsultas]
ON [dbo].[consultasSet]
    ([clinicas_idClinica]);
GO

-- Creating foreign key on [contasBancarias_idContasBancarias] in table 'pagamentosSet'
ALTER TABLE [dbo].[pagamentosSet]
ADD CONSTRAINT [FK_contasBancariaspagamentos]
    FOREIGN KEY ([contasBancarias_idContasBancarias])
    REFERENCES [dbo].[contasBancariasSet]
        ([idContasBancarias])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_contasBancariaspagamentos'
CREATE INDEX [IX_FK_contasBancariaspagamentos]
ON [dbo].[pagamentosSet]
    ([contasBancarias_idContasBancarias]);
GO

-- Creating foreign key on [clinicas_idClinica] in table 'medicosSet'
ALTER TABLE [dbo].[medicosSet]
ADD CONSTRAINT [FK_clinicasmedicos]
    FOREIGN KEY ([clinicas_idClinica])
    REFERENCES [dbo].[clinicasSet]
        ([idClinica])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_clinicasmedicos'
CREATE INDEX [IX_FK_clinicasmedicos]
ON [dbo].[medicosSet]
    ([clinicas_idClinica]);
GO

-- Creating foreign key on [medicos_idMedico] in table 'agendaSet'
ALTER TABLE [dbo].[agendaSet]
ADD CONSTRAINT [FK_medicosagenda]
    FOREIGN KEY ([medicos_idMedico])
    REFERENCES [dbo].[medicosSet]
        ([idMedico])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_medicosagenda'
CREATE INDEX [IX_FK_medicosagenda]
ON [dbo].[agendaSet]
    ([medicos_idMedico]);
GO

-- Creating foreign key on [pacientes_idPaciente] in table 'avaliacoes'
ALTER TABLE [dbo].[avaliacoes]
ADD CONSTRAINT [FK_pacientesavaliacoes]
    FOREIGN KEY ([pacientes_idPaciente])
    REFERENCES [dbo].[pacientesSet]
        ([idPaciente])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_pacientesavaliacoes'
CREATE INDEX [IX_FK_pacientesavaliacoes]
ON [dbo].[avaliacoes]
    ([pacientes_idPaciente]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------