namespace Shared.Models.CommonDto;

public class FetchProcessFlowDto
{
    public long iProcessFlowId { get; set; }
    public string TemplateName { get; set; } = null!;
    public string TemplateJson { get; set; } = null!;
    public bool isSLAEnabled { get; set; }
    public bool isRecursive { get; set; }
    public string? CronExpression { get; set; }

}
