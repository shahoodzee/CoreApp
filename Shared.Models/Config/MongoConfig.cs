using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models.Config
{
    public class MongoConfig
    {
        public string MongoConnection { get; set; }
        public string DatabaseName { get; set; }
        public string AttachmentsCollection { get; set; }
        public string CommentsCollection { get; set; }
    }
}
