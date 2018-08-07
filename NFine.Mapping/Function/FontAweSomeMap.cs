using NFine.Domain.Entity.Function;
/*******************************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: NFine
 * Description: NFine快速开发平台
 * Website：http://www.nfine.cn
*********************************************************************************/
using NFine.Domain.Entity.SystemManage;
using System.Data.Entity.ModelConfiguration;

namespace NFine.Mapping.Function
{
    public class FontAweSomeMap : EntityTypeConfiguration<FontAweSomeEntity>
    {
        public FontAweSomeMap()
        {
            this.ToTable("FontAweSome");
            this.HasKey(t => t.F_Id);
        }
    }
}
