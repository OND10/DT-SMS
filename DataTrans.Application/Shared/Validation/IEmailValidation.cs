
using DataTrans.Application.Common.Handler;

namespace DataTrans.Application.Shared.Validations
{
    public interface IEmailValidation
    {
        Task<Result<string>> Emailvalidate();
    }
}
