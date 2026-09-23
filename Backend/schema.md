Backend/
│
├── MyWorkout.sln
│
├── MyWorkout.Api/
│ ├── Controllers/ sau Endpoints/
│ ├── Program.cs
│ └── appsettings.json
│
├── MyWorkout.Application/
│ ├── Workouts/
│ ├── BodyWeight/
│ ├── Exercises/
│ ├── DTOs/
│ └── Interfaces/
│
├── MyWorkout.Domain/
│ ├── Entities/
│ ├── Enums/
│ └── Exceptions/
│
├── MyWorkout.Infrastructure/
│ ├── Persistence/
│ ├── Migrations/
│ └── Services/
│
└── MyWorkout.Tests/

For databes conection i cread mydbcontext file
put the connection string in appsetings.json

dotnet ef migrations add InitialCreate `

> > --project Backend/MyWorkout.Infrastructure `  --startup-project Backend/MyWorkout.Api`
> > --output-dir Persistence/Migrations

dotnet ef database update --project Backend/MyWorkout.Infrastructure --startup-project Backend/MyWorkout.Api
