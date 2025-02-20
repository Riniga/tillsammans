## Skapad med 
func init judotech.api --dotnet

## Förutsättningar
.NET core 3.1 SDK
func installeras med: npm i -g azure-functions-core-tools@3 --unsafe-perm true
conda install 

## CosmosDB
- Se ..\readme.md för att sätta upp cosmos db i azure och hämta nycklar

## Skapa local.settings.json
- Kopiera local.settings_sample.json till local.settings.json och uppdatera fälten

## Utvecklingsmiljöer (för test och utveckling)
- starta med: func start --csharp
- Debug genom att "Attach to process" -> välj func bland processer

## Anrop mot API
Testa API med hjälp av applikationen https://www.postman.com/