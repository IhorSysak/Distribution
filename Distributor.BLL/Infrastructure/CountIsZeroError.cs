using System;
using System.Collections.Generic;
using Distributor.BLL.Interfaces;

namespace Distributor.BLL.Infrastructure
{
    public class CountIsZeroError : BasicError
    {
        public CountIsZeroError() : this(new List<String> { "No have errors" }) { }
        public CountIsZeroError(string error) : this(new List<string> { error }) { }
        public CountIsZeroError(IEnumerable<string> Error) : base(Error, ErrorCode.CountIsZero) { }
    }
}
