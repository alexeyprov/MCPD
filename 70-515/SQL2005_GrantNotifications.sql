GRANT CREATE PROCEDURE TO [EPUSPRIW0081\kosh];

GRANT CREATE SERVICE TO [EPUSPRIW0081\kosh];

GRANT CREATE QUEUE TO [EPUSPRIW0081\kosh];

GRANT REFERENCES ON CONTRACT::[http://schemas.microsoft.com/SQL/Notifications/PostQueryNotification] TO [EPUSPRIW0081\kosh];

GRANT SUBSCRIBE QUERY NOTIFICATIONS TO [EPUSPRIW0081\kosh];

--GRANT CONTROL ON SCHEMA::[dbo] TO [SqlUser];

--GRANT IMPERSONATE ON USER::DBO TO [SqlUser];
GRANT VIEW DEFINITION TO [EPUSPRIW0081\kosh];

EXEC SP_ADDROLE 'sql_dependency_subscriber';
EXEC SP_ADDROLEMEMBER 'sql_dependency_subscriber', 'kosh';

GRANT SELECT to [EPUSPRIW0081\kosh];
GRANT RECEIVE ON QueryNotificationErrorsQueue TO [EPUSPRIW0081\kosh];

GRANT CONTROL ON SCHEMA::[dbo] TO [EPUSPRIW0081\kosh];

GRANT IMPERSONATE ON USER::DBO TO [EPUSPRIW0081\kosh];
