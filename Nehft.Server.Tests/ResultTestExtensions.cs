using System;

namespace Nehft.Server.Tests
{
    public static class ResultTestExtensions
    {
        public static TValue Value<TValue, TError>(this Result<TValue, TError> result)
        {
            return result.OnBoth(x => x, _ => throw new Exception("Expected Result to be success"));
        }
    }
}