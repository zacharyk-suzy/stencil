﻿#if !WEB && !WINDOWS_PHONE_APP
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stencil.SDK
{

    public static class _SDKExtensions
    {

        public static string SimpleEncrypt(this string dataToEncrypt)
        {
            return VisualEncryption.Encrypt(dataToEncrypt);
        }
        public static string SimpleDecrypt(this string dataToDecrypt)
        {
            return VisualEncryption.Decrypt(dataToDecrypt);
        }

        /// <summary>
        /// Truncates list if over take, and builds stepping info based off of it.
        /// Assumes the caller added an extra item into items to denote extra.
        /// </summary>
        public static ListResult<T> ToSteppedListResult<T>(this IEnumerable<T> items, long skip, long take, long total = 0)
        {
            ListResult<T> result = new ListResult<T>();
            result.success = true;
            result.stepping = new SteppingInfo();
            result.stepping.total = total;

            result.items = items.ToList();
            if (result.items.Count > take)
            {
                result.stepping.more = true;
                long extra = result.items.Count - take;
                for (long i = 0; i < extra; i++)
                {
                    result.items.RemoveAt(result.items.Count - 1);
                }
            }
            result.stepping.current = skip;
            result.stepping.skip = skip + result.items.Count;
            return result;
        }
    }
}


#endif