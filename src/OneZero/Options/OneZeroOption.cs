using AutoMapper.Configuration;
using OneZero.Common.QiniuTool;
using System;
using System.Collections.Generic;
using System.Text;

namespace OneZero
{
    /// <summary>
    /// 框架配置类
    /// </summary>
    public class OneZeroOption
    {


        public DbContextOption DbContextOption { get; set; }
        public JwtOption JwtOption { get; set; }
        public QiniuOption QiniuOption { get; set; }
    }





}
