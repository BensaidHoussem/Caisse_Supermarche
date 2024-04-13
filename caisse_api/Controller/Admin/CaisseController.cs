using Entities.Data;
using Microsoft.AspNetCore.Mvc;

[Produces("application/json")]
[Route("Admin")]
[ApiController]
public class CaisseController:Controller{
    protected readonly IService<Caisse> _service;
    public CaisseController(IService<Caisse> service)
    {
        _service = service;
    }

    [Route("GettAllCaisse")]
    [HttpGet]
    public async Task<ActionResult<List<Caisse>>> GetAllArticle(){
        try{
            var Caisses = await _service.GetAll();
            return Caisses.Count()>0 ? Ok(Caisses.OrderBy(x=>x.Id)):NotFound(new{Message="Liste Caisse Vide"});
            
        }catch(Exception ex){
            return BadRequest(new{Message="erreur"+ex.Message});
        }
    }
    [Route("GetCaisseById")]
    [HttpGet]
    public async Task<ActionResult<Caisse>> GetByIdCaisse(int id){
        try{
            var caisse = await _service.GetById(id);
            return caisse!=null ? Ok(caisse):NotFound(new{Message="caisse n'existe pas verifier id"});

        }catch(Exception ex){return BadRequest(new{Message="erreur: "+ex.Message});}

    }
    [Route("AddCaisse")]
    [HttpPost]
    public async Task<ActionResult<bool>> AddArticle(Caisse caisse){
        try{
            bool result = await _service.Add(caisse);
            return result?Ok(new {Message="Votre caisse a ete ajouter"}):BadRequest(new{
            Message="caisse n'est pas enregistrer"
            });

        }catch(Exception ex){return BadRequest(new{Message="erreur: "+ex.Message});}

    } 

    [Route("AddListCaisse")]
    [HttpPost]
    public async Task<ActionResult<bool>> AddlistArticle(List<Caisse> caisses){
        try{
            bool result = await _service.AddList(caisses);
            return result?Ok(new {Message="Votre liste de caisse a ete ajouter"}):BadRequest(new{
            Message="liste de caisse n'est pas ajouter"
            });

        }catch(Exception ex){return BadRequest(new{Message="erreur: "+ex});}

    }     
    [Route("UpdateCaisse")]
    [HttpPut]
    public async Task<ActionResult<bool>> UpdateArticle(Caisse caisse){
        try{
            bool result = await _service.Update(caisse);
            return result?Ok(new {Message="Votre caisse a ete Modifier"}):BadRequest(new{
            Message="caisse n'est pas Modifier"
            });

        }catch(Exception ex){return BadRequest(new{Message="Erreur:"+ex.Message});}
        
    }


    [Route("DeleteCaisse")]
    [HttpDelete]
    public async Task<ActionResult<bool>> DeleteArticle(Caisse caisse){
        try{
            bool result = await _service.Remove(caisse);
            return result?Ok(new {Message="Votre caisse a ete supprimer"}):BadRequest(new{
            Message="caisse n'existe pas verifier votre caisse"
            });

        }catch(Exception ex){return BadRequest(new{Message="Erreur: "+ex.Message});}

    }     
    [Route("DeletCaisseById")]
    [HttpDelete]
    public async Task<ActionResult<bool>> DeleteArticleById(int id){
        try{
            bool result = await _service.RemoveById(id);
            return result?Ok(new {Message="Votre caisse a ete supprimer"}):BadRequest(new{
            Message="caisse n'est pas supprimer verifier votre id article"
            });

        }catch(Exception ex) {return BadRequest(new{Message=" Erreur: " + ex.Message});}

    }     


}