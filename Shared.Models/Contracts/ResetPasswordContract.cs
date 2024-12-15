using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models.Contracts;

public class ResetPasswordContract
{
    public string name { get; set; }
    public string email { get; set; }
    public string confirmationLink { get; set; }
}
