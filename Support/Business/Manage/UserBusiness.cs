using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Reflection;

using EnvironmentalMonitor.Support.Hibernate.Environmental;
using EnvironmentalMonitor.Support.Hibernate.Manage;
using EnvironmentalMonitor.Support.Hibernate;
using EnvironmentalMonitor.Support.Module.Environmental;
using EnvironmentalMonitor.Support.Module.Manage;
using EnvironmentalMonitor.Support.Module;

namespace EnvironmentalMonitor.Support.Business.Manage
{
    /// <summary>
    /// 用户业务
    /// </summary>
    public class UserBusiness
    {
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="page">页码</param>
        /// <param name="rows">每页行数</param>
        /// <param name="total">总数</param>
        /// <returns>用户集合</returns>
        public List<User> Query(int page, int rows, ref int total)
        {
            List<User> results = new List<User>();

            UserHibernate hibernate = new UserHibernate();
            results = hibernate.Query(page, rows, ref total);

            return results;
        }

        public User QueryByAccount(string account)
        {
            User result = null;

            UserHibernate hibernate = new UserHibernate();
            result = hibernate.SelectByAccount(account);

            return result;
        }

        public List<User> QueryByInsertUser(User value)
        {
            List<User> results = null;

            UserHibernate hibernate = new UserHibernate();
            results = hibernate.QueryByInsertUser(value.Guid);

            return results;
        }

        public List<Room> RefreshRooms(User value)
        {
            List<Room> results = null;

            RoomHibernate roomHibernate = new RoomHibernate();
            int total = 0;
            List<Room> rooms = roomHibernate.Query(1, int.MaxValue, ref total);
            if (value.Prerogative)
            {
                results = rooms;
            }
            else
            {
                UserRoomHiberante userRoomHiberante = new UserRoomHiberante();
                List<UserRoom> userRooms = userRoomHiberante.QueryByUser(1, int.MaxValue, ref total, value.Guid);
                if ((userRooms != null) && (userRooms.Count > 0))
                {
                    if ((rooms != null) && (rooms.Count > 0))
                    {
                        results = new List<Room>();
                        for (int i = 0; i < userRooms.Count; i++)
                        {
                            for (int j = 0; j < rooms.Count; j++)
                            {
                                if (string.Equals(userRooms[i].RoomId, rooms[j].Guid, StringComparison.CurrentCulture))
                                {
                                    results.Add(rooms[j]);
                                    break;
                                }
                            }
                        }
                    }
                }
            }

            return results;
        }

        public List<UserModule> RefreshUserModules(User value)
        {
            List<UserModule> results = null;

            if (value.Prerogative)
            {
                results = new List<UserModule>();
                for (int i = 0; i < UserModule.Modules.Length; i++)
                {
                    UserModule userModule = new UserModule();
                    userModule.ModuleCode = UserModule.Modules[i];
                    results.Add(userModule);
                }
            }
            else
            {
                UserModuleBusiness business = new UserModuleBusiness();
                int total = 0;
                results = business.QueryByUser(1, int.MaxValue, ref total, value.Guid);
            }

            return results;
        }

        public List<User> RefreshUsers(User value)
        {
            List<User> results = null;

            int total = 0;
            if (value.Prerogative)
            {
                results = this.Query(1, int.MaxValue, ref total);
            }
            else
            {
                results = this.QueryByInsertUser(value);
            }
            for (int i = results.Count - 1; i >= 0; i--)
            {
                if (string.Equals(value.Guid, results[i].Guid, StringComparison.CurrentCulture))
                {
                    results.RemoveAt(i);
                }
            }

            return results;
        }

        public void Refresh(User value)
        {
            value.Rooms = this.RefreshRooms(value);
            value.Users = this.RefreshUsers(value);
            value.UserModules = this.RefreshUserModules(value);

            if (value.UserModules != null)
            {
                value.Catalogs = new List<Catalog>();
                List<UserModule> userModules = value.UserModules;
                for (int i = 0; i < userModules.Count; i++)
                {
                    Catalog catalog = null;
                    switch (userModules[i].ModuleCode)
                    {
                        case UserModule.MANAGE_MODULE:
                            catalog = new Catalog(UserModule.MANAGE_MODULE, "用户管理", "#");
                            catalog.Catalogs = new List<Catalog>();
                            catalog.Catalogs.Add(new Catalog(UserModule.MANAGE_MODULE, "新增用户", string.Format("{0}Manages/Users/Insert.aspx", EnvironmentalMonitor.Support.Resource.Variable.VirtualRootPath)));
                            catalog.Catalogs.Add(new Catalog(UserModule.MANAGE_MODULE, "修改用户", string.Format("{0}Manages/Users/Update.aspx", EnvironmentalMonitor.Support.Resource.Variable.VirtualRootPath)));
                            catalog.Catalogs.Add(new Catalog(UserModule.MANAGE_MODULE, "删除用户", string.Format("{0}Manages/Users/Delete.aspx", EnvironmentalMonitor.Support.Resource.Variable.VirtualRootPath)));
                            break;
                        case UserModule.ROOM_MODULE:
                            catalog = new Catalog(UserModule.ROOM_MODULE, "机房配置", "#");
                            catalog.Catalogs = new List<Catalog>();
                            catalog.Catalogs.Add(new Catalog(UserModule.ROOM_MODULE, "新增机房", string.Format("{0}Environmentals/Rooms/Insert.aspx", EnvironmentalMonitor.Support.Resource.Variable.VirtualRootPath)));
                            catalog.Catalogs.Add(new Catalog(UserModule.ROOM_MODULE, "修改机房", string.Format("{0}Environmentals/Rooms/Update.aspx", EnvironmentalMonitor.Support.Resource.Variable.VirtualRootPath)));
                            catalog.Catalogs.Add(new Catalog(UserModule.ROOM_MODULE, "删除机房", string.Format("{0}Environmentals/Rooms/Delete.aspx", EnvironmentalMonitor.Support.Resource.Variable.VirtualRootPath)));
                            break;
                        case UserModule.MACHINE_MODULE:
                            catalog = new Catalog(UserModule.MACHINE_MODULE, "检测仪配置", "#");
                            catalog.Catalogs = new List<Catalog>();
                            catalog.Catalogs.Add(new Catalog(UserModule.MACHINE_MODULE, "新增检测仪", string.Format("{0}Environmentals/Machines/Insert.aspx", EnvironmentalMonitor.Support.Resource.Variable.VirtualRootPath)));
                            catalog.Catalogs.Add(new Catalog(UserModule.MACHINE_MODULE, "修改检测仪", string.Format("{0}Environmentals/Machines/Update.aspx", EnvironmentalMonitor.Support.Resource.Variable.VirtualRootPath)));
                            catalog.Catalogs.Add(new Catalog(UserModule.MACHINE_MODULE, "删除检测仪", string.Format("{0}Environmentals/Machines/Delete.aspx", EnvironmentalMonitor.Support.Resource.Variable.VirtualRootPath)));
                            break;
                        case UserModule.MACHINE_ADVANCED_MODULE:
                            catalog = new Catalog(UserModule.MACHINE_ADVANCED_MODULE, "检测仪高级配置", "#");
                            catalog.Catalogs = new List<Catalog>();
                            catalog.Catalogs.Add(new Catalog(UserModule.MACHINE_ADVANCED_MODULE, "检测仪IP设置", string.Format("{0}Environmentals/Machines/SetupIp.aspx", EnvironmentalMonitor.Support.Resource.Variable.VirtualRootPath)));
                            catalog.Catalogs.Add(new Catalog(UserModule.MACHINE_ADVANCED_MODULE, "检测仪物理地址设置", string.Format("{0}Environmentals/Machines/SetupMac.aspx", EnvironmentalMonitor.Support.Resource.Variable.VirtualRootPath)));
                            break;
                        case UserModule.DETECTOR_TYPE_MODULE:
                            catalog = new Catalog(UserModule.DETECTOR_TYPE_MODULE, "探头类型配置", "#");
                            catalog.Catalogs = new List<Catalog>();
                            catalog.Catalogs.Add(new Catalog(UserModule.DETECTOR_TYPE_MODULE, "新增探头类型", string.Format("{0}Environmentals/DetectorTypes/Insert.aspx", EnvironmentalMonitor.Support.Resource.Variable.VirtualRootPath)));
                            catalog.Catalogs.Add(new Catalog(UserModule.DETECTOR_TYPE_MODULE, "修改探头类型", string.Format("{0}Environmentals/DetectorTypes/Update.aspx", EnvironmentalMonitor.Support.Resource.Variable.VirtualRootPath)));
                            catalog.Catalogs.Add(new Catalog(UserModule.DETECTOR_TYPE_MODULE, "删除探头类型", string.Format("{0}Environmentals/DetectorTypes/Delete.aspx", EnvironmentalMonitor.Support.Resource.Variable.VirtualRootPath)));
                            break;
                        case UserModule.DETECTOR_MODULE:
                            catalog = new Catalog(UserModule.DETECTOR_MODULE, "探头配置", "#");
                            catalog.Catalogs = new List<Catalog>();
                            catalog.Catalogs.Add(new Catalog(UserModule.DETECTOR_MODULE, "新增探头", string.Format("{0}Environmentals/Detectors/Insert.aspx", EnvironmentalMonitor.Support.Resource.Variable.VirtualRootPath)));
                            catalog.Catalogs.Add(new Catalog(UserModule.DETECTOR_MODULE, "修改探头", string.Format("{0}Environmentals/Detectors/Update.aspx", EnvironmentalMonitor.Support.Resource.Variable.VirtualRootPath)));
                            catalog.Catalogs.Add(new Catalog(UserModule.DETECTOR_MODULE, "删除探头", string.Format("{0}Environmentals/Detectors/Delete.aspx", EnvironmentalMonitor.Support.Resource.Variable.VirtualRootPath)));
                            break;
                        case UserModule.ALARM_MODULE:
                            catalog = new Catalog(UserModule.ALARM_MODULE, "报警设置", string.Format("{0}Environmentals/Machines/SetupAlarm.aspx", EnvironmentalMonitor.Support.Resource.Variable.VirtualRootPath));
                            break;
                        case UserModule.MONITOR_MODULE:
                            catalog = new Catalog(UserModule.MONITOR_MODULE, "监控模式", "#");
                            catalog.Catalogs = new List<Catalog>();
                            catalog.Catalogs.Add(new Catalog(UserModule.MONITOR_MODULE, "平面图配置", string.Format("{0}Environmentals/Monitors/FloorPlan.aspx", EnvironmentalMonitor.Support.Resource.Variable.VirtualRootPath)));
                            catalog.Catalogs.Add(new Catalog(UserModule.MONITOR_MODULE, "实时监控", string.Format("{0}Environmentals/Monitors/Realtime.aspx", EnvironmentalMonitor.Support.Resource.Variable.VirtualRootPath)));
                            break;
                        case UserModule.QUERY_MODULE:
                            catalog = new Catalog(UserModule.QUERY_MODULE, "查询模式", string.Format("{0}Environmentals/Querys/Default.aspx", EnvironmentalMonitor.Support.Resource.Variable.VirtualRootPath));
                            break;
                    }
                    if (catalog != null)
                    {
                        value.Catalogs.Add(catalog);
                    }
                }
            }
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="account">帐号</param>
        /// <param name="password">密码</param>
        /// <returns>用户</returns>
        public User Login(string account, string password)
        {
            User result = null;

            UserHibernate hibernate = new UserHibernate();
            User user = hibernate.SelectByAccount(account);
            if (user != null)
            {
                if ((user.Password == password) && (user.Validity))
                {
                    result = user;
                    user.Authentication = true;
                }
            }
            else
            {
                int count = hibernate.Count();
                if (count == 0)
                {
                    result = new User();
                    result.Guid = "Prerogative";
                    result.Prerogative = true;
                    result.Authentication = true;
                }
            }
            if ((result != null) && (result.Authentication))
            {
                this.Refresh(result);
            }

            return result;
        }

        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="value">值</param>
        /// <returns>结果</returns>
        public bool Insert(User value)
        {
            bool result = false;

            UserHibernate hibernate = new UserHibernate();
            result = hibernate.Insert(value);

            return result;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="value">值</param>
        /// <returns>结果</returns>
        public bool Update(User value)
        {
            bool result = false;

            UserHibernate hibernate = new UserHibernate();
            result = hibernate.Update(value);

            return result;
        }

        /// <summary>
        /// 验证页面
        /// </summary>
        /// <param name="user">用户</param>
        /// <param name="pageName">页面类型</param>
        /// <returns>结果</returns>
        public bool VerifyPage(User user, Type pageType)
        {
            bool result = false;

            if (!result)
            {
                if (user.Prerogative)
                {
                    result = user.Prerogative;
                }
            }
            if (!result)
            {
                object page = pageType.Assembly.CreateInstance(pageType.FullName);
                if (page != null)
                {
                    PropertyInfo propertyInfo = pageType.GetProperty("Code");
                    if (propertyInfo != null)
                    {
                        object codeValue = propertyInfo.GetValue(page, null);
                        if (codeValue != null)
                        {
                            string code = codeValue as string;
                            if (code != null)
                            {
                                if (string.Equals(code, UserModule.DEFAULT_MODULE, StringComparison.CurrentCulture))
                                {
                                    result = true;
                                }
                                else
                                {
                                    for (int i = 0; i < user.UserModules.Count; i++)
                                    {
                                        if (string.Equals(code, user.UserModules[i].ModuleCode, StringComparison.CurrentCulture))
                                        {
                                            result = true;
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return result;
        }

        public bool Delete(User value)
        {
            bool result = false;

            UserHibernate hibernate = new UserHibernate();
            result = hibernate.Delete(value);

            return result;
        }

        public bool ChangePassword(string guid, string password)
        {
            bool result = false;

            UserHibernate hibernate = new UserHibernate();
            result = hibernate.ChangePassword(guid, password);

            return result;
        }

        public User QueryByGuid(string guid)
        {
            User result = null;

            UserHibernate hibernate = new UserHibernate();
            result = hibernate.QueryByGuid(guid);

            return result;
        }
    }
}
