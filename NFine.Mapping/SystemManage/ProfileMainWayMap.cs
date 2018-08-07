﻿using NFine.Domain.Entity.SystemManage;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Mapping.SystemManage
{
    public class ProfileMainWayMap : EntityTypeConfiguration<ProfileMainWayEntity>
    {
        public ProfileMainWayMap()
        {
            this.ToTable("ProfileMainWay");
            this.HasKey(t => t.F_Id);
        }
    }
}
