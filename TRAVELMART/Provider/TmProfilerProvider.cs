using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Reflection;
using System.Configuration;
using System.IO;
using System.Text;
using System.Web.Profile;
using TRAVELMART.Common;

namespace TRAVELMART.Provider
{
    public class TmProfilerProvider : SqlProfileProvider
    {
        public override void Initialize(string name, System.Collections.Specialized.NameValueCollection config)
        {
            base.Initialize(name, config);
            try
            {
                FieldInfo connectionStringField = typeof(SqlProfileProvider).GetField("_sqlConnectionString", BindingFlags.Instance | BindingFlags.NonPublic);
                connectionStringField.SetValue(this, ConnectionSetting.GetConnectionSecuritySetting());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
    }
}