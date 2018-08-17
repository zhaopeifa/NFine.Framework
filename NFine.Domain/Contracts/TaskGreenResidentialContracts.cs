using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Domain.Contracts
{
    public class TaskDetailGreenResidentialContracts : ITaskDetail
    {
        public string F_Id { get; set; }

        public string DataId { get; set; }
        public string F_EnCode { get; set; }

        public string ResidentialName { get; set; }

        public string Address { get; set; }

        public bool CompleteState { get; set; }

        public string PersonInChargeId { get; set; }

        public string PersonInChargeName { get; set; }

        public DateTime CompletionTime { get; set; }
    }
}
