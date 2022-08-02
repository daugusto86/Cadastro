using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Cadastro.Api.Controllers
{
    [ApiController]
    public abstract class MainController : ControllerBase
    {
        //TODO
        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            throw new NotImplementedException();   
        }
    }
}
