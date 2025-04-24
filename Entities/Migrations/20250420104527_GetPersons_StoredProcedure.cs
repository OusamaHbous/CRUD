using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entities.Migrations
{
    /// <inheritdoc />
    public partial class GetPersons_StoredProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            //Here we have created the store procedure, means our sql statement
            //is already sotred in the database
            string sp_GetAllPersons = @"
            CREATE  procedure [dbo].[GetAllPersons]
            AS BEGIN
                    select PersonID, PersonName, Email, DateOfBirth, Gender, CountryID,
                     Address, ReceiveNewsLetters FROM [dbo].[Persons]
            END
            ";
            migrationBuilder.Sql(sp_GetAllPersons);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string sp_GetAllPersons = @"
            DROP PROCEDURE [dbo].[GetAllPersons]
            ";
            migrationBuilder.Sql(sp_GetAllPersons);
        }
    }
}
