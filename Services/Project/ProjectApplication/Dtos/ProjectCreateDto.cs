﻿using System.ComponentModel.DataAnnotations;

namespace ProjectApplication.Dtos;

public class ProjectCreateDto
{
    [Required]
    public string Name { get; set; } = default!;
    [Required]
    [RegularExpression("^((?!00000000-0000-0000-0000-000000000000).)*$", ErrorMessage = "Cannot use default Guid")]
    public Guid ResponsibleUser { get; set; } = default!;
    public List<Guid> Tasks { get; set; } = new List<Guid>();
    public List<Guid> Users { get; set; } = new List<Guid>();
}
