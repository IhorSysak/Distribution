using System;
using System.Collections.Generic;
using Distributor.BLL.Interfaces;

namespace Distributor.BLL.Infrastructure
{
    public class NullableItemError : BasicError
    {
        public NullableItemError() : this(new List<String> { "Nullable Item" }) { }
        public NullableItemError(IEnumerable<string> Error) : base(Error, ErrorCode.NullableItem) { }
    }
}
