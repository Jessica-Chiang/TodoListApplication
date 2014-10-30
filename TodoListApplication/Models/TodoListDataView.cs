using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using TodoListApplication.Service;

namespace TodoListApplication.Models
{
    public class TodoListDataView
    {
        [DisplayName("依需要時間搜尋：")]
        public string Search { get; set; }
        [DisplayName("依完成時間搜尋：")]
        public DateTime SearchDoneTime { get; set; }
        [DisplayName("依建立時間搜尋：")]
        public DateTime SearchCreateTime { get; set; }
        public List<TodoListTable> DataList { get; set; }
        public ForPaging Paging { get; set; }
    }
}