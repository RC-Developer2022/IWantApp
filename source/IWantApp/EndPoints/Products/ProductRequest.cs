namespace IWantApp.EndPoints.Products;

public record ProductRequest(string Name, Guid categoryId, string Description, bool HasStock, bool Active);