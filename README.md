# ProjectManagement
 

## Migrations erstellen:

```powershell
Add-Migration Create_Database -Context <DbContext>

```

```powershell
dotnet ef migrations add InitialCreate

```


## User anlegen bei KeyCloak

Damit ein User aus Keycloak verwendet werden kann müssen bestimmte Attribute gesetzt werden.
Die Rolle muss gesetzt werden:
Unter Realm roles gibt es 
	- admin 
	- appuser

Unter Users im Reiter `Role` mapping muss die Rolle gesetzt werden.