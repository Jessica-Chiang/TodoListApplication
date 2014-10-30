using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TodoListApplication.Service;
using TodoListApplication.Models;

namespace TodoListApplication.Controllers
{
    public class TodoListController : Controller
    {
        TodoListDBService todolistService = new TodoListDBService();
        //
        // GET: /TodoList/

        public ActionResult Index( string Search,int Page = 1 )
        {
            TodoListDataView Data = new TodoListDataView();
            Data.Search = Search;
            Data.Paging = new ForPaging(Page);
            Data.DataList = todolistService.GetDataList(Data.Paging, Data.Search);
            return View(Data);
        }

        [HttpPost]
        public ActionResult Index([Bind(Include = "Search")]TodoListDataView Data)
        {
            return RedirectToAction("Index", new { Search = Data.Search });
        }
        #region 新增項目
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Cancel()
        {
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Create([Bind(Include = "Content,NeedTime,DoneTime")]TodoListTable Data)
        {
             todolistService.InsertTodoListTable(Data);
             return RedirectToAction("Index");
           
        }
        #endregion
        #region 修改項目
        public ActionResult Edit( int Id )
        {
            TodoListTable Data = todolistService.GetDataById(Id);
            return View(Data);
        }

        [HttpPost]
        public ActionResult Edit(int Id, [Bind(Include = "Content,NeedTime,DoneTime")]TodoListTable UpdateData)
        {
            /*if (todolistService.CeckUpdate(Id))
            {*/
                UpdateData.Id = Id;
                todolistService.UpdateTodoListTable(UpdateData);
                return RedirectToAction("Index");
            /*}
            else
            {
                return RedirectToAction("Index");
            }*/
        }
        #endregion
        #region 刪除項目
        public ActionResult Delete(int Id)
        {
            todolistService.DeleteTodoListTable(Id);
            return RedirectToAction("Index");
        }
        #endregion
        #region 排序項目
        public ActionResult Order()
        {
            TodoListDataView Data = new TodoListDataView();
            Data.Paging = new ForPaging(1);
            todolistService.OrderByDoneTime(Data.Paging);
            return RedirectToAction("Index");
        }
        #endregion
    }
}
