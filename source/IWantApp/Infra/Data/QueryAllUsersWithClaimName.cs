using Dapper;
using IWantApp.Domain.Employee;
using Microsoft.Data.SqlClient;

namespace IWantApp.Infra.Data;

public class QueryAllUsersWithClaimName
{
    private readonly IConfiguration configuration;

    public QueryAllUsersWithClaimName(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public async Task<IEnumerable<EmployeeResponse>> Execute(int page, int rows)
    {
        var db = new SqlConnection(configuration.GetConnectionString("IWantSQlite"));
        var query = @"SELECT Email, ClaimValue AS Name
                        FROM AspNetUser u 
                        INNER JOIN AspNetUserClaims as c
                        ON u.id - c.UserId and claimType = 'Name'
                        ORDER BY Name
                        OFFSET (@page -1) * @rows ROWS FETCH NEXT @rows ROWS ONLY
                    ";
        return await db.QueryAsync<EmployeeResponse>(
            query,
            new {page, rows}
        );
    }
}