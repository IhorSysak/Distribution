using System;
using System.Collections.Generic;
using Distributor.BLL.Interfaces;

namespace Distributor.BLL.Infrastructure
{
    public class CreationError : BasicError
    {
        public CreationError() : this(new List<String> { "Cant create , Check Atributes that your write" }) { }
        public CreationError(IEnumerable<string> Error) : base(Error, ErrorCode.CreationError) { }
    }
}
