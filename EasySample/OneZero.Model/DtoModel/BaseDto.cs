using System;
using System.Collections.Generic;


namespace OneZero.Model
{
    public class BaseDto:Dto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public BaseDto()
        {
           Datas = new List<DtoData>();
        }

    }
}