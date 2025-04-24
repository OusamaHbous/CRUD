using System;
using Microsoft.EntityFrameworkCore;

namespace Entities;

public class PersonsDbContext : DbContext
{
    public PersonsDbContext(DbContextOptions options) : base (options)
    {

    }
    public DbSet<Country> Countries {get; set; }
    public DbSet<Person> Persons {get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Country>().ToTable("Countries");
        modelBuilder.Entity<Person>().ToTable("Persons");

        //Seed to Countries from our json file
        string countriesJson = System.IO.File.ReadAllText("countries.json");
        List<Country> countries = System.Text.Json.JsonSerializer.Deserialize<List<Country>>(countriesJson);
        foreach ( Country country in countries)
        {
            modelBuilder.Entity<Country>().HasData(country);
        }

        //Seed to Persons from our json file
        string personsJson = System.IO.File.ReadAllText("persons.json");
        List<Person> persons = System.Text.Json.JsonSerializer.Deserialize<List<Person>>(personsJson);
        foreach ( Person person in persons)
            modelBuilder.Entity<Person>().HasData(person);
    }

    //Here we are invoking our stored procedure with thie Execute statement
    //Whenever the  sql statement executes, we will get the resut set that contains
    //the list of persons, and that will be converted to list of person objects
     public List<Person> sp_GetAllPersons()
    {
      return Persons.FromSqlRaw("EXECUTE [dbo].[GetAllPersons]").ToList();
    }
}
