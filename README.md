# MovieAPI2
API'et innehåller en databas med filmer och skådespelare. Det är byggt med .NET 10 och använder Entity Framework Core för databasinteraktion.
<br>
#### API'et innehåller följande features:

* CRUD-operationer för filmer och skådespelare.
* Sökfunktioner för att hitta filmer baserat på titel, genre eller skådespelare.
* Tester
* Swagger-dokumentation för att enkelt testa och förstå API'et.
* CLEAN-arkitektur med tydlig separation av ansvar mellan lager.
<img src="https://github.com/KarlqvistLars/MovieAPI_Clean/blob/main/docs/CleanArkitekture.jpg" hight="400">

#### Endpoints(v1.0):
GET /api/movies - Hämta alla filmer<br>
GET /api/movies/{id} - Hämta en film baserat på ID<br>
POST /api/movies - Skapa en ny film<br>
PUT /api/movies/{id} - Uppdatera en film baserat på ID<br>
DELETE /api/movies/{id} - Ta bort en film baserat på ID<br>

GET /api/actors - Hämta alla skådespelare<br>
GET /api/actors/{id} - Hämta en skådespelare baserat på ID<br>
POST /api/actors - Skapa en ny skådespelare<br>
PUT /api/actors/{id} - Uppdatera en skådespelare baserat på ID<br>
DELETE /api/actors/{id} - Ta bort en skådespelare baserat på ID<br>

GET /api/reviews - Hämta alla recensioner<br>
GET /api/reviews/{id} - Hämta en recension baserat på ID<br>
POST /api/reviews - Skapa en ny recension<br>
PUT /api/reviews/{id} - Uppdatera en recension baserat på ID<br>
DELETE /api/reviews/{id} - Ta bort en recension baserat på ID<br>

#### _Kommande endpoints:_
GET /api/movies/search?title={title} - Sök efter filmer baserat på titel<br>
GET /api/movies/search?genre={genre} - Sök efter filmer baserat på genre<br>
GET /api/movies/search?actor={actor} - Sök efter filmer baserat på skådespelare<br>
POST /api/movies/{id}/actors - Lägg till en skådespelare till en film<br>

## Programmets arkitektur - Clean Architecture

![Clean Architecture](https://github.com/KarlqvistLars/MovieAPI2/blob/main/docs/UMLStruktur.jpg)

### Starta API'et från rotkatalogen med: 
```
dotnet run --project MovieAPI2
```
Prova API'et med Swagger på:
```
https://localhost:7221/swagger/index.html
```
Mer dokumentation kommer...<br>
### Disclamer: 
API'et är inte helt färdigt.<br> Alla metoder fungerar men ska ev. förändras i betende.
