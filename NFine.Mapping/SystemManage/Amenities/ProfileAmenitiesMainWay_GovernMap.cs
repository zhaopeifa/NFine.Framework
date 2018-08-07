using NFine.Domain.Entity.SystemManage;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Mapping.SystemManage
{
    public class ProfileAmenitiesMainWay_GovernMap: EntityTypeConfiguration<ProfileAmenitiesMainWay_GovernEntity>
    {
        public ProfileAmenitiesMainWay_GovernMap()
        {
            this.ToTable("ProfileAmenitiesMainWay_Govern");
            this.HasKey(t => t.F_Id);
        }
    }
}
