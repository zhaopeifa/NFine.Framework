using NFine.Domain.Entity.SystemManage;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Mapping.SystemManage
{
    /// <summary>
    /// 环评-环卫-公厕
    /// </summary>
    public class ProfileSanitationTandasMap: EntityTypeConfiguration<ProfileSanitationTandasEntity>
    {
        public ProfileSanitationTandasMap()
        {
            this.ToTable("ProfileSanitationTandas");
            this.HasKey(t => t.F_Id);
        }
    }
}
