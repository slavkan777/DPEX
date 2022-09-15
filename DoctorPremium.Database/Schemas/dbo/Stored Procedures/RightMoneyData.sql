--USE [DoctorPremium]
--GO
--/****** Object:  StoredProcedure [dbo].[RightMoneyData]    Script Date: 27.06.2016 18:44:49 ******/
--SET ANSI_NULLS ON
--GO
--SET QUOTED_IDENTIFIER ON
--GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[RightMoneyData]
	@DataUserIdForMany int


AS
BEGIN
declare @one1 money = 0
 declare @one2 money = 0
 declare @seven1  money = 0
declare  @seven2 money = 0
declare @thirty1 money = 0
declare @thirty2 money = 0
declare @sixty1 money = 0
 declare @sixty2 money = 0
 declare @ninety1 money = 0
 declare @ninety2 money = 0
 declare @oneHundredTwenty1 money = 0
 declare @oneHundredTwenty2 money = 0
 declare @oneHundredEighty1 money = 0
  declare @oneHundredEighty2 money = 0
    DECLARE @deleteDay bit
  SET @deleteDay = 'false'

  set @one1 = (SELECT SUM(CostOfFeautureServices)  from  ScheduleRecords where  RecordDate >= CAST(GETDATE() AS DATE) and RecordDate < DATEADD(day,0, getdate()) and userid=@DataUserIdForMany) 
  set @one2 = (SELECT SUM(CostOfServices)  from  PatientVisitPayments INNER JOIN PatientVisits pv ON PatientVisitPayments.PatientVisitId = pv.PatientVisitId and pv.IsDeleted = @deleteDay
   INNER JOIN Patients p ON pv.PatientId = p.PatientId  where VisitDate <= CAST(GETDATE() AS DATE) and VisitDate > DATEADD(day,-1, getdate()) and p.userid=@DataUserIdForMany )  
	
 set @seven1 = (SELECT SUM(CostOfFeautureServices)  from  ScheduleRecords where  RecordDate >= CAST(GETDATE() AS DATE) and RecordDate < DATEADD(day,7, getdate()) and userid=@DataUserIdForMany) 
 set @seven2 = (SELECT SUM(CostOfServices)  from  PatientVisitPayments INNER JOIN PatientVisits pv ON PatientVisitPayments.PatientVisitId = pv.PatientVisitId and pv.IsDeleted = @deleteDay
   INNER JOIN Patients p ON pv.PatientId = p.PatientId  where VisitDate <= CAST(GETDATE() AS DATE) and VisitDate > DATEADD(day,-7, getdate()) and p.userid=@DataUserIdForMany)  

 set @thirty1 = (SELECT SUM(CostOfFeautureServices)  from  ScheduleRecords where  RecordDate >= CAST(GETDATE() AS DATE) and RecordDate < DATEADD(day,30, getdate()) and userid=@DataUserIdForMany) 
 set @thirty2 = (SELECT SUM(CostOfServices)  from  PatientVisitPayments INNER JOIN PatientVisits pv ON PatientVisitPayments.PatientVisitId = pv.PatientVisitId and pv.IsDeleted = @deleteDay
   INNER JOIN Patients p ON pv.PatientId = p.PatientId  where VisitDate <= CAST(GETDATE() AS DATE) and VisitDate > DATEADD(day,-30, getdate()) and p.userid=@DataUserIdForMany)  

 set @sixty1 = (SELECT SUM(CostOfFeautureServices)  from  ScheduleRecords where  RecordDate >= CAST(GETDATE() AS DATE) and RecordDate < DATEADD(day,60, getdate()) and userid=@DataUserIdForMany) 
 set @sixty2 = (SELECT SUM(CostOfServices)  from  PatientVisitPayments INNER JOIN PatientVisits pv ON PatientVisitPayments.PatientVisitId = pv.PatientVisitId and pv.IsDeleted = @deleteDay
   INNER JOIN Patients p ON pv.PatientId = p.PatientId  where VisitDate <= CAST(GETDATE() AS DATE) and VisitDate > DATEADD(day,-60, getdate()) and p.userid=@DataUserIdForMany)  
	
set @ninety1 = (SELECT SUM(CostOfFeautureServices)  from  ScheduleRecords where  RecordDate >= CAST(GETDATE() AS DATE) and RecordDate < DATEADD(day,90, getdate()) and userid=@DataUserIdForMany) 
 set @ninety2 = (SELECT SUM(CostOfServices)  from  PatientVisitPayments INNER JOIN PatientVisits pv ON PatientVisitPayments.PatientVisitId = pv.PatientVisitId and pv.IsDeleted = @deleteDay
   INNER JOIN Patients p ON pv.PatientId = p.PatientId  where VisitDate <= CAST(GETDATE() AS DATE) and VisitDate > DATEADD(day,-90, getdate()) and p.userid=@DataUserIdForMany)  

   set @oneHundredTwenty1 = (SELECT SUM(CostOfFeautureServices)  from  ScheduleRecords where  RecordDate >= CAST(GETDATE() AS DATE) and RecordDate < DATEADD(day,120, getdate()) and userid=@DataUserIdForMany) 
 set @oneHundredTwenty2 = (SELECT SUM(CostOfServices)  from  PatientVisitPayments INNER JOIN PatientVisits pv ON PatientVisitPayments.PatientVisitId = pv.PatientVisitId  and pv.IsDeleted = @deleteDay
   INNER JOIN Patients p ON pv.PatientId = p.PatientId  where VisitDate <= CAST(GETDATE() AS DATE) and VisitDate > DATEADD(day,-120, getdate()) and p.userid=@DataUserIdForMany)  

   
   set @oneHundredEighty1 = (SELECT SUM(CostOfFeautureServices)  from  ScheduleRecords where  RecordDate >= CAST(GETDATE() AS DATE) and RecordDate < DATEADD(day,180, getdate()) and userid=@DataUserIdForMany) 
 set @oneHundredEighty2 = (SELECT SUM(CostOfServices)  from  PatientVisitPayments INNER JOIN PatientVisits pv ON PatientVisitPayments.PatientVisitId = pv.PatientVisitId and pv.IsDeleted = @deleteDay
   INNER JOIN Patients p ON pv.PatientId = p.PatientId  where VisitDate <= CAST(GETDATE() AS DATE) and VisitDate > DATEADD(day,-180, getdate()) and p.userid=@DataUserIdForMany)  

	SELECT   @one1 as one1,   @one2 as one2, @seven1 as seven1 , @seven2 as  seven2 , @thirty1 as thirty1, @thirty2 as  thirty2, @sixty1 as sixty1 , @sixty2 as sixty2 , @ninety1 as ninety1, @ninety2 as ninety2, @oneHundredTwenty1 as oneHundredTwenty1, @oneHundredTwenty2 as oneHundredTwenty2, @oneHundredEighty1 as oneHundredEighty1, @oneHundredEighty2 as oneHundredEighty2 
END
--RETURN 0