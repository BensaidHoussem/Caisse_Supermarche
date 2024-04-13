using Entities.Data;
using Microsoft.AspNetCore.Mvc;

[Produces("application/json")]
[Route("Admin")]
[ApiController]
public class UtilisateurController:Controller{
    protected readonly IService<Utilisateur> _service;
    protected readonly IHash_password _password;
    public UtilisateurController(IService<Utilisateur> service,IHash_password pass)
    {
        _service = service;
        _password=pass;
    }

    [Route("GettAllUtilisateurs")]
    [HttpGet]
    public async Task<ActionResult<List<Utilisateur>>> GetAllUtilisateur(){
        try{
            var articles = await _service.GetAll();
            return articles.Count()>0 ? Ok(articles.OrderBy(x=>x.Id)):NotFound(new{Message="Liste Utilisateur Vide"});
            
        }catch(Exception ex){
            return BadRequest(new{Message="erreur"+ex.Message});
        }
    }
    [Route("GetUtilisateurById")]
    [HttpGet]
    public async Task<ActionResult<Utilisateur>> GetByIdUtilisateur(int id){
        try{
            var Utilisateur = await _service.GetById(id);
            return Utilisateur!=null ? Ok(Utilisateur):NotFound(new{Message="Utilisateur n'existe pas verifier id"});

        }catch(Exception ex){return BadRequest(new{Message="erreur: "+ex.Message});}

    }
    [Route("AddUtilisateur")]
    [HttpPost]
    public async Task<ActionResult<bool>> AddUtilisateur(Utilisateur utilisateur){
        utilisateur.Password=await _password.hashPassword(utilisateur.Password);
        try{
            bool result = await _service.Add(utilisateur);
            return result?Ok(new {Message="Votre Utilisateur a ete ajouter"}):BadRequest(new{
            Message="Utilisateur n'est pas enregistrer"
            });

        }catch(Exception ex){return BadRequest(new{Message="erreur: "+ex.Message});}

    } 

    [Route("AddListUtilisateur")]
    [HttpPost]
    public async Task<ActionResult<bool>> AddlistUtilisateur(List<Utilisateur> utilisateurs){
        foreach(var utilisateur in utilisateurs){
            utilisateur.Password=await _password.hashPassword(utilisateur.Password);
        }
        try{
            bool result = await _service.AddList(utilisateurs);
            return result?Ok(new {Message="Votre liste d'utilisateur a ete ajouter"}):BadRequest(new{
            Message="liste d'utilisateur n'est pas ajouter"
            });

        }catch(Exception ex){return BadRequest(new{Message="erreur: "+ex});}

    }     
    [Route("Updateutilisateur")]
    [HttpPut]
    public async Task<ActionResult<bool>> UpdateUtilisateur(Utilisateur utilisateur){
        utilisateur.Password=await _password.hashPassword(utilisateur.Password);
        try{
            bool result = await _service.Update(utilisateur);
            return result?Ok(new {Message="Votre utilisateur a ete Modifier"}):BadRequest(new{
            Message="utilisateur n'est pas Modifier"
            });

        }catch(Exception ex){return BadRequest(new{Message="Erreur:"+ex.Message});}
        
    }


    [Route("DeleteUtilisateur")]
    [HttpDelete]
    public async Task<ActionResult<bool>> DeleteUtilisateur(Utilisateur utilisateur){

        try{
            bool result = await _service.Remove(utilisateur);
            return result?Ok(new {Message="Votre Utilisateur a ete supprimer"}):BadRequest(new{
            Message="Utilisateur n'existe pas verifier votre article"
            });

        }catch(Exception ex){return BadRequest(new{Message="Erreur: "+ex.Message});}

    }     
    [Route("DeleteUtilisateurById")]
    [HttpDelete]
    public async Task<ActionResult<bool>> DeleteUtilisateurById(int id){
        try{
            bool result = await _service.RemoveById(id);
            return result?Ok(new {Message="Votre Utilisateur a ete supprimer"}):BadRequest(new{
            Message="Utilisateur n'est pas supprimer verifier votre id article"
            });

        }catch(Exception ex) {return BadRequest(new{Message=" Erreur: " + ex.Message});}

    }     


}