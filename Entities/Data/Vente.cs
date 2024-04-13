using System;
using System.Collections.Generic;

namespace Entities.Data;

public partial class Vente
{
    public int Id { get; set; }

    public DateTime? DateVente { get; set; }

    public decimal Total { get; set; }

    public int IdSession { get; set; }

    public virtual Session IdSessionNavigation { get; set; }

    public virtual ICollection<Lignedevente>? Lignedeventes { get; set; } = new List<Lignedevente>();

    public virtual ICollection<Paiement>? Paiements { get; set; } = new List<Paiement>();
}
