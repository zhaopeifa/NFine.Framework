using NFine.Domain.Entity.SystemManage;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Mapping.SystemManage
{
    public class ProfileGrading_OptionsMap : EntityTypeConfiguration<ProfileGrading_OptionsEntity>
    {
        public ProfileGrading_OptionsMap()
        {
            this.ToTable("ProfileGrading_Options");
            this.HasKey(t => t.F_Id);
        }
    }
}
