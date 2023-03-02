using IdentityServer.Shared.Client.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GlowingNews.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private IJwtTokenRepository _jwtTokenRepository;

        public WeatherForecastController(IJwtTokenRepository jwtTokenRepository)
        {
            _jwtTokenRepository = jwtTokenRepository;
        }

        [HttpPost]
        public async Task<IActionResult> getTokenDetail([FromForm]string token)
        {
            var aa = _jwtTokenRepository.ExtractUserDataFromToken(token);
            return Ok(aa);
        }
    }
}