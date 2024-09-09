using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Place
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Required]
    [StringLength(40)]
    [RegularExpression(@"^[A-ZČĆŽŠĐ][a-zčćžšđ]*(\s[A-Za-zČĆŽŠĐčćžšđ]+)*$", ErrorMessage = "Name must start with an uppercase letter and can contain multiple words.")]

    public string Name { get; set; }

    [Required]
    [Range(11000, 45000, ErrorMessage = "PostalCode must be between 11000 and 45000.")]
    public int PostalCode { get; set; }

    [Required]
    [Range(0, 2000000, ErrorMessage = "Population must be between 0 and 2,000,000.")]
    public int Population { get; set; }

    public ICollection<Person> PeopleBornHere { get; set; } = new List<Person>();
    public ICollection<Person> PeopleLivingHere { get; set; } = new List<Person>();
}
