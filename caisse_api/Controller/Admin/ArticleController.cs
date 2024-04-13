using Entities.Data;
using Microsoft.AspNetCore.Mvc;

[Produces("application/json")]
[Route("Admin")]
[ApiController]
public class ArticleController:Controller{
    protected readonly IService<Article> _service;
    public ArticleController(IService<Article> service)
    {
        _service = service;
    }

    [Route("GettAllArticle")]
    [HttpGet]
    public async Task<ActionResult<List<Article>>> GetAllArticle(){
        try{
            var articles = await _service.GetAll();
            return articles.Count()>0 ? Ok(articles.OrderBy(x=>x.Id)):NotFound(new{Message="Liste Article Vide"});
            
        }catch(Exception ex){
            return BadRequest(new{Message="erreur"+ex.Message});
        }
    }
    [Route("GetArticleById")]
    [HttpGet]
    public async Task<ActionResult<Article>> GetByIdArticle(int id){
        try{
            var article = await _service.GetById(id);
            return article!=null ? Ok(article):NotFound(new{Message="Article n'existe pas verifier id"});

        }catch(Exception ex){return BadRequest(new{Message="erreur: "+ex.Message});}

    }
    [Route("AddArticle")]
    [HttpPost]
    public async Task<ActionResult<bool>> AddArticle(Article article){
        try{
            bool result = await _service.Add(article);
            return result?Ok(new {Message="Votre article a ete ajouter"}):BadRequest(new{
            Message="article n'est pas enregistrer"
            });

        }catch(Exception ex){return BadRequest(new{Message="erreur: "+ex.Message});}

    } 

    [Route("AddListArticle")]
    [HttpPost]
    public async Task<ActionResult<bool>> AddlistArticle(List<Article> articles){
        try{
            bool result = await _service.AddList(articles);
            return result?Ok(new {Message="Votre liste d'article a ete ajouter"}):BadRequest(new{
            Message="liste d'article n'est pas ajouter"
            });

        }catch(Exception ex){return BadRequest(new{Message="erreur: "+ex});}

    }     
    [Route("UpdateArticle")]
    [HttpPut]
    public async Task<ActionResult<bool>> UpdateArticle(Article article){
        try{
            bool result = await _service.Update(article);
            return result?Ok(new {Message="Votre article a ete Modifier"}):BadRequest(new{
            Message="article n'est pas Modifier"
            });

        }catch(Exception ex){return BadRequest(new{Message="Erreur:"+ex.Message});}
        
    }


    [Route("DeleteArticle")]
    [HttpDelete]
    public async Task<ActionResult<bool>> DeleteArticle(Article article){
        try{
            bool result = await _service.Remove(article);
            return result?Ok(new {Message="Votre article a ete supprimer"}):BadRequest(new{
            Message="article n'existe pas verifier votre article"
            });

        }catch(Exception ex){return BadRequest(new{Message="Erreur: "+ex.Message});}

    }     
    [Route("DeleteArticleById")]
    [HttpDelete]
    public async Task<ActionResult<bool>> DeleteArticleById(int id){
        try{
            bool result = await _service.RemoveById(id);
            return result?Ok(new {Message="Votre article a ete supprimer"}):BadRequest(new{
            Message="article n'est pas supprimer verifier votre id article"
            });

        }catch(Exception ex) {return BadRequest(new{Message=" Erreur: " + ex.Message});}

    }     


}