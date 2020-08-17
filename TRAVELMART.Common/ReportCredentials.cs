using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Security.Principal;
using Microsoft.Reporting.WebForms;

namespace TRAVELMART.Common
{
    [Serializable]
    public class ReportCredentials : IReportServerCredentials
    {
        string _userName, _password, _domain;

        
        public ReportCredentials(string userName, string password, string domain)
        {

            _userName = userName;

            _password = password;

            _domain = domain;

        }

        public System.Security.Principal.WindowsIdentity ImpersonationUser
        {

            get
            {

                return null;

            }

        }



        public System.Net.ICredentials NetworkCredentials
        {

            get
            {

                return new System.Net.NetworkCredential(_userName, _password, _domain);

            }

        }



        public bool GetFormsCredentials(out System.Net.Cookie authCoki, out string userName, out string password, out string authority)
        {

            userName = _userName;

            password = _password;

            authority = _domain;

            authCoki = new System.Net.Cookie(".ASPXAUTH", ".ASPXAUTH", "/", "Domain");

            return true;

        }

    }
}
