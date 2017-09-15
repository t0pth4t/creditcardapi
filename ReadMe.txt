Download .NET.Core https://github.com/dotnet/core/blob/master/release-notes/download-archives/1.1.2-download.md

Create an empty *.db file somewhere on your system

Update the appsettings.json DataSourceLocation value to point to your newly created *.db file

In the project directory "~\CreditCardAPI\CreditCardAPI" open a command window and execute the command
	
	dotnet restore
	dotnet ef database update

To run the application execute the command
	
	dotnet run

Navigate to http://localhost:5000/swagger/ for API documentation