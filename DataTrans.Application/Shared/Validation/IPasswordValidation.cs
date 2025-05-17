
using DataTrans.Application.Common.Handler;

namespace DataTrans.Application.Shared.Validations
{
    public interface IPasswordValidation
    {
        Task<Result<string>> Passwordvalidate();
    }
}
