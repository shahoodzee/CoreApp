namespace Shared.Models.CommonDto;

public class UserDashboardStatsDto
{
    public long TotalSR { get; set; } = 0;
    public long OpenSR { get; set; } = 0;
    public long ClosedSR { get; set; } = 0;
    public long InProgressSR { get; set; } = 0;
}
