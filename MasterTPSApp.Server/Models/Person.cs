using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class Person
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Required]
    [StringLength(33)]
    [RegularExpression(@"^[A-ZČĆŽŠĐ][a-zA-ZčćžšđČĆŽŠĐ]*$", ErrorMessage = "Name must start with an uppercase letter.")]

    public string Name { get; set; }

    [Required]
    [StringLength(33)]
    [RegularExpression(@"^[A-ZČĆŽŠĐ][a-zA-ZčćžšđČĆŽŠĐ]*$", ErrorMessage = "Surname must start with an uppercase letter.")]
    public string Surname { get; set; }

    [Required]
    [DataType(DataType.Date)]
    [CustomValidation(typeof(Person), nameof(ValidateBirthDate))]
    public DateTime BirthDate { get; set; }

    [Required]
    [StringLength(13, MinimumLength = 13, ErrorMessage = "JMBG must be exactly 13 characters.")]
    [RegularExpression(@"^\d{13}$", ErrorMessage = "JMBG must be numeric and exactly 13 digits.")]
    public string PersonalIdNumber { get; set; }
    [Required]
    [Range(20, 300, ErrorMessage = "Height must be between 20 and 300.")]
    public long Height { get; set; }

    [Required]
    public long PlaceOfBirthId { get; set; }

    public long? PlaceOfResidenceId { get; set; }

    [ForeignKey("PlaceOfBirthId")]
    public Place PlaceOfBirth { get; set; }

    [ForeignKey("PlaceOfResidenceId")]
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
