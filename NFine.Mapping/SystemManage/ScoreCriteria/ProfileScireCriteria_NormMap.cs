using NFine.Domain.Entity.SystemManage;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Mapping.SystemManage
{
    public class ProfileScireCriteria_NormMap: EntityTypeConfiguration<ProfileScireCriteria_NormEntity>
    {
        public ProfileScireCriteria_NormMap()
        {
            this.ToTable("ProfileScireCriteria_Norm");
            this.HasKey(t => t.SNormId);
        }
    }
}
