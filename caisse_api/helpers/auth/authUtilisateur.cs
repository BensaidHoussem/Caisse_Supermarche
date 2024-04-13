
using System.Collections;
using Entities.Data;
using Microsoft.EntityFrameworkCore;

public class authUtilisateur : IauthUtilisateur
{
    protected readonly IDbContextCaisse _dbContextCaisse;
    protected CaisseDbContext _dbContext=>_dbContextCaisse?.DbContext;
    protected readonly IService<Session> _service;
    protected readonly ICaise_Dispo _Dispo;
    protected readonly IHash_password _password;


    public authUtilisateur(IDbContextCaisse dbContextCaisse,IService<Session> service,ICaise_Dispo dispo,IHash_password pass)
    {
        _dbContextCaisse = dbContextCaisse;
        _service=service;
        _Dispo=dispo;
        _password=pass;
    }
    public async Task<bool> Authentication(string login, string password,decimal init_amount=75)
    {
        string caisse_libre=await _Dispo.CaisseLibre();
        if (caisse_libre !=null){
            var k = await _dbContext.Utilisateurs.Where(x => x.Login == login).FirstOrDefaultAsync();
            if(k!=null){
                bool success = await _password.VerifyPassword(password,k.Password);
                if(success){
                    await _service.Add(new(){Id=((byte)_dbContext.Sessions.Max(p=>p.Id))+1,DateDebut=DateTime.Now,DateFin=null,MontantInitial=init_amount,IdUtilisateur=k.Id,
                    IdCaisse=await _dbContext.Caisses.Where(o=>o.Poste==caisse_libre).Select(t=>t.Id).FirstOrDefaultAsync()});
                    return await Task.FromResult(true);
                }
            }
        }
        
        return await Task.FromResult(false);
    }

    public async Task<bool> LogOut(string loginName)
    {
        int j=await _dbContext.Utilisateurs.Where(l=>l.Login==loginName).Select(y=>y.Id).FirstOrDefaultAsync();
        int current_session= await _dbContext.Sessions.Where(i=>i.DateFin==null && i.IdUtilisateur==j).Select(u=>u.Id).FirstOrDefaultAsync();
        Session c=await _service.GetById(current_session);
        c.DateFin=DateTime.Now;
        await _service.Update(c);
        return Task.FromResult(true).Result;
    }
}