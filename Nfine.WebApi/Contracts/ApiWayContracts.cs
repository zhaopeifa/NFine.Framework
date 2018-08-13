using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nfine.WebApi.Contracts
{
    /// <summary>
    /// 道路
    /// </summary>
    public class ApiWayContracts
    {
        public string Title { get; set; }
        public string Id { get; set; }

        public string StreetId { get; set; }
        public string StreetName { get; set; }
        public string Name { get; set; }

        /// <summary>
        /// 派发时间
        /// </summary>
        public DateTime DeliveryTime { get; set; }

        /// <summary>
        /// 完成时间
        /// </summary>
        public DateTime CompletionTime { get; set; }

        /// <summary>
        /// 是否完成
        /// </summary>
        public int IsComplete { get; set; }
    }
}