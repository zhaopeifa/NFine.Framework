using NFine.Domain.Entity.SystemManage;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Mapping.SystemManage
{
    public class ProfileAmenitiesScenicSpotsMap: EntityTypeConfiguration<ProfileAmenitiesScenicSpotsEntity>
    {
        public ProfileAmenitiesScenicSpotsMap()
        {
            this.ToTable("ProfileAmenitiesScenicSpots");
            this.HasKey(t => t.F_Id);
        }
    }
}
