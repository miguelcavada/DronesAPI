# DronesAPI
Drones API Repo

This application was develop with technologies from Microsoft .NET Platforms. :100:

:point_right: Software requirements:

.NET 6.0.10 (SDK). Available to download from [Microsoft Page] (https://dotnet.microsoft.com/en-us/download/dotnet/6.0)

Build instructions:

:wrench: Tools:

Visual Studio or Visual Studio Code

:rocket: Run instructions:

Open in the Windows console command inside the application directory and execute the dotnet run command. This instruction runs the application locally 
without needing to be hosted on a web server.

Copy the url shown in the console, adding /api/Drones to the end. Example: https://localhost:7086/api/Drones

:pray: Do not close the windows command console while the application is running

:test_tube: Test instructions:

:wrench: Tools:

:astronaut: Postman

:apple: Features:

Checking available drones for loading. Example (GET): https://localhost:7086/api/Drones

Check drone battery level for a given drone. Example (GET): https://localhost:7086/api/Drones/BatteryLevel-7f73c38b-4ded-4c5c-a094-92e8a7559480

Parameters:

Drone ID (GUID Data type): 7f73c38b-4ded-4c5c-a094-92e8a7559480

Loading a drone with medication items (PUT). 

Example: https://localhost:7086/api/Drones/1b209c7b-788e-4427-9099-4dc9245aae5f

Parameters:

Drone ID (GUID Data type): 1b209c7b-788e-4427-9099-4dc9245aae5f

Medications (JSON Format) in the body: 
[
  {
    "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "name": "string",
    "weight": 0,
    "code": "string",
    "image": "string"
  }
]

Registering a drone. Example (POST): https://localhost:7086/api/Drones

Parameters:

Drone (JSON Format) in the body: 
{
    "serialNumber": "DRON-03",
    "model": "Cruiserweight",
    "weightLimit": 500,
    "batteryCapacity": 100,
    "state": "IDLE"
}

Checking loaded medication items for a given drone. Example (GET): https://localhost:7086/api/Drones/1b209c7b-788e-4427-9099-4dc9245aae5f

Parameters:

Drone ID (GUID Data type): 1b209c7b-788e-4427-9099-4dc9245aae5f


