using System;
using System.Collections.Generic;

namespace Entities.Data;

public partial class Caisse
{
    public int Id { get; set; }

    public string Poste { get; set; }

    public virtual ICollection<Session> Sessions { get; set; } = new List<Session>();
}
