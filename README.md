# tillsammans
nytt namn... JudoTech Ecosystems 

## Ett ekosystem
Samling av flera IT system för att tillsammans utgöra platformen för all teknik inom judo.

## Funktionalitet
Dokumentation av funktionalitet finns samlad [här](documentation/requirement.md)

### En funktionalitet består av:
- Titel
- Beställare
- Användarscenario 
- status: todo, doing, done
- lösning
    - beskrivning av lösning
    - Datastruktur och lagring
    - API implmentation

### Språk
- All dokumentation, kommunikation m.m. sker för närvarande på <u>svenska</u>)>.
- Samtliga filer och kataloger är på <u>engelska</u> 
- liksom all kodning, variabelnamn, kommentarer o.s.v.

### Struktur
- All källkod samlas under katalogen "source"
- Varje tjänst skall vara så "atomär", d.v.s. så liten som möjligt och enbart tillhandahålla grundläggande funktion för sitt syfte.
- Varje tjänst beskrivs under resp. katalog med en readme.md m.m.

### Tjänster
- **web** - Statisk webbplats för att tillhandahålla grafiskt gränssnitt för användarna
- **api** - C# .NET för att tillhandahålla REST API
- **library** - C# .NET bibliotek för grundläggande modeller etc

## Tools
- Visual Studio Code Extencion: Azure Resource Manager (ARM) Tools
- Azure CLI: https://learn.microsoft.com/en-us/cli/azure/install-azure-cli-windows?source=recommendations&tabs=azure-cli
* Miniconda (https://docs.anaconda.com/miniconda/install/)


## Environment
I use Anaconda to create virtuela environments.
* conda update --all
* conda search python
* conda create -n azure python=3.13.2
* conda activate azure

## Modules
* Azure Cli(Not sure, I have it installed globaly): conda install azure-cli-core

## Azure Cli
- az --version
- az upgrade

## Azure Account
- Logion using dialog: az login --tenant <Tenant ID found in Azure Entra ID - Properties>
- Show my account details: az account show

## Resource Group
- List resource groups: az group list
- Create a new group: az group create --name Judoka --location swedencentral
- To remove the grouo and all content: az group delete --name Judoka --yes --no-wait 

## App Service Plan 
- Create an app service plan in the environment: az appservice plan create -g Judoka -n JudokaServicePlan

## Storage Account
- az storage account create -n JudokaStorage -g Judoka -l swedencentral --sku Standard_LRS
- To retrieve existing connectionstring: az storage account show-connection-string --name JudokaStorage --resource-group Judoka

[//]: # sku {Premium_LRS, Premium_ZRS, Standard_GRS, Standard_GZRS, Standard_LRS, Standard_RAGRS, Standard_RAGZRS, Standard_ZRS}]

### Cosmos DB
- az cosmosdb create --name judokacosmosdb --resource-group Judoka
- az cosmosdb keys list --name judokacosmosdb --resource-group Judoka

