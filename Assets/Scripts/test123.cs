using System.Data;
using System.Data.SqlClient;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace ExcelData.DirectReading
{
    public class test123 : MonoBehaviour
    {
        public GameObject XiangXiXinXi;
        //数据库连接语句 .为项目所在服务器IP SqlTest为数据库名称 web为采用用户名登录方式的用户名 web为密码
        private static readonly string sqlAddress = SqlConstant.sqlAddress;

        //进行连接
        private SqlConnection sqlCon = new SqlConnection(sqlAddress);
        public string lname;

        private void Start()
        {
            ////开启数据库
            //sqlCon.Open();
        }

        private void Update()
        {
            //ZhanShiModel(lname, "管线");
        }

        public void ZhanShiModel(string modelName)
        {



            XiangXiXinXi.SetActive(true);
            //开启数据库
            sqlCon.Open();

            #region 数据库类型扩展内容
            //提高程序的可扩展性，防止不同模型的字段不同，根据模型类型搭建数据库
            //可尽量分类，某些模型不具备的字段可以设空，如果为“”那么就字段名字也不展示，如果null那么字段名字展示
            //可以进行提前判断，然后用总字段数减去ID减去ImgPath减去""字段数

            ////数据库类型
            //string ShuJuKu = "";

            ////判断数据库类型
            //if (type == "消防设备")
            //{
            //    ShuJuKu = "SheBeiTest";
            //}
            //else if (type == "管线")
            //{
            //    ShuJuKu = "SheBeiTest1";
            //}
            //else if (type == "汽车")
            //{
            //    ShuJuKu = "SheBeiTest2";
            //}
            //else if (type == "阀门")
            //{
            //    ShuJuKu = "SheBeiTest3";
            //}

            //数据库执行语句
            string sql = "select * from Equipment where DeviceId='" + modelName + "'";
            #endregion


            //检索保存数据
            SqlDataAdapter sqlAdapter = new SqlDataAdapter(sql, sqlCon);

            //数据存储表
            DataTable sqlDataTable = new DataTable();

            //执行查询
            sqlAdapter.Fill(sqlDataTable);

            #region 提前清空模型显示框
            for (int i = 1; i < 5; i++)
            {
                string sqlAttr = "select HanYi from SheBeiTestAttribute where ZiDuanMingCheng='" + sqlDataTable.Columns[i] + "'";
                SqlDataAdapter sqlAdapterAttr = new SqlDataAdapter(sqlAttr, sqlCon);
                DataTable sqlDataTableAttr = new DataTable();
                sqlAdapterAttr.Fill(sqlDataTableAttr);
                //执行查询
                int SearchResult = sqlAdapterAttr.Fill(sqlDataTableAttr);

                if (SearchResult == 0)
                {
                    break;
                }

                string title = "Canvas/详细信息/table/label" + i + "/title" + i;
                string text = "Canvas/详细信息/table/label" + i + "/text" + i;
                GameObject.Find(title).GetComponent<Text>().text = sqlDataTableAttr.Rows[0].ItemArray[0].ToString();
                GameObject.Find(text).GetComponent<Text>().text = "";
            }
            for (int i = 5; i < 9; i++)
            {
                string sqlAttr = "select HanYi from SheBeiTestAttribute where ZiDuanMingCheng='" + sqlDataTable.Columns[i] + "'";
                SqlDataAdapter sqlAdapterAttr = new SqlDataAdapter(sqlAttr, sqlCon);
                DataTable sqlDataTableAttr = new DataTable();
                sqlAdapterAttr.Fill(sqlDataTableAttr);
                //执行查询
                int SearchResult = sqlAdapterAttr.Fill(sqlDataTableAttr);

                if (SearchResult == 0)
                {
                    break;
                }

                string title = "Canvas/详细信息/table2/label" + i + "/title" + i;
                string text = "Canvas/详细信息/table2/label" + i + "/text" + i;
                GameObject.Find(title).GetComponent<Text>().text = sqlDataTableAttr.Rows[0].ItemArray[0].ToString();
                GameObject.Find(text).GetComponent<Text>().text = "";
            }
            for (int i = 9; i < 13; i++)
            {
                string sqlAttr = "select HanYi from SheBeiTestAttribute where ZiDuanMingCheng='" + sqlDataTable.Columns[i] + "'";
                SqlDataAdapter sqlAdapterAttr = new SqlDataAdapter(sqlAttr, sqlCon);
                DataTable sqlDataTableAttr = new DataTable();
                //执行查询
                int SearchResult = sqlAdapterAttr.Fill(sqlDataTableAttr);

                if (SearchResult == 0)
                {
                    break;
                }

                string title = "Canvas/详细信息/table3/label" + i + "/title" + i;
                string text = "Canvas/详细信息/table3/label" + i + "/text" + i;
                GameObject.Find(title).GetComponent<Text>().text = sqlDataTableAttr.Rows[0].ItemArray[0].ToString();
                GameObject.Find(text).GetComponent<Text>().text = "";
            }
            //提前清空图片显示
            string rawImgPath = "Canvas/详细信息/Image_text/RawImage";
            GameObject.Find(rawImgPath).GetComponent<RawImage>().texture = null;
            #endregion



            //获取每一行数据
            foreach (DataRow row in sqlDataTable.Rows)
            {
                //字段数减2，是减去ID与ImagePath   未来应改为减4，减去Path与SourcePath ，注意除ID外，均要放置在最后几列
                int lableNum = sqlDataTable.Columns.Count - 4;

                //如果lableNum <= 4，只放在第一行table中
                if (lableNum <= 4)
                {
                    //对于该行的每一个数据
                    for (int i = 1; i < 5; i++)
                    {
                        //查询该字段对应的中文名
                        string sqlAttr = "select HanYi from [SqlTest].[dbo].[SheBeiTestAttribute] where ZiDuanMingCheng='" + sqlDataTable.Columns[i] + "'";

                        //执行查询与保存
                        SqlDataAdapter sqlAdapterAttr = new SqlDataAdapter(sqlAttr, sqlCon);

                        //数据存储表
                        DataTable sqlDataTableAttr = new DataTable();

                        //执行查询
                        int SearchResult = sqlAdapterAttr.Fill(sqlDataTableAttr);
                        //如果无数据，退出循环
                        if (SearchResult == 0)
                        {
                            break;
                        }

                        //该行该列字段数据
                        string str = row[i].ToString();

                        //存储字段中文名的路径
                        string title = "Canvas/详细信息/table/label" + i + "/title" + i;

                        //存储字段英文名的路径
                        string text = "Canvas/详细信息/table/label" + i + "/text" + i;

                        //在title处存放字段的名称
                        GameObject.Find(title).GetComponent<Text>().text = sqlDataTableAttr.Rows[0].ItemArray[0].ToString();

                        //在text处存放字段的数据
                        GameObject.Find(text).GetComponent<Text>().text = str;

                    }
                }
                else if (lableNum >= 5 && lableNum <= 8)
                {
                    for (int i = 1; i < 5; i++)
                    {
                        string sqlAttr = "select HanYi from [SqlTest].[dbo].[SheBeiTestAttribute] where ZiDuanMingCheng='" + sqlDataTable.Columns[i] + "'";
                        SqlDataAdapter sqlAdapterAttr = new SqlDataAdapter(sqlAttr, sqlCon);
                        DataTable sqlDataTableAttr = new DataTable();
                        //执行查询
                        int SearchResult = sqlAdapterAttr.Fill(sqlDataTableAttr);

                        if (SearchResult == 0)
                        {
                            break;
                        }
                        string str = row[i].ToString();
                        string title = "Canvas/详细信息/table/label" + i + "/title" + i;
                        string text = "Canvas/详细信息/table/label" + i + "/text" + i;
                        GameObject.Find(title).GetComponent<Text>().text = sqlDataTableAttr.Rows[0].ItemArray[0].ToString();
                        GameObject.Find(text).GetComponent<Text>().text = str;
                    }
                    for (int i = 5; i < 9; i++)
                    {
                        string sqlAttr = "select HanYi from [SqlTest].[dbo].[SheBeiTestAttribute] where ZiDuanMingCheng='" + sqlDataTable.Columns[i] + "'";
                        SqlDataAdapter sqlAdapterAttr = new SqlDataAdapter(sqlAttr, sqlCon);
                        DataTable sqlDataTableAttr = new DataTable();
                        //执行查询
                        int SearchResult = sqlAdapterAttr.Fill(sqlDataTableAttr);

                        if (SearchResult == 0)
                        {
                            break;
                        }
                        string str = row[i].ToString();
                        string title = "Canvas/详细信息/table2/label" + i + "/title" + i;
                        string text = "Canvas/详细信息/table2/label" + i + "/text" + i;
                        GameObject.Find(title).GetComponent<Text>().text = sqlDataTableAttr.Rows[0].ItemArray[0].ToString();
                        GameObject.Find(text).GetComponent<Text>().text = str;
                    }
                }
                else
                {
                    for (int i = 1; i < 5; i++)
                    {
                        string sqlAttr = "select HanYi from SheBeiTestAttribute where ZiDuanMingCheng='" + sqlDataTable.Columns[i] + "'";
                        SqlDataAdapter sqlAdapterAttr = new SqlDataAdapter(sqlAttr, sqlCon);
                        DataTable sqlDataTableAttr = new DataTable();
                        sqlAdapterAttr.Fill(sqlDataTableAttr);
                        //执行查询
                        int SearchResult = sqlAdapterAttr.Fill(sqlDataTableAttr);

                        if (SearchResult == 0)
                        {
                            break;
                        }
                        string str = row[i].ToString();
                        string title = "Canvas/详细信息/table/label" + i + "/title" + i;
                        string text = "Canvas/详细信息/table/label" + i + "/text" + i;
                        GameObject.Find(title).GetComponent<Text>().text = sqlDataTableAttr.Rows[0].ItemArray[0].ToString();
                        GameObject.Find(text).GetComponent<Text>().text = str;
                    }
                    for (int i = 5; i < 9; i++)
                    {
                        string sqlAttr = "select HanYi from SheBeiTestAttribute where ZiDuanMingCheng='" + sqlDataTable.Columns[i] + "'";
                        SqlDataAdapter sqlAdapterAttr = new SqlDataAdapter(sqlAttr, sqlCon);
                        DataTable sqlDataTableAttr = new DataTable();
                        sqlAdapterAttr.Fill(sqlDataTableAttr);
                        //执行查询
                        int SearchResult = sqlAdapterAttr.Fill(sqlDataTableAttr);

                        if (SearchResult == 0)
                        {
                            break;
                        }
                        string str = row[i].ToString();
                        string title = "Canvas/详细信息/table2/label" + i + "/title" + i;
                        string text = "Canvas/详细信息/table2/label" + i + "/text" + i;
                        GameObject.Find(title).GetComponent<Text>().text = sqlDataTableAttr.Rows[0].ItemArray[0].ToString();
                        GameObject.Find(text).GetComponent<Text>().text = str;
                    }
                    for (int i = 9; i < 13; i++)
                    {
                        string sqlAttr = "select HanYi from SheBeiTestAttribute where ZiDuanMingCheng='" + sqlDataTable.Columns[i] + "'";
                        SqlDataAdapter sqlAdapterAttr = new SqlDataAdapter(sqlAttr, sqlCon);
                        DataTable sqlDataTableAttr = new DataTable();
                        //执行查询
                        int SearchResult = sqlAdapterAttr.Fill(sqlDataTableAttr);

                        if (SearchResult == 0)
                        {
                            break;
                        }
                        string str = row[i].ToString();
                        string title = "Canvas/详细信息/table3/label" + i + "/title" + i;
                        string text = "Canvas/详细信息/table3/label" + i + "/text" + i;
                        GameObject.Find(title).GetComponent<Text>().text = sqlDataTableAttr.Rows[0].ItemArray[0].ToString();
                        GameObject.Find(text).GetComponent<Text>().text = str;
                    }
                }

                string imgPath = Application.dataPath + "//Resources//folder//modelImg//" + row["ImgPath"].ToString();
                rawImgPath = "Canvas/详细信息/Image_text/RawImage";
                Texture2D tx = new Texture2D(220, 220);
                tx.LoadImage(getImageByte(imgPath));
                GameObject.Find(rawImgPath).GetComponent<RawImage>().texture = tx;
            }
            sqlCon.Close();
        }

        /// <summary>  
        /// 根据图片路径返回图片的字节流byte[]  
        /// </summary>  
        /// <param name="imagePath">图片路径</param>  
        /// <returns>返回的字节流</returns>  
        private static byte[] getImageByte(string imagePath)
        {
            FileStream files = new FileStream(imagePath, FileMode.Open);
            byte[] imgByte = new byte[files.Length];//给imgByte空间
            files.Read(imgByte, 0, imgByte.Length);
            //将files中的信息读进imgByte中,public override int Read(byte[] array, int offset, int count);
            files.Close();//关闭文件流
            return imgByte;
        }
    }
}

