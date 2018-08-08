using NFine.Domain.Entity.SystemManage;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Mapping.SystemManage
{
    public class ProfileTaskMap: EntityTypeConfiguration<ProfileTaskEntity>
    {
        public ProfileTaskMap()
        {
            this.ToTable("ProfileTask");
            this.HasKey(t => t.F_Id);
        }
    }
}
