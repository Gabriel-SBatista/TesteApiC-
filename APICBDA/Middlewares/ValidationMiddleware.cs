using APICBDA.Models;
using Azure.Core;
using Microsoft.AspNetCore.Http.Headers;
using Microsoft.Extensions.Primitives;
using System.Text;
using System.Text.Json;
using System.Text.Unicode;

namespace APICBDA.Middlewares;

public class ValidationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IHttpClientFactory _clientFactory;

    public ValidationMiddleware(RequestDelegate next, IHttpClientFactory clientFactory)
    {
         _next = next;
        _clientFactory = clientFactory;
    }

    public async Task Invoke(HttpContext context)
    {
        string dadosParaEnviar = "{\"key\":\"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjUiLCJuYmYiOjE2OTk4NzkyOTEsImV4cCI6MTY5OTg3OTMyMSwiaWF0IjoxNjk5ODc5MjkxfQ.-gKEP-S2ibc7HIYdA30X0D4Vn3aAEsxS52FOlOJA3HQ\"}";

        HttpContent conteudo = new StringContent(dadosParaEnviar, Encoding.UTF8, "application/json");

        var client = _clientFactory.CreateClient();

        HttpResponseMessage resposta = await client.PostAsync("https://localhost:7006/usuarios/validacao", conteudo);

        if (resposta.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsJsonAsync("Usuario nao autorizado!");
            return;
        }
                
        await _next(context);
    }
}
