using System;
using System.Collections.Generic;
using Distributor.BLL.Interfaces;

namespace Distributor.BLL.Infrastructure
{
    public class CantGetByIdError : BasicError
    {
        public CantGetByIdError() : this(new List<String> { "No have errors" }) { }
        public CantGetByIdError(string error) : this(new List<string> { error }) { }
        public CantGetByIdError(IEnumerable<string> Error) : base(Error, ErrorCode.CantGetById) { }
    }
}
