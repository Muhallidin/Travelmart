using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Reflection;
using System.Configuration;
using System.IO;
using System.Text;
using TRAVELMART.Common;

namespace TRAVELMART.Provider
{

    public class TmRoleProvider : SqlRoleProvider
    {
        public override void  Initialize(string name, System.Collections.Specialized.NameValueCollection config)
        {
 	     base.Initialize(name, config);
         try
         {
             FieldInfo connectionStringField = typeof(SqlRoleProvider).GetField("_sqlConnectionString", BindingFlags.Instance | BindingFlags.NonPublic);
             connectionStringField.SetValue(this, ConnectionSetting.GetConnectionSecuritySetting());
         }
         catch (Exception ex)
         {
             throw ex;
         }
        }
        
    }
}
