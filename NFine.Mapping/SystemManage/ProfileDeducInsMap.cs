using NFine.Domain.Entity.SystemManage;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Mapping.SystemManage
{
    public class ProfileDeducInsMap: EntityTypeConfiguration<ProfileDeducInsEntity>
    {
        public ProfileDeducInsMap()
        {
            this.ToTable("ProfileDeducIns");
            this.HasKey(t => t.DeducIns_Id);
        }
    }
}
