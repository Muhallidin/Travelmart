using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Data;
using System.Reflection;

using System.Web.Configuration;
using System.Configuration;
using System.IO;
using System.Drawing;
using System.Collections.ObjectModel;
using System.Net;


namespace TRAVELMART.Common
{
    /// <summary>
    /// Date Created: 25/07/2011
    /// Created By: Muhallidin G Wali
    /// Description: Checked the Data Input      
    /// </summary>
    
    public class GlobalCode
    {
        public static long Field2Long(object sender)
        {
            long vLong = 0;
            try
            {
                if (sender != null)
                {
                    switch (sender.GetType().Name.ToString())
                    {

                        case "TextBox":
                            TextBox textbox = (TextBox)sender;
                            vLong = Convert.ToInt64(textbox.Text.ToString());
                            break;
                        case "String":
                            string sType = (string)sender;
                            vLong = Convert.ToInt64(sType.ToString());
                            break;
                        default:
                            vLong = Convert.ToInt64(sender.ToString());
                            break;

                    }
                }
                return vLong;
            }
            catch
            {
                return 0;
            }
        }
        public static Int32 Field2Int(object sender)
        {
            Int32 vInt = 0;
            try
            {
                if (sender != null)
                {
                    switch (sender.GetType().Name.ToString()) 
                    {
                            
                        case "TextBox":
                            TextBox textbox = (TextBox)sender;
                            vInt = Convert.ToInt32(textbox.Text.ToString());
                            break;
                        case "String":
                            string sType = (string)sender;
                            vInt = Convert.ToInt32(sType.ToString());
                            break;
                        default:
                            vInt = Convert.ToInt32(sender.ToString());
                            break;
                    }
                }
                return vInt;
            }
            catch 
            {
                return 0; 
            }
        }

        public static float Field2Float(object sender)
        {
            float vInt = 0;
            try
            {
                if (sender != null)
                {
                    switch (sender.GetType().Name.ToString())
                    {

                        case "TextBox":
                            TextBox textbox = (TextBox)sender;
                            vInt = Convert.ToSingle(textbox.Text.ToString());
                            break;
                        case "String":
                            string sType = (string)sender;
                            vInt = Convert.ToSingle(sType.ToString());
                            break;
                        default:
                            vInt = Convert.ToSingle(sender.ToString());
                            break;
                    }
                }
                return vInt;
            }
            catch
            {
                return 0;
            }
        }


        public static Int16 Field2TinyInt(object sender)
        {
            Int16 vInt = 0;
            try
            {
                if (sender != null)
                {
                    switch (sender.GetType().Name.ToString())
                    {
                        case "TextBox":
                            TextBox textbox = (TextBox)sender;
                            vInt = Convert.ToInt16(textbox.Text.ToString());
                            break;
                        case "String":
                            String SType = (String)sender;
                            vInt = Convert.ToInt16(SType.ToString());
                            break;
                        default:
                            vInt = Convert.ToInt16(sender.ToString());
                            break;
                    }
                }
                return vInt;
            }
            catch
            {
                return 0;
            }
        }

        public static bool Field2Bool(object sender)
        {
            bool vbool = false;
            try
            {
                if (sender != null)
                {
                    switch (sender.GetType().ToString())
                    {
                        case "TextBox":
                            TextBox textbox = (TextBox)sender;
                            vbool = Convert.ToBoolean(textbox.Text.ToString());
                            break;
                        case "String":
                            String SType = (String)sender;
                            vbool = Convert.ToBoolean(SType.ToString());
                            break;
                        default:
                            vbool = Convert.ToBoolean(sender);
                            break;
                    }
                }
                return vbool;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Date Modified:     02/02/2012
        /// Modified By:       Josephine Gad
        /// (description)      Add CultureInfo enCulture = new CultureInfo(TravelMartVariable.UserCultureInfo); for conversion
        ///                    Convert object to Date
        /// </summary>
        /// <param name="sender"></param>
        /// <returns></returns>
        public static DateTime Field2DateTime(object sender)
        {
            CultureInfo enCulture = new CultureInfo(TravelMartVariable.UserCultureInfo);
            DateTime vDateTime = DateTime.Now;
            try
            {               
                if (sender != null)
                {
                    switch (sender.GetType().Name.ToString())
                    {
                        case "TextBox":
                            TextBox textbox = (TextBox)sender;
                            vDateTime = DateTime.Parse(textbox.Text.ToString(), enCulture);
                            break;
                        case "String":
                            String SType = (String)sender;
                            vDateTime = DateTime.Parse(SType.ToString(), enCulture);
                            break;
                        default:
                            vDateTime = DateTime.Parse(sender.ToString(), enCulture);
                            break;
                    }
                }
                return vDateTime;
            }
            catch
            {
                return  DateTime.Parse(DateTime.Now.ToString(TravelMartVariable.DateFormat), enCulture) ;
            }
        }

        /// <summary>
        /// Date Modified:     02/02/2012
        /// Modified By:       Josephine Gad
        /// (description)      Add CultureInfo enCulture = new CultureInfo(TravelMartVariable.UserCultureInfo); for conversion
        ///                    Convert object to Date
        /// </summary>
        /// <param name="sender"></param>
        /// <returns></returns>
        public static DateTime? Field2DateTimeNull(object sender)
        {
            CultureInfo enCulture = new CultureInfo(TravelMartVariable.UserCultureInfo);
            DateTime vDateTime = DateTime.Now;
            try
            {
                if (sender != null)
                {
                    switch (sender.GetType().Name.ToString())
                    {
                        case "TextBox":
                            TextBox textbox = (TextBox)sender;
                            vDateTime = DateTime.Parse(textbox.Text.ToString(), enCulture);
                            break;
                        case "String":
                            String SType = (String)sender;
                            vDateTime = DateTime.Parse(SType.ToString(), enCulture);
                            break;
                        default:
                            vDateTime = DateTime.Parse(sender.ToString(), enCulture);
                            break;
                    }
                }
                return vDateTime;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Date Modified:     02/02/2012
        /// Modified By:       Josephine Gad
        /// (description)      Add CultureInfo enCulture = new CultureInfo(TravelMartVariable.UserCultureInfo); for conversion
        ///                    Convert object to Date
        /// </summary>
        /// <param name="sender"></param>
        /// <returns></returns>
        public static DateTime? Field2DateTimewithNull(object sender)
        {
            CultureInfo enCulture = new CultureInfo(TravelMartVariable.UserCultureInfo);
            DateTime vDateTime = DateTime.Now;
            try
            {
                if (sender != null)
                {
                    switch (sender.GetType().Name.ToString())
                    {
                        case "TextBox":
                            TextBox textbox = (TextBox)sender;
                            vDateTime = DateTime.Parse(textbox.Text.ToString(), enCulture);
                            break;
                        case "String":
                            String SType = (String)sender;
                            vDateTime = DateTime.Parse(SType.ToString(), enCulture);
                            break;
                        default:
                            vDateTime = DateTime.Parse(sender.ToString(), enCulture);
                            break;
                    }
                }
                return vDateTime;
            }
            catch
            {
                return null;// DateTime.Parse(DateTime.Now.ToString(TravelMartVariable.DateFormat), enCulture);
            }
        }

        /// <summary>
        /// Date Modified:     02/02/2012
        /// Modified By:       Josephine Gad
        /// (description)      Add CultureInfo enCulture = new CultureInfo(TravelMartVariable.UserCultureInfo); for conversion
        ///                    Convert object to Date
        /// </summary>
        /// <param name="sender"></param>
        /// <returns></returns>
        public static String Field2Date(object sender)
        {
            CultureInfo enCulture = new CultureInfo(TravelMartVariable.UserCultureInfo);
            DateTime vDateTime = DateTime.Now;
            try
            {
                if (sender != null)
                {
                    switch (sender.GetType().Name.ToString())
                    {
                        case "TextBox":
                            TextBox textbox = (TextBox)sender;
                            vDateTime = DateTime.Parse(textbox.Text.ToString(), enCulture);
                            break;
                        case "String":
                            String SType = (String)sender;
                            vDateTime = DateTime.Parse(SType.ToString(), enCulture);
                            break;
                        default:
                            vDateTime = DateTime.Parse(sender.ToString(), enCulture);
                            break;
                    }
                }
                return vDateTime.Date.ToShortDateString();
            }
            catch
            {
                return DateTime.Parse(DateTime.Now.ToString(TravelMartVariable.DateFormat), enCulture).Date.ToShortDateString();
            }
        }

        /// <summary>
        /// Date Created:     03/02/2012
        /// Created By:       Josephine Gad
        /// (description)     Convert object to datetime
        /// </summary>
        /// <param name="sender"></param>
        /// <returns></returns>
        public static DateTime Field2DateTimeWithTime(object sender)
        {
            CultureInfo enCulture = new CultureInfo(TravelMartVariable.UserCultureInfo);
            DateTime vDateTime = DateTime.Now;
            try
            {
                if (sender != null)
                {
                    switch (sender.GetType().Name.ToString())
                    {
                        case "TextBox":
                            TextBox textbox = (TextBox)sender;
                            vDateTime = DateTime.Parse(textbox.Text.ToString(), enCulture);
                            break;
                        case "String":
                            String SType = (String)sender;
                            vDateTime = DateTime.Parse(SType.ToString(), enCulture);
                            break;
                        default:
                            vDateTime = DateTime.Parse(sender.ToString(), enCulture);
                            break;
                    }
                }
                return vDateTime;
            }
            catch
            {
                return DateTime.Parse(DateTime.Now.ToString(TravelMartVariable.DateTimeFormat), enCulture); 
            }
        }
        public static string Field2String(object sender)
        {
            try
            {
                if (sender != null)
                    return sender.ToString();
                else
                    return "";
            }
            catch
            {
                return "";
            }

        }
        /// <summary>
        /// Date Created:   02/01/2011
        /// Created By:     Josephine Gad
        /// (description)   Convert string to decimal with validation
        /// =============================================================
        /// </summary>
        /// <param name="sender"></param>
        /// <returns></returns>
        public static decimal Field2Decimal(object sender)
        {
            decimal vDec = 0;
            try
            {
                if (sender != null)
                {
                    switch (sender.GetType().Name.ToString())
                    {

                        case "TextBox":
                            TextBox textbox = (TextBox)sender;
                            vDec = Convert.ToDecimal(textbox.Text.ToString());
                            break;
                        case "String":
                            string sType = (string)sender;
                            vDec = Convert.ToDecimal(sType.ToString());
                            break;
                        default:
                            vDec = Convert.ToDecimal(sender.ToString());
                            break;
                    }
                }
                return vDec;
            }
            catch
            {
                return 0;
            }
        }
        /// <summary>
        /// Date Created:   20/04/2012
        /// Created By:     Josephine Gad
        /// (description)   Convert string to double with validation
        /// =============================================================
        /// </summary>
        /// <param name="sender"></param>
        /// <returns></returns>
        public static double Field2Double(object sender)
        {
            double vDouble = 0;
            try
            {
                if (sender != null)
                {
                    switch (sender.GetType().Name.ToString())
                    {

                        case "TextBox":
                            TextBox textbox = (TextBox)sender;
                            vDouble = Convert.ToDouble(textbox.Text.ToString());
                            break;
                        case "String":
                            string sType = (string)sender;
                            vDouble = Convert.ToDouble(sType.ToString());
                            break;
                        default:
                            vDouble = Convert.ToDouble(sender.ToString());
                            break;
                    }
                }
                return vDouble;
            }
            catch
            {
                return 0;
            }
        }



        public static DateTime GetClientTime()
        {
            DateTime vLocalTime = DateTime.Now;

            TimeZoneInfo cst = TimeZoneInfo.FindSystemTimeZoneById(TimeZone.CurrentTimeZone.StandardName);
            TimeZoneInfo local = TimeZoneInfo.Local;
            vLocalTime = TimeZoneInfo.ConvertTime(DateTime.Now, local, cst);

            TimeZoneInfo.ClearCachedData();
            try
            {
                return TimeZoneInfo.ConvertTime(DateTime.Now, local, cst); 
            }
            catch 
            {
                return vLocalTime;
            }
        }


        /// <summary>
        /// Author:       Muhallidin G Wali
        /// Date Created: 04/11/2013
        /// Description:  convert list to datatable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <returns></returns>
        public DataTable getDataTable<T>(List<T> items)
        {
            var tb = new DataTable(typeof(T).Name);
            PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo prop in props)
            {
                //tb.Columns.Add(prop.Name, t);
                tb.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }

            foreach (T item in items)
            {
                var values = new object[props.Length];
                for (int i = 0; i < props.Length; i++)
                {
                    values[i] = props[i].GetValue(item, null);
                }
                tb.Rows.Add(values);
            }
            return tb;
        }

        /// <summary>
        /// Author:       Muhallidin G Wali
        /// Date Created: 04/11/2013
        /// Description:  get item type
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static Type GetCoreType(Type t)
        {
            if (t != null)
            {
                if (!t.IsValueType)
                {
                    return t;
                }
                else
                {
                    return Nullable.GetUnderlyingType(t);
                }
            }
            else
            {
                return t;
            }
        }

        public static int GetdateDiff(DateTime dateFrom, DateTime dateTo)
        {
            int dateDiff = 0;
            System.DateTime dtdateFrom = new System.DateTime(dateFrom.Year, dateFrom.Month, dateFrom.Day, 12, 0, 0);
            System.DateTime dtdateTo = new System.DateTime(dateTo.Year, dateTo.Month, dateTo.Day, 12, 0, 0);

            System.TimeSpan diffResult = dtdateTo - dtdateFrom;

            System.TimeSpan diffResults = dateTo - dateFrom;


            //TimeSpan GiveTheTimeOfTheDay = dateFrom.Date - dateTo.Date;
            dateDiff = int.Parse(diffResult.Days.ToString());

            try
            {
                return dateDiff;
            }
            catch
            {
                return 0;
            }
        }



        public static int GetselectedIndex(DropDownList sender, int? value)
        {

            int id = -1;
            try
            {
                int i = 0;
                for (i = 0; sender.Items.Count >= i; i++)
                { 
                    sender.SelectedIndex = i;
                    if (sender.SelectedValue == value.ToString()) return i;
                }

                return id;
            }
            catch 
            {
                return id;
            }

            
        }

        public static int GetselectedIndexText(DropDownList sender, string value)
        {

            int id = -1;
            try
            {
                int i = 0;
                for (i = 0; sender.Items.Count >= i; i++)
                {
                    sender.SelectedIndex = i;
                    if (sender.SelectedItem.Text == value.ToString()) return i;
                }
                return id;
            }
            catch
            {
                return id;
            }


        }

        public static int GetselectedIndex(DropDownList sender, string value)
        {

            int id = -1;
            try
            {
                int i = 0;
                for (i = 0; sender.Items.Count >= i; i++)
                {
                    sender.SelectedIndex = i;
                    //sender.SelectedItem.Text.Substring(0, 3);

                    if (sender.SelectedItem.Text.Substring(0, 3) == value.ToString()) return i;
                }

                return id;
            }
            catch
            {
                return id;
            }


        }

        public static int GetselectedIndexValue(DropDownList sender, string value)
        {

            int id = -1;
            try
            {
                int i = 0;
                for (i = 0; sender.Items.Count >= i; i++)
                {
                    sender.SelectedIndex = i;
                    //sender.SelectedItem.Text.Substring(0, 3);

                    if (sender.SelectedValue == value.ToString()) return i;
                }

                return id;
            }
            catch
            {
                return id;
            }


        }

        public static int GetselectedIndex(ListBox sender, string value)
        {

            int id = -1;
            try
            {
                int i = 0;
                for (i = 0; sender.Items.Count >= i; i++)
                {
                    sender.SelectedIndex = i;
                    if (sender.SelectedItem.Value == value.ToString()) return i;
                }

                return id;
            }
            catch
            {
                return id;
            }


        }
        public static int GetselectedValue(DropDownList sender, string value)
        {
            int id = -1;
            try
            {
                int i = 0;
                for (i = 0; sender.Items.Count >= i; i++)
                {
                    sender.SelectedIndex = i;
                    if (sender.SelectedItem.Text.ToString() == value.ToString()) return Field2Int(sender.SelectedValue);
                }
                return id;
            }
            catch
            {
                return id;
            }


        }



        public static DateTime Field2Time(string value)
        {
            DateTime id =  DateTime.ParseExact("1999-09-01 00:00 PM", "yyyy-MM-dd HH:mm tt",null);
            try
            {

                DateTime MyDateTime;
                MyDateTime = new DateTime();

                MyDateTime = Convert.ToDateTime(String.Format("{0} {1}",("1999-09-01"), value));

                return MyDateTime;
            }
            catch
            {
                return id;
            }
        }

        public static int GetselectedValue(RadioButtonList sender, string value)
        {
            int id = -1;
            try
            {
                int i = 0;
                for (i = 0; sender.Items.Count >= i; i++)
                {
                    sender.SelectedIndex = i;
                    if (sender.SelectedItem.Value.ToString() == value.ToString()) return Field2Int(sender.SelectedValue);
                }
                return id;
            }
            catch
            {
                return id;
            }


        }
      
        public void SetAppSetting(string value)
        {
            try
            { 
                Configuration config = WebConfigurationManager.OpenWebConfiguration("/");
                string oldValue = config.AppSettings.Settings["SessionUser"].Value;
                config.AppSettings.Settings["SessionUser"].Value = value;
                config.Save();

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public string GetAppSetting()
        {
            try
            {
                Configuration config = WebConfigurationManager.OpenWebConfiguration("/");
                return config.AppSettings.Settings["SessionUser"].Value;
            }
            catch 
            {
                return "";
            }

        }

        public static byte[] Field2ImageByte(System.Drawing.Image x)
        {
            try
            {
                ImageConverter _imageConverter = new ImageConverter();
                byte[] xByte = (byte[])_imageConverter.ConvertTo(x, typeof(byte[]));
                return xByte;

            }
            catch {

                return null;
            }
            
        }


        public static byte[] Field2BitmapByte(System.Drawing.Bitmap x)
        {
            try
            {
                ImageConverter _imageConverter = new ImageConverter();
                byte[] xByte = (byte[])_imageConverter.ConvertTo(x, typeof(byte[]));
                return xByte;

            }
            catch
            {

                return null;
            }

        }

        public static Bitmap Field2ByteImage(byte[] blob)
        {
            try
            {
                MemoryStream mStream = new MemoryStream();
                byte[] pData = blob;
                mStream.Write(pData, 0, Convert.ToInt32(pData.Length));
                Bitmap bm = new Bitmap(mStream, false);
                mStream.Dispose();
                return bm;
            }
            catch {
                return null;
            
            }
           

        }

        public static byte[] Field2PictureByte(object  blob)
        {
            try
            {
                byte[] barrImg = (byte[])blob;
                return barrImg;
            }
            catch
            {
                return null;

            }


        }
        public static string Field2PictureImage(byte[] bytes, string immageType)
        {
            try
            {
                //byte[] barrImg = (byte[])blob;
                //return barrImg;
                string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);

                if (immageType.ToUpper() == "JPG")
                    return "data:image/jpg;base64," + base64String;
                else if  (immageType.ToUpper() == "PNG")
                    return "data:image/png;base64," + base64String;
                else if (immageType.ToUpper() == "TIF")
                    return "data:image/tip;base64," + base64String;
                else if (immageType.ToUpper() == "GIF")
                    return "data:image/gif;base64," + base64String;
                else 
                    return  "data:image/jpg;base64," + base64String;
            }
            catch
            {
                return null;

            }


        }




        public static DateTime Field2TimeZoneTime(DateTime currentTime, string tzID)
        {
            DateTime datatime = new DateTime();
            try
            {
                string timezone = ConfigurationSettings.AppSettings["timezone"].ToString();
                datatime = currentTime;
              
                
                //TimeZoneInfo timeZoneInfo;
                //timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(timezone);
                //datatime = TimeZoneInfo.ConvertTime(currentTime, timeZoneInfo);

                ReadOnlyCollection<TimeZoneInfo> tzi = TimeZoneInfo.GetSystemTimeZones();   

                foreach (TimeZoneInfo timeZone in tzi)
                {
                    if (timeZone.Id == tzID 
                        || timeZone.DaylightName == tzID 
                        || timeZone.DisplayName == tzID
                        )
                    {
                        datatime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(currentTime, timezone, timeZone.Id); //,  timeZone. .GetUtcOffset(currentTime);
                    }
                }
                return datatime;
            }
            catch {
                return currentTime;
            }
        }


        public static DateTime Field2SelectedTimeZone(DateTime currentTime,string LocalTimeZone, string tzID)
        {
            DateTime datatime = new DateTime();
            try
            {
               
                 
                ReadOnlyCollection<TimeZoneInfo> tzi = TimeZoneInfo.GetSystemTimeZones();

                foreach (TimeZoneInfo timeZone in tzi)
                {
                    if (timeZone.Id == tzID
                        || timeZone.DaylightName == tzID
                        || timeZone.DisplayName == tzID
                        )
                    {
                         //,  timeZone. .GetUtcOffset(currentTime);
                        datatime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(currentTime, LocalTimeZone, timeZone.Id); //,  timeZone. .GetUtcOffset(currentTime);
                        break;
                    }
                }

                



                return datatime;
            }
            catch
            {
                return currentTime;
            }
        }



        /// <summary>
        /// Date Created:   22/Nov/2017
        /// Created By:     Muhallidin G Wali
        /// (description)  Get Vehicle Photo
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        /// 
        public VehicleImageFile GetPhoto(string URL)
        {

            VehicleImageFile img = new VehicleImageFile();
            try
            {
                ////string url =  "http://10.80.0.49:7001/vendors/transportation/vehicle/" + ID + "?at=8c193d19e210e3f5705f95c21cd60f614db1bc26"  ;
                StringBuilder _sb = new StringBuilder();
                Byte[] _byte = GetImage(URL);
                _sb.Append(Convert.ToBase64String(_byte, 0, _byte.Length));
                img.Image = _sb.ToString();
            }
            catch (Exception exp)
            {
                img.Image = null;
            }
            return img;
        }


        /// <summary>
        /// Date Created:   04/Jan/2018
        /// Created By:     Muhallidin G Wali
        /// (description)  Get Vehicle Photo
        /// ------------------------------------------
        /// Date Modified:  05/Jan/2018
        /// Created By:     JMonteza
        /// (description)   Close stream and response in finally block
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private static byte[] GetImage(string url)
        {
            Stream stream = null;
            byte[] buf;
            HttpWebResponse response = null;

            try
            {
                WebProxy myProxy = new WebProxy();
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);

                response = (HttpWebResponse)req.GetResponse();
                stream = response.GetResponseStream();

                using (BinaryReader br = new BinaryReader(stream))
                {
                    int len = (int)(response.ContentLength);
                    buf = br.ReadBytes(len);
                    br.Close();
                }
            }
            catch (Exception exp)
            {
                buf = null;
                throw exp;
            }
            finally
            {
                stream.Close();
                response.Close();
            }

            return (buf);
        }



    }
}
