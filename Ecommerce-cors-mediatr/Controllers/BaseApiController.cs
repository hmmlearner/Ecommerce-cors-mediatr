using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce_cors_mediatr.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController : ControllerBase
    {
        private ISender _mediatr = null!;
        protected ISender Mediatr => _mediatr ??= HttpContext.RequestServices.GetRequiredService<ISender>();
    }
}
