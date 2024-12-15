namespace Shared.Models.CommonDto;

public class DashboardStatsDto
{
    public long TotalSR { get; set; } = 0;
    public long OpenSR { get; set; } = 0;
    public long ClosedSR { get; set; } = 0;
    public long InProgressSR { get; set; } = 0;
    public long WaitingCalls { get; set; } = 0;
}
