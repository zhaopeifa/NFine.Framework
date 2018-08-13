using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nfine.WebApi.Contracts
{
    /// <summary>
    /// web api 返回参数
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ApiBackParameter<T>
    {
        public ApiBackParameter() { }
        public ApiBackParameter(T data)
        {
            this.Data = data;
        }
        public ApiBackParameter(int statusCode, T data)
        {
            this.StatusCode = statusCode; ;
            this.Data = data;
        }

        public int StatusCode { get; set; }

        public string Message { get; set; }
        public T Data { get; set; }

        public static ApiBackParameter<T> Get(int statusCode, T data)
        {
            return new ApiBackParameter<T>(statusCode, data);
        }
        public static ApiBackParameter<T> Get(T data)
        {
            return new ApiBackParameter<T>(data);
        }
        public static ApiBackParameter<T> Get(Action<ApiBackParameter<T>> fun)
        {
            var obj = new ApiBackParameter<T>();
            fun(obj);
            return obj;
        }
    }
}
