using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models.CommonDto;

[BsonIgnoreExtraElements]
public class FetchCommentsWithReplyDto : FetchCommentsDto
{
    public List<FetchReplyDto> replies { get; set; }
    
}
