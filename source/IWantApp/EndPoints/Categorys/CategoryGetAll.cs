using IWantApp.Infra.Data;

namespace IWantApp.EndPoints.Categorys;

public class CategoryGetAll
{
    public static string Template => "/categories";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    public static IResult Action(CategoryResponse categoryResponse, ApplicationDbContext context)
    {
        var categories = context.Categorias.ToList();
        var response = categories.Select(c => new CategoryResponse(c.Id, c.Name, c.Active));

        return Results.Ok(response);
    }
}