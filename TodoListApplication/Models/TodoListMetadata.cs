using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TodoListApplication.Models
{
    [MetadataType(typeof(TodoListMetadata))]
    public partial class TodoListTable
    {
        private class TodoListMetadata
        {
            [DisplayName("編號：")]
            public int Id { get; set; }

            [DisplayName("工作內容：")]
            [Required(ErrorMessage = "請輸入工作內容")]
            [StringLength(500, ErrorMessage = "內容不可超過500字元")]
            public string Content { get; set; }

            [DisplayName("工作所需時間：")]
            [Required(ErrorMessage = "請輸入工作需要的時間")]
            [StringLength(10, ErrorMessage = "內容不可超過10字元")]
            public string NeedTime { get; set; }

            [DisplayName("預計完成時間：")]
            [Required(ErrorMessage = "請輸入預計完成時間")]
            [DataType(DataType.Date, ErrorMessage = "欄位必須是日期")]
            public DateTime DoneTime { get; set; }

            [DisplayName("項目新增時間：")]
            public DateTime CreateTime { get; set; }
        }
    }
}