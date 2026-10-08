# Backend TODO

Lista este ordonată după prioritate și dependențe. Nu începe etapa următoare până când proiectul compilează și etapa curentă este verificată.

## P0 - Repararea modelelor și a migrațiilor EF Core

- [x] Păstrează workout-urile personale prin relația obligatorie `Workout.UserId`.
- [x] Modelează exercițiile ca un catalog public, independent de workout-uri.
- [x] Păstrează opțional creatorul unui exercițiu prin `Exercise.CreatedByUserId`.
- [x] Leagă exercițiile de workout-uri prin entitatea `WorkoutExercise`.
- [x] Leagă seturile de `WorkoutExercise`, astfel încât fiecare workout să aibă propriile seturi.
- [x] Configurează relațiile, indexurile și regulile de ștergere în `OnModelCreating`.
- [x] Repară istoricul migrațiilor:
  - [x] Elimină migrațiile duplicate `AddRefreshTokens` și `UpdateWorkoutAndExerciseModel`.
  - [x] Înlocuiește istoricul duplicat cu migrația curată `InitialCreate`.
  - [x] Include în Git migrațiile `InitialCreate` și `AddPublicExerciseCatalog`.
- [x] Aplică toate migrațiile pe o bază SQLite nouă, goală.
- [x] Verifică `dotnet ef migrations list`, diferențele modelului și `dotnet build`.

## P1 - Contractele din Application

- [ ] Creează folderul `Interfaces` în proiectul Application.
- [ ] Definește interfețele:
  - [ ] `IAuthService`.
  - [ ] `IWorkoutService`.
  - [ ] `IExerciseService`.
  - [ ] `IUserService`, numai dacă există operații reale de profil/administrare.
- [ ] Separă DTO-urile pe operații:
  - [ ] `RegisterRequest`.
  - [ ] `LoginRequest`.
  - [ ] `RefreshTokenRequest`.
  - [ ] `AuthResponse`.
  - [ ] `CreateWorkoutRequest`.
  - [ ] `UpdateWorkoutRequest`.
  - [ ] `WorkoutResponse`.
  - [ ] DTO-uri pentru `Exercise` și `Set`.
- [ ] Corectează `WOrkoutDto` și fă toate proprietățile necesare publice.
- [ ] Elimină câmpurile care nu corespund modelului Domain, precum `Description`, `Repetitions` și numărul simplu de seturi, sau mapează-le intenționat.
- [ ] Nu expune entitățile EF direct din servicii sau controllere.

## P2 - Serviciile și dependency injection

- [ ] Fă fiecare serviciu să implementeze interfața corespunzătoare.
- [ ] Înregistrează în DI toate serviciile ca `Scoped`.
- [ ] Repară `AuthServices`:
  - [ ] Aplică doar `Trim()` email-ului la register și login, fără schimbarea literelor mari/mici.
  - [ ] Păstrează verificarea parolei cu BCrypt.
  - [ ] Mută configurarea JWT într-o clasă de opțiuni validată.
  - [ ] Implementează refresh token rotation și revocarea token-urilor.
- [ ] Repară `WorkoutServices`:
  - [ ] Creează și salvează workout-ul în baza de date.
  - [ ] Adaugă metode pentru listare și obținere după ID.
  - [ ] Include exercițiile și seturile unde este necesar.
  - [ ] Primește întotdeauna ID-ul utilizatorului pentru operațiile protejate.
  - [ ] Verifică ownership-ul în query, nu după încărcarea oricărui workout.
- [ ] Repară `ExerciseService`:
  - [ ] Înlocuiește `FindAsync(name)` cu un query după `Name` sau ID.
  - [ ] Corectează numele metodelor `Crete`, `Modify` și `Detele`.
  - [ ] Stabilește cum se creează, modifică și șterg seturile.
- [ ] Repară sau elimină `UserServices`:
  - [ ] Corectează verificarea inversată din `CreateUser`.
  - [ ] Corectează actualizarea entității urmărite de EF.
  - [ ] Nu salva niciodată parole nehash-uite.
- [ ] Adaugă `CancellationToken` metodelor asincrone.
- [ ] Verifică build-ul după finalizarea serviciilor.

## P3 - Controllere și endpoint-uri REST

- [ ] Implementează `AccountController` folosind `IAuthService`:
  - [ ] `POST /api/account/register`.
  - [ ] `POST /api/account/login`.
  - [ ] `POST /api/account/refresh`.
  - [ ] `POST /api/account/logout`.
- [ ] Refactorizează `WorkoutController` să folosească `IWorkoutService`, nu `MyWorkoutDbContext`.
- [ ] Elimină lista hardcodată de exerciții.
- [ ] Implementează endpoint-urile pentru workout-uri:
  - [ ] `GET /api/workouts`.
  - [ ] `GET /api/workouts/{id}`.
  - [ ] `POST /api/workouts`.
  - [ ] `PUT /api/workouts/{id}`.
  - [ ] `DELETE /api/workouts/{id}`.
- [ ] Adaugă endpoint-uri pentru exerciții și seturi doar dacă nu sunt gestionate direct prin workout.
- [ ] Returnează codurile HTTP corecte: `200`, `201`, `204`, `400`, `401`, `403`, `404`, `409`.
- [ ] Verifică manual că endpoint-urile corespund URL-urilor folosite de frontend.

## P4 - JWT, autorizare și ownership

- [ ] Configurează `Jwt:Key` prin user secrets sau variabile de mediu; nu salva cheia în Git.
- [ ] Configurează `AddAuthentication().AddJwtBearer(...)`.
- [ ] Adaugă `UseAuthentication()` înainte de `UseAuthorization()`.
- [ ] Adaugă `UseAuthorization()` înainte de maparea controllerelor.
- [ ] Validează issuer, audience, cheia, semnătura și expirarea token-ului.
- [ ] Include în JWT ID-ul utilizatorului și rolul cu claim-uri consecvente.
- [ ] Adaugă `[AllowAnonymous]` numai pentru register, login și refresh.
- [ ] Adaugă `[Authorize]` pentru endpoint-urile de workout, profil și logout.
- [ ] Adaugă politici sau `[Authorize(Roles = "Admin")]` pentru operațiile administrative.
- [ ] Aplică ownership pentru citire, actualizare și ștergere: utilizatorul poate accesa numai propriile workout-uri.
- [ ] Revocă refresh token-ul la logout și după rotație.
- [ ] Nu păstra refresh token-ul în clar în baza de date.

## P5 - Validare, erori și OpenAPI

- [ ] Adaugă validare pentru toate request DTO-urile:
  - [ ] Email valid și normalizat.
  - [ ] Cerințe minime pentru parolă.
  - [ ] Nume obligatorii și lungimi maxime.
  - [ ] Repetări pozitive și greutate nenegativă.
- [ ] Alege Data Annotations sau FluentValidation și folosește aceeași abordare peste tot.
- [ ] Configurează tratarea globală a excepțiilor.
- [ ] Returnează erorile într-un format unitar `ProblemDetails`.
- [ ] Definește excepții sau rezultate clare pentru `NotFound`, `Conflict`, `Forbidden` și validare.
- [ ] Elimină din răspunsuri detaliile interne și stack trace-urile.
- [ ] Adaugă OpenAPI/Swagger.
- [ ] Configurează schema Bearer JWT în Swagger UI.
- [ ] Documentează răspunsurile și codurile HTTP importante.
- [ ] Limitează CORS la origin-urile necesare și mută configurarea în settings.
- [ ] Înlocuiește parolele fixe din seed cu o configurare sigură sau elimină seed-ul din afara mediului local.

## P6 - Teste automate

- [ ] Creează proiectul `MyWorkout.Tests` și adaugă-l în soluție.
- [ ] Adaugă teste unitare pentru:
  - [ ] Înregistrare și autentificare.
  - [ ] Parole greșite și email duplicat.
  - [ ] Generare, refresh și revocare token-uri.
  - [ ] Operațiile serviciului de workout.
  - [ ] Regulile de ownership.
- [ ] Adaugă teste de integrare pentru controllere cu `WebApplicationFactory`.
- [ ] Testează răspunsurile `401`, `403`, `404` și `409`.
- [ ] Testează aplicarea migrațiilor pe o bază goală.
- [ ] Folosește o bază SQLite temporară pentru a păstra comportamentul relațional în teste.

## P7 - CI și verificarea finală

- [ ] Adaugă un workflow CI care rulează la push și pull request.
- [ ] Rulează în CI:
  - [ ] `dotnet restore`.
  - [ ] `dotnet build --no-restore`.
  - [ ] `dotnet test --no-build`.
- [ ] Fă workflow-ul să eșueze la teste sau build nereușite.
- [ ] Verifică să nu existe chei JWT, baze locale sau alte secrete în Git.
- [ ] Actualizează README-ul cu pașii pentru configurare, migrații și pornirea API-ului.
- [ ] Rulează testul final login → creare workout → citire → modificare → ștergere → logout.

## Definiția de finalizare

- [ ] `dotnet build Backend/MyWorkout.sln` trece fără erori.
- [ ] `dotnet test Backend/MyWorkout.sln` trece integral.
- [ ] Migrațiile se aplică pe o bază nouă fără erori.
- [ ] Frontend-ul poate face register și login folosind API-ul.
- [ ] Un utilizator nu poate citi sau modifica workout-urile altui utilizator.
- [ ] Swagger permite autentificarea Bearer și testarea endpoint-urilor protejate.
- [ ] CI este verde.
