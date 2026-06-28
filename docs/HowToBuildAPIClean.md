## ASP.NET Core 10 API med Clean Architecture
```
1. Skapa ett nytt projekt från ASP.NET Core 10
```
```
2. Skapa arketekturen (Clean, MVC, MVVM, etc.)
```
```
3. Lägg till nödvändiga Nugetpaket och beroenden i projektet.
   Microsoft.AspNetCore.OpenApi
   Microsoft.EntityFrameworkCore.SqlServer
   Microsoft.EntityFrameworkCore.Tools
   Microsoft.EntityFrameworkCore.Design
```
```
4. Implementera affärslogik och datalager.
   Dvs. Skriv klasser ocg metoder samt DTOer så långt det är logiskt möjligt.
   Lägg allt detta i:
   Core/Models: Klasser som representerar affärslogik och datalager.
   Core/ModelDto: Klasser(DTO'er) som representerar data som ska skickas och tas emot via API:et.
   Core/Contracts: Gränssnitt och kontrakt för tjänster och repository.
```
```
5. Skapa DbContext Program.cs.
   Gör detta genom att högerklicka på properties mappen och välja "Add" -> 
   "NewScaffoldedItem" -> "API Controller - With actions, using Entity Framework". Namnge den t.ex. "Movie2APIContext".
```
```
6. Skapa API-kontroller och konfigurera routning i Program.cs.
   Gör detta genom att högerklicka på controller mappen och välja "Add" -> 
   "@Controller" -> "API Controller - With actions, using Entity Framework". Namnge den t.ex. "MovieController".
```
```
7. Lägg till nödvändiga paket och beroenden i projektet.
```
```
8. Skapa en databas och konfigurera Entity Framework Core för att hantera datalagring.
```
```
9. Skapa modeller och DTOs (Data Transfer Objects) för att representera data som ska skickas och tas emot via API:et.
```
10. 