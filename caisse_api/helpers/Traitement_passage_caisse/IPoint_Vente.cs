public interface IPoint_Vente{
    Task<string>AddVente(int articleid,int qte);
    Task<bool>Payement(string typePayment);
}