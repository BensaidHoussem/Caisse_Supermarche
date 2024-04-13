using System;
using System.Collections.Generic;

namespace Entities.Data;

public partial class Session
{
    public int Id { get; set; }

    public DateTime? DateDebut { get; set; }

    public DateTime? DateFin { get; set; }

    public decimal? MontantInitial { get; set; }

    public int IdUtilisateur { get; set; }

    public int IdCaisse { get; set; }

    public virtual Caisse IdCaisseNavigation { get; set; }

    public virtual Utilisateur IdUtilisateurNavigation { get; set; }

    public virtual ICollection<Vente> Ventes { get; set; } = new List<Vente>();
}
