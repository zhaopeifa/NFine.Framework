using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Code.Excel
{
    public class InstructionsExcel
    {
        private XSSFWorkbook hssfworkbook;
        public int StartRowNumber = 1;
        public int StartColNumber = 1;

        public void InitializeWorkbook(string path)
        {
            using (FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                hssfworkbook = new XSSFWorkbook(file);
            }
        }

        public DataSet ConvertToDataTable()
        {
            DataSet ds = new DataSet();
            string sheetName = String.Empty;
            for (int i = 0; i < hssfworkbook.NumberOfSheets; i++)
            {
                sheetName = hssfworkbook.GetSheetName(i);
                try
                {
                    DataTable dt = getDataTable(sheetName, true);
                    ds.Tables.Add(dt);
                }
                catch (Exception ex)
                {
                    //对于异常的Excel工作表，不导入
                }
            }
            return ds;
        }

        public DataTable getDataTable(string sheetName, bool IsHeader)
        {
            int z = 0;
            string temp = String.Empty;
            string temp12 = String.Empty;
            string temp13 = String.Empty;
            ISheet sheet = hssfworkbook.GetSheet(sheetName);

            System.Collections.IEnumerator rows = sheet.GetRowEnumerator();

            IRow headerRow = sheet.GetRow(this.StartColNumber - 1);//第一行为标题行 
            int cellCount = headerRow.LastCellNum;//LastCellNum = PhysicalNumberOfCells 
            int rowCount = sheet.LastRowNum;//LastRowNum = PhysicalNumberOfRows - 1 

            DataTable dt = new DataTable(sheetName);

            if (IsHeader == false)
            {
                for (int j = 0; j < cellCount; j++)
                {
                    dt.Columns.Add(Convert.ToChar(((int)'A') + j).ToString());
                }
            }
            else
            {
                int firstHeaderRowIndex = this.StartColNumber;
                int lastHeaderRowIndex = this.StartColNumber;

                var dict = new Dictionary<int, string>();

                for (int i = firstHeaderRowIndex - 1; i < lastHeaderRowIndex; i++)
                {
                    headerRow = sheet.GetRow(i);
                    for (int j = headerRow.FirstCellNum; j < cellCount; j++)
                    {
                        if (!string.IsNullOrEmpty(headerRow.GetCell(j).StringCellValue.Trim()))
                        {
                            //根据键值是否已存在做不同处理
                            try
                            {
                                string oldValue = dict[j];
                                dict.Remove(j);
                                dict.Add(j, oldValue + headerRow.GetCell(j).StringCellValue.Trim());
                            }
                            catch (Exception)
                            {
                                dict.Add(j, headerRow.GetCell(j).StringCellValue.Trim());
                            }
                        }
                    }

                    int a = 0;
                }

                for (int i = 0; i < dict.Count; i++)
                {
                    dt.Columns.Add(dict[i]);
                }


                //for (int i = 0; i < headerRow.LastCellNum; i++)
                //{
                //    ICell cell = headerRow.GetCell(i);
                //    if (cell.IsMergedCell == true)
                //    {
                //        dt.Columns.Add(cell.ToString().Replace("\r", "").Replace("\n", ""));
                //    }
                //}
            }

            while (rows.MoveNext())
            {
                z++;
                if (z >= StartRowNumber + 1)
                {
                    IRow row = (HSSFRow)rows.Current;
                    DataRow dr = dt.NewRow();

                    for (int i = 0; i < row.LastCellNum; i++)
                    {
                        ICell cell = row.GetCell(i);
                        if (cell == null)
                        {
                            dr[i] = null;
                        }
                        else
                        {
                            if (cell.IsMergedCell == true)
                            {
                                if (cell.ToString() != "")
                                {
                                    if (i == 0)
                                    {
                                        if (temp != cell.ToString())
                                        {
                                            temp12 = "";
                                        }
                                        temp = cell.ToString();
                                    }
                                    else if (i == 1)
                                    {
                                        temp12 = cell.ToString();
                                    }
                                    dr[i] = cell.ToString();
                                }
                                else
                                {
                                    try
                                    {
                                        if (i == 0)
                                        {
                                            //第一列同上一行
                                            dr[i] = temp;
                                        }
                                        else if (i == 1)
                                        {
                                            //第一列同上一行
                                            dr[i] = temp12;
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        //kevin
                                    }
                                }
                            }
                            else
                            {
                                try
                                {
                                    dr[i] = cell.ToString();
                                }
                                catch (Exception ex)
                                {
                                    //kevin
                                }
                            }
                        }
                    }
                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }

        public static DataTable GetDataTable(string path, string excelName)
        {
            DataTable dtGBPatient = new DataTable();

            string strConn;

            //注意：把一个excel文件看做一个数据库，一个sheet看做一张表。语法 "SELECT * FROM [sheet1$]"，表单要使用"[]"和"$"

            // 1、HDR表示要把第一行作为数据还是作为列名，作为数据用HDR=no，作为列名用HDR=yes；
            // 2、通过IMEX=1来把混合型作为文本型读取，避免null值。
            strConn = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source='{0}';Extended Properties='Excel 8.0;HDR=YES;IMEX=1';";
            string strConnection = string.Format(strConn, path);
            OleDbConnection conn = new OleDbConnection(strConnection);
            conn.Open();
            OleDbDataAdapter oada = new OleDbDataAdapter("select * from [" + excelName + "$]", strConnection);

            dtGBPatient.TableName = "gbPatientInfo";
            oada.Fill(dtGBPatient);//获得datatable
            conn.Close();

            return dtGBPatient;
        }
    }
}
