namespace HouseExpenseTracker.Models;

public class ExpenseGroupListItemDto : List<ExpenseListItemDto>
{
    /// <summary>
    /// Month and Year of consolidated group
    /// </summary>
    public string GroupName { get; set; }

    public float TotalAmount { get; set; }

    public ExpenseGroupListItemDto(string groupName, float totalAmount, List<ExpenseListItemDto> expenses) : base(expenses)
    {
        GroupName = groupName;
        TotalAmount = totalAmount;
    }
}
