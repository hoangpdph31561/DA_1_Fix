using BaseSolution.Application.ValueObjects.Common;
using BaseSolution.Application.ValueObjects.Exceptions;
using MediatR;
using System.Diagnostics.CodeAnalysis;

namespace BaseSolution.Application.ValueObjects.Response
{
    /// <summary>
    /// To generate result for all actions in this system.
    /// Result contains:
    ///     1. Succeeded flag: true/false
    ///     2. Errors list: List of errors if any in case of failure
    ///     3. Data object: Data object if action need to pass data back to caller
    ///     4. Message string: Message content if needed
    /// </summary>
    /// <typeparam name="TDataType"></typeparam>
    [ExcludeFromCodeCoverage]
    public class RequestResult<TDataType> : IRequest<Unit>
    {
        public RequestResult(bool success, TDataType? data, string? message, IEnumerable<ErrorItem>? errors)
        {
            Success = success;
            Errors = errors?.ToArray();
            Data = data;
            Message = message;
        }

        public TDataType? Data { get; private set; }
        public bool Success { get; private set; }
        public IEnumerable<ErrorItem>? Errors { get; private set; }
        public string? Message { get; set; }

        /// <summary>
        /// Handle success
        /// </summary>
        /// <param name="data"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static RequestResult<TDataType> Succeed(TDataType? data = default, string? message = null)
        {
            return new RequestResult<TDataType>(true, data, message, Array.Empty<ErrorItem>());
        }

        /// <summary>
        /// Handle fail while processing request
        /// </summary>
        /// <param name="message"></param>
        /// <param name="errors"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static RequestResult<TDataType> Fail(string? message, IEnumerable<ErrorItem>? errors = null, TDataType? data = default)
        {
            return new RequestResult<TDataType>(false, data, message, errors);
        }

        /// <summary>
        /// Handle forbiden error
        /// </summary>
        /// <returns></returns>
        public static RequestResult<TDataType> Forbid(string message, Tracker tracker)
        {
            throw new UnauthorizedException(nameof(RequestResult<TDataType>), nameof(UnauthorizedException), message, tracker);
            //return new RequestResult<TDataType>(false, errors, data, true);
        }
    }
}