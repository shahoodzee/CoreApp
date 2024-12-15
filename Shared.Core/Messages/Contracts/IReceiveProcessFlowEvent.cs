using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Core.Messages.Contracts;

public interface IReceiveProcessFlowEvent
{
    string JsonTemplate { get; set; }
}
