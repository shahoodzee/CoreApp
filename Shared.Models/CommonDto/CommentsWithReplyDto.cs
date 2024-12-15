using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models.CommonDto;

public class CommentsWithReplyDto : CommentsDto
{
    public List<ReplyDto> replies { get; set; }
}
