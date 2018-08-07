using System;

//Nhibernate Code Generation Template 1.0
//author:MythXin
//blog:www.cnblogs.com/MythXin
//Entity Code Generation Template
namespace NFine.Domain.Entity.Function
{
	 	//ERPNForm
    public class ERPNFormEntity : IEntity<ERPNFormEntity>, ICreationAudited, IDeleteAudited, IModificationAudited
	{
	
      	/// <summary>
		/// ID
        /// </summary>
        public virtual string F_Id
        {
            get; 
            set; 
        }        
		/// <summary>
		/// 表单名称
        /// </summary>
        public virtual string FormName
        {
            get; 
            set; 
        }        
		/// <summary>
		/// 所属分类ID
        /// </summary>
        public virtual int? TypeID
        {
            get; 
            set; 
        }        
		/// <summary>
		/// 允许使用人
        /// </summary>
        public virtual string UserListOK
        {
            get; 
            set; 
        }        
		/// <summary>
		/// 允许使用部门
        /// </summary>
        public virtual string DepListOK
        {
            get; 
            set; 
        }        
		/// <summary>
		/// 允许使用角色
        /// </summary>
        public virtual string JiaoSeListOK
        {
            get; 
            set; 
        }        
		/// <summary>
		/// 排序字符
        /// </summary>
        public virtual string PaiXuStr
        {
            get; 
            set; 
        }        
		/// <summary>
		/// 录入人
        /// </summary>
        public virtual string UserName
        {
            get; 
            set; 
        }        
		/// <summary>
		/// 录入时间
        /// </summary>
        public virtual DateTime? TimeStr
        {
            get; 
            set; 
        }        
		/// <summary>
		/// 表单内容
        /// </summary>
        public virtual string ContentStr
        {
            get; 
            set; 
        }        
		/// <summary>
		/// 表单中数据列
        /// </summary>
        public virtual string ItemsList
        {
            get; 
            set; 
        }        
		/// <summary>
		/// 是否启用
        /// </summary>
        public virtual string IFOK
        {
            get; 
            set; 
        }        
		/// <summary>
		/// FormDataName
        /// </summary>
        public virtual string FormDataName
        {
            get; 
            set; 
        }        
		/// <summary>
		/// ItemList
        /// </summary>
        public virtual string ItemList
        {
            get; 
            set; 
        }        
		/// <summary>
		/// 流程id
        /// </summary>
        public virtual string LCID
        {
            get; 
            set; 
        }        
		/// <summary>
		/// 流程名称
        /// </summary>
        public virtual string LCName
        {
            get; 
            set; 
        }        
		/// <summary>
		/// F_SortCode
        /// </summary>
        public virtual int? F_SortCode
        {
            get; 
            set; 
        }        
		/// <summary>
		/// F_DeleteMark
        /// </summary>
        public virtual bool? F_DeleteMark
        {
            get; 
            set; 
        }        
		/// <summary>
		/// F_EnabledMark
        /// </summary>
        public virtual bool? F_EnabledMark
        {
            get; 
            set; 
        }        
		/// <summary>
		/// F_Description
        /// </summary>
        public virtual string F_Description
        {
            get; 
            set; 
        }        
		/// <summary>
		/// F_CreatorTime
        /// </summary>
        public virtual DateTime? F_CreatorTime
        {
            get; 
            set; 
        }        
		/// <summary>
		/// F_CreatorUserId
        /// </summary>
        public virtual string F_CreatorUserId
        {
            get; 
            set; 
        }        
		/// <summary>
		/// F_LastModifyTime
        /// </summary>
        public virtual DateTime? F_LastModifyTime
        {
            get; 
            set; 
        }        
		/// <summary>
		/// F_LastModifyUserId
        /// </summary>
        public virtual string F_LastModifyUserId
        {
            get; 
            set; 
        }        
		/// <summary>
		/// F_DeleteTime
        /// </summary>
        public virtual DateTime? F_DeleteTime
        {
            get; 
            set; 
        }        
		/// <summary>
		/// F_DeleteUserId
        /// </summary>
        public virtual string F_DeleteUserId
        {
            get; 
            set; 
        }        
		   
	}
}