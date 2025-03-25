# LibraryAPI 🏛️

Prosty system zarządzania biblioteką z REST API, napisany w .NET 8.  


## 🚀 Funkcjonalności

### Backend (gotowe)
- **CRUD dla książek**
  - Dodawanie / edycja / usuwanie książek
  - Zmiana statusu (Dostępna/Wypożyczona/Zarezerwowana)
  - Rezerwacja książek
- **Wyszukiwanie książek** po tytule i autorze
- **Sprawdzanie dostępności** książki
- **Testy jednostkowe** pokrywające kluczowe scenariusze

### Frontend (w planach)
- Interfejs użytkownika w React/HTMX/Razor Pages
- Lista książek z filtrami
- Formularz rezerwacji
- Panel administracyjny

---

## 🔧 Technologie

- **Backend**:  
  ![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet)
  ![Entity Framework](https://img.shields.io/badge/Entity_Framework_Core-9.0.4-blueviolet)
  ![Swagger](https://img.shields.io/badge/Swagger-23.0-green?logo=swagger)

- **Testy**:  
  ![xUnit](https://img.shields.io/badge/xUnit-2.4.2-blue)
  ![Moq](https://img.shields.io/badge/Moq-4.18.4-yellowgreen)

---

## 📦 Jak uruchomić?

1. **Wymagania**:
   - .NET 8 SDK
   - SQL Server (lokalny lub Docker)

2. **Uruchomienie API**:
   ```bash
   git clone https://github.com/adakde/LibrarySystemApi.git
   cd LibraryAPI
   dotnet run
Dokumentacja API:
Otwórz w przeglądarce:
http://localhost:7015/swagger

🧪 Testy
# Uruchom wszystkie testy
dotnet test

Pokrycie kluczowych scenariuszy:
1. `GetBooks_ReturnsEmptyList_WhenNoBooks` - Weryfikuje zachowanie gdy brak książek
2. `ReserveBook_UpdatesStatus_ToReserved` - Testuje poprawną rezerwację
3. `ReserveBook_ReturnsNotFound_WhenBookNotExists` - Sprawdza obsługę błędów

📅 Roadmap
Wersja 1.1 (obecna)
Podstawowe endpointy REST

Testy jednostkowe

Dokumentacja Swagger

Wersja 2.0 (w przygotowaniu)
Prosty frontend

Autoryzacja użytkowników

Historia rezerwacji

📝 Licencja
MIT

🔨 Autor: adakde
