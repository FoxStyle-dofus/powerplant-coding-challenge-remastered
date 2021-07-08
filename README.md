# Powerplant Coding Challenge Remastered
Engie's powerplant coding challenge remastered according to their feedback

## Objective
The objective was to calculate how much power each of a multitude of different powerplants need to produce (a.k.a. the production-plan) when the load is given and taking into account the cost of the underlying energy sources (gas, kerosine) and the Pmin and Pmax of each powerplant.

## Architecture
This solution is an ASP.NET Core RESTful Web API following the principles of Clean Architecture.

## Production Plan Calculation
This solution is doing 3 steps in order to perform the production plan calculation from a payload.
### Calculate cost for each powerplant
For each powerplant according to its fuel type, calculate the cost to generate electrical power. 
Gas and kerosine will get their cost and efficiency from payload.
Wind has zero cost and will get its percentage of wind and efficiency also from payload.
### Order powerplant by cost
After the calculation, the solution will order the powerplants to use by their cost.
### Calculate how much power each powerplant need to produce
And finally, the solution will calculate how much power each powerplant need to produce according to their minimum and maximum power to reach the load amount.
## Source
### Engie's Challenge
https://github.com/gem-spaas/powerplant-coding-challenge
### Clean Architecture
https://docs.microsoft.com/en-us/dotnet/architecture/modern-web-apps-azure/common-web-application-architectures#clean-architecture
