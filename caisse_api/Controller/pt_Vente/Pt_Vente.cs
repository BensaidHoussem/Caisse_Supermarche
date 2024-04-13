using Microsoft.AspNetCore.Mvc;

[Produces("application/json")]
[Route("Passage_Caisse")]
[ApiController]
public class Pt_Vente:Controller{
    protected readonly IPoint_Vente _pt;
    public Pt_Vente(IPoint_Vente pt)
    {
        _pt = pt;
    }
    [Route("vente")]
    [HttpPost]
    public async Task<ActionResult<string>>AjouterVente(int articleref,int qte=1){
        do{
            return Ok(Task.FromResult(await _pt.AddVente(articleref,qte)).Result);

        }while(articleref==0);



    }

}