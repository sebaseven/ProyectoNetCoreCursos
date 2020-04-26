using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.ManejadorErrores;
using Dominio_ML;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Aplicacion_BL.Seguridad
{
    public class Login
    {
        public class Ejecuta : IRequest<Usuario>
        {
            public string EMail { get; set; }
            public string Password { get; set; }
        }
        public class EjecutaValidacion : AbstractValidator<Ejecuta>
        {
            public EjecutaValidacion()
            {
                RuleFor(x => x.EMail).NotEmpty();
                RuleFor(x => x.Password).NotEmpty();
            }
        }
        public class Manejador : IRequestHandler<Ejecuta, Usuario>
        {
            private readonly UserManager<Usuario> _userManager;
            private readonly SignInManager<Usuario> _signInManager;

            public Manejador(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager)
            {
                _signInManager = signInManager;
                _userManager = userManager;
            }
            public async Task<Usuario> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var usuario = await _userManager.FindByEmailAsync(request.EMail);
                if (usuario == null)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.Unauthorized);
                }
                var result = await _signInManager.CheckPasswordSignInAsync(usuario, request.Password, false);
                if (result.Succeeded)
                {
                    return usuario;
                }
                else
                {
                    throw new ManejadorExcepcion(HttpStatusCode.Unauthorized);
                }

            }
        }
    }
}