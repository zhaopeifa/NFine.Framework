using NFine.Domain.Entity.SystemManage;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Mapping.SystemManage
{
    public class ProfileGrading_NormMap: EntityTypeConfiguration<ProfileGrading_NormEntity>
    {
        public ProfileGrading_NormMap()
        {
            this.ToTable("ProfileGrading_Norm");
            this.HasKey(t => t.F_Id);
        }
    }
}
