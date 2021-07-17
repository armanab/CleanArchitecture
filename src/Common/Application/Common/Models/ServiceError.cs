using System;

namespace CleanApplication.Application.Common.Models
{
    /// <summary>
    /// All errors contained in ServiceResult objects must return an error of this type
    /// Error codes allow the caller to easily identify the received error and take action.
    /// Error messages allow the caller to easily show error messages to the end user.
    /// </summary>
    [Serializable]
    public class ServiceError
    {
        /// <summary>
        /// CTOR
        /// </summary>
        public ServiceError(string message, int code)
        {
            Message = message;
            Code = code;
        }

        public ServiceError() { }

        /// <summary>
        /// Human readable error message
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Machine readable error code
        /// </summary>
        public int Code { get; }

        /// <summary>
        /// Default error for when we receive an exception
        /// </summary>
        public static ServiceError DefaultError => new ServiceError("خطای ناشناخته. لطفا با پشتیبانی تماس بگیرید.", 999);

        /// <summary>
        /// Default validation error. Use this for invalid parameters in controller actions and service methods.
        /// </summary>
        public static ServiceError ModelStateError(string validationError)
        {
            return new ServiceError(validationError, 998);
        }
        public static ServiceError WalletIsNotEnough => new ServiceError("موجودی کافی نیست!", 1017);
        public static ServiceError OrderHasAlreadyBeenApproved => new ServiceError("این سفارش قبلا تایید شده!", 1017);
        public static ServiceError NotPossibleCancelOrder => new ServiceError("امکان لغو سفارش وجود ندارد!", 1017);
        public static ServiceError NotPossibleToCancelThisOrder => new ServiceError("امکان حذف این سفارش وجود ندارد!", 1016);
        public static ServiceError MediaNotFound => new ServiceError("مدیا مورد نظر با شناسه {0} یافت نشد.", 1015);
        public static ServiceError CourseNotFound => new ServiceError("دوره مورد نظر با شناسه {0} یافت نشد.", 1014);

        public static ServiceError FileNotSupported => new ServiceError("فایل مورد نظر پشتیبانی نمیشود.", 1013);
        public static ServiceError InvalidId => new ServiceError("شناسه معتبر نمی باشد.", 1012);
        public static ServiceError InvalidRefreshToken => new ServiceError("توکن معتبر نمی باشد.", 1011);
        public static ServiceError InvalidUserGuestRole => new ServiceError("کاربر میهمان نمیباشد.", 1010);
        public static ServiceError ExistUser => new ServiceError("این کاربر قبلا در سیستم ثبت شده است.", 1009);
        public static ServiceError GuestUserExistInGuests => new ServiceError("این شماره تلفن به عنوان میهمان ثبت کرده است.", 1008);
        public static ServiceError UserIsNotAdmin => new ServiceError("کاربر ادمین فقط مجاز ورود به کنترل پنل را دارند..", 1007);
        public static ServiceError GuestUserExistInUsers => new ServiceError("این شماره تلفن حساب کاربری ثبت کرده است.", 1006);
        public static ServiceError GuestUserInvalidDate => new ServiceError("کاربر میهمان بیشتر از 20 روز استفاده شده و باید حساب کاربری بسازد.", 1005);

        public static ServiceError DuplicatePhoneNumberOrEmail => new ServiceError("کاربر با این مشخصات وجود دارد.", 1004);
        public static ServiceError InvalidVerifyCode => new ServiceError("کد تاییدیه شماره تلفن معتبر نمی باشد.", 1003);
        public static ServiceError EmptyVerifyCode => new ServiceError("کد تاییدیه معتبر نمی باشد.", 1002);
        public static ServiceError CreateUserException => new ServiceError("خطا در ثبت حساب کاربری.", 1000);

        public static ServiceError UserIsLockedOut => new ServiceError("این حساب کاربری بسته است..", 1001);
        /// <summary>
        /// Use this for unauthorized responses.
        /// </summary>
        public static ServiceError ForbiddenError => new ServiceError("شما مجاز به تماس با این سرویس نیستید.", 998);

        /// <summary>
        /// Use this to send a custom error message
        /// </summary>
        public static ServiceError CustomMessage(string errorMessage)
        {
            return new ServiceError(errorMessage, 999);

        }
      
        public static ServiceError InccrrectUsernameOrPassword => new ServiceError("ایمیل یا کلمه عبور نادرست است.", 997);
        public static ServiceError UserNotFound => new ServiceError("کاربری یافت نشد.", 996);

        public static ServiceError UserFailedToCreate => new ServiceError("خطا در ثبت کاربر.", 995);

        public static ServiceError Canceled => new ServiceError("درخواست با موفقیت لغو گردید.!", 994);

        public static ServiceError NotFound => new ServiceError("The specified resource was not found.", 990);

        public static ServiceError ErrorInSaveOrUpdate => new ServiceError("خطا در ثبت اطلاعات.", 902);
        public static ServiceError ValidationFormat => new ServiceError("Request object format is not true.", 901);
        
        public static ServiceError Validation => new ServiceError("One or more validation errors occurred.", 900);

        public static ServiceError SearchAtLeastOneCharacter => new ServiceError("Search parameter must have at least one character!", 898);

        /// <summary>
        /// Default error for when we receive an exception
        /// </summary>
        public static ServiceError ServiceProviderNotFound => new ServiceError("Service Provider with this name does not exist.", 700);

        public static ServiceError ServiceProvider => new ServiceError("Service Provider failed to return as expected.", 600);

        public static ServiceError DateTimeFormatError => new ServiceError("Date format is not true. Date format must be like yyyy-MM-dd (2019-07-19)", 500);

        #region Override Equals Operator

        /// <summary>
        /// Use this to compare if two errors are equal
        /// Ref: https://msdn.microsoft.com/ru-ru/library/ms173147(v=vs.80).aspx
        /// </summary>
        public override bool Equals(object obj)
        {
            // If parameter cannot be cast to ServiceError or is null return false.
            var error = obj as ServiceError;

            // Return true if the error codes match. False if the object we're comparing to is nul
            // or if it has a different code.
            return Code == error?.Code;
        }

        public bool Equals(ServiceError error)
        {
            // Return true if the error codes match. False if the object we're comparing to is nul
            // or if it has a different code.
            return Code == error?.Code;
        }

        public override int GetHashCode()
        {
            return Code;
        }

        public static bool operator ==(ServiceError a, ServiceError b)
        {
            // If both are null, or both are same instance, return true.
            if (ReferenceEquals(a, b))
            {
                return true;
            }

            // If one is null, but not both, return false.
            if ((object)a == null || (object)b == null)
            {
                return false;
            }

            // Return true if the fields match:
            return a.Equals(b);
        }

        public static bool operator !=(ServiceError a, ServiceError b)
        {
            return !(a == b);
        }

        #endregion
    }

}
