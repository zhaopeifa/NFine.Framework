using NFine.Domain.Entity.SystemManage;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Mapping.SystemManage
{
    public class ProfileSanitationCompressionStationMap:EntityTypeConfiguration<ProfileSanitationCompressionStationEntity>
    {
        public ProfileSanitationCompressionStationMap()
        {
            this.ToTable("ProfileSanitationCompressionStation");
            this.HasKey(t => t.F_Id);
        }
    }
}
