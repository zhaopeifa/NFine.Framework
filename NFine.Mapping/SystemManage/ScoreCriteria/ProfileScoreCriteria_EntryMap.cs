using NFine.Domain.Entity.SystemManage;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Mapping.SystemManage
{
    public class ProfileScoreCriteria_EntryMap : EntityTypeConfiguration<ProfileScoreCriteria_EntryEntity>
    {
        public ProfileScoreCriteria_EntryMap()
        {
            this.ToTable("ProfileScoreCriteria_Entry");
            this.HasKey(t => t.SEntryId);
        }
    }
}
