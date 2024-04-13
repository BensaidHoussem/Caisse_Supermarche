
using System.Collections;
using Entities.Data;
using Microsoft.EntityFrameworkCore;

public class Caise_Dispo : ICaise_Dispo
{

    protected readonly IDbContextCaisse _dbContextCaisse;
    protected CaisseDbContext _dbContext=>_dbContextCaisse?.DbContext;
    protected readonly IService<Session> _service;
    private ArrayList Caisse_Exist=new ArrayList();
    private ArrayList Caisse_reserver=new ArrayList();
    private ArrayList Caisse_libre=new ArrayList();

    public Caise_Dispo(IDbContextCaisse dbContextCaisse,IService<Session> service)
    {
        _dbContextCaisse = dbContextCaisse;
        _service = service;
    }

    public async Task<string> CaisseLibre()
    {
        Caisse_Exist.AddRange(await _dbContext.Caisses.Select(p=>p.Poste).ToArrayAsync());
        List<int> poste=await _dbContext.Sessions.Where(x=>x.DateFin==null).Select(t=>t.IdCaisse).ToListAsync();
        foreach(int g in poste){
            Caisse_reserver.Add(await _dbContext.Caisses.Where(h=>h.Id==g).Select(p=>p.Poste).FirstOrDefaultAsync());
        }
        foreach(var item in Caisse_Exist){
            if(!Caisse_reserver.Contains(item)){
                Caisse_libre.Add(item);
            }
        }
        if(Caisse_libre.Count > 0){
            Random rnd=new Random();
            int f=rnd.Next(0,Caisse_libre.Count+1);  
            var c= Caisse_libre[f].ToString();
            Caisse_libre.RemoveAt(f);
            return c;
        }
        else
        return null;

                 
    }
}