using NFine.Domain.Entity.SystemManage;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Mapping.SystemManage
{
    public class ProfileAmenitiesMainWay_ConstructionMap: EntityTypeConfiguration<ProfileAmenitiesMainWay_ConstructionEntity>
    {
        public ProfileAmenitiesMainWay_ConstructionMap()
        {
            this.ToTable("ProfileAmenitiesMainWay_Construction");
            this.HasKey(t => t.F_Id);
        }
    }
}
