EXECUTE sp_addrolemember @rolename = N'db_datawriter', @membername = N'BLADEX\npascual';


GO
EXECUTE sp_addrolemember @rolename = N'db_datareader', @membername = N'BLADEX\npascual';


GO
EXECUTE sp_addrolemember @rolename = N'db_backupoperator', @membername = N'BLADEX\npascual';


GO
EXECUTE sp_addrolemember @rolename = N'db_accessadmin', @membername = N'BLADEX\npascual';

