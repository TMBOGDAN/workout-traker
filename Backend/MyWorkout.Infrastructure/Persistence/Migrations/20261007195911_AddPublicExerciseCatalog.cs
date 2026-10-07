using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyWorkout.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddPublicExerciseCatalog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exercises_Workouts_WorkoutId",
                table: "Exercises");

            migrationBuilder.DropForeignKey(
                name: "FK_Sets_Exercises_ExerciseId",
                table: "Sets");

            migrationBuilder.DropIndex(
                name: "IX_Users_NormalizedEmail",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Exercises_WorkoutId",
                table: "Exercises");

            migrationBuilder.DropColumn(
                name: "NormalizedEmail",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "WorkoutId",
                table: "Exercises");

            migrationBuilder.RenameColumn(
                name: "ExerciseId",
                table: "Sets",
                newName: "WorkoutExerciseId");

            migrationBuilder.RenameIndex(
                name: "IX_Sets_ExerciseId",
                table: "Sets",
                newName: "IX_Sets_WorkoutExerciseId");

            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserId",
                table: "Exercises",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "WorkoutExercises",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    WorkoutId = table.Column<int>(type: "INTEGER", nullable: false),
                    ExerciseId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkoutExercises", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkoutExercises_Exercises_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "Exercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkoutExercises_Workouts_WorkoutId",
                        column: x => x.WorkoutId,
                        principalTable: "Workouts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Exercises_CreatedByUserId",
                table: "Exercises",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Exercises_Name",
                table: "Exercises",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutExercises_ExerciseId",
                table: "WorkoutExercises",
                column: "ExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutExercises_WorkoutId_ExerciseId",
                table: "WorkoutExercises",
                columns: new[] { "WorkoutId", "ExerciseId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Exercises_Users_CreatedByUserId",
                table: "Exercises",
                column: "CreatedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Sets_WorkoutExercises_WorkoutExerciseId",
                table: "Sets",
                column: "WorkoutExerciseId",
                principalTable: "WorkoutExercises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exercises_Users_CreatedByUserId",
                table: "Exercises");

            migrationBuilder.DropForeignKey(
                name: "FK_Sets_WorkoutExercises_WorkoutExerciseId",
                table: "Sets");

            migrationBuilder.DropTable(
                name: "WorkoutExercises");

            migrationBuilder.DropIndex(
                name: "IX_Users_Email",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Exercises_CreatedByUserId",
                table: "Exercises");

            migrationBuilder.DropIndex(
                name: "IX_Exercises_Name",
                table: "Exercises");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "Exercises");

            migrationBuilder.RenameColumn(
                name: "WorkoutExerciseId",
                table: "Sets",
                newName: "ExerciseId");

            migrationBuilder.RenameIndex(
                name: "IX_Sets_WorkoutExerciseId",
                table: "Sets",
                newName: "IX_Sets_ExerciseId");

            migrationBuilder.AddColumn<string>(
                name: "NormalizedEmail",
                table: "Users",
                type: "TEXT",
                maxLength: 254,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "WorkoutId",
                table: "Exercises",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Users_NormalizedEmail",
                table: "Users",
                column: "NormalizedEmail",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Exercises_WorkoutId",
                table: "Exercises",
                column: "WorkoutId");

            migrationBuilder.AddForeignKey(
                name: "FK_Exercises_Workouts_WorkoutId",
                table: "Exercises",
                column: "WorkoutId",
                principalTable: "Workouts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sets_Exercises_ExerciseId",
                table: "Sets",
                column: "ExerciseId",
                principalTable: "Exercises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
