using NFine.Domain.Entity.SystemManage;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Mapping.SystemManage
{
    public class ProfileAmenitiesMainWay_WatersMap: EntityTypeConfiguration<ProfileAmenitiesMainWay_WatersEntity>
    {
        public ProfileAmenitiesMainWay_WatersMap()
        {
            this.ToTable("ProfileAmenitiesMainWay_Waters");
            this.HasKey(t => t.F_Id);
        }
    }
}
