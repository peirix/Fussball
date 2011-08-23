﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.235
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Fussball.Models
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
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="Fussball")]
	public partial class FussballDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertGame(Game instance);
    partial void UpdateGame(Game instance);
    partial void DeleteGame(Game instance);
    partial void InsertPlayer(Player instance);
    partial void UpdatePlayer(Player instance);
    partial void DeletePlayer(Player instance);
    partial void InsertGoal(Goal instance);
    partial void UpdateGoal(Goal instance);
    partial void DeleteGoal(Goal instance);
    #endregion
		
		public FussballDataContext() : 
				base(global::System.Configuration.ConfigurationManager.ConnectionStrings["FussballConnectionString1"].ConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public FussballDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public FussballDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public FussballDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public FussballDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<Game> Games
		{
			get
			{
				return this.GetTable<Game>();
			}
		}
		
		public System.Data.Linq.Table<Player> Players
		{
			get
			{
				return this.GetTable<Player>();
			}
		}
		
		public System.Data.Linq.Table<Goal> Goals
		{
			get
			{
				return this.GetTable<Goal>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Game")]
	public partial class Game : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _ID;
		
		private System.DateTime _DateStart;
		
		private System.DateTime _DateEnd;
		
		private int _Blue1;
		
		private int _Blue2;
		
		private int _Red1;
		
		private int _Red2;
		
		private int _WinningTeam;
		
		private bool _IsTest;
		
		private EntitySet<Goal> _Goals;
		
		private EntityRef<Player> _Player;
		
		private EntityRef<Player> _Player1;
		
		private EntityRef<Player> _Player2;
		
		private EntityRef<Player> _Player3;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIDChanging(int value);
    partial void OnIDChanged();
    partial void OnDateStartChanging(System.DateTime value);
    partial void OnDateStartChanged();
    partial void OnDateEndChanging(System.DateTime value);
    partial void OnDateEndChanged();
    partial void OnBlue1Changing(int value);
    partial void OnBlue1Changed();
    partial void OnBlue2Changing(int value);
    partial void OnBlue2Changed();
    partial void OnRed1Changing(int value);
    partial void OnRed1Changed();
    partial void OnRed2Changing(int value);
    partial void OnRed2Changed();
    partial void OnWinningTeamChanging(int value);
    partial void OnWinningTeamChanged();
    partial void OnIsTestChanging(bool value);
    partial void OnIsTestChanged();
    #endregion
		
		public Game()
		{
			this._Goals = new EntitySet<Goal>(new Action<Goal>(this.attach_Goals), new Action<Goal>(this.detach_Goals));
			this._Player = default(EntityRef<Player>);
			this._Player1 = default(EntityRef<Player>);
			this._Player2 = default(EntityRef<Player>);
			this._Player3 = default(EntityRef<Player>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int ID
		{
			get
			{
				return this._ID;
			}
			set
			{
				if ((this._ID != value))
				{
					this.OnIDChanging(value);
					this.SendPropertyChanging();
					this._ID = value;
					this.SendPropertyChanged("ID");
					this.OnIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DateStart", DbType="DateTime NOT NULL")]
		public System.DateTime DateStart
		{
			get
			{
				return this._DateStart;
			}
			set
			{
				if ((this._DateStart != value))
				{
					this.OnDateStartChanging(value);
					this.SendPropertyChanging();
					this._DateStart = value;
					this.SendPropertyChanged("DateStart");
					this.OnDateStartChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DateEnd", DbType="DateTime NOT NULL")]
		public System.DateTime DateEnd
		{
			get
			{
				return this._DateEnd;
			}
			set
			{
				if ((this._DateEnd != value))
				{
					this.OnDateEndChanging(value);
					this.SendPropertyChanging();
					this._DateEnd = value;
					this.SendPropertyChanged("DateEnd");
					this.OnDateEndChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Blue1", DbType="Int NOT NULL")]
		public int Blue1
		{
			get
			{
				return this._Blue1;
			}
			set
			{
				if ((this._Blue1 != value))
				{
					if (this._Player.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnBlue1Changing(value);
					this.SendPropertyChanging();
					this._Blue1 = value;
					this.SendPropertyChanged("Blue1");
					this.OnBlue1Changed();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Blue2", DbType="Int NOT NULL")]
		public int Blue2
		{
			get
			{
				return this._Blue2;
			}
			set
			{
				if ((this._Blue2 != value))
				{
					if (this._Player1.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnBlue2Changing(value);
					this.SendPropertyChanging();
					this._Blue2 = value;
					this.SendPropertyChanged("Blue2");
					this.OnBlue2Changed();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Red1", DbType="Int NOT NULL")]
		public int Red1
		{
			get
			{
				return this._Red1;
			}
			set
			{
				if ((this._Red1 != value))
				{
					if (this._Player2.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnRed1Changing(value);
					this.SendPropertyChanging();
					this._Red1 = value;
					this.SendPropertyChanged("Red1");
					this.OnRed1Changed();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Red2", DbType="Int NOT NULL")]
		public int Red2
		{
			get
			{
				return this._Red2;
			}
			set
			{
				if ((this._Red2 != value))
				{
					if (this._Player3.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnRed2Changing(value);
					this.SendPropertyChanging();
					this._Red2 = value;
					this.SendPropertyChanged("Red2");
					this.OnRed2Changed();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_WinningTeam", DbType="Int NOT NULL")]
		public int WinningTeam
		{
			get
			{
				return this._WinningTeam;
			}
			set
			{
				if ((this._WinningTeam != value))
				{
					this.OnWinningTeamChanging(value);
					this.SendPropertyChanging();
					this._WinningTeam = value;
					this.SendPropertyChanged("WinningTeam");
					this.OnWinningTeamChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IsTest", DbType="Bit NOT NULL")]
		public bool IsTest
		{
			get
			{
				return this._IsTest;
			}
			set
			{
				if ((this._IsTest != value))
				{
					this.OnIsTestChanging(value);
					this.SendPropertyChanging();
					this._IsTest = value;
					this.SendPropertyChanged("IsTest");
					this.OnIsTestChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Game_Goal", Storage="_Goals", ThisKey="ID", OtherKey="GameID")]
		public EntitySet<Goal> Goals
		{
			get
			{
				return this._Goals;
			}
			set
			{
				this._Goals.Assign(value);
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Player_Game", Storage="_Player", ThisKey="Blue1", OtherKey="ID", IsForeignKey=true, DeleteOnNull=true, DeleteRule="CASCADE")]
		public Player Player
		{
			get
			{
				return this._Player.Entity;
			}
			set
			{
				Player previousValue = this._Player.Entity;
				if (((previousValue != value) 
							|| (this._Player.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Player.Entity = null;
						previousValue.Games.Remove(this);
					}
					this._Player.Entity = value;
					if ((value != null))
					{
						value.Games.Add(this);
						this._Blue1 = value.ID;
					}
					else
					{
						this._Blue1 = default(int);
					}
					this.SendPropertyChanged("Player");
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Player_Game1", Storage="_Player1", ThisKey="Blue2", OtherKey="ID", IsForeignKey=true)]
		public Player Player1
		{
			get
			{
				return this._Player1.Entity;
			}
			set
			{
				Player previousValue = this._Player1.Entity;
				if (((previousValue != value) 
							|| (this._Player1.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Player1.Entity = null;
						previousValue.Games1.Remove(this);
					}
					this._Player1.Entity = value;
					if ((value != null))
					{
						value.Games1.Add(this);
						this._Blue2 = value.ID;
					}
					else
					{
						this._Blue2 = default(int);
					}
					this.SendPropertyChanged("Player1");
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Player_Game2", Storage="_Player2", ThisKey="Red1", OtherKey="ID", IsForeignKey=true)]
		public Player Player2
		{
			get
			{
				return this._Player2.Entity;
			}
			set
			{
				Player previousValue = this._Player2.Entity;
				if (((previousValue != value) 
							|| (this._Player2.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Player2.Entity = null;
						previousValue.Games2.Remove(this);
					}
					this._Player2.Entity = value;
					if ((value != null))
					{
						value.Games2.Add(this);
						this._Red1 = value.ID;
					}
					else
					{
						this._Red1 = default(int);
					}
					this.SendPropertyChanged("Player2");
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Player_Game3", Storage="_Player3", ThisKey="Red2", OtherKey="ID", IsForeignKey=true)]
		public Player Player3
		{
			get
			{
				return this._Player3.Entity;
			}
			set
			{
				Player previousValue = this._Player3.Entity;
				if (((previousValue != value) 
							|| (this._Player3.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Player3.Entity = null;
						previousValue.Games3.Remove(this);
					}
					this._Player3.Entity = value;
					if ((value != null))
					{
						value.Games3.Add(this);
						this._Red2 = value.ID;
					}
					else
					{
						this._Red2 = default(int);
					}
					this.SendPropertyChanged("Player3");
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
		
		private void attach_Goals(Goal entity)
		{
			this.SendPropertyChanging();
			entity.Game = this;
		}
		
		private void detach_Goals(Goal entity)
		{
			this.SendPropertyChanging();
			entity.Game = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Player")]
	public partial class Player : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _ID;
		
		private string _Name;
		
		private string _Image_Small;
		
		private string _Image_Large;
		
		private string _Image_Square;
		
		private EntitySet<Game> _Games;
		
		private EntitySet<Game> _Games1;
		
		private EntitySet<Game> _Games2;
		
		private EntitySet<Game> _Games3;
		
		private EntitySet<Goal> _Goals;
		
		private EntitySet<Goal> _Goals1;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIDChanging(int value);
    partial void OnIDChanged();
    partial void OnNameChanging(string value);
    partial void OnNameChanged();
    partial void OnImage_SmallChanging(string value);
    partial void OnImage_SmallChanged();
    partial void OnImage_LargeChanging(string value);
    partial void OnImage_LargeChanged();
    partial void OnImage_SquareChanging(string value);
    partial void OnImage_SquareChanged();
    #endregion
		
		public Player()
		{
			this._Games = new EntitySet<Game>(new Action<Game>(this.attach_Games), new Action<Game>(this.detach_Games));
			this._Games1 = new EntitySet<Game>(new Action<Game>(this.attach_Games1), new Action<Game>(this.detach_Games1));
			this._Games2 = new EntitySet<Game>(new Action<Game>(this.attach_Games2), new Action<Game>(this.detach_Games2));
			this._Games3 = new EntitySet<Game>(new Action<Game>(this.attach_Games3), new Action<Game>(this.detach_Games3));
			this._Goals = new EntitySet<Goal>(new Action<Goal>(this.attach_Goals), new Action<Goal>(this.detach_Goals));
			this._Goals1 = new EntitySet<Goal>(new Action<Goal>(this.attach_Goals1), new Action<Goal>(this.detach_Goals1));
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int ID
		{
			get
			{
				return this._ID;
			}
			set
			{
				if ((this._ID != value))
				{
					this.OnIDChanging(value);
					this.SendPropertyChanging();
					this._ID = value;
					this.SendPropertyChanged("ID");
					this.OnIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Name", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				if ((this._Name != value))
				{
					this.OnNameChanging(value);
					this.SendPropertyChanging();
					this._Name = value;
					this.SendPropertyChanged("Name");
					this.OnNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Image_Small", DbType="VarChar(255)")]
		public string Image_Small
		{
			get
			{
				return this._Image_Small;
			}
			set
			{
				if ((this._Image_Small != value))
				{
					this.OnImage_SmallChanging(value);
					this.SendPropertyChanging();
					this._Image_Small = value;
					this.SendPropertyChanged("Image_Small");
					this.OnImage_SmallChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Image_Large", DbType="VarChar(255)")]
		public string Image_Large
		{
			get
			{
				return this._Image_Large;
			}
			set
			{
				if ((this._Image_Large != value))
				{
					this.OnImage_LargeChanging(value);
					this.SendPropertyChanging();
					this._Image_Large = value;
					this.SendPropertyChanged("Image_Large");
					this.OnImage_LargeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Image_Square", DbType="VarChar(255)")]
		public string Image_Square
		{
			get
			{
				return this._Image_Square;
			}
			set
			{
				if ((this._Image_Square != value))
				{
					this.OnImage_SquareChanging(value);
					this.SendPropertyChanging();
					this._Image_Square = value;
					this.SendPropertyChanged("Image_Square");
					this.OnImage_SquareChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Player_Game", Storage="_Games", ThisKey="ID", OtherKey="Blue1")]
		public EntitySet<Game> Games
		{
			get
			{
				return this._Games;
			}
			set
			{
				this._Games.Assign(value);
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Player_Game1", Storage="_Games1", ThisKey="ID", OtherKey="Blue2")]
		public EntitySet<Game> Games1
		{
			get
			{
				return this._Games1;
			}
			set
			{
				this._Games1.Assign(value);
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Player_Game2", Storage="_Games2", ThisKey="ID", OtherKey="Red1")]
		public EntitySet<Game> Games2
		{
			get
			{
				return this._Games2;
			}
			set
			{
				this._Games2.Assign(value);
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Player_Game3", Storage="_Games3", ThisKey="ID", OtherKey="Red2")]
		public EntitySet<Game> Games3
		{
			get
			{
				return this._Games3;
			}
			set
			{
				this._Games3.Assign(value);
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Player_Goal", Storage="_Goals", ThisKey="ID", OtherKey="PlayerID")]
		public EntitySet<Goal> Goals
		{
			get
			{
				return this._Goals;
			}
			set
			{
				this._Goals.Assign(value);
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Player_Goal1", Storage="_Goals1", ThisKey="ID", OtherKey="OppDefenseID")]
		public EntitySet<Goal> Goals1
		{
			get
			{
				return this._Goals1;
			}
			set
			{
				this._Goals1.Assign(value);
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
		
		private void attach_Games(Game entity)
		{
			this.SendPropertyChanging();
			entity.Player = this;
		}
		
		private void detach_Games(Game entity)
		{
			this.SendPropertyChanging();
			entity.Player = null;
		}
		
		private void attach_Games1(Game entity)
		{
			this.SendPropertyChanging();
			entity.Player1 = this;
		}
		
		private void detach_Games1(Game entity)
		{
			this.SendPropertyChanging();
			entity.Player1 = null;
		}
		
		private void attach_Games2(Game entity)
		{
			this.SendPropertyChanging();
			entity.Player2 = this;
		}
		
		private void detach_Games2(Game entity)
		{
			this.SendPropertyChanging();
			entity.Player2 = null;
		}
		
		private void attach_Games3(Game entity)
		{
			this.SendPropertyChanging();
			entity.Player3 = this;
		}
		
		private void detach_Games3(Game entity)
		{
			this.SendPropertyChanging();
			entity.Player3 = null;
		}
		
		private void attach_Goals(Goal entity)
		{
			this.SendPropertyChanging();
			entity.Player = this;
		}
		
		private void detach_Goals(Goal entity)
		{
			this.SendPropertyChanging();
			entity.Player = null;
		}
		
		private void attach_Goals1(Goal entity)
		{
			this.SendPropertyChanging();
			entity.Player1 = this;
		}
		
		private void detach_Goals1(Goal entity)
		{
			this.SendPropertyChanging();
			entity.Player1 = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Goal")]
	public partial class Goal : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _ID;
		
		private int _PlayerID;
		
		private System.DateTime _GoalDate;
		
		private int _Position;
		
		private int _Team;
		
		private int _GameID;
		
		private int _OppDefenseID;
		
		private int _SelfGoal;
		
		private EntityRef<Game> _Game;
		
		private EntityRef<Player> _Player;
		
		private EntityRef<Player> _Player1;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIDChanging(int value);
    partial void OnIDChanged();
    partial void OnPlayerIDChanging(int value);
    partial void OnPlayerIDChanged();
    partial void OnGoalDateChanging(System.DateTime value);
    partial void OnGoalDateChanged();
    partial void OnPositionChanging(int value);
    partial void OnPositionChanged();
    partial void OnTeamChanging(int value);
    partial void OnTeamChanged();
    partial void OnGameIDChanging(int value);
    partial void OnGameIDChanged();
    partial void OnOppDefenseIDChanging(int value);
    partial void OnOppDefenseIDChanged();
    partial void OnSelfGoalChanging(int value);
    partial void OnSelfGoalChanged();
    #endregion
		
		public Goal()
		{
			this._Game = default(EntityRef<Game>);
			this._Player = default(EntityRef<Player>);
			this._Player1 = default(EntityRef<Player>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int ID
		{
			get
			{
				return this._ID;
			}
			set
			{
				if ((this._ID != value))
				{
					this.OnIDChanging(value);
					this.SendPropertyChanging();
					this._ID = value;
					this.SendPropertyChanged("ID");
					this.OnIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PlayerID", DbType="Int NOT NULL")]
		public int PlayerID
		{
			get
			{
				return this._PlayerID;
			}
			set
			{
				if ((this._PlayerID != value))
				{
					if (this._Player.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnPlayerIDChanging(value);
					this.SendPropertyChanging();
					this._PlayerID = value;
					this.SendPropertyChanged("PlayerID");
					this.OnPlayerIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_GoalDate", DbType="DateTime NOT NULL")]
		public System.DateTime GoalDate
		{
			get
			{
				return this._GoalDate;
			}
			set
			{
				if ((this._GoalDate != value))
				{
					this.OnGoalDateChanging(value);
					this.SendPropertyChanging();
					this._GoalDate = value;
					this.SendPropertyChanged("GoalDate");
					this.OnGoalDateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Position", DbType="Int NOT NULL")]
		public int Position
		{
			get
			{
				return this._Position;
			}
			set
			{
				if ((this._Position != value))
				{
					this.OnPositionChanging(value);
					this.SendPropertyChanging();
					this._Position = value;
					this.SendPropertyChanged("Position");
					this.OnPositionChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Team", DbType="Int NOT NULL")]
		public int Team
		{
			get
			{
				return this._Team;
			}
			set
			{
				if ((this._Team != value))
				{
					this.OnTeamChanging(value);
					this.SendPropertyChanging();
					this._Team = value;
					this.SendPropertyChanged("Team");
					this.OnTeamChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_GameID", DbType="Int NOT NULL")]
		public int GameID
		{
			get
			{
				return this._GameID;
			}
			set
			{
				if ((this._GameID != value))
				{
					if (this._Game.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnGameIDChanging(value);
					this.SendPropertyChanging();
					this._GameID = value;
					this.SendPropertyChanged("GameID");
					this.OnGameIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_OppDefenseID", DbType="Int NOT NULL")]
		public int OppDefenseID
		{
			get
			{
				return this._OppDefenseID;
			}
			set
			{
				if ((this._OppDefenseID != value))
				{
					if (this._Player1.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnOppDefenseIDChanging(value);
					this.SendPropertyChanging();
					this._OppDefenseID = value;
					this.SendPropertyChanged("OppDefenseID");
					this.OnOppDefenseIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_SelfGoal", DbType="Int NOT NULL")]
		public int SelfGoal
		{
			get
			{
				return this._SelfGoal;
			}
			set
			{
				if ((this._SelfGoal != value))
				{
					this.OnSelfGoalChanging(value);
					this.SendPropertyChanging();
					this._SelfGoal = value;
					this.SendPropertyChanged("SelfGoal");
					this.OnSelfGoalChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Game_Goal", Storage="_Game", ThisKey="GameID", OtherKey="ID", IsForeignKey=true, DeleteOnNull=true, DeleteRule="CASCADE")]
		public Game Game
		{
			get
			{
				return this._Game.Entity;
			}
			set
			{
				Game previousValue = this._Game.Entity;
				if (((previousValue != value) 
							|| (this._Game.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Game.Entity = null;
						previousValue.Goals.Remove(this);
					}
					this._Game.Entity = value;
					if ((value != null))
					{
						value.Goals.Add(this);
						this._GameID = value.ID;
					}
					else
					{
						this._GameID = default(int);
					}
					this.SendPropertyChanged("Game");
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Player_Goal", Storage="_Player", ThisKey="PlayerID", OtherKey="ID", IsForeignKey=true)]
		public Player Player
		{
			get
			{
				return this._Player.Entity;
			}
			set
			{
				Player previousValue = this._Player.Entity;
				if (((previousValue != value) 
							|| (this._Player.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Player.Entity = null;
						previousValue.Goals.Remove(this);
					}
					this._Player.Entity = value;
					if ((value != null))
					{
						value.Goals.Add(this);
						this._PlayerID = value.ID;
					}
					else
					{
						this._PlayerID = default(int);
					}
					this.SendPropertyChanged("Player");
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Player_Goal1", Storage="_Player1", ThisKey="OppDefenseID", OtherKey="ID", IsForeignKey=true)]
		public Player Player1
		{
			get
			{
				return this._Player1.Entity;
			}
			set
			{
				Player previousValue = this._Player1.Entity;
				if (((previousValue != value) 
							|| (this._Player1.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Player1.Entity = null;
						previousValue.Goals1.Remove(this);
					}
					this._Player1.Entity = value;
					if ((value != null))
					{
						value.Goals1.Add(this);
						this._OppDefenseID = value.ID;
					}
					else
					{
						this._OppDefenseID = default(int);
					}
					this.SendPropertyChanged("Player1");
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
