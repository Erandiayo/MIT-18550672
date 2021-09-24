Alter  TABLE [dbo].[SubTasks]  add [EmpId] int  NULL
    
Alter  TABLE [dbo].[SubTasks]  add	[DailyPayId] int  NULL

-- Creating foreign key on [EmpId] in table 'SubTasks'
ALTER TABLE [dbo].[SubTasks]
ADD CONSTRAINT [FK_EmployeeSubTask]
    FOREIGN KEY ([EmpId])
    REFERENCES [dbo].[Employees]
        ([EmployeeID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EmployeeSubTask'
CREATE INDEX [IX_FK_EmployeeSubTask]
ON [dbo].[SubTasks]
    ([EmpId]);
GO

-- Creating foreign key on [DailyPayId] in table 'SubTasks'
ALTER TABLE [dbo].[SubTasks]
ADD CONSTRAINT [FK_DailyPayStudentsSubTask]
    FOREIGN KEY ([DailyPayId])
    REFERENCES [dbo].[DailyPayStudents]
        ([DailyPayStudID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DailyPayStudentsSubTask'
CREATE INDEX [IX_FK_DailyPayStudentsSubTask]
ON [dbo].[SubTasks]
    ([DailyPayId]);
GO


Alter  TABLE [dbo].[PunchMembers]  add	[SponsStudID] int  NULL

-- Creating foreign key on [SponsStudID] in table 'PunchMembers'
ALTER TABLE [dbo].[PunchMembers]
ADD CONSTRAINT [FK_SponsorPayStudentPunchMember]
    FOREIGN KEY ([SponsStudID])
    REFERENCES [dbo].[SponsorPayStudents]
        ([SponsStudentID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SponsorPayStudentPunchMember'
CREATE INDEX [IX_FK_SponsorPayStudentPunchMember]
ON [dbo].[PunchMembers]
    ([SponsStudID]);
GO

Alter TABLE [dbo].[SponsorPayStudent]  add	[PunchNo] int NOT NULL


Alter  TABLE [dbo].[SubTaskStatusLogs]  add [EmpId] int  NULL
    
Alter  TABLE [dbo].[SubTaskStatusLogs]  add	[DailyPayId] int  NULL

-- Creating foreign key on [EmpId] in table 'SubTaskStatusLogs'
ALTER TABLE [dbo].[SubTaskStatusLogs]
ADD CONSTRAINT [FK_EmployeeSubTaskStatusLog]
    FOREIGN KEY ([EmpId])
    REFERENCES [dbo].[Employees]
        ([EmployeeID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EmployeeSubTaskStatusLog'
CREATE INDEX [IX_FK_EmployeeSubTaskStatusLog]
ON [dbo].[SubTaskStatusLogs]
    ([EmpId]);
GO

-- Creating foreign key on [DailyPayId] in table 'SubTaskStatusLogs'
ALTER TABLE [dbo].[SubTaskStatusLogs]
ADD CONSTRAINT [FK_DailyPayStudentsSubTaskStatusLog]
    FOREIGN KEY ([DailyPayId])
    REFERENCES [dbo].[DailyPayStudents]
        ([DailyPayStudID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DailyPayStudentsSubTaskStatusLog'
CREATE INDEX [IX_FK_DailyPayStudentsSubTaskStatusLog]
ON [dbo].[SubTaskStatusLogs]
    ([DailyPayId]);
GO

ALTER TABLE [dbo].[SubTaskStatusLogs] ALTER COLUMN [AssignToMemId] int  NULL

ALTER TABLE [dbo].[ProcuremenetRequests]  ADD [CompletedDate] datetime  NULL

ALTER TABLE [dbo].[KPIDateSetups]  ADD [IsWFHMarksConfirmed] bit  NULL