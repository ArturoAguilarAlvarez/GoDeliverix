CREATE TABLE [Notifications](
    [Uid] UNIQUEIDENTIFIER PRIMARY KEY NOT NULL,
    [Target] INT NOT NULL,
    [TargetUid] UNIQUEIDENTIFIER,
    [JsonTitle] VARCHAR(128),
    [JsonContent] VARCHAR(255),
    [Type] INT NOT NULL,
    [EntityTypeUid] UNIQUEIDENTIFIER,
)