using NFine.Domain.Entity.SystemManage;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Mapping.SystemManage
{
    public class ProfileDeducImgMap : EntityTypeConfiguration<ProfileDeducImgEntiy>
    {
        public ProfileDeducImgMap()
        {
            this.ToTable("ProfileDeducImg");
            this.HasKey(t => t.DeducImg_Id);
        }
    }
}
