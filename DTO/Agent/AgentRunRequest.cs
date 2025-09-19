namespace Mini_Expense_Tracker.DTO.Agent
{
    public record AgentRunRequest(
    string Company,
    string Country,
    string Industry,
    string Region,
    int TargetYear
);
}
