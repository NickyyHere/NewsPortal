# NewsPortal
## Opis projektu

### NewsPortal to aplikacja ASP.NET Core do zarządzania artykułami. Backend korzysta z bazy PostgreSQL. Projekt wspiera uruchamianie lokalne, jak i w kontenerze.
---
## Uruchomienie

### Wymagania
- .NET 8 SDK
- Docker (Opcjonalnie)
- PostgreSQL

---

### Sklonuj i przełącz się na repozytorium
```bash
git clone https://github.com/NickyyHere/NewsPortal.git
cd NewsPortal
dotnet restore
```

## Lokalnie

1. Skonfiguruj bazę PostgreSQL
2. Dodaj zmienną środowiskową "ConnectionStrings__DefaultConnection"
```bash
setx ConnectionStrings__DefaultConnection "Host={HostAddress};Port={PortNumber};Username={Username};Password={Password};Database={DatabaseName}"
```
3. Wykonaj migrację bazy danych
```bash
dotnet ef database update --project NewPortal.Infrastructure --startup-project NewsPortal.API
```
4. Uruchom aplikację
```bash
dotnet run --project NewsPortal.API
```
5. Aplikacje będzie dostępna pod adresem:
HTTPS: https://localhost:7146
HTTP: http://localhost:5141

## Docker

1. Ustaw odpowiedni connection string w pliku .env
2. Zbuduj obraz Docker
```bash
docker build -t newsportal .
```
3. Uruchom kontener z aplikacją
```bash
docker run --env-file .env -p 8080:8080 newsportal
```
4. Aplikacja dostępna jest pod http://localhost:8080

## Make

### build
```bash
make build
```
Buduje aplikację
### test
```bash
make test
```
Uruchamia testy jednostkowe
### run
```bash
make run
```
Uruchamia aplikację lokalnie
### docker-build
```bash
make docker-build
```
Tworzy obraz Dockera
### docker-run
```bash
make docker-run
```
Uruchamia kontener z aplikacją (wymaga poprawnego connection string w pliku .env)

---

## Swagger

Projekt zawiera dokumentację swagger którą można znaleźć dodając do ścieżki "/swagger"

---

## Przykładowe zapytania curl

### Pobierz listę artykułów:
```bash
curl http://localhost:5141/api/Articles
```
### Pobierz artykuł o id 030B4A82-1B7C-11CF-9D53-00AA003C9CB6
```bash
curl http://localhost:5141/api/Articles/030B4A82-1B7C-11CF-9D53-00AA003C9CB6
```
### Dodaj nowy artykuł
```bash
curl -X POST http://lockalhost:5141/api/Aritcles \
  -H "Content-Type: application/json" \
  -d '{"title": "Title", "content": "Some example content", "author": "Autor", "categoryId": "030B4A82-1B7C-11CF-9D53-00AA003C9CB6"}'
```

---

## Struktura projektu

### NewsPortal/
Nadrzędny folder przechowujący wszystkie pliki projektu, definicje obrazu doker, skrypty make, oraz pliki typu .gitignore, .dockerignore, .env, README.md
### NewsPortal.API/
Projekt API - punk wejścia aplikacji
- /Properties/
Znajduje się w nim plik konfiguracyjny launchSettings.json
- /Controllers/
Znajdują się w nim kontrolery WebAPI
- /Extensions/
Znajduje się w nim plik rozszerzeniowy dla kolekcji serwisów
- /Middleware/
Znajduje się w nim plik middleware obsługujący wyjątki wyrzucane przez aplikację
### NewsPortal.Application/
Zawiera logikę biznesową aplikacji
- /DTO/
Zawiera obiekty transferu danych do tworzenia, odczytu, oraz edycji encji
- /Interfaces/
Zawiera interfejsy klas serwisów
- /Mappings/
Zawiera profile do mapowania AutoMapper
- /Services/
Zawiera serwisy implementujące interfejsy które wykonują logikę biznesową
- /Validators/
Zawiera reguły walidacji obiektów transferu danych (FluentValidators)
### NewsPortal.Domain
Modele domenowe, encje, interfejsy repozytoriów
- /Enums/
Zawiera enumerator możliwych statusów artykułów
- /Exceptions/
Zawiera własne wyjątki
- /Interfaces/
Zawiera interfejsy repozytoriów
- /Models/
Zawiera modele aplikacji
- /Services/
Zawiera zawiera implementacje logiki biznesowej niezależnej od infrastruktury
### NewsPortal.Infrastructure
Implementacja repozytoriów, oraz konfiguracja EF Core
- /Migrations/
Zawiera migracje EFCore
- /Repositories/
Zawiera implementację repozytoriów
- /AppDbContext.cs
Konfiguracja EF Core
### NewsPortal.Tests
Testy jednostkowe aplikacji
- /Application/
  - /Services/
  - /Validators/
- /Domain/
  - /Services/
