using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Code.Web
{
    /// <summary>
    /// 文件导入返回model
    /// </summary>
    public class ImportResultModel
    {
        public string Id { get; set; }

        public bool IsSucceed  { get; set; }



        /// <summary>
        /// 导入数据总条数
        /// </summary>
        public int TotalQuantity { get; set; }

        /// <summary>
        /// 成功导入数量
        /// </summary>
        public int SuccessfulQuantity { get; set; }

        /// <summary>
        /// 失败数量
        /// </summary>
        public int FailureQuantity { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 错误消息
        /// </summary>
        public string ErrorMessage { get; set; }

    }
}
