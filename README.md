# JudoTech
JudoTech is a comprehensive ecosystem designed to manage all technical functionalities for our community. It serves as a unified platform for handling memberships, rankings, competition systems, result boards, and much more. 

## Purpose and Vision
JudoTech is built to streamline and enhance the experience for everyone involved in our community. Whether you are a competitor, organizer, or supporter, JudoTech provides the tools needed for seamless interaction and efficient management. Our goal is to create a system that is precise, easy to use, and continuously improved through collaboration.

## Get Involved
Whether you are a developer, designer, or user, your input is valuable in shaping JudoTech. Together, we will build an efficient, transparent, and robust system that serves our community's needs.
Stay connected, contribute, and let’s develop JudoTech into the best system possible!

### Versioning
Every application has a version number (Major.Minor.Patch) indicating it's current status:
* Major: Breaking changes
* Minor: New features (non-breaking)
* Patch: Bug fixes, performance improvements

For stability we add:
* alpha: Under development, unstable and incomplete
* beta: Complete, but unstable - in testenvifonment
* RC: Release Candidate, should be s

## Current ecosystem version
* Development: 1.0.0-alpha.1
    * Core: 1.0.0-alpha.1
    * API: 1.0.0-alpha.1
    * Referree: 1.0.0-alpha.1
* Test: 1.0.0-beta.1 - NOT RELEASED
* UAT: 1.0.0-RC.1 - NOT RELEASED
* Production: 1.0 (2025.04) - NOT RELEASED

## Project structure
- Source code in folder: "source"
- Every application, minimal with basic functionality for its purpose, well documented with its source

## Tools
- Visual Studio Code
- Visual Studio Code Extencion: Azure Resource Manager (ARM) Tools
- Azure CLI: https://learn.microsoft.com/en-us/cli/azure/install-azure-cli-windows?source=recommendations&tabs=azure-cli
* Miniconda (https://docs.anaconda.com/miniconda/install/)

## Environment
I use Anaconda to create a virtual developemnt environment.
* conda update --all
* conda search python
* conda create -n azure python=3.13.2
* conda activate azure

## Modules
* Azure Cli(Not sure, I have it installed globaly): conda install azure-cli-core

## Azure
### Azure Cli
- az --version
- az upgrade

### Azure Account
- Logion using dialog: az login --tenant <Tenant ID found in Azure Entra ID - Properties>
- Show my account details: az account show

### Resource Group
- List resource groups: az group list
- Create a new group: az group create --name Judoka --location swedencentral
- To remove the grouo and all content: az group delete --name Judoka --yes --no-wait 

### App Service Plan 
- Create an app service plan in the environment: az appservice plan create -g Judoka -n JudokaServicePlan

### Storage Account
- az storage account create -n JudokaStorage -g Judoka -l swedencentral --sku Standard_LRS
- To retrieve existing connectionstring: az storage account show-connection-string --name JudokaStorage --resource-group Judoka

[//]: # sku {Premium_LRS, Premium_ZRS, Standard_GRS, Standard_GZRS, Standard_LRS, Standard_RAGRS, Standard_RAGZRS, Standard_ZRS}]

### Cosmos DB
- az cosmosdb create --name judokacosmosdb --resource-group Judoka
- az cosmosdb keys list --name judokacosmosdb --resource-group Judoka