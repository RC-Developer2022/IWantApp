namespace IWantApp.Domain.Employee;

public record class EmployeeRequest(string Email, string Password, string Name, string EmployeeCode);