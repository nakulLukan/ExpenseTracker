namespace HouseExpenseTracker.Models;

public class ExpenseListItemDto
{
    public int Id { get; set; }
    public string ExpenseName { get; set; }
    public string Description { get; set; }
    public string PaidTo { get; set; }
    public string AddedMonth { get; set; }
    public string AddedDate { get; set; }
    public float Amount { get; set; }
}
