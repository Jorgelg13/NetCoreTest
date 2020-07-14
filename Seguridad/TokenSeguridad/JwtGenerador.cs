using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Aplicacion.Contratos;
using Dominio;
using Microsoft.IdentityModel.Tokens;

namespace Seguridad
{
    public class JwtGenerador : IJwtGenerador
    {
        public string crearToken(Usuario usuario)
        {
           var claims = new List<Claim>{
               new Claim(JwtRegisteredClaimNames.NameId, usuario.UserName)
           };

           var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MiPalabraSecreta"));
           var credenciales = new SigningCredentials(key,SecurityAlgorithms.HmacSha512Signature);

            //descripcion del token
           var tokenDescripcion = new SecurityTokenDescriptor{
               Subject = new ClaimsIdentity(claims),
               Expires = DateTime.Now.AddDays(30), //el tiempo de vida del token
               SigningCredentials = credenciales  //tipo de acceso para el login
           };

           var tokenManejador = new JwtSecurityTokenHandler();// se instancia un token handler security
           var token = tokenManejador.CreateToken(tokenDescripcion); //se crea el token basado en la estructura que se creo anteriormente

           return tokenManejador.WriteToken(token);//retorna el token que se va generar
        }
    }
}