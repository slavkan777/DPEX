CREATE TABLE [dbo].[PatientVisitPayments] (
    [PatientVisitPaymentId]             INT              IDENTITY (1, 1) NOT NULL,
    --[PatientId]      INT              NOT NULL,
    [PatientVisitId] INT              NOT NULL,
    [CostOfServices] MONEY            NULL,
    [Paid]           MONEY            NULL,
    [Comment]        NVARCHAR (200)  NULL,
    [CreateDateUtc]   DATETIME         NOT NULL,
    [UpdateDateUtc]   DATETIME         NULL,
    CONSTRAINT [PK_PatientVisitPayment] PRIMARY KEY CLUSTERED ([PatientVisitPaymentId] ASC),
    --CONSTRAINT [FK_PatientVisitPayment_PatientInfo] FOREIGN KEY ([PatientId]) REFERENCES [dbo].[PatientInfo] ([Id]),
    CONSTRAINT [FK_PatientVisitPayment_PatientVisit] FOREIGN KEY ([PatientVisitId]) REFERENCES [dbo].[PatientVisits] ([PatientVisitId])
);
GO
CREATE UNIQUE NONCLUSTERED INDEX [PatientVisitPayment_PatientVisitId]
    ON [dbo].[PatientVisitPayments]([PatientVisitId] ASC);
