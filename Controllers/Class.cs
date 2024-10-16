namespace aspnet_mongo.Controllers;

using Microsoft.AspNetCore.Mvc;
using aspnet_mongo.Models;
using MongoDB.Driver;

[ApiController]
[Route("api/[controller]")]
public class TestController : ControllerBase
{
    private readonly ContextMongoDb _context;

    public TestController(ContextMongoDb context)
    {
        _context = context;
    }

    [HttpGet("test-connection")]
    public IActionResult TestConnection()
    {
        try
        {
            var users = _context.User.Find(_ => true).ToList(); // Tente buscar todos os usuários
            return Ok($"Conexão bem-sucedida! Encontrados {users.Count} usuários.");
        }
        catch (Exception ex)
        {
            return BadRequest($"Erro ao conectar ao MongoDB: {ex.Message}");
        }
    }
}
