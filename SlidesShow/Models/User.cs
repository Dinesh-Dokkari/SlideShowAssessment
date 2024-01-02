using System;
using System.Collections.Generic;

namespace SlidesShow.Models;

public partial class User
{
    public int Id { get; set; }

    public string UserName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int? SavedCompanyId { get; set; }

    public int? SavedClubId { get; set; }
}
