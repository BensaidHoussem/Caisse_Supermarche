
using Entities.Data;
using Microsoft.EntityFrameworkCore;

public class Point_Vente : IPoint_Vente
{
    protected readonly IDbContextCaisse _dbContextCaisse;
    protected CaisseDbContext _dbContext=>_dbContextCaisse?.DbContext;
    protected readonly IService<Vente> _vente;
    protected readonly IService<Lignedevente> _lignedevente;
    protected readonly IService<Article> _article;
    private static int x,y;
    private static decimal total=0;
    public Point_Vente(IDbContextCaisse dbContextCaisse,IService<Vente> vente,IService<Lignedevente> lignedevente,IService<Article> article)
    {
        _dbContextCaisse = dbContextCaisse;
        _vente = vente;
        _lignedevente = lignedevente;
        _article= article;
        x=_dbContext.Lignedeventes.Max(i=>i.Id);
        y=_dbContext.Ventes.Max(i=>i.Id);

    }


    public async Task<string> AddVente(int articleref,int qte=1)
    {
        // 0 pour signale la fin de la vente
        if(articleref !=0){
            if(total==0){
                //JWT ! quelle session fait le vente
                await _vente.Add(new(){Id=y+1,DateVente=DateTime.Now,Total=total,IdSession=_dbContext.Sessions.Max(x=>x.Id)});// dans notre cas la dernier session fait le vente   !!!!'
            }
            Article Aricle_Vendue=await _dbContext.Articles.Where(i => i.NumeroIdentification==articleref).FirstOrDefaultAsync();
            decimal cost=Aricle_Vendue.Prix;
            total+=cost*qte;
            Aricle_Vendue.Qte-=qte;
            await _article.Update(Aricle_Vendue);
            await _lignedevente.Add(new(){Id=x+1,IdArticle=Aricle_Vendue.Id,IdVente=_dbContext.Ventes.Max(i=>i.Id),Quantite=qte,PrixUnitaire =cost,PrixTotal=cost*qte});
            return await Task.FromResult("Nom Article: *"+Aricle_Vendue.Libelle+"* prix unitaire d'article: *"+cost+"* Total= *"+cost*qte+"*");

        }

        Vente T=await _vente.GetById(_dbContext.Ventes.Max(i=>i.Id));
        T.Total=total;
        await _vente.Update(T);
        return await Task.FromResult("Fin De vente ------- Total a paye:"+total);


    }

    public Task<bool> Payement(string typePayment)
    {
        throw new NotImplementedException();
    }

}