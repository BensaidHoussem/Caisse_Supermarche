using System;
using System.Collections.Generic;

namespace Entities.Data;

public partial class Utilisateur
{
    public int Id { get; set; }

    public string Nom { get; set; }

    public string Prenom { get; set; }

    public string Role { get; set; }

    public string Login { get; set; }

    public string Password { get; set; }

    public virtual ICollection<Session> Sessions { get; set; } = new List<Session>();
}
