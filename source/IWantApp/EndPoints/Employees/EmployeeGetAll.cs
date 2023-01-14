using IWantApp.Infra.Data;

namespace IWantApp.EndPoints.Employees;

public class EmployeeGetAll
{
    public static string Template => "/employee";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    public static IResult Action(int? page, int? rows, QueryAllUsersWithClaimName query)
    {
        if (page == null)
            return Results.BadRequest("Page and rows is required");
        if (rows == null || rows > 5)
            return Results.BadRequest("Rows is required and need to be less than 6");
            
        return Results.Ok(query.Execute(page.Value, rows.Value));
    }
}