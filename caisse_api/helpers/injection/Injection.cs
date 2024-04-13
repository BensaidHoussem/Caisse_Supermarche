using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

public static class Injection{


    public static IServiceCollection InjectionHelpers(this IServiceCollection service){
        service.AddScoped<IauthUtilisateur,authUtilisateur>();
        service.AddScoped<IPoint_Vente,Point_Vente>();
        service.AddScoped<ICaise_Dispo,Caise_Dispo>();
        service.AddScoped<IHash_password,Hash_password>();
        return service;

    }
}