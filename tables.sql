create table contents
(
    ContentId      int identity
        constraint contents_pk
            primary key nonclustered,
    ContentType    varchar(128)  not null,
    ContentBody    nvarchar(max) not null,
    ContentOwnerId int,
    Date           bigint        not null
)
go

create unique index contents_ContentId_uindex
    on contents (ContentId)
go

create table users
(
    UserId       int identity
        constraint users_pk
            primary key nonclustered,
    Username     varchar(128) not null,
    UserPassword varchar(40)  not null,
    UserRoleId   int
)
go

create unique index users_Username_uindex
    on users (Username)
go

create unique index users_UserId_uindex
    on users (UserId)
go

