using System.Security.Claims;
using IWantApp.Domain.Orders;
using IWantApp.Domain.Products;
using IWantApp.Infra.Data;
using Microsoft.AspNetCore.Authorization;

namespace IWantApp.EndPoints.Clients;

public class OrderPost
{
    public static string Template => "/orders";
    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
    public static Delegate Handle => Action;

    [Authorize(Policy = "CpfPolicy")]
    public static async Task<IResult> Action(OrderRequest orderRequest, HttpContext http, ApplicationDbContext context)
    {
        var clientId = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
        var clientName = http.User.Claims.First(c => c.Type == "Name").Value;

        // if(orderRequest == null || orderRequest.ProductIds.Any())
        //     return Results.BadRequest("Produto é obrigatório para pedido!");
        // if(string.IsNullOrEmpty(orderRequest.DeliveryAddress))
        //     return Results.BadRequest("Endereço de entrega é obrigatório!");

        List<Product> productsFound = null;

        if(orderRequest.ProductIds != null || orderRequest.ProductIds.Any())
            productsFound = context.Produtos.Where(p => orderRequest.ProductIds.Contains(p.Id)).ToList();

        var order = new Order(clientId, clientName, productsFound, orderRequest.DeliveryAddress);

        if (!order.IsValid)
            return Results.ValidationProblem(order.Notifications.ConvertToProblemDetails());

        await context.Pedidos.AddAsync(order);
        await context.SaveChangesAsync();

        return Results.Created($"/orders/{order.Id}", order.Id);
    }
}