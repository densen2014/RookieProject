
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Input;
using TimedTask.Entity;
using TimedTask.Module;

namespace TimedTask.ViewModel
{
    /// <summary>
    /// 
    /// </summary>
    public class NoteViewModel : ViewModelBase
    {
        private Entity.Note _note;
        private Dal.Note _dalNote = new Dal.Note();

        /// <summary>
        /// 构造
        /// </summary>
        public NoteViewModel()
        {
            this._note = new Entity.Note();
            DeleteCommand = new CommandBase(Delete);
            SaveCommand = new CommandBase(Save);
            CloseCommand = new CommandBase(Close);
            LoadCommand = new CommandBase(Close);

            if (!IsInDesignMode)
            {
                List<Entity.Note> list = this._dalNote.GetList(" 1=1 ", null, " CreateDate DESC");//记事列表
                NoteList = new ListCollectionView(list);
            }
            this.NoteList.SortDescriptions.Add(new SortDescription("Note.ModifyDate", ListSortDirection.Descending));
            this.NoteList.CurrentChanged += NoteList_CurrentChanged;
            //this.NoteList.Filter = this.FilterNote;

        }

        #region 命令
        /// <summary>
        /// 加载
        /// </summary>
        public CommandBase LoadCommand { set; get; }
        /// <summary>
        /// 关闭
        /// </summary>
        public CommandBase CloseCommand { set; get; }
        /// <summary>
        /// 删除命令
        /// </summary>
        public CommandBase DeleteCommand { set; get; }
        /// <summary>
        /// 添加/保存命令
        /// </summary>
        public CommandBase SaveCommand { set; get; }

        #endregion

        #region 属性
        /// <summary>
        /// 列表
        /// </summary>
        public ListCollectionView NoteList { get; private set; }
        //private ObservableCollection<Entity.Note> InnerNoteList { get; set; }

        #endregion

        #region 方法

        private void NoteList_CurrentChanged(object sender, EventArgs e)
        {
            //this._note = (Entity.Note)(sender);
            this.NoteList.Refresh();
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="Id"></param>
        private void Save(object param)
        {
            this._note = (Entity.Note)param;

            //_dalNote.Delete(" Id=" + id);
        }
        /// <summary>
        /// 关闭
        /// </summary>
        private void Close()
        {
            //this.SettingsService.SaveSettings(this.UISettings);
        }
        /// <summary>
        /// 加载
        /// </summary>
        private void Load()
        {
            var action = new Action(() =>
            {
                //this.NotebookListViewModel.InitNotebookList();
                //this.NoteListViewModel.InitNoteList();
                //this.InnerNoteList =
                //foreach (var m in list)
                //{
                //    this.InnerNoteList.Add(m);
                //}
            });
            action.BeginInvoke(ar =>
            {
                action.EndInvoke(ar);
            }, null);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="Id"></param>
        private void Delete(object id)
        {
            _dalNote.Delete(" Id=" + id);
        }
        #endregion
    }
}
