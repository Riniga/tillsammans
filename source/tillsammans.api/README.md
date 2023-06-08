## Skapad med 
func init tillsammans.api --dotnet

## Förutsättningar
func installeras med: npm i -g azure-functions-core-tools@3 --unsafe-perm true

## Utvecklingsmiljöer (för test och utveckling)
starta med: func start --csharp
Debug genom att "Attach to process" -> välj func bland processer



## CosmosDB
För att emulera CosmosDB behvös Azure Cosmos DB Emulator https://aka.ms/cosmosdb-emulator Databasen måste skapas: https://localhost:8081/_explorer/index.html