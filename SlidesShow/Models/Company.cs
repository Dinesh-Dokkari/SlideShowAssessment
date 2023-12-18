using System;
using System.Collections.Generic;

namespace SlidesShow.Models;

public partial class Company
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? CompanyId { get; set; }

    public bool? IsParent { get; set; }

    public virtual ICollection<CompanySlide> CompanySlides { get; set; } = new List<CompanySlide>();
}
