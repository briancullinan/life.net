   M S B A M L   `   `   `  ����?  ;Life, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:Life.TriggersLife.Triggers  5      -clr-namespace:Life.ControlsLife.Controls  64       Life.Controls.Messages   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      0triggersclr-namespace:Life.Triggers     5      0controlsclr-namespace:Life.Controls     64     � DataContext  5      ��  �� RelativeSource �� *Self+ � ColumnDefinitions 5
      �� 5   
   $	��200 �6   �� 5   
   .+�5      �� -   5         ��Name$
 From��5      $��0q�5      $��Hidden=�6   ��5      ��  �� Path$$ (triggers:Application.Results)� �� ElementName$
 From����5   
   �� 5       �� 
TargetType" ��6   .�5      ��  �� Property � �� �� Value$ Stretch=�6?   �� 5       � �� 5      �� 5      $ ActualWidth�6"    5      �� 	 �� Mode$	 FindAncestor=�
 �� AncestorType"
 ��5      5       � 	Resources 5   
    �� DataType   Life.Controls.FromResult��%^   (��       5      *, +)�� 6   "  6   .R�5      �� ��6   �� *From+5      5      5       �� 5!       �� ShowsPreview . 6   &   System.Windows.Controls.DataGrid  5"      -   5          Name$ Results��5"      $��1��6    !   Life.Controls.MessageResult"   60      AutoGenerateColumns .     
IsReadOnly .    CanUserResizeRows .     GridLinesVisibility$
 None=�5%      ��5$      �� $$ (triggers:Application.Results)�$ Results��   	CellStyle 5&   
   �� 5'      *  $System.Windows.Controls.DataGridCell"  6   .�5(      ��  � ��$ 0q�64   5)         RowStyle 5+   
   �� 5,      )  #System.Windows.Controls.DataGridRow"  6   .�5-      ��  � ��$ 0q�64   5.         Columns 50   
   4  .System.Windows.Controls.DataGridTemplateColumn  51      ,  &System.Windows.Controls.DataGridColumn   Header$
 Time��6%      SortDirection$ 	AscendingX�63      Width5  /System.Windows.Controls.DataGridLengthConverter$	 100 6M      CellTemplate 52      �� 53      "  6#   .R�54      �� ��6    �� *Time+55      57        58      $
 Time��6%   $	 100 63    59      �� 5:      "  6#   .R�5;      �� $
��Bold"�69   ��6    �� *From+69   5<      5>        5?      $ Message��6%   $ 1* 66    5@      �� 5A      "  6#   .R�5B      �� $
��Wrap=�6$   ��68   �� *	Message+5C      5E      5G      5H      