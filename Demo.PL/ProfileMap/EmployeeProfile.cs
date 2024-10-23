﻿using AutoMapper;
using Demo.DAL.Models;
using Demo.PL.ViewModels;

namespace Demo.PL.ProfileMap
{
    public class EmployeeProfile:Profile
    {
        public EmployeeProfile()
        {
            CreateMap <EmployeeViewModel,Employee>().ReverseMap();
        }
    }
}