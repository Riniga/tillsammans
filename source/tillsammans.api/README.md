## Skapad med 
func init tillsammans.api --dotnet

## Förutsättningar
.NET core 3.1 SDK
func installeras med: npm i -g azure-functions-core-tools@3 --unsafe-perm true

## Skapa local.settings.json
Kopiera local.settings_sample.json till local.settings.json och uppdatera fälten

## Utvecklingsmiljöer (för test och utveckling)
starta med: func start --csharp
Debug genom att "Attach to process" -> välj func bland processer

## CosmosDB
För att emulera CosmosDB behvös Azure Cosmos DB Emulator https://aka.ms/cosmosdb-emulator 
Starta Cosmos DB Emulator
Databasen måste skapas: https://localhost:8081/_explorer/index.html

## Anrop mot API
Testa API med hjälp av applikationen https://www.postman.com/
