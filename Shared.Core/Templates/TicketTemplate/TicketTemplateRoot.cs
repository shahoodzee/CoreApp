using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Core.Templates.TicketTemplate;

public class TicketTemplateRoot
{
    public string ProcessName { get; set; }
    public bool SlaAllowed { get; set; }
    public int SlaTime { get; set; }
    public List<TicketTemplateSubTasks> Details { get; set; }
      
}
