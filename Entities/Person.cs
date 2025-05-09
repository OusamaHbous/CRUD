using System;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
  /// <summary>
  /// this is the Person domain model class
  /// </summary>
  public class Person
  {

    [Key]
    public Guid PersonID { get; set; }

    [StringLength(40)]
    public string? PersonName { get; set; }

    [StringLength(100)]
    public string? Email { get; set; }
    public DateTime? DateOfBirth { get; set; }

    [StringLength(10)]
    public string? Gender { get; set; }
    public Guid? CountryID { get; set; }

    [StringLength(150)]
    public string? Address { get; set; }
    public bool ReceiveNewsLetters { get; set; }
  }
}
