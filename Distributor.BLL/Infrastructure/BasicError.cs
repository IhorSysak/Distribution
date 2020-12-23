using System;
using System.Collections.Generic;
using Distributor.BLL.Interfaces;

namespace Distributor.BLL.Infrastructure
{
    public abstract class BasicError : Exception, IError
    {
        public BasicError(IEnumerable<string> errors, ErrorCode errorcode)
        {
            this.Errorcode = errorcode;
            this.Errors = errors;
        }
        public ErrorCode Errorcode { get; }
        public IEnumerable<string> Errors { get; }
    }
}
