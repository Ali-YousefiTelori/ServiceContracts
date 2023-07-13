﻿using System;
using System.Collections.Generic;

namespace EasyMicroservices.ServiceContracts
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ListMessageContract<T> : MessageContract<List<T>>
    {
        /// <summary>
        /// when IsSuccess = true and Result has any items
        /// </summary>
        public bool HasItems
        {
            get
            {
                return IsSuccess && Result?.Count > 0;
            }
        }

        /// <summary>
        /// Convert T to MessageContractList<typeparamref name="T"/>
        /// </summary>
        /// <param name="result"></param>
        public static implicit operator ListMessageContract<T>(List<T> result)
        {
            if (result == null)
            {
                return new ListMessageContract<T>()
                {
                    IsSuccess = false,
                    Error = new ErrorContract()
                    {
                        FailedReasonType = FailedReasonType.NotFound,
                        StackTrace = Environment.StackTrace,
                        Message = "یافت نشد."
                    }
                };
            }
            return new ListMessageContract<T>()
            {
                IsSuccess = true,
                Result = result
            };
        }

        /// <summary>
        /// Convert MessageContractList type
        /// </summary>
        /// <typeparam name="TContract"></typeparam>
        /// <returns></returns>
        public ListMessageContract<TContract> ToAnotherListContract<TContract>()
        {
            return new ListMessageContract<TContract>()
            {
                IsSuccess = IsSuccess,
                Error = Error
            };
        }

        /// <summary>
        /// Convert failed reason and message to MessageContractList
        /// </summary>
        /// <param name="details"></param>
        public static implicit operator ListMessageContract<T>((FailedReasonType FailedReasonType, string Message) details)
        {
            return new ListMessageContract<T>()
            {
                IsSuccess = false,
                Error = new ErrorContract()
                {
                    FailedReasonType = details.FailedReasonType,
                    StackTrace = Environment.StackTrace,
                    Message = details.Message
                }
            };
        }

        /// <summary>
        /// Convert FailedReasonType to MessageContractList<typeparamref name="T"/>
        /// </summary>
        /// <param name="failedReasonType"></param>
        public static implicit operator ListMessageContract<T>(FailedReasonType failedReasonType)
        {
            return new ListMessageContract<T>()
            {
                IsSuccess = false,
                Error = new ErrorContract()
                {
                    FailedReasonType = failedReasonType,
                    StackTrace = Environment.StackTrace,
                    Message = failedReasonType.ToString()
                }
            };
        }

        /// <summary>
        /// Convert Exception To MessageContractList<typeparamref name="T"/>
        /// </summary>
        /// <param name="exception"></param>
        public static implicit operator ListMessageContract<T>(Exception exception)
        {
            return new ListMessageContract<T>()
            {
                IsSuccess = false,
                Error = new ErrorContract()
                {
                    FailedReasonType = FailedReasonType.InternalError,
                    StackTrace = Environment.StackTrace,
                    Message = exception.Message,
                    Details = exception.ToString()
                }
            };
        }
    }
}
