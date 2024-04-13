using Microsoft.AspNetCore.Mvc;

[Produces("application/json")]
[Route("Utilisateur")]
[ApiController]
public class Login_Logout:Controller{
    protected readonly IauthUtilisateur _auth;
    public Login_Logout(IauthUtilisateur auth)
    {
        _auth = auth;
    }
    [Route("Login")] 
    [HttpPost]
    public async Task<ActionResult> LoginAsync(string login, string password,decimal init_amount){
        bool success = await _auth.Authentication(login, password,init_amount);
        return success ? Ok(new{Message="Dashboard"}) : NotFound(new{Message="Verfifer votre donnee-- ou caisse reserver "});

    }
    [Route("Logout")]
    [HttpGet]
    public async Task<ActionResult> Logout(string login){
        bool logout=await _auth.LogOut(login);
        return logout ? Ok(new{Message="Page Login"}):BadRequest(new{Message="impossible de logout"});
    }
    


}