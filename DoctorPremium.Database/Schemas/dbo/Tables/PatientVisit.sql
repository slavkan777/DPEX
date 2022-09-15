CREATE TABLE [dbo].[PatientVisits] (
    [PatientVisitId]                   INT              IDENTITY (1, 1) NOT NULL,
    [PatientId]            INT              NOT NULL,
    [VisitDate]            DATE             NOT NULL,
    [VisitStartTime]       TIME (7)         NOT NULL,
    --[VisitEndTime]         TIME (7)         NULL,
    --[StatusId]             INT              NOT NULL,
	[Duration]             INT              NULL,
    [PurposeOfVisit]       NVARCHAR (300)   NULL,
    [SubjectiveComplaints]            NVARCHAR (500)   NULL,
    [AdditionalResearch]           NVARCHAR (500)   NULL,
    --[Diagnosis]            NVARCHAR (4000)   NULL,
    --[Occlusion]            NVARCHAR (MAX)   NULL,
    --[ConditionOralHygiene] NVARCHAR (MAX)   NULL,
    [LaboratoryResearch]                 NVARCHAR (500)   NULL,
    --[ColorScaleVita]       NVARCHAR (MAX)   NULL,
    --[Epicrisis]            NVARCHAR (MAX)   NULL,
    [ClinicalDiagnosis]    NVARCHAR (500)   NULL,
    [Treatment]            NVARCHAR (400)   NULL,
    [DoctorAssignments]          NVARCHAR (400)   NULL,
	[NextVizitRecordId]    INT NULL,
	--[ServiceCost]          NVARCHAR (MAX)   NULL,
	--[Comment]          NVARCHAR (MAX)   NULL,
    [CreateDateUtc]   DATETIME         NOT NULL,
    [UpdateDateUtc]   DATETIME         NULL,
	[IsDeleted]		BIT NOT NULL, 
    CONSTRAINT [PK_PatientVisit] PRIMARY KEY CLUSTERED ([PatientVisitId] ASC),
    CONSTRAINT [FK_PatientVisit_Patients] FOREIGN KEY ([PatientId]) REFERENCES [dbo].[Patients] ([PatientId])
	--CONSTRAINT [FK_PatientVisit_ScheduleRecord] FOREIGN KEY ([ScheduleRecordId]) REFERENCES [dbo].[ScheduleRecords] ([ScheduleRecordId])
    --CONSTRAINT [FK_PatientVisit_VisitStatus] FOREIGN KEY ([StatusId]) REFERENCES [dbo].[VisitStatus] ([Id])
);


--GO
--EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Цель визита', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PatientVisit', @level2type = N'COLUMN', @level2name = N'PurposeOfVisit';


--GO
--EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Субъективные жалобы', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PatientVisit', @level2type = N'COLUMN', @level2name = N'Objective';


--GO
--EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Дополнительные исследования', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PatientVisit', @level2type = N'COLUMN', @level2name = N'Additional';


--GO
--EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Прикус', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PatientVisit', @level2type = N'COLUMN', @level2name = N'Occlusion';


--GO
--EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Состояние гигиена полости рта, слизистой оболочки ротовой полости, десна, альвеолярных отростков и неба индекс ГІ и РМА', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PatientVisit', @level2type = N'COLUMN', @level2name = N'ConditionOralHygiene';


--GO
--EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Данные рентгеновских обследований, лабораторных исследований.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PatientVisit', @level2type = N'COLUMN', @level2name = N'XRay';


--GO
--EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Цвет по шкале "Вита"', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PatientVisit', @level2type = N'COLUMN', @level2name = N'ColorScaleVita';


--GO
--EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Эпикриз', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PatientVisit', @level2type = N'COLUMN', @level2name = N'Epicrisis';


--GO
--EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Клинический диагноз', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PatientVisit', @level2type = N'COLUMN', @level2name = N'ClinicalDiagnosis';


--GO
--EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Проведенное лечение', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PatientVisit', @level2type = N'COLUMN', @level2name = N'Treatment';


--GO
--EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Назначения врача', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PatientVisit', @level2type = N'COLUMN', @level2name = N'Prescribing';

