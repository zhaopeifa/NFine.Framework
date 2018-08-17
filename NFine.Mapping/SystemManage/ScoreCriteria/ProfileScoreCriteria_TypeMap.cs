using NFine.Domain.Entity.SystemManage;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Mapping.SystemManage
{
    public class ProfileScoreCriteria_TypeMap: EntityTypeConfiguration<ProfileScoreCriteria_TypeEntity>
    {
        public ProfileScoreCriteria_TypeMap()
        {
            this.ToTable("ProfileScoreCriteria_Type");
            this.HasKey(t => t.STypeId);
        }
    }
}
