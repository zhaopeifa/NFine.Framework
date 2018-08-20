using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Domain.Entity.SystemManage
{
    /// <summary>
    /// 扣分图片表
    /// </summary>
    public class ProfileDeducImgEntiy
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        public string DeducImg_Id { get; set; }

        /// <summary>
        /// 图片路径
        /// </summary>
        public string DeducImgPath { get; set; }

        /// <summary>
        /// 关联扣分记录表
        /// </summary>
        public string DeducIns_Id { get; set; }
    }
}
