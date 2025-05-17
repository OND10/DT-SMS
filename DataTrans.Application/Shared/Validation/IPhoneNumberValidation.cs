

using DataTrans.Application.Common.Handler;

namespace DataTrans.Application.Shared.Validations
{
    public interface IPhoneNumberValidation
    {
        Task<Result<string>> PhoneNumbervalidate();
    }
}
