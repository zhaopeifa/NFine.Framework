using NFine.Domain.Entity.SystemManage;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Mapping.SystemManage
{
    public class ProfileGrading_Type_RlationMap: EntityTypeConfiguration<ProfileGrading_Type_RlationEntity>
    {
        public ProfileGrading_Type_RlationMap()
        {
            this.ToTable("ProfileGrading_Type_Rlation");
            this.HasKey(t => t.F_Id);
        }
    }
}
