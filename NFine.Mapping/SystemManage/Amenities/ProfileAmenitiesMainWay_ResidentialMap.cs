using NFine.Domain.Entity.SystemManage;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Mapping.SystemManage
{
    public class ProfileAmenitiesMainWay_ResidentialMap: EntityTypeConfiguration<ProfileAmenitiesMainWay_ResidentialEntity>
    {
        public ProfileAmenitiesMainWay_ResidentialMap()
        {
            this.ToTable("ProfileAmenitiesMainWay_Residential");
            this.HasKey(t => t.F_Id);
        }
    }
}
