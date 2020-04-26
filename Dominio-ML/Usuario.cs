using Microsoft.AspNetCore.Identity;

namespace Dominio_ML
{
    public class Usuario : IdentityUser
    {
       public string NombreCompleto { get; set; } 
    }
}