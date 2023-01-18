namespace IWantApp.EndPoints.Products;

public record ProductResponce(string Name, string CategotyName, string Description, decimal Price, bool HasStock, bool Active);
