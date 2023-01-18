namespace IWantApp.EndPoints.Clients;

public record OrderRequest(List<Guid> ProductIds, string DeliveryAddress);