using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EnvironmentalMonitor.Support.Hibernate.Environmental;
using EnvironmentalMonitor.Support.Hibernate;
using EnvironmentalMonitor.Support.Module.Environmental;
using EnvironmentalMonitor.Support.Module.Manage;
using EnvironmentalMonitor.Support.Module;
using EnvironmentalMonitor.Support.Resource;

namespace EnvironmentalMonitor.Support.Business.Environmental
{
    /// <summary>
    /// 探头业务类
    /// </summary>
    public class DetectorBusiness
    {
        public List<Module.Environmental.Detector> QueryByMachine(string machineId)
        {
            List<Detector> results = new List<Detector>();

            DetectorHibernate hibernate = new DetectorHibernate();
            results = hibernate.QueryByMachine(machineId);

            return results;
        }

        public bool Insert(Detector value)
        {
            bool result = false;

            DetectorHibernate hibernate = new DetectorHibernate();
            result = hibernate.Insert(value);

            return result;
        }

        public bool Update(Detector value)
        {
            bool result = false;

            DetectorHibernate hibernate = new DetectorHibernate();
            result = hibernate.Update(value);

            return result;
        }

        public bool Delete(Detector value)
        {
            bool result = false;

            DetectorHibernate hibernate = new DetectorHibernate();
            result = hibernate.Delete(value);

            return result;
        }

        public Detector QueryByGuid(string guid)
        {
            Detector result = null;

            DetectorHibernate hibernate = new DetectorHibernate();
            result = hibernate.QueryByGuid(guid);

            return result;
        }

        public bool UpdatePosition(List<Detector> detectors)
        {
            bool result = false;

            DetectorHibernate hibernate = new DetectorHibernate();
            result = hibernate.UpdatePosition(detectors);

            return result;
        }

        public List<Detector> QueryDataCacheByMachine(string machineId)
        {
            List<Detector> results = new List<Detector>();

            DetectorHibernate hibernate = new DetectorHibernate();
            results = hibernate.QueryDataCacheByMachine(machineId);

            return results;
        }

        public List<Detector> QueryNormalDataCacheByMachine(string machineId, DateTime beginDate, DateTime endDate)
        {
            List<Detector> results = new List<Detector>();

            DetectorHibernate hibernate = new DetectorHibernate();
            results = hibernate.QueryNormalDataCacheByMachine(machineId, beginDate, endDate);

            return results;
        }

        public List<Detector> QueryAlarmDataCacheByMachine(string machineId, DateTime beginDate, DateTime endDate)
        {
            List<Detector> results = new List<Detector>();

            DetectorHibernate hibernate = new DetectorHibernate();
            results = hibernate.QueryAlarmDataCacheByMachine(machineId, beginDate, endDate);

            return results;
        }
    }
}
