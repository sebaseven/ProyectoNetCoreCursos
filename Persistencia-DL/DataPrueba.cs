using System.Linq;
using System.Threading.Tasks;
using Dominio_ML;
using Microsoft.AspNetCore.Identity;
using Persistencia;

namespace Persistencia_DL
{
    public class DataPrueba
    {
        public static async Task InsertarData(CursosOnlineContext context, UserManager<Usuario> usuarioManager)
        {
            if (!usuarioManager.Users.Any())
            {
                var usuario = new Usuario { NombreCompleto = "Seba Morgado", UserName = "sebaseven", Email = "sebaseven@gmail.com" };
                await usuarioManager.CreateAsync(usuario,"Passw@rd123");
            }
        }
    }
}