using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nest;
using Search.API.Models;

namespace Search.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsSearchController : ControllerBase
    {
        private readonly IElasticClient _elasticClient;

        public NewsSearchController(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }

        [HttpGet]
        public async Task<IActionResult> SearchNews([FromQuery] string keyword)
        {
            var result = await _elasticClient
                .SearchAsync<News>(s => s
                    .Query(x => x
                        .Match(m => m
                            .Field(f => f.Text)
                            .Query('*' + keyword + '*')
                        )
                    )
                    .Size(10)
                );

            return Ok(result?.Documents?.ToList());
        }
    }
}