using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TodoListApplication.Models;

namespace TodoListApplication.Service
{
    public class TodoListDBService
    {
        TodoListDataEntities db = new TodoListDataEntities();

        public List<TodoListTable> GetDataList()
        {
            return db.TodoListTable.ToList();
        }
        #region 新增資料
        public void InsertTodoListTable(TodoListTable newData)
        {
            newData.CreateTime = DateTime.Now;
            db.TodoListTable.Add(newData);
            db.SaveChanges();
        }
        #endregion
        #region 查詢一筆資料
        public TodoListTable GetDataById(int Id)
        {
            return db.TodoListTable.Find(Id);
        }
        #endregion
        #region 修改項目
        public void UpdateTodoListTable(TodoListTable UpdateData)
        {
            TodoListTable OldData = db.TodoListTable.Find(UpdateData.Id);
            OldData.Content = UpdateData.Content;
            OldData.NeedTime = UpdateData.NeedTime;
            OldData.DoneTime = UpdateData.DoneTime;
            db.SaveChanges();
        }
        #endregion
        #region 刪除項目
        public void DeleteTodoListTable(int Id)
        {
            TodoListTable DeleteData = db.TodoListTable.Find(Id);
            db.TodoListTable.Remove(DeleteData);
            db.SaveChanges();
        }
        #endregion
        #region 查詢項目
        public List<TodoListTable> GetDataList(ForPaging Paging, string SearchNeedTime)
        {
            IQueryable<TodoListTable> SearchData;
            if (String.IsNullOrEmpty(SearchNeedTime))
            {
                SearchData = GetAllDataList(Paging);
            }
            else
            {
                SearchData = GetAllDataList(Paging, SearchNeedTime);
            }
            return SearchData.OrderBy(p=>p.CreateTime).Skip((Paging.NowPage - 1) * Paging.ItemNum).Take(Paging.ItemNum).ToList();
        }

        public IQueryable<TodoListTable> GetAllDataList(ForPaging Paging)
        {
            IQueryable<TodoListTable> Data = db.TodoListTable;

            Paging.MaxPage = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(Data.Count()) / Paging.ItemNum));
            Paging.SetRightPage();
            return Data;
        }

        public IQueryable<TodoListTable> GetAllDataList(ForPaging Paging, string Search)
        {
            IQueryable<TodoListTable> Data = db.TodoListTable.Where(p => p.NeedTime.Contains(Search));

            Paging.MaxPage = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(Data.Count()) / Paging.ItemNum));
            Paging.SetRightPage();
            return Data;
        }

        public List<TodoListTable> GetDataListDT(DateTime SearchDoneTime)
        {
            IQueryable<TodoListTable> SearchData;
            if (String.IsNullOrEmpty(SearchDoneTime.ToString()))
            {
                SearchData = db.TodoListTable;
            }
            else
            {
                SearchData = db.TodoListTable.Where(p => p.DoneTime.Equals(SearchDoneTime));
            }
            return SearchData.ToList();
        }

        public List<TodoListTable> GetDataOrder(DateTime SearchCreateTime)
        {
            IQueryable<TodoListTable> SearchData;
            if (String.IsNullOrEmpty(SearchCreateTime.ToString()))
            {
                SearchData = db.TodoListTable;
            }
            else
            {
                SearchData = db.TodoListTable.Where(p => p.DoneTime.Equals(SearchCreateTime));
            }
            return SearchData.ToList();
        }
        #endregion
        #region 排序項目
        public void OrderByDoneTime(ForPaging Paging)
        {
            db.TodoListTable.OrderBy(p => p.DoneTime);
            db.SaveChanges();
        }
        #endregion
    }
}