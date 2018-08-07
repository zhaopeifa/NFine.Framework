using NFine.Domain.Entity.SystemManage;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Mapping.SystemManage
{
    public class ProfileGrading_TypeMap: EntityTypeConfiguration<ProfileGrading_TypeEntity>
    {
        public ProfileGrading_TypeMap()
        {
            this.ToTable("ProfileGrading_Type");
            this.HasKey(t => t.F_Id);
        }
    }
}
