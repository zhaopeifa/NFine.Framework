using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Domain.Enums
{
    /// <summary>
    /// 环评 环卫下面数据节点类型
    /// </summary>
    public enum ProfileSanitationEnum
    {
        道路 = 1,
        沿街垃圾收集设施 = 2,
        垃圾箱房 = 3,
        公厕 = 4,
        倒粪池小便池 = 5,
        压缩站 = 6,
        沿街绿化 = 7,
        绿色账户居住区 = 8,
        环卫作业车辆 = 9,
        四通八轮车 = 10,
        飞行保洁车 = 11,
        机扫车 = 12,
        冲洗车 = 13,
        垃圾清运车 = 14,
        废物箱=15,
        沿街垃圾桶=16
    }

    public class SanitationProjctEntry
    {
        private SanitationProjctEntry() { }
        private SanitationProjctEntry(int code, string text, bool haveData)
        {
            this.Id = Guid.NewGuid().ToString();
            this.Code = code;
            this.Text = text;
            this.HaveData = HaveData;
        }
        private SanitationProjctEntry(int code, string text, bool haveData, SanitationProjctEntry parentNode)
        {
            this.Id = Guid.NewGuid().ToString();
            this.Code = code;
            this.Text = text;
            this.HaveData = haveData;
            this.ParentNode = parentNode;

            parentNode.IsHaveChild = true;
        }
        public string Id { get; set; }
        public int Code { get; set; }
        public string Text { get; set; }

        public bool HaveData { get; set; }

        public bool IsHaveChild { get; set; }

        public SanitationProjctEntry ParentNode { get; set; }

        private static List<SanitationProjctEntry> _sanitationProjctEntryCollecion;
        public static List<SanitationProjctEntry> SanitationProjctEntryCollecion
        {
            get
            {
                if (_sanitationProjctEntryCollecion == null)
                {
                    _sanitationProjctEntryCollecion = new List<SanitationProjctEntry>();

                    SanitationProjctEntry wayEntry = new SanitationProjctEntry((int)ProfileSanitationEnum.道路, ProfileSanitationEnum.道路.ToString(), true);
                    SanitationProjctEntry garbageCollectionEntry = new SanitationProjctEntry((int)ProfileSanitationEnum.沿街垃圾收集设施, ProfileSanitationEnum.沿街垃圾收集设施.ToString(), false);
                    SanitationProjctEntry garbageBoxEntry = new SanitationProjctEntry((int)ProfileSanitationEnum.垃圾箱房, ProfileSanitationEnum.垃圾箱房.ToString(), true);
                    SanitationProjctEntry tandasEntry = new SanitationProjctEntry((int)ProfileSanitationEnum.公厕, ProfileSanitationEnum.公厕.ToString(), true);
                    SanitationProjctEntry cesspoolEntry = new SanitationProjctEntry((int)ProfileSanitationEnum.倒粪池小便池, ProfileSanitationEnum.倒粪池小便池.ToString(), true, garbageCollectionEntry);
                    SanitationProjctEntry LitterBin = new SanitationProjctEntry((int)ProfileSanitationEnum.废物箱, ProfileSanitationEnum.废物箱.ToString(), true, garbageCollectionEntry);
                    SanitationProjctEntry StreetTrash = new SanitationProjctEntry((int)ProfileSanitationEnum.沿街垃圾桶, ProfileSanitationEnum.沿街垃圾桶.ToString(), true, garbageCollectionEntry);
                    SanitationProjctEntry compressionStationEntry = new SanitationProjctEntry((int)ProfileSanitationEnum.压缩站, ProfileSanitationEnum.压缩站.ToString(), true);
                    SanitationProjctEntry greeningStationEntry = new SanitationProjctEntry((int)ProfileSanitationEnum.沿街绿化, ProfileSanitationEnum.沿街绿化.ToString(), true);
                    SanitationProjctEntry greenResidentialEntry = new SanitationProjctEntry((int)ProfileSanitationEnum.绿色账户居住区, ProfileSanitationEnum.绿色账户居住区.ToString(), true);
                    SanitationProjctEntry operatingVehiclesEntry = new SanitationProjctEntry((int)ProfileSanitationEnum.环卫作业车辆, ProfileSanitationEnum.环卫作业车辆.ToString(), false);
                    SanitationProjctEntry fourThroughEightWheelsEntry = new SanitationProjctEntry((int)ProfileSanitationEnum.四通八轮车, ProfileSanitationEnum.四通八轮车.ToString(), true, operatingVehiclesEntry);
                    SanitationProjctEntry flightCleanerEntry = new SanitationProjctEntry((int)ProfileSanitationEnum.飞行保洁车, ProfileSanitationEnum.飞行保洁车.ToString(), true, operatingVehiclesEntry);
                    SanitationProjctEntry sweepCarEntry = new SanitationProjctEntry((int)ProfileSanitationEnum.机扫车, ProfileSanitationEnum.机扫车.ToString(), true, operatingVehiclesEntry);
                    SanitationProjctEntry washTheCarEntry = new SanitationProjctEntry((int)ProfileSanitationEnum.冲洗车, ProfileSanitationEnum.冲洗车.ToString(), true, operatingVehiclesEntry);
                    SanitationProjctEntry garbageTruckEntry = new SanitationProjctEntry((int)ProfileSanitationEnum.垃圾清运车, ProfileSanitationEnum.垃圾清运车.ToString(), true, operatingVehiclesEntry);

                    _sanitationProjctEntryCollecion.Add(wayEntry);
                    _sanitationProjctEntryCollecion.Add(garbageCollectionEntry);
                    _sanitationProjctEntryCollecion.Add(garbageBoxEntry);
                    _sanitationProjctEntryCollecion.Add(tandasEntry);
                    _sanitationProjctEntryCollecion.Add(cesspoolEntry);
                    _sanitationProjctEntryCollecion.Add(LitterBin);
                    _sanitationProjctEntryCollecion.Add(StreetTrash);
                    _sanitationProjctEntryCollecion.Add(compressionStationEntry);
                    _sanitationProjctEntryCollecion.Add(greeningStationEntry);
                    _sanitationProjctEntryCollecion.Add(greenResidentialEntry);
                    _sanitationProjctEntryCollecion.Add(operatingVehiclesEntry);
                    _sanitationProjctEntryCollecion.Add(fourThroughEightWheelsEntry);
                    _sanitationProjctEntryCollecion.Add(flightCleanerEntry);
                    _sanitationProjctEntryCollecion.Add(sweepCarEntry);
                    _sanitationProjctEntryCollecion.Add(washTheCarEntry);
                    _sanitationProjctEntryCollecion.Add(garbageTruckEntry);
                }

                return _sanitationProjctEntryCollecion;
            }
        }
    }
}
