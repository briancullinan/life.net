   M S B A M L   `   `   `  ����?  ;Life, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:Life.UtilitiesLife.Utilities  5          Life.Controls.Activity   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      2	utilitiesclr-namespace:Life.Utilities     5        J� SnapsToDevicePixels  .  � UseLayoutRounding .  � ColumnDefinitions 5	      �� 5
   
   �� 5   
   �� 5   
   $��2* �6    � 	Resources 5       �� DataType   Life.Utilities.Trigger ��Name �� CornerRadius �� Padding � Cursor	 �� 
TargetType
 �� Property �� Value �� Triggers �� Binding �� Value �� ElementName �� Path � RowDefinitions �� Orientation   Life.Utilities.Activity��%�  (��       5      *, +)(�� @    5>      *, +)�� 5   
   "  6   .R�5      �� $ Border��5      -   5        
��� � �
�������$��1q�5      $ 10,10,10,10��5      $ 10,10,10,10q�5      $��Center=�5      $��Center=�5      $
 Hand��5      ��5      �� 5      "	 ��6   .�5      �� 
 �@J�Opacity$ 1Q�64   �� 5      
 �@K�ZIndex$ 0��69    5      �� 5       6*   �� *	Enabled+	 False6F   .a�5    "   �� 
 �@J�Opacity$	 .75Q�6<   5!      �� 5"       6*   �� $ Border��$ 	IsFocused�.a�5#   "   �� 
 �@K�ZIndex$ 1��6A   5$      5&      ._�5(      �  5)      �� 5*      $
��Auto �6(   �� 5+       5-      �� 5.      �� 5/      $
��Auto �6+   .+�51      �� .+�52      �� $ 
Horizontal=�6%   .+�53      �� $��Type: ��6(   �� 54      $
��Wrap=�6E   ��6(   �� *Entity.Type+6E   55      �� 56      $ 
Horizontal=�6%   .+�57      �� $��Description: ��6(   �� 58      $
��Wrap=�6E   ��6(   �� *Description+6E   59      5:      5;      5<      5=      �� 5>   
   "  6   .R�5?      �� $ Border��5@      -   5        
��� ���
�������$��1,1,1,1q�5C      $ 10,10,10,10��5D      $ 10,10,10,10q�5E      $��Center=�5F      $��Center=�5G      $
 Hand��5H      ��5I      �� 5J      "	 ��6   .�5K      �� 
 �@J�Opacity$ 1Q�64   �� 5L      
 �@K�ZIndex$ 0��69    5M      �� 5N       6*   �� *	Enabled+	 False6F   .a�5O   "   �� 
 �@J�Opacity$	 .75Q�6<   5P      �� 5Q       6*   �� $ Border��$ 	IsFocused�.a�5R   "   �� 
 �@K�ZIndex$ 1��6A   5S      5U      ._�5W      �  5X      �� 5Y      $
��Auto �6(   �� 5Z       5\      �� 5]      �� 5^      $
��Auto �6+   .+�5`      �� .+�5a      �� $ 
Horizontal=�6%   .+�5b      �� $��Type: ��6(   �� 5c      $
��Wrap=�6E   ��6(   �� *Entity.Type+6E   5d      �� 5e      $ 
Horizontal=�6%   .+�5f      �� $��Description: ��6(   �� 5g      $
��Wrap=�6E   ��6(   �� *Description+6E   5h      5i      5j      5k      5l      5m      .+�5n      �� -   5         ��Name$ Ongoing��5n      .+�5o   
   �� $��On Going��6   $��Center=�6#   5p      �� 5q      -   5        $ Recent��5q      $��1��6!   .+�5r   
   �� $��Recent��6   $��Center=�6!   5s      � 5t      $��2��6    5u   
   �� 5v      $
��Auto �6   �� 5w      .+�5y   
   �� $��Internal��6   $��Center=�6#   5�      5�      