﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoServiceLibrary
{
    public enum WindowsStruct
    {
        Nothing, Repairs, Auto, AddAutoInRep, ViewAutoInRep, ActOfEndsRepairs, Worker, MalfAdd, MalfView,
        SpareAdd, SpareView, Stock, Client, WorkerAdd, WorkerView, Price, ActiveWayBills, FinishedWayBills,
        AddClientInAuto, PushInStock, PopFromStock, AddClientInWay, AddTripInWay, RepairsReport, WayBillsReport
    }
    public enum AddEditOrDelete { Add, Edit, Delete, Nothing };
}
