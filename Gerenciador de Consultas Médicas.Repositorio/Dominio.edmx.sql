
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 07/11/2016 20:51:16
-- Generated from EDMX file: C:\Visual Studio 2015\Projects\Gerenciador de Consultas Médicas\Gerenciador de Consultas Médicas.Repositorio\Dominio.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Consultas medicas];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------


-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'pacientesSet'
CREATE TABLE [dbo].[pacientesSet] (
    [idPaciente] int IDENTITY(1,1) NOT NULL,
    [nome] nvarchar(50)  NOT NULL,
    [idade] smallint  NOT NULL,
    [dataNasc] datetime  NOT NULL,
    [cpf] int  NOT NULL,
    [rg] int  NOT NULL,
    [endereco] nvarchar(50)  NOT NULL,
    [cidade] nvarchar(40)  NOT NULL,
    [estado] nvarchar(30)  NOT NULL,
    [estadoCivil] nvarchar(20)  NULL,
    [telefone] int  NOT NULL,
    [celular] int  NULL,
    [email] nvarchar(60)  NOT NULL,
    [senhaAcesso] nvarchar(18)  NOT NULL
);
GO

-- Creating table 'medicosSet'
CREATE TABLE [dbo].[medicosSet] (
    [idMedico] int IDENTITY(1,1) NOT NULL,
    [nome] nvarchar(50)  NOT NULL,
    [email] nvarchar(60)  NOT NULL,
    [senhaAcesso] nvarchar(18)  NOT NULL,
    [idade] int  NOT NULL,
    [notas] int  NULL,
    [comentários] nvarchar(300)  NULL,
    [media] nvarchar(10)  NULL,
    [especialidades_idEspecialidade] int  NOT NULL
);
GO

-- Creating table 'clinicasSet'
CREATE TABLE [dbo].[clinicasSet] (
    [idClinica] int IDENTITY(1,1) NOT NULL,
    [nome] nvarchar(50)  NOT NULL,
    [cnpj] int  NOT NULL,
    [ds_especialidades] nvarchar(50)  NOT NULL,
    [endereco] nvarchar(50)  NOT NULL,
    [complemento] nvarchar(40)  NULL,
    [bairro] nvarchar(30)  NOT NULL,
    [cep] int  NOT NULL,
    [cidade] nvarchar(50)  NOT NULL,
    [uf] nvarchar(2)  NOT NULL,
    [email] nvarchar(60)  NOT NULL,
    [telefone] int  NOT NULL,
    [fax] int  NULL,
    [hrFuncionamento] nvarchar(30)  NULL
);
GO

-- Creating table 'pagamentosSet'
CREATE TABLE [dbo].[pagamentosSet] (
    [idPagamento] int IDENTITY(1,1) NOT NULL,
    [tipoPag] nvarchar(20)  NOT NULL,
    [valorTotal] float  NOT NULL,
    [dataPgto] datetime  NOT NULL,
    [tipoParcVista] nvarchar(20)  NULL,
    [qtdeParcelas] smallint  NULL,
    [conta] int  NULL,
    [agencia] int  NULL,
    [cartaoCred] int  NULL,
    [descontos] float  NULL,
    [consultas_idConsulta] int  NOT NULL,
    [convenios_idConvenio] int  NOT NULL
);
GO

-- Creating table 'conveniosSet'
CREATE TABLE [dbo].[conveniosSet] (
    [idConvenio] int IDENTITY(1,1) NOT NULL,
    [nome] nvarchar(50)  NOT NULL,
    [planoSaude] nvarchar(50)  NULL,
    [inicioPlano] datetime  NOT NULL,
    [telefone] int  NOT NULL,
    [email] nvarchar(60)  NOT NULL,
    [validade] datetime  NOT NULL,
    [rede] nvarchar(50)  NULL,
    [acomodacao] nvarchar(50)  NULL,
    [site] nvarchar(50)  NULL
);
GO

-- Creating table 'consultasSet'
CREATE TABLE [dbo].[consultasSet] (
    [idConsulta] int IDENTITY(1,1) NOT NULL,
    [dataConsulta] datetime  NOT NULL,
    [horario] time  NOT NULL,
    [check_in] int  NULL,
    [check_out] int  NULL,
    [pacientes_idPaciente] int  NOT NULL,
    [medicos_idMedico] int  NOT NULL,
    [clinicas_idClinica] int  NOT NULL
);
GO

-- Creating table 'clinicas_has_medicosSet'
CREATE TABLE [dbo].[clinicas_has_medicosSet] (
    [horarioTrabalho] nvarchar(30)  NOT NULL,
    [diasSemana] nvarchar(30)  NOT NULL,
    [medicos_idMedico] int  NOT NULL,
    [clinicas_idClinica] int  NOT NULL
);
GO

-- Creating table 'especialidadesSet'
CREATE TABLE [dbo].[especialidadesSet] (
    [idEspecialidade] int IDENTITY(1,1) NOT NULL,
    [ds_especialidade] nvarchar(30)  NOT NULL
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

-- Creating primary key on [clinicas_idClinica], [medicos_idMedico] in table 'clinicas_has_medicosSet'
ALTER TABLE [dbo].[clinicas_has_medicosSet]
ADD CONSTRAINT [PK_clinicas_has_medicosSet]
    PRIMARY KEY CLUSTERED ([clinicas_idClinica], [medicos_idMedico] ASC);
GO

-- Creating primary key on [idEspecialidade] in table 'especialidadesSet'
ALTER TABLE [dbo].[especialidadesSet]
ADD CONSTRAINT [PK_especialidadesSet]
    PRIMARY KEY CLUSTERED ([idEspecialidade] ASC);
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

-- Creating foreign key on [consultas_idConsulta] in table 'pagamentosSet'
ALTER TABLE [dbo].[pagamentosSet]
ADD CONSTRAINT [FK_consultaspagamentos]
    FOREIGN KEY ([consultas_idConsulta])
    REFERENCES [dbo].[consultasSet]
        ([idConsulta])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_consultaspagamentos'
CREATE INDEX [IX_FK_consultaspagamentos]
ON [dbo].[pagamentosSet]
    ([consultas_idConsulta]);
GO

-- Creating foreign key on [convenios_idConvenio] in table 'pagamentosSet'
ALTER TABLE [dbo].[pagamentosSet]
ADD CONSTRAINT [FK_conveniospagamentos]
    FOREIGN KEY ([convenios_idConvenio])
    REFERENCES [dbo].[conveniosSet]
        ([idConvenio])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_conveniospagamentos'
CREATE INDEX [IX_FK_conveniospagamentos]
ON [dbo].[pagamentosSet]
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

-- Creating foreign key on [medicos_idMedico] in table 'clinicas_has_medicosSet'
ALTER TABLE [dbo].[clinicas_has_medicosSet]
ADD CONSTRAINT [FK_medicosclinicas_has_medicos]
    FOREIGN KEY ([medicos_idMedico])
    REFERENCES [dbo].[medicosSet]
        ([idMedico])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_medicosclinicas_has_medicos'
CREATE INDEX [IX_FK_medicosclinicas_has_medicos]
ON [dbo].[clinicas_has_medicosSet]
    ([medicos_idMedico]);
GO

-- Creating foreign key on [clinicas_idClinica] in table 'clinicas_has_medicosSet'
ALTER TABLE [dbo].[clinicas_has_medicosSet]
ADD CONSTRAINT [FK_clinicasclinicas_has_medicos]
    FOREIGN KEY ([clinicas_idClinica])
    REFERENCES [dbo].[clinicasSet]
        ([idClinica])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
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

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------