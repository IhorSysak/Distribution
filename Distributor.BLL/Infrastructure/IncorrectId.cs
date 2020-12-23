using System;
using Distributor.BLL.Interfaces;
using System.Collections.Generic;

namespace Distributor.BLL.Infrastructure
{
    public class IncorrectId : BasicError
    {
        public IncorrectId() : this(new List<String> { "Id Cant be <=0" }) { }
        public IncorrectId(IEnumerable<string> Error) : base(Error, ErrorCode.IncorrectId) { }
    }
}
