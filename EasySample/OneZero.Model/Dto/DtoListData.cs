using System;
using System.Collections.Generic;

namespace OneZero.Model
{
    public class DtoListData:DtoData
    {
         /// <summary>
        /// 对象数据集合
        /// </summary>
        public IEnumerable<IEnumerable<DtoData>> DataList { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public DtoListData()
        {
            DataList = new List<List<DtoData>>();
        }

    }
}