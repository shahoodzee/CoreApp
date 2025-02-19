SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Hamza>
-- Create date: <14/March/2024>
-- Description:	<Use to fetch detail of ticket>
-- =============================================
ALTER PROCEDURE [dbo].[sp_FetchServiceRequestDetail]  
	@iTicketId bigint = -1
AS
BEGIN
	
	SET NOCOUNT ON;

    Select 
	tt.Id [TicketID], 
	tt.TicketNumber [ServiceRequestNumber], 
	tt.Title[Title], 
	tt.Description[Description],
	tt.vTags[Tags],
	ISNULL(tt.notes, null) [Notes],
	tp.PriorityText[Severity],
	tt.iTicketStatus[iStatusId],
	ts.Status[Status],
	tt.CreatedDate [CreatedDate], 
	tt.iCreatedBy [CreatedBy], 
	tt.iCreatedByAdmin [CreatedByAdmin], 
	ISNULL(tt.ModifyDate,'1/1/1900') [ModifyDate],
	td.Id [TicketDetailId],
	td.DetailTitle [DetailTitle],
	td.DetailDescription [DetailDescription],
	ISNULL(tsd.Status,'Invalid') [DetailStatus],
	ISNULL(td.iTicketDetailStatus,-1) [iDetailStatusId],
	td.iAssignedResource [iResourceId], 
	td.HourDeduction [HourDeduction], 
	td.isResourceGroupAssigned [isResourceGroup], 
	td.iResourceGroupId [iResourceGroupId],
	ISNULL(td.iCreatedBy, '') [detailCreatedBy]
	from Tickets tt
	left outer join TicketDetails td on tt.Id = td.TicketId
	left outer join TicketPriorities tp on tt.iPriority = tp.Id
	left outer join TicketStatuses ts on tt.iTicketStatus = ts.Id 
	left outer join TicketStatuses tsd on td.iTicketDetailStatus = tsd.Id
	where tt.id = @iTicketId

END
