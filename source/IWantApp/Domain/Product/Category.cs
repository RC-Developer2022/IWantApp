using Flunt.Validations;

namespace IWantApp.Domain.Product;

public class Category : Entity
{

    public string Name { get; private set; }
    public bool Active { get; private set; }


    public Category(string name, string createdBy, string editedBy)
    {
        Name = name;
        CreatedBy = createdBy;
        CreatedOn = DateTime.Now;
        EditedBy = editedBy;
        EditedOn = DateTime.Now;
        Active = true;

        Validate();
    }

    private void Validate()
    {
        var contract = new Contract<Category>()
            .IsNotNullOrEmpty(Name, "Name").IsGreaterOrEqualsThan(Name, 3, "Name")
            .IsNotNullOrEmpty(CreatedBy, "CreatedBy")
            .IsNotNullOrEmpty(EditedBy, "EditedBy");

        AddNotifications(contract);
    }

    public void EditInfo(string name, bool active)
    {
        Active = active;
        Name = name;
        
        Validate();

    }
}