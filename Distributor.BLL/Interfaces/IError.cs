using System.Collections.Generic;

namespace Distributor.BLL.Interfaces
{
    public enum ErrorCode
    {
        CantGetById = 1001,
        NullableItem = 1002,
        CountIsZero = 1003,
        IncorrectId = 1004,
        CreationError = 1005,
    }
    public interface IError
    {
        ErrorCode Errorcode { get; }
        IEnumerable<string> Errors { get; }
    }
}
