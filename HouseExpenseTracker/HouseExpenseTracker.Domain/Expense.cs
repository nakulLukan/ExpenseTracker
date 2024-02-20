
namespace HouseExpenseTracker.Domain;

public class Expense
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime ExpenseAddedOn { get; set; }
    public int? PaidToId { get; set; }
    public int PaidById { get; set; }
    public float Amount { get; set; }

    public Person PaidTo { get; set; }
    public Person PaidBy { get; set; }
}
