using IWantApp.Infra.Data;
using Microsoft.AspNetCore.Mvc;

namespace IWantApp.EndPoints.Categorys;

public class CategoryGet
{
    public static string Template => "/categories/{id:guid}";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    public static IResult Action([FromRoute] Guid Id, ApplicationDbContext context)
    {
        var categories = context.Categorias.Where(c => c.Id == Id).Select(x => new CategoryResponse { Id = x.Id, Name = x.Name, Active = x.Active }).FirstOrDefault();

        return Results.Ok(categories);
    }
}