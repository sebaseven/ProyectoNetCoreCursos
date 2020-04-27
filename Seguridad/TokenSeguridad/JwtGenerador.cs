using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Aplicacion.Contratos;
using Dominio_ML;
using Microsoft.IdentityModel.Tokens;

namespace Seguridad.TokenSeguridad
{
    public class JwtGenerador : IJwtGenerador
    {

        public string CrearToken(Usuario usuario)
        {
            var claims = new List<Claim>{
               //Payload del token
               new Claim(JwtRegisteredClaimNames.NameId,usuario.UserName)
           };
            var KeyByteArray = Encoding.ASCII.GetBytes("clave1234567890123456789");
            var SigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(KeyByteArray);
            var credenciales = new SigningCredentials(SigningKey, SecurityAlgorithms.HmacSha512Signature);
            var TokenDescripcion = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = credenciales
            };
            var TokenManejador = new JwtSecurityTokenHandler();
            var token = TokenManejador.CreateToken(TokenDescripcion);
            return TokenManejador.WriteToken(token);
        }
    }
}
