using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

public class Person
{
    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public long Id { get; set; }

    [Required]
    [StringLength(33)]
    [RegularExpression(@"^[A-Z][a-zA-Z]*$", ErrorMessage = "Name must start with an uppercase letter.")]
    public string Name { get; set; }

    [Required]
    [StringLength(33)]
    [RegularExpression(@"^[A-Z][a-zA-Z]*$", ErrorMessage = "Surname must start with an uppercase letter.")]
    public string Surname { get; set; }

    [Required]
    [DataType(DataType.Date)]
    [CustomValidation(typeof(Person), nameof(ValidateBirthDate))]
    public DateTime BirthDate { get; set; }

    [Required]
    [StringLength(13, MinimumLength = 13, ErrorMessage = "JMBG must be exactly 13 characters.")]
    [RegularExpression(@"^\d{13}$", ErrorMessage = "JMBG must be numeric and exactly 13 digits.")]
    public string PersonalIdNumber { get; set; }

    public long Height { get; set; }

    [NotMapped]
    public int Age => CalculateAge(BirthDate);

    public long PlaceOfBirthId { get; set; }
    public long? PlaceOfResidenceId { get; set; }

    public Place PlaceOfBirth { get; set; }
    public Place PlaceOfResidence { get; set; }

    private static int CalculateAge(DateTime birthDate)
    {
        var today = DateTime.Today;
        var age = today.Year - birthDate.Year;
        if (birthDate.Date > today.AddYears(-age)) age--;
        return age;
    }

    public static ValidationResult ValidateBirthDate(DateTime birthDate, ValidationContext context)
    {
        if (birthDate < new DateTime(1950, 1, 1))
        {
            return new ValidationResult("BirthDate must be on or after January 1, 1950.");
        }
        return ValidationResult.Success;
    }
}
