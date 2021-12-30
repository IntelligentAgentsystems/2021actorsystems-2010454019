using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrisonersDilemma.Helper
{
    public class Try<T>
    {
        public T Value { get; private set; }
        public Exception Error { get; private set; }

        public Try(T value)
        {
            Value = value;
        }

        public Try(Exception error)
        {
            Error = error;
        }

        public static Try<T> Of(Func<T> supplier)
        {
            try
            {
                return new Try<T>(supplier.Invoke());
            }
            catch (Exception ex)
            {
                return new Try<T>(ex);
            }
        }

        public static async Task<Try<T>> Of(Func<Task<T>> supplier)
        {
            try
            {
                return new Try<T>(await supplier.Invoke());
            }
            catch (Exception ex)
            {
                return new Try<T>(ex);
            }
        }

        public static Try<T> Of(Exception failure)
        {
            return new Try<T>(failure);
        }

        public static Try<T> Of(T value)
        {
            return new Try<T>(value);
        }

        public bool Success => Error == null;
        public bool Failure => Error != null;

        public Try<T> Filter(Predicate<T> filter)
        {
            if (Failure)
                return Of(Error);
            else if (!filter.Invoke(Value))
                return Of(new Exception("Filter does not match!"));
            else return Of(Value);
        }

        public Try<U> Map<U>(Func<T, U> mapper) => FlatMap(Value => Try<U>.Of(mapper.Invoke(Value)));

        public Try<U> FlatMap<U>(Func<T, Try<U>> mapper)
        {
            if (Success)
                return mapper.Invoke(Value);
            else
                return Try<U>.Of(Error);
        }

        public override bool Equals(object? obj)
        {
            if (Failure || !(obj is Try<T>))
                return false;
            else
            {
                Try<T> item = (Try<T>)(obj);
                if (item == null || item.Failure)
                    return false;
                else return Value.Equals(item.Value);
            }
        }

        public override int GetHashCode() => Success ? Value.GetHashCode() : Error.GetHashCode();

        public override string ToString()
        {
            if (Success)
                return $"Try<{typeof(T).Name}> => {Value}";
            else
                return $"Try<{typeof(T).Name}> => {Error}";
        }

        public T OrElseThrow(Func<Exception> exceptionSupplier) => Success ? Value : throw exceptionSupplier.Invoke();
        public T OrElseThrow(Exception exception) => Success ? Value : throw exception;
        public T OrElseThrow() => OrElseThrow(Error);

        public T OrElse(T otherValue) => Success ? Value : otherValue;
        public T OrElse(Func<T> otherValueSupplier) => Success ? Value : otherValueSupplier.Invoke();

        public void ThrowIfFailure()
        {
            if(Failure)
                throw Error;
        }

       
    }
}
