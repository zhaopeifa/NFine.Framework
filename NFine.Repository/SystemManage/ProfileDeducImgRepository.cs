using NFine.Data;
using NFine.Domain.Entity.SystemManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Repository.SystemManage
{
    /// <summary>
    /// 扣分记录图片表
    /// </summary>
    public class ProfileDeducImgRepository : RepositoryBase<ProfileDeducImgEntiy>
    {

        public void SubmitForm(string filePath, string deducInsId)
        {
            
            using (var db = new RepositoryBase().BeginTrans())
            {

                //暂时只做添加s
                ProfileDeducImgEntiy dedicImgEntity = new ProfileDeducImgEntiy()
                {
                    DeducImg_Id = Guid.NewGuid().ToString(),
                    DeducImgPath = filePath,
                    DeducIns_Id = deducInsId
                };

                db.Insert<ProfileDeducImgEntiy>(dedicImgEntity);


                db.Commit();
            }
        }
    }
}
