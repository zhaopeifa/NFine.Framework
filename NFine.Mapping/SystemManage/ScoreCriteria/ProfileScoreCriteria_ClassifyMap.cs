using NFine.Domain.Entity.SystemManage;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Mapping.SystemManage
{
    public class ProfileScoreCriteria_ClassifyMap: EntityTypeConfiguration<ProfileScoreCriteria_ClassifyEntity>
    {
        public ProfileScoreCriteria_ClassifyMap()
        {
            this.ToTable("ProfileScoreCriteria_Classify");
            this.HasKey(t => t.SClassifyId);
        }
    }
}
