public interface IauthUtilisateur{
    Task<bool>Authentication(string login, string password,decimal initamount);
    Task<bool>LogOut(string login);//ou user name pour connaitre le caisse (mais en cas reel id caisse initialiser au debuit)
}