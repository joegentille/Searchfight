# Searchfight

## 1. Description
Determine the popularity of programming languages on the internet 

## 2. Modules
Searchfight has 2 Search Engines to retrieve popularity

**BING**
BaseUrl: https://api.bing.microsoft.com/v7.0/custom/

**GOOGLE**
BaseUrl: https://www.googleapis.com/customsearch/

## 3. Authentication
Currently, there is no authentication

## 4. Deployment

Octopus Project: https://OctopusServer/app#/

Servers:

| QA           | STG           | PROD           | 
| :--          | :---          | :---          | 
| SERVQAAPP   | SERVSTGAPP   | SERVAPP   | 

## 5. Testing Strategies
It is focused on automated unit testing. The project is using XUnit as .Net unit testing framework.

Will use sonarqube for code coverage

## 6. Technical Debt

Some technical decisions...