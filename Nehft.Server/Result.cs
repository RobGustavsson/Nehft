using System;

namespace Nehft.Server
{
    public abstract class Result<TValue, TError>
    {
        public static Result<TValue, TError> Success(TValue value) => new SuccessImpl(value);
        public static Result<TValue, TError> Error(TError error) => new ErrorImpl(error);
        public abstract Result<TNewValue, TError> OnSuccess<TNewValue>(Func<TValue, Result<TNewValue, TError>> onSuccess);
        public abstract Result<TNewValue, TError> OnSuccess<TNewValue>(Func<TValue, TNewValue> onSuccess);

        public abstract TNewValue OnBoth<TNewValue>(
            Func<TValue, TNewValue> onSuccess,
            Func<TError, TNewValue> onError);

        private class SuccessImpl : Result<TValue, TError>
        {
            private readonly TValue _value;

            public SuccessImpl(TValue value)
            {
                _value = value;
            }
            public override Result<TNewValue, TError> OnSuccess<TNewValue>(Func<TValue, Result<TNewValue, TError>> onSuccess)
            {
                return onSuccess(_value);
            }

            public override Result<TNewValue, TError> OnSuccess<TNewValue>(Func<TValue, TNewValue> onSuccess)
            {
                return Result<TNewValue, TError>.Success(onSuccess(_value));
            }

            public override TNewValue OnBoth<TNewValue>(Func<TValue, TNewValue> onSuccess, Func<TError, TNewValue> onError)
            {
                return onSuccess(_value);
            }
        }

        private class ErrorImpl : Result<TValue, TError>
        {
            private readonly TError _error;

            public ErrorImpl(TError error)
            {
                _error = error;
            }
            public override Result<TNewValue, TError> OnSuccess<TNewValue>(Func<TValue, Result<TNewValue, TError>> onSuccess)
            {
                return Result<TNewValue,TError>.Error(_error);
            }

            public override Result<TNewValue, TError> OnSuccess<TNewValue>(Func<TValue, TNewValue> onSuccess)
            {
                return Result<TNewValue, TError>.Error(_error);
            }

            public override TNewValue OnBoth<TNewValue>(Func<TValue, TNewValue> onSuccess, Func<TError, TNewValue> onError)
            {
                return onError(_error);
            }
        }
    }
}