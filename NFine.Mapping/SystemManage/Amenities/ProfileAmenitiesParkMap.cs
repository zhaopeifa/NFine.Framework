using NFine.Domain.Entity.SystemManage;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Mapping.SystemManage
{
    public class ProfileAmenitiesParkMap: EntityTypeConfiguration<ProfileAmenitiesParkEntity>
    {
        public ProfileAmenitiesParkMap()
        {
            this.ToTable("ProfileAmenitiesPark");
            this.HasKey(t => t.F_Id);
        }
    }
}
