using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Core.Templates.TicketTemplate;

public class TicketTemplateSubTasks
{
    public string Title { get; set; }
    public string Description { get; set; }
    public bool AttachmentRequired { get; set; }
    public double HourDeduction { get; set; }
    public bool SLARequired { get; set; }
    public int SLAHourTime { get; set; }
}
