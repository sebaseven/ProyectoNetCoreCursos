using Aplicacion.Contratos;
using Aplicacion.ManejadorErrores;
using Aplicacion.Seguridad;
using Dominio_ML;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Seguridad
{
    public class Registrar
    {
        public class Ejecuta : IRequest<UsuarioData>
        {
            public string Nombre { get; set; }
            public string Apellidos { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public string UserName { get; set; }
        }
        public class EjecutaValidador : AbstractValidator<Ejecuta> {
           public EjecutaValidador()
           {
               RuleFor(x => x.Apellidos).NotEmpty();
               RuleFor(x => x.Email).NotEmpty();
               RuleFor(x => x.Nombre).NotEmpty();
               RuleFor(x => x.Password).NotEmpty();
               RuleFor(x => x.UserName).NotEmpty();
           }
        }
        public class Manejador : IRequestHandler<Ejecuta,UsuarioData>
        {
            private readonly CursosOnlineContext _context;

            private readonly UserManager<Usuario> _usuariomanager;

            private readonly IJwtGenerador _IJwtGenerador;

            public Manejador(CursosOnlineContext context, UserManager<Usuario> usuariomanager, IJwtGenerador IJwtGenerador)
            {
                context = _context;
                usuariomanager = _usuariomanager;
                IJwtGenerador = _IJwtGenerador;

            }
            public async Task<UsuarioData> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                //valido que no exista el email
                var existe = await _context.Users.Where(x => x.Email == request.Email).AnyAsync();
                if (existe)
                {
                    throw new ManejadorExcepcion(System.Net.HttpStatusCode.BadRequest, new { mensaje = "Ya existe un usuario con ese EMail" });
                }
                var existeusername = await _context.Users.Where(x => x.UserName == request.UserName).AnyAsync();
                if (existeusername)
                {
                    throw new ManejadorExcepcion(System.Net.HttpStatusCode.BadRequest, new { mensaje = "Ya existe un usuario con ese UserName" });
                }
                var usuario = new Usuario
                {
                    NombreCompleto = request.Nombre + " " + request.Apellidos,
                    Email = request.Email,
                    UserName=request.UserName
                };
                var result = await _usuariomanager.CreateAsync(usuario, request.Password);
                if (result.Succeeded)
                {
                    return new UsuarioData { 
                    NombreCompleto =usuario.NombreCompleto,
                    Token = _IJwtGenerador.CrearToken(usuario),
                    UserName = usuario.UserName
                    };
                }
                else { throw new Exception("No se pudo agregar el usuario"); }
            }
        }
    }

}


