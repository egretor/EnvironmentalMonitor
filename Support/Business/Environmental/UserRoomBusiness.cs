using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EnvironmentalMonitor.Support.Hibernate.Environmental;
using EnvironmentalMonitor.Support.Hibernate;
using EnvironmentalMonitor.Support.Module.Environmental;
using EnvironmentalMonitor.Support.Module;

namespace EnvironmentalMonitor.Support.Business.Environmental
{
    public class UserRoomBusiness
    {
        public bool Refresh(string userGuid, List<UserRoom> values)
        {
            bool result = false;

            UserRoomHiberante hibernate = new UserRoomHiberante();
            result = hibernate.DeleteByUser(userGuid);
            if (result)
            {
                if (values != null)
                {
                    for (int i = 0; i < values.Count; i++)
                    {
                        result = hibernate.Insert(values[i]);
                        if (!result)
                        {
                            break;
                        }
                    }
                }
            }

            return result;
        }
    }
}
