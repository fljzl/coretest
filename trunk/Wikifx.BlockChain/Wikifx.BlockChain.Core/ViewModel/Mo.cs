using System;
using System.Collections.Generic;
using System.Text;

namespace Wikifx.BlockChain.Core.ViewModel
{
    public class CategoriesItem
    {
        /// <summary>
        ///
        /// </summary>
        public string cid { get; set; }
        /// <summary>
        ///
        /// </summary>
        public string name { get; set; }
        /// <summary>
        ///
        /// </summary>
        public string icon { get; set; }
        /// <summary>
        ///
        /// </summary>
        public string color { get; set; }
        /// <summary>
        ///
        /// </summary>
        public bool IsCommentClosed { get; set; }
        /// <summary>
        ///
        /// </summary>
        public bool IsFollow { get; set; }
        /// <summary>
        ///
        /// </summary>
        public double ViewCount { get; set; }
    }

    public class ApiBarsItem
    {
        /// <summary>
        ///
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        ///
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        ///
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        ///
        /// </summary>
        public List<CategoriesItem> categories { get; set; }
    }

    public class Result
    {
        /// <summary>
        ///
        /// </summary>
        public List<ApiBarsItem> apiBars { get; set; }
    }

    public class MarketType
    {
        /// <summary>
        ///
        /// </summary>
        public Result result { get; set; }
        /// <summary>
        ///
        /// </summary>
        public bool succeed { get; set; }
        /// <summary>
        ///
        /// </summary>
        public string message { get; set; }
    }
}
