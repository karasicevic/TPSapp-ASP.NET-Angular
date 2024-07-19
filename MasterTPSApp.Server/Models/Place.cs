using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Place
{
    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public long Id { get; set; }

    [Required]
    [StringLength(40)]
    public string Name { get; set; }

    [Required]
    [Range(11000, 45000, ErrorMessage = "PostalCode must be between 11000 and 45000.")]
    public int PostalCode { get; set; }

    [Required]
    [Range(0, 1999999, ErrorMessage = "Population must be between 0 and 1,999,999.")]
    public int Population { get; set; }

    public ICollection<Person> PeopleBornHere { get; set; }
    public ICollection<Person> PeopleLivingHere { get; set; }
}
