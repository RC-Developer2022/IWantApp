namespace IWantApp.EndPoints.Products;

public record ProductRequest(string Name, Guid categoryId, string Description, decimal Price, bool HasStock, bool Active);