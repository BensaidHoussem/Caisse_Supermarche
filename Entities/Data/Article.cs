using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Entities.Data;

public partial class Article
{
    public int Id { get; set; }

    public int? NumeroIdentification { get; set; }

    public string Libelle { get; set; }

    public decimal Prix { get; set; }

    public string ImageUrl { get; set; }

    public int? Qte { get; set; }

    public int IdCategory { get; set; }

    public virtual Category? IdCategoryNavigation { get; set; }=null;

    public virtual ICollection<Lignedevente>? Lignedeventes { get; set; } =new List<Lignedevente>();
}
