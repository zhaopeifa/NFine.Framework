using NFine.Domain.Entity.SystemManage;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Mapping.SystemManage
{
    public class ProfileTaskEntrySanitationMap: EntityTypeConfiguration<ProfileTaskEntrySanitationEntity>
    {
        public ProfileTaskEntrySanitationMap()
        {
            this.ToTable("ProfileTaskEntrySanitation");
            this.HasKey(t => t.F_Id);
        }
    }
}
