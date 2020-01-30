using System;
using System.Collections.Generic;
using System.Linq;
using Intermech.Interfaces;

namespace NavisElectronics.TechPreparation.Interfaces.Services
{
    public class LastVersionService
    {
        public long GetLastOrderVersionId(long objectId)
        {
            using (SessionKeeper keeper = new SessionKeeper())
            {
                IEnumerable<long> list = keeper.Session.GetObjectVersions(objectId);
                long max = int.MinValue;
                foreach (long versioId in list)
                {
                    max = Math.Max(max, Math.Abs(versioId));
                }

                return max;
            }

        }
    }
}