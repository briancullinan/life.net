   M S B A M L   `   `   `  ����?  ;YALV, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:YALV.CommonYALV.Common  5      1clr-namespace:YALV.PropertiesYALV.Properties  6-       YALV.Common.MainMenu   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      +localclr-namespace:YALV.Common     5      4
Propertiesclr-namespace:YALV.Properties     6-   
������ $��20��5      $��0,0,0,1q�5         BorderColor#��[  5      .@�5      m�    YALV.Properties.Resources1    *Common_Components_MainMenu_FileMenu_Header#��Z  5   	   .@�5   
   m� :   3Common_Components_MainMenu_FileMenu_OpenFile_Header#��Z 5       m� CommandParameter$
 OPEN��5       m� Command 5      ��  �� Path$ CommandOpenFile�5      m� 5   
   9   2Common_Components_MainMenu_FileMenu_AddFile_Header#��Z 5      $	 ADD��5       5      �� $ CommandOpenFile�5      m� 5   
   >   7Common_Components_MainMenu_FileMenu_SelectFolder_Header#��Z 5       5      �� $ CommandSelectFolder�5      m� 5   
   ?   8Common_Components_MainMenu_FileMenu_AddFolderPath_Header#��Z 5       5       �� $ CommandSaveFolder�5!      �� 5#   
   m� 5%   
   =   6Common_Components_MainMenu_FileMenu_RecentFiles_Header#��Z 5&      .@�5'          YALV.Common.RecentFileList  -   5        	  Name$	 RecentFileListMenu��5(      5)      �� 5+   
   m� 5-   
   6
   /Common_Components_MainMenu_FileMenu_Exit_Header#��Z
 5.       m� InputGestureText$ Alt+F4��50       5/      �� $ CommandExit�51      52      m� 53      1   *Common_Components_MainMenu_ViewMenu_Header#��Z 54   	   .@�55   
   m� D   =Common_Components_MainMenu_ViewMenu_OpenSelectedFolder_Header#��Z 56       57      �� $ CommandOpenSelectedFolder�58      m� 59   
   A   :Common_Components_MainMenu_ViewMenu_RefreshFileList_Header#��Z 5:      $ Ctrl+F5��5<       5;      �� $ CommandRefreshFiles�5=      m� 5>   
   <   5Common_Components_MainMenu_ViewMenu_RefreshLog_Header#��Z 5?      $ F5��5A       5@      �� $ CommandRefresh�5B      m� 5C   
   >   7Common_Components_MainMenu_ViewMenu_ClearFilters_Header#��Z 5D      $ Ctrl+D��5F       5E      �� $ CommandClear�5G      m� 5H   
   <   5Common_Components_MainMenu_ViewMenu_DeleteFile_Header#��Z 5I      $	 DEL��5K       5J      �� $ CommandDelete�5L      5M      m� 5N      2   +Common_Components_MainMenu_AboutMenu_Header#��Z 5O   	    5P   	   �� $ CommandAbout�5Q      5R      