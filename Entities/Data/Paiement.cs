using System;
using System.Collections.Generic;

namespace Entities.Data;

public partial class Paiement
{
    public int Id { get; set; }

    public int IdVente { get; set; }

    public int IdModepaiement { get; set; }

    public decimal Montant { get; set; }

    public decimal? MontantRendu { get; set; }

    public string NumeroPieceIdentite { get; set; }

    public bool? AutorisationEnvoyee { get; set; }

    public virtual Modepaiement IdModepaiementNavigation { get; set; }

    public virtual Vente IdVenteNavigation { get; set; }
}
