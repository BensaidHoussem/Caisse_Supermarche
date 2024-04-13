using System;
using System.Collections.Generic;

namespace Entities.Data;

public partial class Lignedevente
{
    public int Id { get; set; }

    public int IdArticle { get; set; }

    public int IdVente { get; set; }

    public int Quantite { get; set; }

    public decimal PrixUnitaire { get; set; }

    public decimal? PrixTotal { get; set; }

    public virtual Article? IdArticleNavigation { get; set; }

    public virtual Vente? IdVenteNavigation { get; set; }
}
