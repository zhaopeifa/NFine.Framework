using NFine.Domain.Entity.SystemManage;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Mapping.SystemManage
{
    public class ProfileAmenitiesWatersMap: EntityTypeConfiguration<ProfileAmenitiesWatersEntity>
    {
        public ProfileAmenitiesWatersMap()
        {
            this.ToTable("ProfileAmenitiesWaters");
            this.HasKey(t => t.F_Id);
        }
    }
}
