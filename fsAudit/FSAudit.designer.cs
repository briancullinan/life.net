﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18010
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace fsAudit
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="Life")]
	public partial class FSAuditDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertFilesystem(Filesystem instance);
    partial void UpdateFilesystem(Filesystem instance);
    partial void DeleteFilesystem(Filesystem instance);
    #endregion
		
		public FSAuditDataContext() : 
				base(global::fsAudit.Properties.Settings.Default.LifeConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public FSAuditDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public FSAuditDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public FSAuditDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public FSAuditDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<Filesystem> Filesystems
		{
			get
			{
				return this.GetTable<Filesystem>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Filesystem")]
	public partial class Filesystem : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private decimal _Id;
		
		private string _Type;
		
		private string _Filepath;
		
		private System.DateTime _Time;
		
		private decimal _EventId;
		
		private string _Username;
		
		private decimal _AccessMask;
		
		private bool _Success;
		
		private string _Computer;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(decimal value);
    partial void OnIdChanged();
    partial void OnTypeChanging(string value);
    partial void OnTypeChanged();
    partial void OnFilepathChanging(string value);
    partial void OnFilepathChanged();
    partial void OnTimeChanging(System.DateTime value);
    partial void OnTimeChanged();
    partial void OnEventIdChanging(decimal value);
    partial void OnEventIdChanged();
    partial void OnUsernameChanging(string value);
    partial void OnUsernameChanged();
    partial void OnAccessMaskChanging(decimal value);
    partial void OnAccessMaskChanged();
    partial void OnSuccessChanging(bool value);
    partial void OnSuccessChanged();
    partial void OnComputerChanging(string value);
    partial void OnComputerChanged();
    #endregion
		
		public Filesystem()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", AutoSync=AutoSync.OnInsert, DbType="Decimal(18,0) NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public decimal Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._Id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Type", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string Type
		{
			get
			{
				return this._Type;
			}
			set
			{
				if ((this._Type != value))
				{
					this.OnTypeChanging(value);
					this.SendPropertyChanging();
					this._Type = value;
					this.SendPropertyChanged("Type");
					this.OnTypeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Filepath", DbType="VarChar(2048) NOT NULL", CanBeNull=false)]
		public string Filepath
		{
			get
			{
				return this._Filepath;
			}
			set
			{
				if ((this._Filepath != value))
				{
					this.OnFilepathChanging(value);
					this.SendPropertyChanging();
					this._Filepath = value;
					this.SendPropertyChanged("Filepath");
					this.OnFilepathChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Time", DbType="DateTime NOT NULL")]
		public System.DateTime Time
		{
			get
			{
				return this._Time;
			}
			set
			{
				if ((this._Time != value))
				{
					this.OnTimeChanging(value);
					this.SendPropertyChanging();
					this._Time = value;
					this.SendPropertyChanged("Time");
					this.OnTimeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_EventId", DbType="Decimal(18,0) NOT NULL")]
		public decimal EventId
		{
			get
			{
				return this._EventId;
			}
			set
			{
				if ((this._EventId != value))
				{
					this.OnEventIdChanging(value);
					this.SendPropertyChanging();
					this._EventId = value;
					this.SendPropertyChanged("EventId");
					this.OnEventIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Username", DbType="VarChar(128) NOT NULL", CanBeNull=false)]
		public string Username
		{
			get
			{
				return this._Username;
			}
			set
			{
				if ((this._Username != value))
				{
					this.OnUsernameChanging(value);
					this.SendPropertyChanging();
					this._Username = value;
					this.SendPropertyChanged("Username");
					this.OnUsernameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_AccessMask", DbType="Decimal(18,0) NOT NULL")]
		public decimal AccessMask
		{
			get
			{
				return this._AccessMask;
			}
			set
			{
				if ((this._AccessMask != value))
				{
					this.OnAccessMaskChanging(value);
					this.SendPropertyChanging();
					this._AccessMask = value;
					this.SendPropertyChanged("AccessMask");
					this.OnAccessMaskChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Success", DbType="Bit NOT NULL")]
		public bool Success
		{
			get
			{
				return this._Success;
			}
			set
			{
				if ((this._Success != value))
				{
					this.OnSuccessChanging(value);
					this.SendPropertyChanging();
					this._Success = value;
					this.SendPropertyChanged("Success");
					this.OnSuccessChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Computer", DbType="VarChar(64) NOT NULL", CanBeNull=false)]
		public string Computer
		{
			get
			{
				return this._Computer;
			}
			set
			{
				if ((this._Computer != value))
				{
					this.OnComputerChanging(value);
					this.SendPropertyChanging();
					this._Computer = value;
					this.SendPropertyChanged("Computer");
					this.OnComputerChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
#pragma warning restore 1591
