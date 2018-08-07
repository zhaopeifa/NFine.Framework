using NFine.Domain.Entity.SystemManage;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Mapping.SystemManage
{
    public class ProfileStreetMap : EntityTypeConfiguration<ProfileStreetEntity>
    {
        public ProfileStreetMap()
        {
            this.ToTable("ProfileStreet");
            this.HasKey(t => t.F_Id);
        }
    }
}
