   M S B A M L   `   `   `  ����?  ;Life, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:Life.UtilitiesLife.Utilities  5          Life.Controls.Settings   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      2	utilitiesclr-namespace:Life.Utilities     5        J� SnapsToDevicePixels  .  � UseLayoutRounding .  � DataContext 5      ��  �� RelativeSource �� *Self+ � ColumnDefinitions 5
      �� 5   
    � RowDefinitions 5      �� 5   
   �� 5   
   $
��Auto �6    � 	Resources 5       �� DataType   Life.Utilities.Trigger ��Name	 �� CornerRadius
 �� Padding � Cursor �� 
TargetType �� Property �� Value �� Triggers �� Binding �� Value �� ElementName �� Path �� Orientation   Life.Utilities.Activity��%�  (��       5      *, +)(�� @    5A      *, +)�� 5   
   "  6   .R�5      �� $ Border��5      -   5        
��� � �
�������$��1q�5      $	 10,10,10,10��5      $
 10,10,10,10q�5      $��Center=�5      $��Center=�5      $
 Hand��5      ��5      �� 5      " ��6   .�5      ��  �@J�Opacity$ 1Q�64   �� 5        �@K�ZIndex$ 0��69    5!      �� 5"       6*   �� *	Enabled+	 False6F   .a�5#   "   ��  �@J�Opacity$	 .75Q�6<   5$      �� 5%       6*   �� $ Border��$ 	IsFocused�.a�5&   "   ��  �@K�ZIndex$ 1��6A   5'      5)      ._�5+      �  5,      �� 5-      $
��Auto �6(   �� 5.       50      �� 51      �� 52      $
��Auto �6+   .+�54      �� .+�55      �� $ 
Horizontal=�6%   .+�56      �� $��Type: ��6(   �� 57      $
��Wrap=�6E   ��6(   �� *Entity.Type+6E   58      �� 59      $ 
Horizontal=�6%   .+�5:      �� $��Description: ��6(   �� 5;      $
��Wrap=�6E   ��6(   �� *Description+6E   5<      5=      5>      5?      5@      �� 5A   
   "  6   .R�5B      �� $ Border��5C      -   5        
��� ���
�������$��1,1,1,1q�5F      $	 10,10,10,10��5G      $
 10,10,10,10q�5H      $��Center=�5I      $��Center=�5J      $
 Hand��5K      ��5L      �� 5M      " ��6   .�5N      ��  �@J�Opacity$ 1Q�64   �� 5O       �@K�ZIndex$ 0��69    5P      �� 5Q       6*   �� *	Enabled+	 False6F   .a�5R   "   ��  �@J�Opacity$	 .75Q�6<   5S      �� 5T       6*   �� $ Border��$ 	IsFocused�.a�5U   "   ��  �@K�ZIndex$ 1��6A   5V      5X      ._�5Z      �  5[      �� 5\      $
��Auto �6(   �� 5]       5_      �� 5`      �� 5a      $
��Auto �6+   .+�5c      �� .+�5d      �� $ 
Horizontal=�6%   .+�5e      �� $��Type: ��6(   �� 5f      $
��Wrap=�6E   ��6(   �� *Entity.Type+6E   5g      �� 5h      $ 
Horizontal=�6%   .+�5i      �� $��Description: ��6(   �� 5j      $
��Wrap=�6E   ��6(   �� *Description+6E   5k      5l      5m      5n      5o      5p      .+�5y      �� $��1��6   $ 
Horizontal=�6   .+�5z   
   �� -   5         ��Name$ Types��5z       �� DisplayMemberPath$ FullName��6P   $��5q�6m   ��6"   �� *ActivitiesAndTriggers+6m   �� 5{   
   -   5        $	��Add��5{      $��5q�6   6*   �� 5|   
   $��Remove��6   $��5q�6"   5}      5~      