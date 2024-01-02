using System;
using System.Collections.Generic;

namespace SlidesShow.Models;

public partial class CompanySlide
{
    public int Id { get; set; }

    public int PropertyId { get; set; }

    public string? ImagePath { get; set; }

    public string? Name { get; set; }

    public int? Width { get; set; }

    public int? Height { get; set; }

    public long? FileSize { get; set; }

    public int? Duration { get; set; }

    public int? BackgroundColor { get; set; }

    public bool? Footer { get; set; }

    public bool? IsImage { get; set; }

    public virtual Company Property { get; set; } = null!;

}
