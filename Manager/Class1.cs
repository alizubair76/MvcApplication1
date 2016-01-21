using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;

namespace Manager
{
    public class ActMgr
    {
        public static object GetEmployee()
        {

            return _GeneralFunctions.ExecuteScalar("Select Top 1 EmployeeID From Employee");
        }
    }
}
