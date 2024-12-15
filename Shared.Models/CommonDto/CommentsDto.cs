using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models.CommonDto;

public class CommentsDto
{
    public string id { get; set; }
    public long iCreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    public long? iModifiedBy { get; set; }
    public DateTime? ModifyDate { get; set; }
    public bool isActive { get; set; }
    public long iServiceRequestId { get; set; }
    public string vComment { get; set; }
    public string vCreatedby { get; set; }
}
