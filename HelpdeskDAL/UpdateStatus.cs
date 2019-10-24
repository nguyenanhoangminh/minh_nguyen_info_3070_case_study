//Author Minh Nguyen
//description: enum for status
using System;
using System.Collections.Generic;
using System.Text;

namespace HelpdeskDAL
{
    public enum UpdateStatus
    {
        Ok = 1,
        Failed = -1,
        Stale = -2
    };
}
