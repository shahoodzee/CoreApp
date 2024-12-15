using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models.CommonDto;

[BsonIgnoreExtraElements]
public class FetchReplyDto
{
    public ObjectId id { get; set; }
    public long iCreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    public long? iModifiedBy { get; set; }
    public DateTime? ModifyDate { get; set; }
    public bool isActive { get; set; }
    public ObjectId iCommentId { get; set; }
    public string vReply { get; set; }
    public string vCreatedby { get; set; }
  
}
