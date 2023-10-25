# TAG - Backend

Powered by [.NET 7](https://learn.microsoft.com/en-us/dotnet/)

# Setup
## Requirements
* [.NET 7](https://dotnet.microsoft.com/en-us/download/dotnet/7.0/)
* Visual Studio Code extensions
    * C# Dev Kit
    * CSharpier - Code formatter

Visual Studio Code should automatically ask you to install these extensions

## Setup appsettings.json file
* Copy `appsettings.Example.json` and rename it to `appsettings.json`
* Fill values

## Run server
### Command line
Starts on `https://localhost:7210` and `http://localhost:5075`
Swagger is available on `https://localhost:7210/swagger` and `http://localhost:5075/swagger`
```bash
dotnet run
```
The command will build and run the project

### VS Code
Select Run and Debug (CTRL+SHIFT+D)
Start debugging (F5)
